﻿using UnityEngine;
using System.Collections;

public class WeaponPrototype : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject katana = WeaponFactory.create ("Katana");
		PickupSpawner.SpawnWeapon (katana, new Vector3(0, 15, 0));

		GameObject daggers = WeaponFactory.create ("Daggers");
		PickupSpawner.SpawnWeapon (daggers, new Vector3(5, 15, 0));
	}
}
