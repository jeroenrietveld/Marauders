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

	public struct SkillModel
	{
		public string utilitySkill;
		public string defensiveSkill;
		public string offensiveSkill;
	}
	public SkillModel skills;
	public GamePad controller;

	public GameObject avatar { get; private set; }

	public PlayerRef(PlayerIndex index)
	{
		this.index = index;

		controller = ControllerInput.GetController (index);
		skills = new SkillModel ();


		// Not sure if we want to do this here... Jeroen?
		//GameManager.Instance.playerRefs.Add (this);
	}

	public void Update()
	{

	}

	public void CreateAvatar()
	{
		avatar = GameObject.Instantiate(Resources.Load("Prefabs/Marauders/" + marauder)) as GameObject;

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
}
