using UnityEngine;
using System.Collections.Generic;

public class Windsweep : SkillBase
{
	// Angles in radians.
	public float angle = 100 * Mathf.Deg2Rad;
	public float range = 5;
	public float force = 750;
	public float damage = .1f;

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
				var heartbeat = avatar.GetComponentInChildren<Heartbeat>();

				var direction = transform.position - avatar.transform.position;
				direction.y = 0;
				direction.Normalize();

				var source = new DamageSource(
					GetComponent<Avatar>().player,
					direction,
					-direction * force,
					damage,
					0,
					false
					);

				heartbeat.DoAttack(source);
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
