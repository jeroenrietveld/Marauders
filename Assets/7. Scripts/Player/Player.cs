using UnityEngine;
using System.Collections;

/// <summary>
/// The player.
/// </summary>
public partial class Player : MonoBehaviour
{
	/// <summary>
	/// The xbox controller used by the player
	/// </summary>
	public ControllerMapping controller;

	/// <summary>
	/// This player's primary weapon
	/// </summary>
	public Weapon srimaryWeapon;

	/// <summary>
	/// This player's secondary weapon ( Only used for buffering )
	/// </summary>
	public Weapon secondaryWeapon;

	/// <summary>
	/// Initializes a new instance of the <see cref="Player"/> class.
	/// </summary>
	public Player()
	{
	
	}

	/// <summary>
	/// Picks up weapon and puts in in 'primaryWeapon' 
	/// </summary>
	/// <param name="weapon">The weapon that's beeing picked up</param>
	/// <param name="gametypeSpecificObj">If true, moves primary to secondary, else; drops primary weapon</param>
	void PickUpWeapon(Weapon weapon, boolean gametypeSpecificObj)
	{

	}
}
