using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PlayerRef {

	private static Color[] _colors = new []{Color.red, Color.green, Color.blue, Color.yellow};

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

	private GameObject _avatar;

	public PlayerRef(PlayerIndex index)
	{
		controller = ControllerInput.GetController (index);
		skills = new SkillModel ();
	}

	public void Update()
	{

	}

	public void CreateAvatar()
	{
		//TODO: make avatar dynamic
		 _avatar = GameObject.Instantiate(Resources.Load("Prefabs/Marauders/Samurai_avatar")) as GameObject;

		_avatar.AddComponent<CameraTracking> ();
		_avatar.AddComponent<Avatar> ();
		_avatar.AddComponent<ControllerMapping> ();
		_avatar.AddComponent<AnimationHandler> ();
		_avatar.AddComponent<Movement> ();
		_avatar.AddComponent<Attack> ();
		_avatar.AddComponent<Jump> ();
		_avatar.AddComponent<Interactor> ();

		Avatar avatar = _avatar.GetComponent<Avatar> ();
		avatar.Initialize (this);
	}

	public void DestroyAvatar()
	{
		GameObject.Destroy (_avatar);
	}
}
