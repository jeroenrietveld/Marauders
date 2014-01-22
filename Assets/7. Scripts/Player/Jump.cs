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
    private Avatar _avatar;
	
	void Start () {
		_avatar = GetComponent<Avatar> ();
		
		ControllerMapping controllerMapping = GetComponent<ControllerMapping> ();
		controllerMapping.AddAction(Button.A, this);
		
		AnimationHandler animationHandler = GetComponent<AnimationHandler> ();

		//Will only look at the attack info and speed
		AttackInfo info = new AttackInfo("Jump", 1.0f, -1f);

		animationHandler.AddAnimation(
			new AnimationHandler.AnimationSettings(
				info,
				AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
				2,
				WrapMode.ClampForever
			));

		//Will only look at the attack info and speed
		info = new AttackInfo("Jump Land", 1.0f, -1f);

		animationHandler.AddAnimation(
			new AnimationHandler.AnimationSettings(
				info,
				AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
				2,
				WrapMode.Clamp
			));
        _jumpSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
	}
	
	public override void PerformAction()
	{
		if(_isJumping == false)
		{
			_isJumping = true;
            GameManager.Instance.soundInGame.PlaySoundRandom(_jumpSource, _avatar.player.marauder + "-jump", false);
			
			var velocity = rigidbody.velocity;
			velocity.y = 0;
			rigidbody.velocity = velocity;

			rigidbody.AddForce(Vector3.up * jumpHeight);
			//rigidbody.AddForce(transform.forward * 10000f);
			
			AnimationJump();
		}
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
                // Make the type of sound to play dynamic. Instead of always leather and wood sound types.
                GameManager.Instance.soundInGame.PlaySoundRandom(_jumpSource, _avatar.player.footsole + "-" + GameManager.Instance.matchSettings.groundType + "-land", true);
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
