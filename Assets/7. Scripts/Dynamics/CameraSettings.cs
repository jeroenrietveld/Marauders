using UnityEngine;
using System.Collections;

public class CameraSettings : MonoBehaviour {

	private static CameraSettings _cameraSettings;
	public static CameraSettings cameraSettings { 
		get
		{
			return _cameraSettings;
		}
	}

	// Use this for initialization
	void Start () {
		if(_cameraSettings != null)
		{
			Debug.Log("Camera Settings already exists");
		}

		_cameraSettings = this;
	}

	public Vector3 PointToWorldPoint(Vector3 point)
	{
		point = Camera.main.WorldToViewportPoint (point);
		point.z = 0.5f;
		return GetComponent<Camera> ().ViewportToWorldPoint (point);
	}

	public GameObject Notify(string path, Vector3 wsPosition, float duration, Vector2 translation)
	{
		GameObject gameObject = ResourceCache.GameObject (path);

		gameObject.transform.position = CameraSettings.cameraSettings.PointToWorldPoint(wsPosition);
		Notification notification = gameObject.AddComponent<Notification> ();
		notification.Initialize (duration, translation);

		return gameObject;
	}

	// Update is called once per frame
	void Update () {
	}

	void OnDestroy()
	{
		_cameraSettings = null;
	}
}
