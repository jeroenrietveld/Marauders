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

			//The grips center is where the character holds the weapon
			GameObject grip = new GameObject();
			grip.name = "grip";
			//The prefab is the weapon game object
			GameObject prefab = GameObject.Instantiate(Resources.Load(prefabPath + node["prefab"].Value)) as GameObject;
			prefab.transform.parent = grip.transform;

			Weapon weapon = grip.AddComponent<Weapon>();
			weapon.name = node["name"].Value;
			weapon.range = node["range"].AsFloat;
			for(int i = 0; i < node["animations"].AsArray.Count; i++)
			{
				weapon.AddAnimation(node["animations"][i]);
			}

			//Add a rigidbody to the weapon, this makes sures collision events are always fired
			Rigidbody rigidbody = prefab.AddComponent<Rigidbody>();
			rigidbody.mass = 1.0f;
			rigidbody.isKinematic = true;
			rigidbody.useGravity = false;

			return grip;
		}
		else
		{
			throw new ArgumentException("File does not exist");
		}
	}
}
