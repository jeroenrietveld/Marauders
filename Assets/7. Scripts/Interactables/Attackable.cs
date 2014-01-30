using UnityEngine;
using System.Collections;

public abstract class Attackable : MonoBehaviour 
{
	public bool isAttackable = true;

	protected abstract bool ApplyAttack(DamageSource source);

	public bool DoAttack(DamageSource source)
	{
		if(isAttackable)
		{
			return ApplyAttack(source);
		}

		return false;
	}
}
