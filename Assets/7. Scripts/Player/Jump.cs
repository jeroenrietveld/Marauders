using UnityEngine;
using System.Collections;
using System.Collections.Generic;
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

	public float jumpHeight = 30000f;
	private bool _isJumping = false;

	private float _distanceToGroundTolerance = 0.5f;

	private AudioSource _jumpSource;
    private List<AudioClip> _jumpClips;
	
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
        LoadSounds();
	}
	
	public override void PerformAction()
	{
		if(_isJumping == false)
		{
			_isJumping = true;

			_jumpSource.clip = _jumpClips[new System.Random().Next(0, _jumpClips.Count)];
            _jumpSource.Play();
			
			var velocity = rigidbody.velocity;
			velocity.y = 0;
			rigidbody.velocity = velocity;

			rigidbody.AddForce(Vector3.up * jumpHeight);
			//rigidbody.AddForce(transform.forward * 10000f);
			
			AnimationJump();
		}
	}
	
	private void LoadSounds()
    {
        _jumpSource = gameObject.AddComponent<AudioSource>();
        _jumpSource.playOnAwake = false;
        _jumpSource.minDistance = 200f;
        _jumpSource.maxDistance = 250f;

        string name = GetComponent<Avatar>().player.marauder;
        _jumpClips = new List<AudioClip>();
        _jumpClips.Add(Resources.Load<AudioClip>("Sounds/Characters/" + name + "/jump1"));
        _jumpClips.Add(Resources.Load<AudioClip>("Sounds/Characters/" + name + "/jump2"));
    }

	private void AnimationStop()
	{
		animation.Stop("Jump");
	}

	private void AnimationJump()
	{
		animation.Play ("Jump", PlayMode.StopSameLayer);
	}

	void Update()
	{
		CalculateDistanceToGround ();

		if(onGround)
		{
			if ( rigidbody.velocity.y < 0)
			{
				if ( rigidbody.velocity.y < -5)
				{
				 	animation.CrossFade("Jump Land", 0.1f, PlayMode.StopSameLayer);
				} else
				{
					AnimationStop();
				}

				//We've landed
				_isJumping = false;
			}


		}
		/*else
		{
			if (rigidbody.velocity.y < -5)
			{
				AnimationJump();
			}
		}*/
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
