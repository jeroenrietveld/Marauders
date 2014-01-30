using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public struct AvatarSpawnEvent
{
	public Player player;
	public float spawnDelay;

	public AvatarSpawnEvent(Player player, float delay)
	{
		this.player = player;
		this.spawnDelay = delay;
	}
}

public class Player
{
	private static string _marauderDataPath = "Marauders/";
	private static string _marauderPrefabPath = "Prefabs/Marauders/";

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
	private static Color[] _colors = new []
	{	
		new Color(207/255f, 8/255f, 33/255f), new Color(58/255f, 174/255f, 223/255f), 
		new Color(20/255f, 171/255f, 87/255f), new Color(150/255f, 65/255f, 150/255f)
	};

	public int kills { get; set; }
	public int deaths { get; set; }
	public int timeSync { get; set; }
	public bool isTimeSynced
	{
		get
		{
			return timeSync == GameManager.Instance.matchSettings.timeSync;
		}
	}
	
    public string marauder { get; set; }
	public GameObject avatar { get; private set; }

    public string footsole { get; private set; }
	public string[] skills = new string[2];
	public GamePad controller;
	
	public Player(PlayerIndex index)
	{
		this.index = index;
		controller = ControllerInput.GetController (index);

		timeSync = 0;
	}

	public void StartSpawnProcedure(bool initial = false)
	{
		if(GameManager.Instance.gameEnded) { return; }

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

		float spawnDelay = 4;
		ObjectSpawner.Create(newAvatar, spawnPoint, spawnDelay, spawnTarget * timeBubble.exitForce);
		Event.dispatch (new AvatarSpawnEvent (this, spawnDelay));
	}

	private GameObject CreateAvatar(Vector3 initialPosition)
	{
		var node = ResourceCache.json (_marauderDataPath + marauder);
        footsole = node["footsole"].Value;

		avatar = ResourceCache.GameObject(_marauderPrefabPath + node["prefab"].Value);
		avatar.transform.position = initialPosition;

		var heartbeatIndicator = ResourceCache.GameObject("Prefabs/Heartbeat");
		heartbeatIndicator.transform.SetParentKeepLocal(avatar.transform);

		avatar.AddComponent<CameraTracking> ();
		avatar.AddComponent<Avatar> ();
		avatar.AddComponent<ControllerMapping> ();
		avatar.AddComponent<AnimationHandler> ();
		avatar.AddComponent<Movement> ();
		avatar.AddComponent<Attack> ();
		avatar.AddComponent<Jump> ();
		avatar.AddComponent<Interactor> ();
		avatar.AddComponent<AvatarGraphics> ();
		avatar.AddComponent<Slide> ();
		avatar.AddComponent<Dash> ();
		avatar.AddComponent<Stun> ();
		avatar.AddComponent<PlayerNotification> ();
		
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
		AddTimeSync (timeSync, new Vector3 (float.NaN, float.NaN, float.NaN));
	}

	public void AddTimeSync(int timeSync, Vector3 position)
	{
		int timeSyncLimit = GameManager.Instance.matchSettings.timeSync;
		this.timeSync = Mathf.Clamp(this.timeSync + timeSync, 0, timeSyncLimit);

		Event.dispatch(new PlayerTimeSyncEvent(this, timeSync, position));
		if (this.timeSync >= timeSyncLimit)
		{
			Event.dispatch(new PlayerTimeSyncedEvent(this));
		}
	}
}
