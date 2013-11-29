using UnityEngine;
using System.Collections;

public class MenuCameraMovement : MonoBehaviour {

	public Vector3 targetPosition { set; private get; }
	private float _cameraSpeed;
	public float cameraSpeed
	{
		set
		{
			_cameraSpeed = value;
		}
	}

	public bool isMoving 
	{
		get 
		{
			return (targetPosition != transform.position);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, _cameraSpeed * Time.deltaTime);
	}
}
