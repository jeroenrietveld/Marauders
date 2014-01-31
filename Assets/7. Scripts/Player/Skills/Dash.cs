﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dash : SkillBase
{

	//private Player _player;
	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		new AttackInfo("Dash", 1.0f, -1f),
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
	);

	private Timer _dashing;

	public Dash() : base(4, _animationSettings)
	{
		_dashing = new Timer (0.3f);
		_dashing.AddCallback (delegate {
            if(!gameObject.rigidbody.isKinematic)
            {
                rigidbody.velocity = Vector3.zero;
            }
			
		});
	}
	
	protected override void OnStart()
    {
        
    }

	protected override void OnPerformAction()
	{
		_dashing.Start ();
    }

	protected override void OnUpdate()
	{
		_dashing.Update ();

		if(_dashing.running && !gameObject.rigidbody.isKinematic)
		{
            GameManager.Instance.soundInGame.PlaySound(skillAudioSource, "Dash", true);
			rigidbody.velocity = transform.forward * 10;
		}
	}
}
