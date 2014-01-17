using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Player
{
	private static string _marauderDataPath = "Marauders/";
	private static string _marauderPrefabPath = "Prefabs/Marauders/";

	private static Color[] _colors = new []{Color.red, Color.green, Color.blue, Color.yellow};

    public string marauder { get; set; }
	public PlayerIndex index;
	public int indexInt
	{
		get
		{
			return (int)index;
		}
	}
	public Color color
	{
		get
		{
			return _colors[indexInt];
		}
	}

	public string[] skills = new string[3];
	public GamePad controller;

	public GameObject avatar { get; private set; }
	
	public int timeSync;

	public Player(PlayerIndex index)
	{
		this.index = index;
		controller = ControllerInput.GetController (index);

		timeSync = 0;
	}

	public void StartSpawnProcedure(bool initial = false)
	{
		Vector3 initialPosition = Vector3.zero; // Used for camera tracking during spawn proc.

		if (avatar)
		{
			initialPosition = avatar.transform.position;
			DestroyAvatar();
		}

		var newAvatar = CreateAvatar(initialPosition);
		//TODO: Error handling
		var timeBubbleObj = GameObject.Find ("TimeBubble");
		var timeBubble = timeBubbleObj.GetComponent<TimeBubble> ();

		var spawnPoint = timeBubble.GetSpawnPoint(Quaternion.AngleAxis((float)index * 90, Vector3.up) * new Vector3(-1, 10, 0));
		var spawnTarget = initial ? SpawnTarget.GetPlayerTargetDirection (spawnPoint, index) : SpawnTarget.GetRandomTargetDirection (spawnPoint);

		ObjectSpawner.Create(newAvatar, spawnPoint, 4, spawnTarget * timeBubble.exitForce);
	}

	private GameObject CreateAvatar(Vector3 initialPosition)
	{
		var node = ResourceCache.json (_marauderDataPath + marauder);

		avatar = GameObject.Instantiate(Resources.Load(_marauderPrefabPath + node["prefab"].Value)) as GameObject;
		avatar.transform.position = initialPosition;

		var heartbeatIndicator = GameObject.Instantiate(Resources.Load("Prefabs/Heartbeat_indicator")) as GameObject;
		heartbeatIndicator.transform.SetParentKeepLocal(avatar.transform);

		avatar.AddComponent<CameraTracking> ();
		avatar.AddComponent<Avatar> ();
		avatar.AddComponent<ControllerMapping> ();
		avatar.AddComponent<AnimationHandler> ();
		avatar.AddComponent<Movement> ();
		avatar.AddComponent<Attack> ();
		avatar.AddComponent<Jump> ();
		avatar.AddComponent<Interactor> ();
		avatar.AddComponent<AvatarGraphics>();
		avatar.AddComponent<Slide> ();
		avatar.AddComponent<Dash> ();
		
		for(int i = 0; i < skills.Length; ++i)
		{
			var skillName = skills[i];

			if(skillName != null)
			{
				var skill = ((SkillBase)avatar.AddComponent(skillName));
				if(skill) skill.skillType = (SkillType)i;
			}
		}

		Avatar avatarComponent = avatar.GetComponent<Avatar> ();
		avatarComponent.Initialize (this);

		var weapon = WeaponFactory.create(node["weapon"].Value);
		avatar.GetComponent<Attack>().SetWeapon(weapon.GetComponent<Weapon>());

		return avatar;
	}

	public void DestroyAvatar()
	{
		GameObject.Destroy (avatar);
	}
	
	public void AddTimeSync(int timeSync)
	{
		int timeSyncLimit = GameManager.Instance.matchSettings.timeSync;

		this.timeSync = Mathf.Clamp(this.timeSync + timeSync, 0, timeSyncLimit);
		
		if (this.timeSync >= timeSyncLimit)
		{
			Event.dispatch(new PlayerTimeSyncedEvent(this));
		}
	}

	public void SetTimeSync(int timeSync)
	{
		this.timeSync = timeSync;
	}
	
	public int GetTimeSync()
	{
		return timeSync;
	}

    public int kills { get; set; }
    public int deaths { get; set; }
}
