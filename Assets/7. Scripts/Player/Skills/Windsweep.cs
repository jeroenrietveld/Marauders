using UnityEngine;
using System.Collections.Generic;

public class Windsweep : SkillBase
{
	// Angles in radians.
	public float angle = 100 * Mathf.Deg2Rad;
	public float range = 5;
	public float force = 750;

	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		//TODO: Use correct animation
		new AttackInfo("Dash", 1.0f, -1f),
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
	);
	
	public Windsweep() : base(5, _animationSettings)
	{

	}
	
	protected override void OnStart()
    {
		
    }

	protected override void OnPerformAction()
	{
        GameManager.Instance.soundInGame.PlaySound(skillAudioSource, "Windsweep", true);

		foreach(var player in GameManager.Instance.playerRefs)
		{
			var avatar = player.avatar;

			if(ValidTarget(avatar))
			{
				avatar.rigidbody.AddForce(
					(avatar.transform.position - transform.position).normalized * force,
					ForceMode.Impulse
				);
			}
		}
	}

	private bool ValidTarget(GameObject obj)
	{
		if (obj == gameObject) return false;

		var targetPos = obj.transform.position;
		
		if (Vector3.Distance (transform.position, targetPos) > range) return false;

		return Mathf.Acos (Vector3.Dot (transform.forward, Vector3.Scale(targetPos - transform.position, new Vector3(1, 0, 1)).normalized)) <= angle;
	}

	protected override void OnUpdate() {}
}
