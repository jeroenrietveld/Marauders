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
		string filePath = dataPath + weaponName;
		TextAsset jsonString;

		if(jsonString = Resources.Load(filePath) as TextAsset)
		{
			var node = JSON.Parse(jsonString.text);

			GameObject weaponHolder = new GameObject("Weapon_holder");

			for(int i = 0; i < node["prefabs"].AsArray.Count; i++)
			{
				GameObject grip = new GameObject("Grip_"+i);
				grip.transform.parent = weaponHolder.transform;

				GameObject prefab = GameObject.Instantiate(Resources.Load(prefabPath + node["prefabs"].AsArray[i].Value)) as GameObject;
				prefab.transform.parent = grip.transform;
			}

			Weapon weapon = weaponHolder.AddComponent<Weapon>();
			weapon.name = node["name"].Value;
			weapon.range = node["range"].AsFloat;
			weapon.damage = node["damage"].AsFloat;
			for(int i = 0; i < node["animations"].AsArray.Count; i++)
			{
				weapon.AddAnimation(node["animations"][i]);
			}

			return weaponHolder;
		}
		else
		{
			throw new ArgumentException("File does not exist");
		}
	}
}
