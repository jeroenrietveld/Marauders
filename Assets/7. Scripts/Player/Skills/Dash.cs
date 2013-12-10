using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dash : SkillBase
{
	private Player _player;

	private Timer _dashing;

	public Dash()
	{
		animationName = "Dash";

		_dashing = new Timer (0.3f);
		_dashing.AddCallback (_dashing.endTime, delegate {
			_player.animation.Stop();
			_player.rigidbody.velocity = Vector3.zero;
		});

		cooldown = new Timer (10f);
	}

	void Start()
	{
		_player = GetComponent<Player> ();
		initializeAnimation ();
	}

    public override void performAction()
	{
		_dashing.Start ();
		cooldown.Start ();
    }

	public void initializeAnimation()
	{
		Animation animation = _player.animation;

		animation[animationName].speed = 1.0f;
		animation[animationName].wrapMode = WrapMode.Loop;
		animation[animationName].layer = 2;
	}

	public void Update()
	{
		_dashing.Update ();
		cooldown.Update ();

		if(_dashing.running)
		{
			_player.rigidbody.velocity = _player.transform.forward * 20;
		}
	}
}
