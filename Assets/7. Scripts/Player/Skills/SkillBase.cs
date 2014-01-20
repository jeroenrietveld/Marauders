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
	Utility,
	Movement
}

public abstract class SkillBase : ActionBase
{
	private static Button[] _buttonMapping = new []{ Button.X, Button.B, Button.Y, Button.LeftShoulder };

	public SkillType skillType = SkillType.Movement;
	public Timer cooldown { get; private set; }
	public AnimationHandler.AnimationSettings animationSettings { get; private set; }

	protected abstract void OnPerformAction();
	protected abstract void OnUpdate();
	protected abstract void OnStart();

    protected AudioSource skillAudioSource;

	protected SkillBase(float cooldownTime, AnimationHandler.AnimationSettings animationSettings)
	{
		cooldown = new Timer(cooldownTime);
		cooldown.Start ();
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
		OnStart();
        skillAudioSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
	}

	void Update()
	{
		cooldown.Update();
		OnUpdate ();
	}
}

