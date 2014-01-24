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
			Debug.Log ("There already is a CameraSettings instance");
		}

		_cameraSettings = this;
	}

	public Vector3 PointToWorldPoint(Vector3 point)
	{
		point = Camera.main.WorldToViewportPoint (point);
		point.z = 0.5f;
		return GetComponent<Camera> ().ViewportToWorldPoint (point);
	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnDestroy()
	{
		_cameraSettings = null;
	}
}
