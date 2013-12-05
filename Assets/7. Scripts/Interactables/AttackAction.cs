using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class AttackAction : MonoBehaviour
{
	public Weapon weapon;

	void OnTriggerEnter(Collider collider)
	{
		Player player = null;

		if(this.enabled && (player = collider.GetComponent<Player>()) && player != weapon.owner)
		{
			weapon.ApplyDamage(player);
		}
	}
}
