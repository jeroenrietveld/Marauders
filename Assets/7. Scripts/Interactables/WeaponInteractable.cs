using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class WeaponInteractable : Interactable {

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

	
	public override void OnInteract(GameObject obj)
	{
		var attack = obj.GetComponent<Attack>();

		if (attack)
		{
			attack.SetWeapon(weapon);
			Destroy(gameObject);
		}
	}
	
	public override void ShowMessage()
	{
		//Getting the cube's location on screen and storing it		 
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
		//GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), Images.Get(interactButton) );
		GUI.Label (new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), Locale.Current["weapon_pickup"] + " " + weapon.name);
	}
}
