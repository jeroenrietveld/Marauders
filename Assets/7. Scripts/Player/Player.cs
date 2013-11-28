﻿using UnityEngine;
using System.Collections;

/// <summary>
/// The player.
/// </summary>
public partial class Player : MonoBehaviour
{
	public Vector3 pos;

	/// <summary>
	/// The xbox controller used by the player
	/// </summary>
	public ControllerMapping controller;

	/// <summary>
	/// This player's primary weapon
	/// </summary>
	public Weapon primaryWeapon;

	/// <summary>
	/// This player's secondary weapon ( Only used for buffering )
	/// </summary>
	public Weapon secondaryWeapon;

	/// <summary>
	/// Initializes a new instance of the <see cref="Player"/> class.
	/// </summary>
	public Player()
	{
		pos = transform.position;
	}

	/// <summary>
	/// Picks up weapon and puts in in 'primaryWeapon' 
	/// </summary>
	/// <param name="weapon">The weapon that's beeing picked up</param>
	/// <param name="gametypeSpecificObj">If true, moves primary to secondary, else; drops primary weapon</param>
	public void PickUpWeapon(Weapon weapon, bool gametypeSpecificObj)
	{
		if (gametypeSpecificObj)
		{
			secondaryWeapon = primaryWeapon;
		}

		//Setting our primary weapon
		primaryWeapon = weapon;

		//Hiding the weapon
		weapon.gameObject.SetActive(false);

		//We're the owner
		weapon.Owner = this;
	}

	public void DropPrimaryWeapon()
	{
		//Dropping at our current location
		primaryWeapon.gameObject.SetActive(true);
		primaryWeapon.transform.position = transform.position;
		//Rigidbody FlagClone  = (Rigidbody)Inistantiate (Flag, transform.position, transform.rotation);

		primaryWeapon.Owner = null;

		//Moving secondary to primary
		primaryWeapon = secondaryWeapon;

		//Secondary is definately null now
		secondaryWeapon = null;
	}

	public void Update()
	{
		if (controller.GetButtonDown (XboxButton.Y))
		{
			//Dropping weapon
			DropPrimaryWeapon();
		}

		if (transform.position.y < 0)
		{
			transform.position = new Vector3(4, 9, -1);
		}
	}
}