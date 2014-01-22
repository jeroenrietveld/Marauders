using UnityEngine;
using System.Collections;

public struct TimeBubbleEnterEvent
{
	public GameObject obj;

	public TimeBubbleEnterEvent(GameObject obj)
	{
		this.obj = obj;
	}
}

public struct TimeBubbleObjectExitEvent
{
	public GameObject obj;
	public Vector3 exitPosition;
	public float respawnDelay;

	public TimeBubbleObjectExitEvent(GameObject obj, Vector3 exitPosition, float respawnDelay) 
	{
		this.obj = obj;
		this.exitPosition = exitPosition;
		this.respawnDelay = respawnDelay;
	}
}

public struct TimeBubbleAvatarExitEvent
{
	public Avatar avatar;
	public Vector3 exitPosition;
	public float respawnDelay;
	
	public TimeBubbleAvatarExitEvent(Avatar avatar, Vector3 exitPosition, float respawnDelay) 
	{
		this.avatar = avatar;
		this.exitPosition = exitPosition;
		this.respawnDelay = respawnDelay;
	}
}

public class TimeBubble : MonoBehaviour {
	public float exitForce = 10000;
	public float respawnDelay = 1;

	void OnTriggerExit(Collider collider)
	{
		var exitPosition = collider.transform.position;
		var exitDirection = exitPosition - transform.position;
		var spawnPoint = GetSpawnPoint (-exitDirection);

		ObjectSpawner.Create (
			collider.gameObject,
			spawnPoint,
			respawnDelay,
			SpawnTarget.GetClosestTargetDirection (spawnPoint) * exitForce
		);

		
		Avatar avatar = collider.GetComponent<Avatar>();

		if(avatar)
		{
			Event.dispatch(new TimeBubbleAvatarExitEvent(avatar, exitPosition, respawnDelay));
		}
		else
		{
			Event.dispatch (new TimeBubbleObjectExitEvent (collider.gameObject, exitPosition, respawnDelay));
		}
	}

	public Vector3 GetSpawnPoint(Vector3 directionFromCenter)
	{
		return transform.position + Vector3.Scale(directionFromCenter.normalized, transform.localScale * GetComponent<SphereCollider>().radius);
	}
}
