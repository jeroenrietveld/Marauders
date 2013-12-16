using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// A weapon that can interact with the player
/// </summary>
public class Weapon : MonoBehaviour
{
	public bool isGameModeObject = false;
	public float range;

	public List<AttackInfo> attacks = new List<AttackInfo>();
	public int currentAttack { get; set; }

	public float damage
	{
		get;
		set;
	}

	/// <summary>
	/// The amount of Bullets left in the magazine
	/// </summary>
	public int bulletCount
	{
		get;
		set;
	}
	
	/// <summary>
	/// The amount of Magazines left
	/// </summary>
	public int magazineCount
	{
		get;
		set;
	}
	
	/// <summary>
	/// The amount of Bullets per Magazine
	/// </summary>
	public int bulletsPerMagazine
	{
		get;
		set;
	}
	
	/// <summary>
	/// A pointer to the player that holds this weapon
	/// </summary>
	public Player owner
	{
		get;
		set;
	}

	public void Reload()
	{
		throw new System.NotImplementedException();
	}

	public void DetectPlayerHit()
	{
		// get all colliders whose bounds touch the sphere
		Collider[] colls= Physics.OverlapSphere(owner.transform.position, this.range);

		//Looping each collision
		foreach(Collider hit in colls) 
		{
			Player player = hit.collider.gameObject.GetComponent<Player>();

			if (player && player != owner)
			{ 
				if (Vector3.Distance(hit.transform.position, owner.transform.position) <= this.range)
				{
					ApplyDamage(player);
				}
			}
		}

		/*CapsuleCollider coll = owner.GetComponent<CapsuleCollider> ();
		
		RaycastHit[] hits = Physics.CapsuleCastAll (
			owner.transform.TransformPoint(coll.center + new Vector3(0, coll.height/2, 0)),
			owner.transform.TransformPoint(coll.center + new Vector3(0, -coll.height/2, 0)),
			coll.radius,
			owner.transform.forward,
			range);
		
		foreach(var hit in hits)
		{
			Player player = hit.collider.gameObject.GetComponent<Player>();

			if(player && player != owner)
			{
				ApplyDamage(player);
			}
		}*/
	}

	public void ApplyDamage(Player player)
	{
		Vector3 attackDirection = player.transform.position - owner.transform.position;
		
		player.ApplyDamage(-attackDirection, owner.primaryWeapon.damage);
	}
}
