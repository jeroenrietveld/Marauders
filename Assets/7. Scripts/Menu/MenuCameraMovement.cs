using UnityEngine;
using System.Collections;

public class MenuCameraMovement : MonoBehaviour {

	// Position of the camera at the start of the animation
	private Vector3 _startPosition;
	private Vector3 _targetPosition;

	// Absolute time at the start of the animation
	private float _startTime;
	// Time it takes to move from start position to target position
	private float _moveTime = 1f;

	public void SetTarget(Vector3 position, float time)
	{
		_startPosition = transform.position;
		_targetPosition = position;
		_moveTime = time;
		_startTime = Time.time;
	}

	public bool isMoving 
	{
		get 
		{
			return (_targetPosition != transform.position);
		}
	}

	// Update is called once per frame
	void Update () {
		float phase = (Time.time - _startTime) / _moveTime;

		if(phase >= 1f) // Ensure no floating point roundoff errors happen
		{
			transform.position = _targetPosition;
		}
		else
		{
			float lerpFactor = Mathf.Cos(phase * Mathf.PI) * .5f + .5f;
			transform.position = Vector3.Lerp(_targetPosition, _startPosition, lerpFactor);
		}
	}
}
