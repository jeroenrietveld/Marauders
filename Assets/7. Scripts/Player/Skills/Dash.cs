using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dash : SkillBase
{
	//private Player _player;
	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		"Dash",
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
	);

	private Timer _dashing;
	private AudioSource dashSource;
	public Dash() : base(5, _animationSettings)
	{
		_dashing = new Timer (0.3f);
		_dashing.AddCallback (delegate {
			//_player.animation.Stop();
			rigidbody.velocity = Vector3.zero;
		});
	}
	
	protected override void OnStart()
    {
        dashSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(this.gameObject);
    }

	/*
	void Start()
	{
		_player = GetComponent<Player> ();
		initializeAnimation ();
	}
	*/

	protected override void OnPerformAction()
	{
		GameManager.Instance.soundInGame.PlaySound(dashSource, "Dash", true);
		_dashing.Start ();
    }

	/*
	public void initializeAnimation()
	{
		Animation animation = _player.animation;

		animation[animationName].speed = 1.0f;
		animation[animationName].wrapMode = WrapMode.Loop;
		animation[animationName].layer = 2;
	}
	*/

	protected override void OnUpdate()
	{
		_dashing.Update ();

		if(_dashing.running)
		{
			rigidbody.velocity = transform.forward * 10;
		}
	}
}
