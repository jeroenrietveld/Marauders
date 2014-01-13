using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Movement : MonoBehaviour {

	const float MAX_SPEED_IDLE = 0.3f;
	const float MAX_SPEED_WALKING = 3.0f;
	const float MAX_SPEED_RUNNING = 5.0f;

	public DecoratableFloat movementSpeed = new DecoratableFloat (5.0f);
	public float turnSmoothing = 15f;

	private Avatar _avatar;
	private Camera _camera;

	private Vector3 _targetVelocity;
	public Vector3 targetVelocity { get { return _targetVelocity; } }

	void Start () {
		_avatar = GetComponent<Avatar> ();
		_camera = Camera.main;

		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Walk",
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Once
			));
	
		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Run",
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Loop
			));
	}
	
	void FixedUpdate () {
		_targetVelocity = GetTargetVelocity();
		
		if (targetVelocity.sqrMagnitude > 0)
		{
			Rotating();
			
			rigidbody.MovePosition(rigidbody.position + targetVelocity * Time.deltaTime);
		}
	}

	void Update()
	{
		AnimationMovement (targetVelocity.magnitude);
	}

	private void Rotating ()
	{
		if (targetVelocity.sqrMagnitude != 0)
		{
			Vector3 moveDirection = targetVelocity.normalized;

			Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
			Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			
			rigidbody.MoveRotation(newRotation);
		}
	}

	private Vector3 GetTargetVelocity()
	{
		//Calculating the movement, relative to the camera
		Vector3 camDirection = _camera.transform.forward + _camera.transform.up;
		camDirection.y = 0;
		camDirection.Normalize();
		
		Vector3 camRight = _camera.transform.right;
		camDirection.y = 0;
		camDirection.Normalize();
		
		Vector3 targetVelocity = 
			(camDirection * _avatar.controller.Axis (Axis.LeftVertical)) + 
			(camRight * _avatar.controller.Axis (Axis.LeftHorizontal));
		
		return Vector3.ClampMagnitude(targetVelocity, 1.0f) * movementSpeed;
	}

	protected void AnimationMovement(float targetVelocity)
	{
		if(targetVelocity < MAX_SPEED_IDLE)
		{
			animation.CrossFade("Idle", 0.3f, PlayMode.StopSameLayer);
		}
		else if(targetVelocity < MAX_SPEED_WALKING)
		{
			if(!animation.IsPlaying ("Walk"))
			{
				animation.CrossFade("Walk", 0.3f, PlayMode.StopSameLayer);
				animation["Walk"].speed = targetVelocity * 0.7f;
			}
		}
		else
		{
			if (!animation.IsPlaying("Run"))
			{
				animation.CrossFade("Run", 0.3f, PlayMode.StopSameLayer);
				animation["Run"].speed = targetVelocity * 0.2f;
			}
		}
	}
}