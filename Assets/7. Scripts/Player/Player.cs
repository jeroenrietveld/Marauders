using UnityEngine;
using System.Collections;

/// <summary>
/// The player.
/// </summary>
public partial class Player : MonoBehaviour
{
	private int _controllerID;
	private ControllerMapping _controller;
	private string _name;
	private string _primaryWeapon;
	private string _secondaryWeapon;

	public Player()
	{
	
	}

	void PickUpWeapon(Weapon weapon)
	{

	}
}
