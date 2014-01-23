using UnityEngine;
using System.Collections;

public struct DamageSource
{
	public Player inflicter;
	public Vector3 direction;
	public Vector3 force;
	public float amount;
	public float stunTime;
	public bool piercing;

	public DamageSource(Player inflicter, Vector3 direction, Vector3 force, float amount, float stunTime, bool piercing)
	{
		this.inflicter = inflicter;
		this.direction = direction;
		this.force = force;
		this.amount = amount;
		this.stunTime = stunTime;
		this.piercing = piercing;
	}
}