using UnityEngine;
using System.Collections;

public partial class Player : MonoBehaviour
{
	private Transform UpperBody;// = transform.Find("Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2");
	private Transform LeftLeg;// = transform.Find("Character1_Reference/Character1_Hips/Character1_LeftUpLeg");
	private Transform RightLeg;// = transform.Find("Character1_Reference/Character1_Hips/Character1_RightUpLeg");
	
	private string PreviousMovementAnimation;

	private float crossFadeDuration = 0.3f;

	private bool inAir = false;

	protected void InitializeAnimations()
	{
		UpperBody = transform.Find("Character1_Reference/Character1_Hips/");
		LeftLeg = transform.Find("Character1_Reference/Character1_Hips/Character1_LeftUpLeg");
		RightLeg = transform.Find("Character1_Reference/Character1_Hips/Character1_RightUpLeg");

		//Starting with idle
		animation.Play ("Idle");
		animation["Idle"].wrapMode = WrapMode.Once;
		animation["Walk"].wrapMode = WrapMode.Once;
		animation["Run"].wrapMode = WrapMode.Once;
		animation["Jump"].wrapMode = WrapMode.ClampForever;
		animation["Jump Land"].wrapMode = WrapMode.Once;
	}

	protected void AnimationIdle()
	{
		animation["Idle"].AddMixingTransform(UpperBody);
		animation["Idle"].AddMixingTransform(LeftLeg);
		animation["Idle"].AddMixingTransform(RightLeg);
		animation.CrossFade("Idle", crossFadeDuration, PlayMode.StopAll);
	}

	protected void AnimationJump()
	{
		//Should jump
		animation.Play ("Jump");
	}

	protected void AnimationMovement(float speed)
	{
		if (this.IsGrounded())
		{
			//Should play land animation
			if(inAir)
			{
				animation.Play ("Jump Land");
				inAir = false;
			}

			//Idling
			if (speed < 1f)
			{
				AnimationIdle();
				return;
			}

			//Walking
			if (speed < 4f)
			{
				animation["Walk"].AddMixingTransform(UpperBody);
				animation["Walk"].AddMixingTransform(LeftLeg);
				animation["Walk"].AddMixingTransform(RightLeg);
				animation["Walk"].speed = speed * 0.45f;
				animation.CrossFade("Walk", crossFadeDuration, PlayMode.StopAll);

				return;
			}

			//Running 
			animation["Run"].AddMixingTransform(UpperBody);
			animation["Run"].AddMixingTransform(LeftLeg);
			animation["Run"].AddMixingTransform(RightLeg);
			animation["Run"].speed = speed * 0.2f;
			animation.CrossFade("Run", crossFadeDuration, PlayMode.StopAll);

			return;
		} else
		{
			inAir = true;
		}
	}

	protected void AnimationAttack(int index)
	{
		//animation["Attack1"].AddMixingTransform(Transform);
		//animation.Play("Attack1");
	}

	protected void AnimationLand()
	{
		
	}
}