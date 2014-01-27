using UnityEngine;
using System.Collections;

public struct DamageSource
{
	public Player inflicter;
	public Vector3 direction;
	public Vector3 force;
	public float amount;
	public float stunTime;
	public int comboCount;
	public int totalAttacks;
	public bool piercing;

	public bool isCombo { get { return comboCount == totalAttacks - 1; } }

	public DamageSource (Player inflicter) : this(inflicter, Vector3.zero, Vector3.zero, 0, 0, false, 0, int.MaxValue)
	{

	}

	public DamageSource(
		Player inflicter, 
		Vector3 direction, 
		Vector3 force, 
		float amount,
		float stunTime, 
		bool piercing, 
		int comboCount = 0, 
		int totalAttacks = int.MaxValue)
	{
		this.inflicter = inflicter;
		this.direction = direction;
		this.force = force;
		this.amount = amount;
		this.stunTime = stunTime;
		this.piercing = piercing;
		this.comboCount = comboCount;
		this.totalAttacks = totalAttacks;
	}
}