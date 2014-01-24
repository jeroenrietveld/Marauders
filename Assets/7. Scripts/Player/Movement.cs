using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Movement : MonoBehaviour {

	const float MAX_SPEED_IDLE = 0.3f;
	const float MAX_SPEED_WALKING = 3.0f;
	const float MAX_SPEED_RUNNING = 5.0f;

	public float maxMagnetDistance = 5f;
	public float magnetAngle = 140f;

	public DecoratableFloat movementSpeed = new DecoratableFloat (5.0f);
	public float turnSmoothing = 15f;

	private Avatar _avatar;
	private Camera _camera;

	public bool isTeleporting;

	private Vector3 _targetVelocity;
	public Vector3 targetVelocity { get { return _targetVelocity; } }
	
	private Vector3 _rigidbodyYAxis = new Vector3(0, 0.75f, 0);

    private AudioSource movementSource;

	void Start () {
        movementSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volumemovement);

		_avatar = GetComponent<Avatar> ();
		_camera = Camera.main;

		//Will only look at the attack info name
		AttackInfo info = new AttackInfo("Walk", 1.0f, -1f);

		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				info,
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Once
			));
	

		//Will only look at the attack info name
		info = new AttackInfo("Run", 1.0f, -1f);
		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				info,
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Loop
			));
	}
	
	void FixedUpdate () {

		_targetVelocity = GetTargetVelocity();

		if (!GetComponent<Jump>().onGround)
		{
			//Debug.DrawRay(rigidbody.position + _rigidbodyYAxis, _targetVelocity.normalized * (1), Color.green);
			
			// If you don't want a gameobject to collide with a raycast, select the "Ignore Raycast" layer via the Unity Inspector.
			int layerMask = 1 << 2;
			layerMask = ~layerMask;
			
			RaycastHit hit;

			if(Physics.Raycast(rigidbody.position + _rigidbodyYAxis, _targetVelocity.normalized, out hit, 1, layerMask))
			{
				// Decrease Y velocity?
			}
			else if (targetVelocity.sqrMagnitude > 0)
			{
				Rotating();
				
				rigidbody.MovePosition(rigidbody.position + targetVelocity * Time.deltaTime);
				MagnetAim ();
			}
		}
		else if (targetVelocity.sqrMagnitude > 0)
		{
			Rotating();
			
			rigidbody.MovePosition(rigidbody.position + targetVelocity * Time.deltaTime);
			MagnetAim ();
		}
	}

	void MagnetAim()
	{
		Avatar myAvatar = GetComponent<Avatar> ();

		foreach(Player player in GameManager.Instance.playerRefs)
		{
			if(player != myAvatar.player)
			{
				GameObject avatar = player.avatar;

				Vector3 difference = avatar.transform.position - myAvatar.transform.position;
				difference.y = 0;
				float distance = difference.magnitude;

				if(distance < maxMagnetDistance)
				{
					difference = difference.normalized;

					float factor = 1-(distance / maxMagnetDistance);

					if(Mathf.Acos(Vector3.Dot(_targetVelocity.normalized, difference)) < (factor * magnetAngle) * Mathf.Deg2Rad)
					{
						_targetVelocity = difference * _targetVelocity.magnitude;
						
						Quaternion targetRotation = Quaternion.LookRotation(_targetVelocity, Vector3.up);
						rigidbody.MoveRotation(targetRotation);
					}
				}
			}
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
			//Quaternion newRotation = Quaternion.Lerp(rigidbody.rotation, targetRotation, turnSmoothing * Time.deltaTime);
			
			rigidbody.MoveRotation(targetRotation);
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
            GameManager.Instance.soundInGame.StopSound(movementSource);
			animation.CrossFade("Idle", 0.3f, PlayMode.StopSameLayer);
		}
		else if(targetVelocity < MAX_SPEED_WALKING)
		{
            // Make the type of sound to play dynamic. Instead of always wood sound types.
            if(GetComponent<Jump>().onGround)
            {
                GameManager.Instance.soundInGame.PlaySoundLoopAndEndtime(movementSource, _avatar.player.footsole + "-" + GameManager.Instance.matchSettings.groundType +"-walk", false, 0.65f);
            }

            if(!animation.IsPlaying ("Walk"))
			{
                animation.CrossFade("Walk", 0.3f, PlayMode.StopSameLayer);
				animation["Walk"].speed = targetVelocity * 0.7f;
            }
		}
		else
		{
            // Make the type of sound to play dynamic. Instead of always wood sound types.
            if (GetComponent<Jump>().onGround)
            {
                GameManager.Instance.soundInGame.PlaySoundLoopAndEndtime(movementSource, _avatar.player.footsole + "-" + GameManager.Instance.matchSettings.groundType + "-run", false, 0.4f);
            }
            
            if (!animation.IsPlaying("Run"))
			{
				animation.CrossFade("Run", 0.3f, PlayMode.StopSameLayer);
				animation["Run"].speed = targetVelocity * 0.2f;
			}
		}
	}
}