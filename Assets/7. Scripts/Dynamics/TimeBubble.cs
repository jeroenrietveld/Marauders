using UnityEngine;
using System.Collections;

public struct TimeBubbleEnterEvent
{
}

public struct TimeBubbleExitEvent
{

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

	void OnTriggerEnter(Collider collider)
	{
		collider.gameObject.SetActive(false);

		var exitDirection = collider.transform.position - transform.position;
		collider.transform.position = transform.position - exitDirection * exitDistanceScale;

		var playerSpawner = new GameObject("PlayerSpawner").AddComponent<PlayerSpawner>();
		playerSpawner.player = collider.gameObject;
		playerSpawner.exitForce = exitDirection.normalized * exitForce;
		playerSpawner.spawnDelay = 3f;

		Event.dispatch (new TimeBubbleExitEvent ());
	}
}
