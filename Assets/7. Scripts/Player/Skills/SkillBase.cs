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
	Movement
}

public abstract class SkillBase : ActionBase
{
	private static Button[] _buttonMapping = new []{ Button.X, Button.Y, Button.LeftShoulder };

	public SkillType skillType = SkillType.Movement;
	public Timer cooldown { get; private set; }
	public AnimationHandler.AnimationSettings animationSettings { get; set; }
	// Hold all of the animations. In the Start() method add them to the animation handler.
    public List<AnimationHandler.AnimationSettings> allAnimationSettings { get; set; }
	
	protected abstract void OnPerformAction();
	protected abstract void OnUpdate();
    protected AudioSource skillAudioSource;
	protected virtual void OnStart() {}

	protected SkillBase(float cooldownTime, params AnimationHandler.AnimationSettings[] animationSettings)
	{
		cooldown = new Timer(cooldownTime);
		cooldown.Start ();
		this.allAnimationSettings = animationSettings.ToList();
		this.animationSettings = animationSettings[0];
	}

    public override void PerformAction()
	{
		if (!cooldown.running)
		{
			animation.Play(animationSettings.attackInfo.animationName);
			cooldown.Start ();
			OnPerformAction ();
		}
	}

	void Start()
	{
		GetComponent<ControllerMapping>().AddAction(_buttonMapping[(int)skillType], this);
		AnimationHandler handler = GetComponent<AnimationHandler>();
        foreach (AnimationHandler.AnimationSettings item in allAnimationSettings)
        {
            handler.AddAnimation(item);
        }
		OnStart();
        skillAudioSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
	}

	void Update()
	{
		cooldown.Update();
		OnUpdate ();
	}
}

