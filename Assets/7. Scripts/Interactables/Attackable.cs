using UnityEngine;
using System.Collections;

public abstract class Attackable : MonoBehaviour 
{
	public bool isAttackable = true;

	protected abstract void ApplyAttack(DamageSource source);

	public void DoAttack(DamageSource source)
	{
		if(isAttackable)
		{
			ApplyAttack(source);
		}
	}
}
