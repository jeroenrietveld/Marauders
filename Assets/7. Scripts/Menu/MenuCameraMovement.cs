using UnityEngine;
using System.Collections;

public class MenuCameraMovement : MonoBehaviour {

	public Vector3 targetPosition { set; private get; }
	public float cameraSpeed;
	public bool isMoving 
	{
		get 
		{
			return (targetPosition != transform.position);
		}
	}

	// Update is called once per frame
	void Update () {
		transform.position = Vector3.MoveTowards(transform.position, targetPosition, cameraSpeed * Time.deltaTime);
	}
}
