using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Jump : ActionBase {

	public float distanceToGround;
	public bool onGround
	{
		get
		{
			if(distanceToGround < _distanceToGroundTolerance && distanceToGround != -1)
			{
				return true;
			}

			return false;
		}
	}

	public float jumpHeight = 25000f;
	private bool _isJumping = false;

	private float _distanceToGroundTolerance = 0.5f;

	void Start () {
		Avatar avatar = GetComponent<Avatar> ();
		
		ControllerMapping controllerMapping = GetComponent<ControllerMapping> ();
		controllerMapping.AddAction(Button.A, this);
		
		AnimationHandler animationHandler = GetComponent<AnimationHandler> ();

		animationHandler.AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Jump",
				AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
				2,
				WrapMode.ClampForever
			));

		animationHandler.AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Jump Land",
				AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
				2,
				WrapMode.Clamp
			));
	}
	
	public override void PerformAction()
	{
		if(onGround || _isJumping == false)
		{
			var velocity = rigidbody.velocity;
			velocity.y = 0;
			rigidbody.velocity = velocity;

			rigidbody.AddForce(Vector3.up * jumpHeight);

			_isJumping = true;
			
			AnimationJump();
		}
	}

	private void AnimationJump()
	{
		animation.Play ("Jump", PlayMode.StopSameLayer);
	}

	void Update()
	{
		CalculateDistanceToGround ();

		if(onGround && rigidbody.velocity.y < -5f)
		{
			animation.CrossFade("Jump Land", 0.1f, PlayMode.StopSameLayer);

			_isJumping = false;
		}
	}

	private void CalculateDistanceToGround()
	{
		RaycastHit hit;
		Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity);

		if(hit.collider != null)
		{
			distanceToGround = hit.distance;
		}
		else
		{
			distanceToGround = -1;
		}
	}
}
