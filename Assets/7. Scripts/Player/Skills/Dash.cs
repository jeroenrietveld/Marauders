using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Dash : SkillBase
{
	public override string animationName { get; set; }

	private bool _dashing = false;
	private Player _player;

	public Dash()
	{
		animationName = "Dash";
	}

    public override void performAction(Player player)
    {
		_dashing = true;
		_player = player;
		//player.rigidbody.AddForce(Vector3.up * 1000);
    }

	public void Update()
	{
		if(_dashing)
		{
			_player.rigidbody.velocity = _player.transform.forward * 100;
			_dashing = false;
		}
		else
		{
			_player.rigidbody.velocity = Vector3.zero;
		}
	}
}
