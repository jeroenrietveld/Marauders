using UnityEngine;
using System.Collections;

public class MenuCameraMovement : MonoBehaviour {

	// Position of the camera at the start of the animation
	private Vector3 _startPosition;
	private Vector3 _targetPosition;

    private Timer _timer = new Timer(0);

	public void SetTarget(Vector3 position, float time)
	{
        _timer.endTime = time;
        _timer.Start();

		_startPosition = transform.position;
		_targetPosition = position;
	}

	public bool isMoving 
	{
		get 
		{
			return (_timer.running);
		}
	}

	// Update is called once per frame
	void Update () {
        _timer.Update();

		if(_timer.Phase() >= 1f) // Ensure no floating point roundoff errors happen
		{
			transform.position = _targetPosition;
		}
		else
		{
			float lerpFactor = Mathf.Cos(_timer.Phase() * Mathf.PI) * .5f + .5f;
			transform.position = Vector3.Lerp(_targetPosition, _startPosition, lerpFactor);
		}
	}
}
