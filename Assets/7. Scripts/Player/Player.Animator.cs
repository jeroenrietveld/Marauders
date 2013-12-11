using UnityEngine;
using System.Collections;
using System;

public partial class Player : MonoBehaviour
{
	private Transform LowerBody;
	private Transform UpperBody;
	
	private float crossFadeDuration = 0.3f;
	
	private bool inAir = false;
	
	private bool attackLowerBody = false;
	
	private int attackAnimationIndex = 0;
	private DateTime attackAnimationStart;
	private string attackAnimationName = "";
	
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
		} else
		{
			//Removing lower body attack animation, cause we're walking!
			if ((attackAnimationName != "") && (attackLowerBody))
			{
				attackLowerBody = false;
				animation[attackAnimationName].RemoveMixingTransform(LowerBody);
			} 
			
			inAir = true;
		}
	}
	
	protected void AnimationAttack()
	{
		//Security checks
		if (primaryWeapon == null) { return ; }
		if (primaryWeapon.animations.Count == 0 ) { return ; }
		
		//Can be null
		if (attackAnimationStart == null)
		{
			attackAnimationStart = DateTime.Now;
			attackAnimationIndex = -1;
		}
		
		//Calculating the amount the time that has passed since last accak
		TimeSpan span = DateTime.Now.Subtract(attackAnimationStart);
		
		//checking which attack animation should be played
		if (span.TotalSeconds > 2)
		{
			//ressetting it to -1, will get updated to 0 later on
			attackAnimationIndex = -1;
		} 
		
		//Upping the animation index
		attackAnimationIndex = (attackAnimationIndex + 1) % primaryWeapon.animations.Count;
		
		
		
		//Can not attack 2x at the same time
		if (!animation.IsPlaying(attackAnimationName))
		{
			//Playing the attack animation
			//Setting the attack name
			attackAnimationName = primaryWeapon.animations[attackAnimationIndex];
			animation[attackAnimationName].AddMixingTransform(UpperBody);
			animation[attackAnimationName].AddMixingTransform(LowerBody);
			animation[attackAnimationName].speed = 1.0f;
			animation[attackAnimationName].wrapMode = WrapMode.Once;
			animation[attackAnimationName].layer = 2;
			attackLowerBody = true;
			
			//Setting the event
			AnimationEvent e = new AnimationEvent();
			e.time = 0.2f;
			e.functionName = "Attack";
			animation[attackAnimationName].clip.AddEvent(e);
			
			//Resseting the start time
			attackAnimationStart = DateTime.Now;
			
			//animation.Play (attackAnimationName, PlayMode.StopSameLayer);
			//animation.Play(attackAnimationName, PlayMode.StopSameLayer);
			animation.CrossFade(attackAnimationName, 0.1f, PlayMode.StopSameLayer);
		}
	}
}