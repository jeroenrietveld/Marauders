using UnityEngine;
using System.Collections;

public abstract class Attackable : MonoBehaviour 
{

	public bool isAttackable = true;

	protected abstract void ApplyAttack(Attack attacker);

	public void DoAttack(Attack attacker)
	{
		if(isAttackable)
		{
			ApplyAttack(attacker);
		}
	}
}
