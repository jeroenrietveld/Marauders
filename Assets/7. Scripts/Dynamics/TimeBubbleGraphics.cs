using UnityEngine;
using System.Collections;

public class TimeBubbleGraphics : MonoBehaviour {

	private Vector4 _currentOffsets = Vector4.zero;
	public Vector4 offsetMovementSpeed = Vector4.zero;
	public Vector4 offsetSpeedExitMultiplier = Vector4.one;

	private float secondaryDistortionStartTime;
	private float secondaryDistortionAnimationTime;
	private float secondaryDistortionStartValue;
	private float secondaryDistortionEndValue;

	// Use this for initialization
	void Start () {
		Event.register<TimeBubbleEnterEvent> (OnEnterEvent);
		//Event.register<TimeBubbleObjectExitEvent> (OnExitEvent);

		_currentOffsets = new Vector4(Random.value, Random.value, Random.value, Random.value);
		AnimateSecondaryDistortion (1.5f, float.Epsilon);
	}

	void OnDestroy()
	{
		Event.unregister<TimeBubbleEnterEvent> (OnEnterEvent);
		//Event.unregister<TimeBubbleObjectExitEvent> (OnExitEvent);
	}

	private void OnEnterEvent(TimeBubbleEnterEvent evt)
	{
		AnimateSecondaryDistortion (secondaryDistortionEndValue == -0.025f ? 1.5f : -0.025f , 1.5f);
		offsetMovementSpeed.Scale(offsetSpeedExitMultiplier);
		//AnimateSecondaryDistortion (2, 3);
	}

	private void OnExitEvent(TimeBubbleObjectExitEvent evt)
	{

	}

	void AnimateSecondaryDistortion(float targetValue, float time)
	{
		secondaryDistortionStartTime = Time.time;
		secondaryDistortionAnimationTime = time;
		secondaryDistortionStartValue = renderer.material.GetFloat ("DistortionCenter2");
		secondaryDistortionEndValue = targetValue;
	}
	
	// Update is called once per frame
	void Update () {
		var movement = Vector4.Scale (offsetMovementSpeed, Vector4.one * Time.deltaTime);

		_currentOffsets = new Vector4 (
			(_currentOffsets.x + movement.x) % 1f,
			(_currentOffsets.y + movement.y) % 1f,
			(_currentOffsets.z + movement.z) % 1f,
			(_currentOffsets.w + movement.w) % 1f
		);

		renderer.material.SetVector ("FractalOffsets", _currentOffsets);


		float phase = Mathf.Clamp01(
			(Time.time - secondaryDistortionStartTime) / secondaryDistortionAnimationTime
		);

		renderer.material.SetFloat (
			"DistortionCenter2", 
			Mathf.Lerp(secondaryDistortionStartValue, secondaryDistortionEndValue, phase)
		);
	}
}
