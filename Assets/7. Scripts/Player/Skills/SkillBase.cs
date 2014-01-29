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
	private static string[] _notificationPrefabPath = new []{ "Offensive", "Defensive", "Dash"};
	private static Vector3[] _notificationOffset = new [] 
	{
		new Vector3(-1, 0.5f),
		new Vector3(1, 0.5f),
		new Vector3(0, 1.5f)
	};

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
		cooldown.AddPhaseCallback(DisplayNotification);

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

	private void DisplayNotification()
	{
		var notification = CameraSettings.cameraSettings.Notify(
			"Prefabs/GUI/" + _notificationPrefabPath[(int)skillType],
			transform.position + _notificationOffset[(int)skillType],
			1.5f,
			Vector3.up * 0.25f
		);
	}
}

