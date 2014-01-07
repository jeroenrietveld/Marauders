using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Timeshift : SkillBase
{
	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		"Timeshift",
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
	);

	//private Player _player;
	
	private Timer _shifting;
	
	public Timeshift() : base(2, _animationSettings)
	{
		_shifting = new Timer (0.5f);
		_shifting.AddCallback(_shifting.startTime, delegate {
			//_player.frozen = true;
			//transparency
		});
		_shifting.AddCallback (_shifting.endTime, delegate {
			//_player.frozen = false;
			//_player.renderer.material.color.a = 1f;
		});
	}
	
	void Start()
	{
		//_player = GetComponent<Player> ();
	}
	
	protected override void OnPerformAction()
	{
		_shifting.Start ();
	}
	
	protected override void OnUpdate()
	{
		_shifting.Update ();
	}
}
