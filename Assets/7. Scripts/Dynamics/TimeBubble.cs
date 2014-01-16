using UnityEngine;
using System.Collections;
using System;

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
	public float respawnDelay;

	public TimeBubbleObjectExitEvent(GameObject obj, float respawnDelay) 
	{
		this.obj = obj;
		this.respawnDelay = respawnDelay;
	}
}

public struct TimeBubbleAvatarExitEvent
{
	public Avatar avatar;
	public float respawnDelay;
	
	public TimeBubbleAvatarExitEvent(Avatar avatar, float respawnDelay) 
	{
		this.avatar = avatar;
		this.respawnDelay = respawnDelay;
	}
}

public class TimeBubble : MonoBehaviour {
	public float exitForce = 10000;
	public float respawnDelay = 1;

	void OnTriggerExit(Collider collider)
	{
		var exitDirection = collider.transform.position - transform.position;
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
			Transform avatarTransform = avatar.transform.FindInChildren("Heartbeat_indicator(Clone)");

			Heartbeat heartbeat = avatarTransform.GetComponent<Heartbeat>();

			//Checking if we got killed
			if ((DateTime.Now - heartbeat.lastAttackTime).TotalMilliseconds < 4000)
			{
				heartbeat.health = 0;

				//After death respan
				Event.dispatch(new AvatarDeathEvent(avatar.player, heartbeat.lastAttacker));
				
				//avatar.player.StartSpawnProcedure();
			} else
			{
				//Normal respawn
				Event.dispatch(new TimeBubbleAvatarExitEvent(avatar, respawnDelay));
			}
		}
		else
		{
			Event.dispatch (new TimeBubbleObjectExitEvent (collider.gameObject, respawnDelay));
		}
	}

	public Vector3 GetSpawnPoint(Vector3 directionFromCenter)
	{
		return transform.position + Vector3.Scale(directionFromCenter.normalized, transform.localScale * GetComponent<SphereCollider>().radius);
	}
}
