using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject obj;
	public float spawnDelay = 1;
	public Vector3 exitForce = Vector3.zero;
	public Vector3 position =  Vector3.zero;
	public Timer timer;

	void Start () {
		gameObject.AddComponent<CameraTracking>();
		transform.position = position;

		timer = new Timer (spawnDelay);
		timer.AddCallback (SpawnPlayer);
		timer.Start ();
	}

	void Update () {
		timer.Update ();
	}

	public void SpawnPlayer()
	{
		obj.SetActive(true);
		obj.rigidbody.velocity = Vector3.zero;
		obj.rigidbody.AddForce(exitForce, ForceMode.Impulse);
		obj.transform.position = position;
		
		Event.dispatch(new TimeBubbleEnterEvent(obj));
		
		Destroy(gameObject);
	}
}
