using UnityEngine;
using System.Collections;

public class CameraTracking : MonoBehaviour
{
	private CameraMovement camera;
	
	void Awake()
	{
		camera = GameObject.FindGameObjectWithTag(Tags.MainCamera).GetComponent<CameraMovement>();
		camera.AddTrackableObject(this.gameObject);
	}
	
	void OnDisable()
	{
		camera.RemoveTrackableObject(this.gameObject);
	}
}
