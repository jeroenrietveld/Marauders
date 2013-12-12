using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public partial class Player : MonoBehaviour
{
	public PlayerIndex playerIndex;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	public float jumpHeight = 22500;

	public GamePad controller { get { return _controller; } }
	private Camera _camera;
	/// <summary>
	/// The move speed, 5 is default
	/// </summary>
	public DecoratableFloat movementSpeed;

	/// <summary>
	/// Determines whether this instance is grounded.
	/// </summary>
	/// <returns><c>true</c> if this instance is grounded; otherwise, <c>false</c>.</returns>
	public bool isGrounded {
		get
		{
			return (distanceToGround < 0.5f);
		}
	}

	private float distanceToGround { 
		get
		{
			return _distanceToGround;
		}
		set
		{
			_distanceToGround = value;
			if (value > highestDistanceToGround)
			{
				highestDistanceToGround = value;
			}
		}
	}
	private float _distanceToGround;

	private float highestDistanceToGround {get;set;}

	private GamePad _controller;

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
	
	void Jump()
	{
		// Setting the jumping velocity to 0, from where we can add a fixed amount
		// This provides a constent heigvht
		var velocity = rigidbody.velocity;
		velocity.y = 0;
		rigidbody.velocity = velocity;
		rigidbody.AddForce(Vector3.up * this.jumpHeight);

		//Playing the jump
		AnimationJump();
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