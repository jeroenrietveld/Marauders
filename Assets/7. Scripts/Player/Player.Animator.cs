using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public partial class Player : MonoBehaviour
{
	public Transform LowerBody;
	public Transform UpperBody;
	
	private float crossFadeDuration = 0.3f;

	private bool attackLowerBody = false;

	private DateTime attackAnimationStart;

	private List<string> attackAnimationsInitialized = new List<string>();

	private string attackAnimationName 
	{ 
		get 
		{ 
			if (primaryWeapon != null)
			{
				return primaryWeapon.attacks[primaryWeapon.currentAttack].animationName;
			}

			return "";
		}
	}

	private void InitializeAnimationEvents(Weapon weapon)
	{
		foreach (AttackInfo attack in weapon.attacks)
		{
			if (!attackAnimationsInitialized.Contains(attack.animationName))
			{
				//Setting the event
				AnimationEvent e = new AnimationEvent();
				e.time = attack.timing * attack.speed;
				e.functionName = "DetectPlayerHit";
				animation[attackAnimationName].clip.AddEvent(e);

				attackAnimationsInitialized.Add (attack.animationName);
			}
		}
	}

	protected void InitializeAnimations()
	{
		UpperBody = transform.Find("Character1_Reference/Character1_Hips/Character1_Spine");
		LowerBody = transform.Find("Character1_Reference/Character1_Hips/");
		
		//Starting with idle, need to start like this
		animation.Play ("Idle");
		animation["Idle"].wrapMode = WrapMode.Once;
		animation["Idle"].layer = 1;
		animation["Idle"].AddMixingTransform(LowerBody);
		
		animation["Walk"].wrapMode = WrapMode.Once;
		animation["Walk"].layer = 1;
		animation["Walk"].AddMixingTransform(LowerBody);
		
		animation["Run"].wrapMode = WrapMode.Once;
		animation["Run"].layer = 1;
		animation["Run"].AddMixingTransform(LowerBody);
		
		animation["Jump"].wrapMode = WrapMode.ClampForever;
		animation["Jump"].layer = 1;
		animation["Jump"].AddMixingTransform(LowerBody);
		
		animation["Jump Land"].wrapMode = WrapMode.Once;
		animation["Jump Land"].layer = 1;
		animation["Jump Land"].AddMixingTransform(LowerBody);
		
	}
	
	protected void AnimationIdle()
	{
		//We want the FULL attack animation when standing still
		if ((attackAnimationName != "") && (attackLowerBody == false))
		{
			animation[attackAnimationName].AddMixingTransform(LowerBody);
			attackLowerBody = true;
		}
		animation.CrossFade("Idle", crossFadeDuration, PlayMode.StopSameLayer);
	}
	
	protected void AnimationJump()
	{
		//Should jump
		animation.Play ("Jump", PlayMode.StopSameLayer);
	}
	
	protected void AnimationMovement(float speed)
	{
		if (this.isGrounded)
		{
			//Should play land animation
			if(highestDistanceToGround > 0.5)
			{
				animation.Play ("Jump Land");
			}

			//Resetting it
			highestDistanceToGround = 0.1f;
			
			//Idling
			if (speed < 0.2f)
			{
				AnimationIdle();
				return;
			}
			
			//Removing lower body attack animation, cause we're walking!
			if ((attackAnimationName != "") && (attackLowerBody))
			{
				attackLowerBody = false;
				animation[attackAnimationName].RemoveMixingTransform(LowerBody);
			} 
			
			//Walking
			if (speed < 3f)
			{
				animation.CrossFade("Walk", crossFadeDuration, PlayMode.StopSameLayer);
				animation["Walk"].speed = speed * 0.7f;
				
				return;
			}
						
			animation.CrossFade("Run", crossFadeDuration, PlayMode.StopSameLayer);
			animation["Run"].speed = speed * 0.2f;
						
			return;
		} 
		else
		{
			if ( highestDistanceToGround > 0.5 )
			{
				AnimationJump();
			}

			//Removing lower body attack animation, cause we're walking!
			if ((attackAnimationName != "") && (attackLowerBody))
			{
				attackLowerBody = false;
				animation[attackAnimationName].RemoveMixingTransform(LowerBody);
			} 
		}
	}
	
	protected void AnimationAttack()
	{
		//Security checks
		if (primaryWeapon == null) { return ; }
		if (primaryWeapon.attacks.Count == 0 ) { return ; }
		
		//Calculating the amount the time that has passed since last accak
		TimeSpan span = DateTime.Now.Subtract(attackAnimationStart);
		
		//checking which attack animation should be played
		if (span.TotalMilliseconds > 2500)
		{
			//ressetting it to -1, will get updated to 0 later on
			primaryWeapon.currentAttack = 0;
		}

		//Can not attack 2x at the same time
		if (!animation.IsPlaying(primaryWeapon.attacks[primaryWeapon.currentAttack].animationName))
		{
			//Upping the animation index
			primaryWeapon.currentAttack = (primaryWeapon.currentAttack + 1) % primaryWeapon.attacks.Count;

			//Setting the info
			AttackInfo attack = primaryWeapon.attacks[primaryWeapon.currentAttack];
			animation[attack.animationName].AddMixingTransform(UpperBody);
			animation[attack.animationName].AddMixingTransform(LowerBody);
			animation[attack.animationName].speed = attack.speed;
			animation[attack.animationName].wrapMode = WrapMode.Once;
			animation[attack.animationName].layer = 2;
			attackLowerBody = true;

			//Resseting the start time
			attackAnimationStart = DateTime.Now;

			animation.CrossFade(attackAnimationName, 0.1f, PlayMode.StopSameLayer);
		}
	}
}