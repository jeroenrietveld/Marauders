using UnityEngine;
using System.Collections;

public class PlayerSpawner : MonoBehaviour {

	public GameObject player;
	public float spawnDelay = 1;
	public Vector3 exitForce = Vector3.zero;

	// Use this for initialization
	void Start () {
		gameObject.AddComponent<CameraTracking>();
		transform.position = player.transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		spawnDelay -= Time.deltaTime;

		if(spawnDelay <= 0)
		{
			player.SetActive(true);
			player.rigidbody.velocity = Vector3.zero;
			player.rigidbody.AddForce(exitForce, ForceMode.Impulse);

			Event.dispatch(new TimeBubbleEnterEvent());

			Destroy(gameObject);
		}
	}
}
