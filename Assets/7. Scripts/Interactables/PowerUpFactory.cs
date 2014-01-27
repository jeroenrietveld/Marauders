using UnityEngine;
using System.Collections;
using System.IO;
using System;
using SimpleJSON;

public static class PowerUpFactory
{
	private static string dataPath = "PowerUps/";
	private static string prefabPath = "Prefabs/Interactables/PowerUps/";
	
	public static GameObject create(string weaponName)
	{
		var node = ResourceCache.json(dataPath + weaponName);
		
		GameObject prefab = ResourceCache.GameObject(prefabPath + node["prefab"].Value);
		//prefab.transform.parent = grip.transform;

		PowerUp powerUp = prefab.AddComponent<PowerUp>();
		powerUp.name = node["name"].Value;
		
		return prefab;
	}
}
