using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dash : SkillBase
{
	//private Player _player;
	private AudioSource _dashSource;
	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		"Dash",
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
	);

	private Timer _dashing;

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
        _dashSource = gameObject.AddComponent<AudioSource>();
        _dashSource.clip = Resources.Load<AudioClip>("Sounds/Abilities/Dash");
        _dashSource.playOnAwake = false;
        _dashSource.minDistance = 200f;
        _dashSource.maxDistance = 250f;
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
		_dashSource.Play();
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
