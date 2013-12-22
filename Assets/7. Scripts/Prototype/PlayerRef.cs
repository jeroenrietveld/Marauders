using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerRef
{
	private static string _marauderDataPath = "Marauders/";
	private static string _marauderPrefabPath = "Prefabs/Marauders/";

	private static Color[] _colors = new []{Color.red, Color.green, Color.blue, Color.yellow};
    public string marauder { get; set; }
	public PlayerIndex index;
	public Color color
	{
		get
		{
			return _colors[(int)index];
		}
	}

	public string[] skills = new string[3];
	public GamePad controller;

	public GameObject avatar { get; private set; }
	
	private int _timeSync;

	public PlayerRef(PlayerIndex index)
	{
		this.index = index;
		controller = ControllerInput.GetController (index);

		_timeSync = 0;
	}

	public void StartSpawnProcedure()
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

		var spawnPoint = timeBubble.GetSpawnPoint(Vector3.up);
		ObjectSpawner.Create(newAvatar, spawnPoint, 4, SpawnTarget.GetRandomTargetDirection(spawnPoint) * timeBubble.exitForce);
	}

	private GameObject CreateAvatar(Vector3 initialPosition)
	{
		var node = ResourceCache.json (_marauderDataPath + marauder);

		avatar = GameObject.Instantiate(Resources.Load(_marauderPrefabPath + node["prefab"].Value)) as GameObject;
		avatar.transform.position = initialPosition;

		avatar.AddComponent<CameraTracking> ();
		avatar.AddComponent<Avatar> ();
		avatar.AddComponent<ControllerMapping> ();
		avatar.AddComponent<AnimationHandler> ();
		avatar.AddComponent<Movement> ();
		avatar.AddComponent<Attack> ();
		avatar.AddComponent<Jump> ();
		avatar.AddComponent<Interactor> ();
		avatar.AddComponent<SoundPlayer>();
		
		foreach(SkillType skillType in System.Enum.GetValues(typeof(SkillType)))
		{
			var skillName = skills[(int)skillType];

			if(skillName != null)
			{
				var skill = ((SkillBase)avatar.AddComponent(skillName));
				if(skill) skill.skillType = skillType;
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
		int timeSyncLimit = GameManager.Instance.TimeSyncLimit;

		_timeSync = Mathf.Clamp(_timeSync + timeSync, 0, timeSyncLimit);
		
		if (_timeSync >= timeSyncLimit)
		{
			Event.dispatch(new PlayerTimeSyncedEvent(this));
		}
	}

	public void SetTimeSync(int timeSync)
	{
		_timeSync = timeSync;
	}
	
	public int GetTimeSync()
	{
		return _timeSync;
	}
}
