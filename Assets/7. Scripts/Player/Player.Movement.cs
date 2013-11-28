using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public partial class Player : MonoBehaviour
{
	/// <summary>
	/// 
	/// </summary>
	public PlayerIndex playerIndex;
	public float turnSmoothing = 15f;
	public float speedDampTime = 0.1f;

	/// <summary>
	/// The height of the jump.
	/// </summary>
	public float jumpHeight;
	
	private Animator anim;
	private HashIDs hash;
	
	private Camera _camera;

	/// <summary>
	/// The move speed, 5 is default
	/// </summary>
	public DecoratableFloat movementSpeed = new DecoratableFloat(10);

	void Awake()
	{
		anim = GetComponent<Animator>();
		hash = GetComponent<HashIDs>();
		_camera = Camera.main;
	}
	
	void FixedUpdate()
	{
		//Debug.DrawRay(transform.position, -Vector3.up * 0.1f, Color.blue);
		//Debug.DrawRay(transform.position, Vector3.forward * 11f, Color.red);
		
		Vector3 camDirection = _camera.transform.forward + _camera.transform.up;
		camDirection.y = 0;
		camDirection.Normalize();
		
		Vector3 camRight = _camera.transform.right;
		camDirection.y = 0;
		camDirection.Normalize();

		//Xbox Controls:
		float h = GamePad.GetState(playerIndex).ThumbSticks.Left.X;
		float v = GamePad.GetState(playerIndex).ThumbSticks.Left.Y;
		bool jump = GamePad.GetState(playerIndex).Buttons.A == ButtonState.Pressed;
		
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
		
		MovementManagement(moveSpeed, jump);
	}
	
	void MovementManagement (Vector3 moveSpeed, bool jump)
	{
		if (moveSpeed.sqrMagnitude > 0)
		{
			Rotating(moveSpeed);

			anim.SetFloat(hash.speedFloat, moveSpeed.magnitude * this.movementSpeed, speedDampTime, Time.deltaTime);

			rigidbody.MovePosition(rigidbody.position + moveSpeed * (this.movementSpeed / 2f) * Time.deltaTime);
		}
		else
		{
			anim.SetFloat(hash.speedFloat, 0f);
		}
		
		if (jump)
		{
			Jump(jumpHeight);
			anim.SetBool(hash.jumpBool, true);
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

	//bool AgainstWall()
	//{
		//return Physics.Raycast(transform.position, Vector3.forward, out hit, 10f);

		//RaycastHit hit;
		//if (Physics.Raycast(transform.position, Vector3.forward, out hit))
		//{
			//Debug.Log("Hit");
			//if (hit.transform.tag == "wall")
			//{
				//return true;
			//}
		//}

		//return false;
	//}
}