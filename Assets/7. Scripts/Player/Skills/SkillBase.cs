using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

public enum SkillType
{
	Offensive,
	Defensive,
	Utility
}

public abstract class SkillBase : ActionBase
{
	private static Button[] _buttonMapping = new []{ Button.X, Button.B, Button.Y };


	public SkillType skillType { get; private set; }
	public Timer cooldown { get; private set; }
	public AnimationHandler.AnimationSettings animationSettings { get; private set; }

	protected abstract void OnPerformAction();
	protected abstract void OnUpdate();

	protected SkillBase(SkillType skillType, float cooldownTime, AnimationHandler.AnimationSettings animationSettings)
	{
		this.skillType = skillType;
		cooldown = new Timer(cooldownTime);
		this.animationSettings = animationSettings;
	}

    public override void PerformAction()
	{
		if (!cooldown.running)
		{
			animation.Play(animationSettings.animationName);
			cooldown.Start ();
			OnPerformAction ();
		}

	}

	void Start()
	{
		GetComponent<ControllerMapping>().AddAction(_buttonMapping[(int)skillType], this);
		GetComponent<AnimationHandler>().AddAnimation(animationSettings);
	}

	void Update()
	{
		cooldown.Update();
		OnUpdate ();
	}
}

