using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	private ControllerMapping input;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;
	
	public float jumpHeight;
	
	private Animator anim;
	private HashIDs hash;
	
	private Camera _camera;
	
	void Awake()
	{
		anim = GetComponent<Animator>();
		hash = GetComponent<HashIDs>();
		input = InputWrapper.Instance.GetController(1);
		_camera = Camera.main;
	}
	
	void FixedUpdate()
	{
		Debug.DrawRay(transform.position, -Vector3.up * 0.1f, Color.blue);
		
		Vector3 camDirection = _camera.transform.forward + _camera.transform.up;
		camDirection.y = 0;
		camDirection.Normalize();
		
		Vector3 camRight = _camera.transform.right;
		camDirection.y = 0;
		camDirection.Normalize();
		
		float h = input.GetAxis(XboxAxis.HorizontalLeft);
		float v = input.GetAxis(XboxAxis.VerticalLeft);
		bool jump = input.GetButton(XboxButton.A);
		
		Vector3 moveSpeed = camDirection * v + camRight * h;
		
		//Disabling walk animation
		if (!IsGrounded())
		{
			anim.SetFloat (hash.speedFloat, 0, speedDampTime, Time.deltaTime);
			jump = false;
		}
		
		MovementManagement(moveSpeed, jump);
		
		
	}
	
	void MovementManagement (Vector3 moveSpeed, bool jump)
	{
		if (moveSpeed.sqrMagnitude > 0)
		{
			Rotating(moveSpeed);
			anim.SetFloat(hash.speedFloat, moveSpeed.magnitude * 5, speedDampTime, Time.deltaTime);
		}
		else
		{
			anim.SetFloat(hash.speedFloat, 0f);
		}
		
		if (jump)
		{
			Jump(jumpHeight);
		}
	}
	
	void Rotating (Vector3 moveSpeed)
	{
		Vector3 moveDirection = moveSpeed.normalized;
		
		if (moveDirection.sqrMagnitude != 0)
		{
			Quaternion targetRotation = Quaternion.LookRotation(moveDirection, Vector3.up);
			Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			
			rigidbody.MoveRotation(newRotation);
		}
	}
	
	void Jump(float height)
	{
		//Not working
		var velocity = rigidbody.velocity;
		velocity.y = 0;
		rigidbody.velocity = velocity;
		
		rigidbody.AddForce(Vector3.up * height);
	}
	
	/// <summary>
	/// Determines whether this instance is grounded.
	/// </summary>
	/// <returns><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</returns>
	bool IsGrounded()
	{
		return Physics.Raycast(transform.position, -Vector3.up, 0.1f);
	}
}