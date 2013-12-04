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
		//File file;
		TextAsset jsonString;

		if(jsonString = Resources.Load(filePath) as TextAsset)
		{
			var node = JSON.Parse(jsonString.text);

			GameObject grip = new GameObject();
			grip.name = "grip";
			GameObject prefab = GameObject.Instantiate(Resources.Load(prefabPath + node["prefab"].Value)) as GameObject;
			prefab.transform.parent = grip.transform;
			
			Weapon weapon = grip.AddComponent<Weapon>();
			weapon.name = node["name"].Value;

			return grip;
		}
		else
		{
			throw new ArgumentException("File does not exist");
		}
	}
}
