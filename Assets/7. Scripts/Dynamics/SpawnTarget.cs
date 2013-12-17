using UnityEngine;
using System.Collections.Generic;

public class SpawnTarget : MonoBehaviour {

	private static List<SpawnTarget> activeInstances = new List<SpawnTarget>();
	private static System.Random rng = new System.Random();

	public static Vector3 GetRandomSpawnTarget()
	{
		if(activeInstances.Count == 0) return Vector3.zero;

		return activeInstances[rng.Next(activeInstances.Count)].transform.position;
	}

	void OnEnable()
	{
		activeInstances.Add(this);
	}

	void OnDisable()
	{
		activeInstances.Remove(this);
	}
}
