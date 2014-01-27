using UnityEngine;
using System.Collections.Generic;

public class Destabilize : SkillBase
{
	private Timer _destabilize;

	private float _duration = 3f;

	private Vector3 _velocity;

	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		//TODO: Use correct animation
		new AttackInfo("Dash", 1.0f, -1f),
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
		);

	public Destabilize() : base(5, _animationSettings)
	{
		Material mat = (Material)Resources.Load ("Materials/Destabilize");

		_destabilize = new Timer (_duration);
		_destabilize.AddCallback (0f, delegate {
			GetComponentInChildren<Attackable>().isAttackable = false;
			GetComponent<AvatarGraphics>().AddMaterial(mat);

			GetComponent<Stun>().SetStunTime(_duration);

			_velocity = rigidbody.velocity;

			rigidbody.isKinematic = true;
		});

		_destabilize.AddCallback (0.1f, delegate {
			GetComponent<AnimationHandler>().Pause();
		});

		_destabilize.AddCallback (delegate {
			GetComponentInChildren<Attackable>().isAttackable = true;
			GetComponent<AvatarGraphics>().RemoveMaterial(mat);

			GetComponent<AnimationHandler>().UnPause();

			rigidbody.isKinematic = false;

			rigidbody.velocity = _velocity;
		});
	}

	protected override void OnPerformAction()
	{
		_destabilize.Start ();
	}

	protected override void OnUpdate() 
	{
		_destabilize.Update ();
	}
}
