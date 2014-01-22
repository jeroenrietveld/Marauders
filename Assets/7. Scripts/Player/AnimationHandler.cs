using UnityEngine;
using System.Collections;
using System;
using System.Collections.Generic;

public class AnimationHandler : MonoBehaviour 
{
	public Transform lowerBody;
	public Transform upperBody;
	
	public struct AnimationSettings
	{
		public AttackInfo attackInfo;
		public MixTransforms mixTransform;
		public int layer;
		public WrapMode wrapMode;

		public AnimationSettings(AttackInfo attackInfo, MixTransforms mixTransform, int layer, WrapMode wrapMode)
		{
			this.attackInfo = attackInfo;
			this.mixTransform = mixTransform;
			this.layer = layer;
			this.wrapMode = wrapMode;
		}
	}

	[Flags]
	public enum MixTransforms
	{
		Lowerbody = 1,
		Upperbody = 2
	}

	void OnEnable () {
		upperBody = transform.Find("Character1_Reference/Character1_Hips/Character1_Spine");
		lowerBody = transform.Find("Character1_Reference/Character1_Hips");
	}
	
	public void AddAnimation(AnimationSettings settings)
	{
		animation [settings.attackInfo.animationName].wrapMode = settings.wrapMode;
		animation [settings.attackInfo.animationName].layer = settings.layer;
				
		if((settings.mixTransform & MixTransforms.Lowerbody) == MixTransforms.Lowerbody)
		{
			animation[settings.attackInfo.animationName].AddMixingTransform(lowerBody);
		}
		if((settings.mixTransform & MixTransforms.Upperbody) == MixTransforms.Upperbody)
		{
			animation[settings.attackInfo.animationName].AddMixingTransform(upperBody);
		}

		if (settings.attackInfo.timing != -1f)
		{
			//Applying spee
			animation[settings.attackInfo.animationName].speed = settings.attackInfo.speed;
		}
	}

	public void Pause()
	{
		foreach(AnimationState animState in GetComponent<Animation>())
		{
			animState.enabled = false;
		}
	}

	public void UnPause()
	{
		foreach(AnimationState animState in GetComponent<Animation>())
		{
			animState.enabled = true;
		}
	}
}
