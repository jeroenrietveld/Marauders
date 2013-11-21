using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour
{
	private CameraMovement _camera;
	
	void Awake()
	{
		_camera = GameObject.FindGameObjectWithTag(Tags.MAINCAMERA).GetComponent<CameraMovement>();
		_camera.AddTrackableObject(this.gameObject);
	}
	
	void OnDisable()
	{
		_camera.RemoveTrackableObject(this.gameObject);
	}
}
