using UnityEngine;
using System.Collections;

public struct TimeBubbleEnterEvent
{
}

public struct TimeBubbleExitEvent
{
	public GameObject obj;
	public Vector3 collisionPosition;

	public TimeBubbleExitEvent(GameObject obj, Vector3 pos) 
	{
		this.obj = obj;
		this.collisionPosition = pos;
	}
}

public class TimeBubble : MonoBehaviour {

	public float exitForce = 10000;
	public float exitDistanceScale = .9f;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	void OnTriggerExit(Collider collider)
	{
		collider.gameObject.SetActive(false);
		Vector3 pos = collider.transform.position;

		var exitDirection = collider.transform.position - transform.position;
		collider.transform.position = transform.position - Vector3.Scale(exitDirection.normalized, transform.localScale * GetComponent<SphereCollider>().radius);

		var playerSpawner = new GameObject("PlayerSpawner").AddComponent<PlayerSpawner>();
		playerSpawner.player = collider.gameObject;
		playerSpawner.exitForce = exitDirection.normalized * exitForce;
		playerSpawner.spawnDelay = 0.5f;

		Event.dispatch (new TimeBubbleExitEvent (collider.gameObject, pos));
	}
}
