using UnityEngine;
using System.Collections.Generic;

public class Destabilize : SkillBase
{
	private Timer _destabilize;

	private float _duration = 1f;

	private Vector3 _velocity;

	private ParticleSystem _particleSystem;

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

	protected override void OnStart()
	{
		var emitterObject = ResourceCache.GameObject("Prefabs/ParticleEmitters/DestabilizeEmitter");
		emitterObject.transform.SetParentKeepLocal (transform);
		_particleSystem = emitterObject.GetComponent<ParticleSystem> ();

		_destabilize.AddPhaseCallback (0, _particleSystem.Play);
		_destabilize.AddPhaseCallback (_particleSystem.Stop);
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
