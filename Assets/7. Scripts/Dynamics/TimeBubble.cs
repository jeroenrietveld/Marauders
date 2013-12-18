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
		collider.gameObject.SetActive(false);

		var exitDirection = collider.transform.position - transform.position;

		var spawner = new GameObject("ObjectSpawner").AddComponent<ObjectSpawner>();
		spawner.obj = collider.gameObject;
		spawner.spawnDelay = respawnDelay;
		spawner.position = transform.position - Vector3.Scale(exitDirection.normalized, transform.localScale * GetComponent<SphereCollider>().radius);
		spawner.exitForce = SpawnTarget.GetClosestTargetDirection(spawner.position) * exitForce;
		
		var avatar = collider.GetComponent<Avatar>();
		if(avatar)
		{
			Event.dispatch(new TimeBubbleAvatarExitEvent(avatar, respawnDelay));
		}
		else
		{
			Event.dispatch (new TimeBubbleObjectExitEvent (collider.gameObject, respawnDelay));
		}
	}
}
