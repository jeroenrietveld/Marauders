using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// A weapon that can interact with the player
/// </summary>
public abstract class Weapon : Interactable
{
	/// <summary>
	/// Initializes a new instance of the <see cref="Weapon"/> class.
	/// </summary>
	public abstract string GetWeaponName();

	/// <summary>
	/// Are we displaying a pickup message?
	/// </summary>
	private bool showPickupMessage = false;
	private List<Player> pickupPlayers = new List<Player>();

	/// <summary>
	/// Are we a gametype Object? 
	/// </summary>
	private bool gametypeObject = false;

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
	public Player Owner
	{
		get;
		set;
	}

	public override void OnInteractEnter(Player player)
	{
		//Lets show this message
		this.showPickupMessage = true;

		if (!pickupPlayers.Contains (player))
		{
			//Adding us to the players that are able to pick this up
			pickupPlayers.Add (player);
		}
	}
	
	public override void OnInteractLeave(Player player)
	{
		this.showPickupMessage = false;


		pickupPlayers.Remove (player);
	}

	public void Update()
	{
		//showMessage
		if (showPickupMessage)
		{
			foreach (Player player in pickupPlayers)
			{
				if (GamePad.GetState(Owner.playerIndex).Buttons.X == ButtonState.Pressed || Input.GetKeyDown("f"))
				{
					//Pick up weapon
					player.PickUpWeapon(this, this.gametypeObject);
				}
			}
		}

	}

	public void OnGUI()
	{
		if (showPickupMessage)
		{
			//Getting the cube's location on screen and storing it		 
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);	
			//GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), ControllerMapping.ButtonImages[pickupPlayers[0].controller.PickupGametypeObject]);
			GUI.Label (new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), Locale.Current["weapon_pickup"] + " " + GetWeaponName());
		}
	}

	/// <summary>
	/// Reloads the weapon
	/// </summary>
	public void Reload()
	{
		throw new System.NotImplementedException();
	}
}
