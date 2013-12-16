using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject obj;
	public float spawnDelay = 1;
	public Vector3 exitForce = Vector3.zero;
	public Vector3 position =  Vector3.zero;
	public Timer timer;

	private Vector3 _startPosition;

	void Start () {
		if(obj.GetComponent<CameraTracking>())
		{
			gameObject.AddComponent<CameraTracking>();
		}

		_startPosition = obj.transform.position;
		obj.transform.position = position;

		timer = new Timer (spawnDelay);
		timer.AddCallback (SpawnPlayer);
		timer.Start ();
	}

	void Update () {
		timer.Update ();

		transform.position = Vector3.Lerp(_startPosition, position, timer.Phase());
	}

	public void SpawnPlayer()
	{
		obj.SetActive(true);

		var objRigidbody = obj.GetComponent<Rigidbody>();
		if(objRigidbody)
		{
			objRigidbody.velocity = Vector3.zero;
			objRigidbody.AddForce(exitForce, ForceMode.Impulse);
		}
		obj.transform.position = position;
		
		Event.dispatch(new TimeBubbleEnterEvent(obj));
		
		Destroy(gameObject);
	}
}
