using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class WeaponInteractable : Interactable {
	
	private List<Player> pickupPlayers = new List<Player>();

	private Weapon weapon;

	public GameObject weaponObject 
	{
		get
		{
			return _weaponObject;
		}
		set
		{
			_weaponObject = value;
			weapon = _weaponObject.GetComponent<Weapon>();
		}
	}

	private GameObject _weaponObject;
	
	public override void OnInteractEnter(Player player)
	{
		if (!pickupPlayers.Contains (player))
		{
			//Adding us to the players that are able to pick this up
			pickupPlayers.Add (player);
		}
	}
	
	public override void OnInteractLeave(Player player)
	{
		pickupPlayers.Remove (player);
	}
	
	public void Update()
	{
		foreach (Player player in pickupPlayers)
		{
			if (player.controller.JustPressed(Button.X))
			{
				player.PickUpWeapon(weapon, weapon.isGameModeObject);
				weapon.transform.parent = player.transform;
				Destroy(gameObject);
			}
		}
	}
	
	public void OnGUI()
	{
		if (pickupPlayers.Count > 0 && isInteractable)
		{
			//Getting the cube's location on screen and storing it		 
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);	
			//GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), ControllerMapping.ButtonImages[pickupPlayers[0].controller.PickupGametypeObject]);
			GUI.Label (new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), Locale.Current["weapon_pickup"] + " " + weapon.name);
		}
	}
}
