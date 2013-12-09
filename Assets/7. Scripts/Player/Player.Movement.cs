using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public partial class Player : MonoBehaviour
{
	public PlayerIndex playerIndex;
	private Animator anim;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	public float jumpHeight = 22500;

	public GamePad controller { get { return _controller; } }
	
	//private Animator anim;
	private HashIDs hash;
	
	private Camera _camera;
	/// <summary>
	/// The move speed, 5 is default
	/// </summary>
	public DecoratableFloat movementSpeed;

	private GamePad _controller;

	void FixedUpdate()
	{
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
		bool[] attacks = new bool[3];
		//attacks[0] = Input.GetKeyDown(KeyCode.LeftControl);
		attacks [0] = _controller.Axis (Axis.RightTrigger) > 0.1;
		attacks[1] = Input.GetKeyDown(KeyCode.LeftShift);
		attacks[2] = _controller.Axis(Axis.LeftTrigger) != 0;
		
		//PC Controls:
		//float h = Input.GetAxis("Horizontal");
		//float v = Input.GetAxis ("Vertical");
		//bool jump = Input.GetKeyDown("space");
		//bool[] attacks = new bool[3];
		//attacks[0] = Input.GetKeyDown(KeyCode.LeftControl);
		//attacks[1] = Input.GetKeyDown(KeyCode.LeftShift);
		//attacks[2] = Input.GetKeyDown(KeyCode.Z);
		
		Vector3 moveSpeed = camDirection * v + camRight * h;
		
		//Disabling walk animation
		if (!IsGrounded())
		{
			jump = false;
		}


		MovementManagement(moveSpeed);

		JumpManagement(moveSpeed, jump);

		AttackManagement(attacks);
	}
	
	void MovementManagement (Vector3 moveSpeed)
	{
		if (moveSpeed.sqrMagnitude > 0)
		{
			Rotating(moveSpeed);
			//anim.SetFloat(hash.speedFloat, moveSpeed.magnitude * movementSpeed, speedDampTime, Time.deltaTime);
		
		
			rigidbody.MovePosition(rigidbody.position + moveSpeed * movementSpeed * Time.deltaTime);
		}
		else
		{
			//anim.SetFloat(hash.speedFloat, 0f);
		}

		AnimationMovement(moveSpeed.magnitude * movementSpeed);
	}

	void JumpManagement(Vector3 direction, bool jump)
	{
		if (jump && !AgainstWall(direction))
		{
			Jump(jumpHeight);
		}
	}

	void AttackManagement(bool[] attacks)
	{

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

		//Playing the jump
		AnimationJump();
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