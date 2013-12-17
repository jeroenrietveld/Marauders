using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Attack : ActionBase {

	private Weapon _weapon;

	void Start () {
		ControllerMapping controllerMapping = GetComponent<ControllerMapping> ();
		controllerMapping.AddAction(Button.RightShoulder, this);
	}

	public override void PerformAction()
	{
	}

	public void UpdateAttackAnimations(Weapon weapon)
	{
		_weapon = weapon;

		AnimationHandler animationHandler = GetComponent<AnimationHandler> ();

		foreach(AttackInfo attackInfo in weapon.attacks)
		{	
			animationHandler.AddAnimation(
				new AnimationHandler.AnimationSettings(
					attackInfo.animationName,
					AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
					3,
					WrapMode.Once
				));
		}
	}

	void Update()
	{

	}
}
