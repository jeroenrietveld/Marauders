using UnityEngine;
using System.Collections;
using System.IO;
using System;
using SimpleJSON;

public static class WeaponFactory
{
	private static string dataPath = "Weapons/";
	private static string prefabPath = "Prefabs/Interactables/Weapons/";

	public static GameObject create(string weaponName)
	{
		GameObject weaponHolder = new GameObject("Weapon_holder");
		var node = ResourceCache.json(dataPath + weaponName);

		for(int i = 0; i < node["prefabs"].AsArray.Count; i++)
		{
			GameObject grip = new GameObject("Grip_"+i);
			grip.transform.parent = weaponHolder.transform;

			GameObject prefab = ResourceCache.GameObject(prefabPath + node["prefabs"].AsArray[i].Value);
			prefab.transform.parent = grip.transform;
		}

		Weapon weapon = weaponHolder.AddComponent<Weapon>();
		weapon.name = node["name"].Value;
		weapon.range = node["range"].AsFloat;
		weapon.damage = node["damage"].AsFloat;
        weapon.weaponType = node["weaponHandling"].Value;

		//Adding all the attacks
		for(int i = 0; i < node["attacks"].AsArray.Count; i++)
		{
			weapon.attacks.Add (new AttackInfo(
				node["attacks"][i]["animation"],
				node["attacks"][i]["speed"].AsFloat,
				node["attacks"][i]["timing"].AsFloat));
		}

		return weaponHolder;
	}
}
