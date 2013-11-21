using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// A weapon that can interact with the player
/// </summary>
public class Weapon
{
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
	private Player Owner
	{
		get;
		set;
	}
	
	/// <summary>
	/// Reloads the weapon
	/// </summary>
	public void Reload()
	{
		throw new System.NotImplementedException();
	}
}
