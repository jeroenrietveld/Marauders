using UnityEngine;
using System.Collections;

public class Sunderstrike : SkillBase {

	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		//TODO: Use correct animation
		new AttackInfo("Dash", 1.0f, -1f),
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
		);
	
	public Sunderstrike() : base(5, _animationSettings)
	{
	}
	
	protected override void OnPerformAction()
	{
		Attack attack = (Attack)GetComponent<Attack> ();
		attack.damageSource.AddFilter (delegate(DamageSource value) {
				value.piercing = true;
				return value;
		}, true);
		attack.SetCombo ();
		attack.PerformAction ();
	}

	protected override void OnUpdate() {}
};
