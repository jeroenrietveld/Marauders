using UnityEngine;
using System.Collections;

public class ShrineMovement : MonoBehaviour {

	private Vector3 _initialPosition;
	private Vector3 _activatedOffset = new Vector3(0, 1, 0);
	private bool _activated = false;

	private Timer _movementTimer;

	// Use this for initialization
	void Start () {
		_movementTimer = new Timer (1);

		_movementTimer.AddTickCallback (delegate {
			float from = _activated ? 0 : 1;
			float to  = _activated ? 1 : 0;

			transform.position = _initialPosition + _activatedOffset * Mathf.SmoothStep(from, to, _movementTimer.Phase());
		});

		_initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_movementTimer.Update ();
	}

	public void SetShrineActive(bool active)
	{
		_activated = active;
		_movementTimer.Start ();
	}
}
