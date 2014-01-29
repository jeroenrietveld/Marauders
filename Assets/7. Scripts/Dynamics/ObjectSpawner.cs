using UnityEngine;
using System.Collections;

public class ObjectSpawner : MonoBehaviour {

	public GameObject obj;
	public float spawnDelay = 1;
	public Vector3 exitForce = Vector3.zero;
	public Vector3 position =  Vector3.zero;
	public Timer timer;

	private Vector3 _startPosition;

	public static void Create(GameObject obj, Vector3 targetPosition, float spawnDelay, Vector3 exitForce)
	{
		obj.SetActive (false);

		var spawner = new GameObject("ObjectSpawner").AddComponent<ObjectSpawner>();
		spawner.obj = obj;
		spawner.spawnDelay = spawnDelay;
		spawner.position = targetPosition;
		spawner.exitForce = exitForce;
	}

	void Start () {
		if(obj.GetComponent<CameraTracking>())
		{
			gameObject.AddComponent<CameraTracking>();
		}

		_startPosition = obj.transform.position;
		obj.transform.position = position;

		timer = new Timer (spawnDelay);
		timer.AddCallback (SpawnObject);
		timer.Start ();
	}

	void Update () {
		timer.Update ();

		transform.position = Vector3.Lerp(_startPosition, position, timer.Phase());
	}

	public void SpawnObject()
	{
		obj.SetActive(true);

		var objRigidbody = obj.GetComponent<Rigidbody>();
		if(objRigidbody)
		{
			objRigidbody.velocity = Vector3.zero;
			objRigidbody.AddForce(exitForce, ForceMode.VelocityChange);
		}
		obj.transform.position = position;
		
		Event.dispatch(new TimeBubbleEnterEvent(obj));
		
		Destroy(gameObject);
	}
}
