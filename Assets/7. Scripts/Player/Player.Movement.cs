﻿using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public partial class Player : MonoBehaviour
{
	public PlayerIndex playerIndex;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	public float jumpHeight;

	public GamePad controller { get { return _controller; } }
	
	private Animator anim;
	private HashIDs hash;
	
	private Camera _camera;
	/// <summary>
	/// The move speed, 5 is default
	/// </summary>
	public DecoratableFloat movementSpeed;

	private GamePad _controller;

	void Awake()
	{
		anim = GetComponent<Animator>();
		hash = GetComponent<HashIDs>();
		_camera = Camera.main;
		_controller = ControllerInput.GetController (playerIndex);
	}
	
	void FixedUpdate()
	{
		//Debug.DrawRay(transform.position, -Vector3.up * 0.1f, Color.blue);

		Vector3 camDirection = _camera.transform.forward + _camera.transform.up;
		camDirection.y = 0;
		camDirection.Normalize();
		
		Vector3 camRight = _camera.transform.right;
		camDirection.y = 0;
		camDirection.Normalize();

		//Xbox Controls:
		float h = _controller.Axis (Axis.LeftHorizantal);
		float v = _controller.Axis (Axis.LeftVertical);
		bool jump = _controller.Pressed (Button.A);
		
		//PC Controls:
		/*float h = Input.GetAxis("Horizontal");
		float v = Input.GetAxis ("Vertical");
		bool jump = Input.GetKeyDown("space");*/
		
		Vector3 moveSpeed = camDirection * v + camRight * h;
		
		//Disabling walk animation
		if (!IsGrounded())
		{
			anim.SetFloat (hash.speedFloat, 0, speedDampTime, Time.deltaTime);
			jump = false;
		}

		//Debug.DrawRay(transform.position, moveSpeed * 1f, Color.red);
		MovementManagement(moveSpeed, jump);
	}
	
	void MovementManagement (Vector3 moveSpeed, bool jump)
	{
		if (moveSpeed.sqrMagnitude > 0)
		{
			Rotating(moveSpeed);

			anim.SetFloat(hash.speedFloat, moveSpeed.magnitude * movementSpeed, speedDampTime, Time.deltaTime);

			rigidbody.MovePosition(rigidbody.position + moveSpeed * movementSpeed * Time.deltaTime);
		}
		else
		{
			anim.SetFloat(hash.speedFloat, 0f);
		}
		
		if (jump)
		{
			if (!AgainstWall(moveSpeed))
			{
				Jump(jumpHeight);
				anim.SetBool(hash.jumpBool, true);
			}
		}
		else
		{
			anim.SetBool(hash.jumpBool, false);
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
		// This solves the high jumping when the player is on a lovecube
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

	/// <summary>
	/// Returns true if the player walks against an object with a "Wall" tag.
	/// </summary>
	/// <returns><c>true</c>, if player walks against an object with "Wall" tag, <c>false</c> otherwise.</returns>
	/// <param name="direction">Direction.</param>
	bool AgainstWall(Vector3 direction)
	{
		RaycastHit hit;
		if (Physics.Raycast(transform.position, direction, out hit, 3f))
		{
			// a tag is case sensitive
			if (hit.transform.tag == "Wall")
			{
				return true;
			}
		}

		return false;
	}
}