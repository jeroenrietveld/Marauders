using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerRef {

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

	private float _timeSyncLimit;
	private float _timeSync;

	public PlayerRef(PlayerIndex index)
	{
		this.index = index;

		controller = ControllerInput.GetController (index);

		// Not sure if we want to do this here... Jeroen?
		GameManager.Instance.playerRefs.Add (this);

		_timeSync = 0f;
		_timeSyncLimit = 500f;
	}

	public void Update()
	{

	}

	public void CreateAvatar()
	{
		avatar = GameObject.Instantiate(Resources.Load("Prefabs/Marauders/" + marauder)) as GameObject;
		Debug.Log ("test");
		avatar.AddComponent<CameraTracking> ();
		avatar.AddComponent<Avatar> ();
		avatar.AddComponent<ControllerMapping> ();
		avatar.AddComponent<AnimationHandler> ();
		avatar.AddComponent<Movement> ();
		avatar.AddComponent<Attack> ();
		avatar.AddComponent<Jump> ();
		avatar.AddComponent<Interactor> ();
		avatar.AddComponent<SoundPlayer>();

		Avatar avatarComponent = avatar.GetComponent<Avatar> ();
		avatarComponent.Initialize (this);
	}

	public void DestroyAvatar()
	{
		GameObject.Destroy (avatar);
	}

	public void SetTimeSyncLimit(float limit)
	{
		_timeSyncLimit = limit;
	}
	
	public void AddTimeSync(float timeSync)
	{
		if (timeSync > 0f)
		{
			_timeSync = _timeSync + timeSync;
		}
		
		if (_timeSync >= _timeSyncLimit)
		{
			Event.dispatch(new PlayerTimeSyncEvent(this));
		}
	}
	
	public float GetTimeSync()
	{
		return _timeSync;
	}
}
