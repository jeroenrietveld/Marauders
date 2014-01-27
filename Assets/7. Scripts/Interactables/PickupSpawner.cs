using UnityEngine;
using System.Collections;
using System;

public static class PickupSpawner
{
	public static void SpawnWeapon(GameObject weapon, Vector3 position)
	{
		if(weapon.GetComponent<Weapon>() == null)
		{
			throw new ArgumentException("Invalid weapon");
		}

		GameObject pickup = ResourceCache.GameObject("Prefabs/Interactables/Pickup");

		Vector3 rotation = weapon.transform.rotation.eulerAngles;
		rotation.Scale(new Vector3(0, 1, 0));

		weapon.transform.parent = pickup.transform;
		pickup.transform.position = position;

		weapon.transform.localPosition = Vector3.zero;
		weapon.transform.rotation = Quaternion.Euler(rotation);
		pickup.GetComponent<WeaponInteractable> ().weaponObject = weapon;
	}

	public static void SpawnPowerUp(Vector3 position)
	{	
		GameObject powerUp = ResourceCache.GameObject("Prefabs/Interactables/PowerUps/PowerUp");
		GameObject pickup = ResourceCache.GameObject("Prefabs/Interactables/PowerUpPickup");

		Vector3 rotation = powerUp.transform.rotation.eulerAngles;
		rotation.Scale(new Vector3(0, 1, 0));

		powerUp.transform.parent = pickup.transform;
		pickup.transform.position = position;

		powerUp.transform.localPosition = Vector3.zero;
		powerUp.transform.rotation = Quaternion.Euler (rotation);
	}
}
