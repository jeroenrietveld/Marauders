using UnityEngine;
using System.Collections;

public class TimeBubbleGraphics : MonoBehaviour {

	private Vector4 _currentOffsets = Vector4.zero;
	public Vector4 offsetMovementSpeed = Vector4.zero;

	private Timer _distortionTimer;

	private float secondaryDistortionStartTime;
	private float secondaryDistortionAnimationTime;
	private float secondaryDistortionStartValue;
	private float secondaryDistortionEndValue;

	// Use this for initialization
	void Start () {
		Event.register<TimeBubbleEnterEvent> (OnEnterEvent);

		_distortionTimer = new Timer (1.5f);
		_distortionTimer.endPhase = 1.5f;
		_distortionTimer.startPhase = -0.025f;

		_currentOffsets = new Vector4(Random.value, Random.value, Random.value, Random.value);
	}

	void OnDestroy()
	{
		Event.unregister<TimeBubbleEnterEvent> (OnEnterEvent);
	}

	private void OnEnterEvent(TimeBubbleEnterEvent evt)
	{
		if (!_distortionTimer.running) _distortionTimer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		if (_distortionTimer != null)
		{
			_distortionTimer.Update ();
		}

		var movement = Vector4.Scale (offsetMovementSpeed, Vector4.one * Time.deltaTime);

		_currentOffsets = new Vector4 (
			(_currentOffsets.x + movement.x) % 1f,
			(_currentOffsets.y + movement.y) % 1f,
			(_currentOffsets.z + movement.z) % 1f,
			(_currentOffsets.w + movement.w) % 1f
		);

		renderer.material.SetVector ("FractalOffsets", _currentOffsets);

		renderer.material.SetFloat (
			"DistortionCenter2",
			_distortionTimer.Phase()
		);
	}
}
