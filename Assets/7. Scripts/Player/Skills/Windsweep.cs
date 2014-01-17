using UnityEngine;
using System.Collections.Generic;

public class Windsweep : SkillBase
{
	// Angles in radians.
	public float angle = 1;
	public float range = 3;
	public float force = 750;

	private static AnimationHandler.AnimationSettings _animationSettings = new AnimationHandler.AnimationSettings (
		//TODO: Use correct animation
		"Dash",
		AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
		1,
		WrapMode.Loop
	);
	
	private AudioSource windsweepSource;
	
	public Windsweep() : base(5, _animationSettings)
	{

	}
	
	protected override void OnStart()
    {
		windsweepSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(this.gameObject);
    }

	protected override void OnPerformAction()
	{
		GameManager.Instance.soundInGame.PlaySound(windsweepSource, "Windsweep", true);
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

		return Mathf.Acos (Vector3.Dot (transform.forward, (targetPos - transform.position).normalized)) <= angle;
	}

	protected override void OnUpdate() {}
}
