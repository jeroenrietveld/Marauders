using UnityEngine;
using System.Collections.Generic;

public class SpawnTarget : MonoBehaviour {

	private static List<SpawnTarget> activeInstances = new List<SpawnTarget>();

	private static System.Random _rng = new System.Random ();

	public PlayerIndex player;

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

	public static Vector3 GetRandomTargetDirection(Vector3 position)
	{
		if(activeInstances.Count == 0) return -position.normalized;

		return (activeInstances[_rng.Next(activeInstances.Count)].transform.position - position).normalized;
	}

	public static Vector3 GetPlayerTargetDirection(Vector3 position, PlayerIndex index)
	{
		foreach(var target in activeInstances)
		{
			if(target.player == index) return (target.transform.position - position).normalized;
		}
		
		return Vector3.down;
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
