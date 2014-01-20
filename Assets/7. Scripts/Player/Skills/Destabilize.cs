using UnityEngine;
using System.Collections.Generic;

public class Destabilize : SkillBase
{
	private Timer _destabilize;

	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		//TODO: Use correct animation
		"Dash",
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
		);

	public Destabilize() : base(1, _animationSettings)
	{
		_destabilize = new Timer (1f);
		_destabilize.AddCallback (0f, delegate {
			GetComponentInChildren<Attackable>().isAttackable = false;
			GetComponent<AvatarGraphics>().ApplySkillMaterial(transform, (Material)Resources.Load("Materials/Destabilize"));
		});

		_destabilize.AddCallback (_destabilize.endTime, delegate {
			GetComponentInChildren<Attackable>().isAttackable = true;
			GetComponent<AvatarGraphics>().ApplyDefaultMaterial(transform);
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
