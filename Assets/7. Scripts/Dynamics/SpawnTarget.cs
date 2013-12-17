using UnityEngine;
using System.Collections.Generic;

public class SpawnTarget : MonoBehaviour {

	private static List<SpawnTarget> activeInstances = new List<SpawnTarget>();

	public static Vector3 GetClosestTargetDirection(Vector3 position)
	{
		if(activeInstances.Count == 0) return -position.normalized;

		Vector3 bestDirection = new Vector3(float.PositiveInfinity, float.PositiveInfinity);

		foreach(var p in activeInstances)
		{
			var direction = p.transform.position - position;
			if(direction.sqrMagnitude < bestDirection.sqrMagnitude)
			{
				bestDirection = direction;
			}
		}

		return bestDirection.normalized;
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
