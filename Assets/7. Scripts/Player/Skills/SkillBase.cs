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

	public string animationName { get; protected set; }

	public Timer cooldown { get; private set; }

	public SkillType skillType { get; private set; }

	protected abstract void OnPerformAction();
	protected abstract void OnUpdate();

	protected SkillBase(SkillType skillType, float cooldownTime)
	{
		this.skillType = skillType;
		cooldown = new Timer(cooldownTime);
	}

    public override void PerformAction()
	{
		//TODO: Play animation

		if (!cooldown.running)
		{
			cooldown.Start ();
			OnPerformAction ();
		}

	}

	void Start()
	{
		GetComponent<ControllerMapping>().AddAction(_buttonMapping[(int)skillType], this);
	}

	void Update()
	{
		cooldown.Update();
		OnUpdate ();
	}
}

