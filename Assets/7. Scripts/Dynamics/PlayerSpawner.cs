using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject player;
	public float spawnDelay = 1;
	public Vector3 exitForce = Vector3.zero;
	public Timer timer;

	void Start () {
		gameObject.AddComponent<CameraTracking>();
		transform.position = player.transform.position;

		timer = new Timer (spawnDelay);
		timer.AddCallback (spawnDelay, SpawnPlayer);
		timer.Start ();
	}

	void Update () {
		timer.Update ();
	}

	public void SpawnPlayer()
	{
		player.SetActive(true);
		player.rigidbody.velocity = Vector3.zero;
		player.rigidbody.AddForce(exitForce, ForceMode.Impulse);
		
		Event.dispatch(new TimeBubbleEnterEvent());
		
		Destroy(gameObject);
	}
}
