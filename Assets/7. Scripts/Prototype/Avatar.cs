using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public class Avatar : MonoBehaviour 
{	
	public GamePad controller;
	
	private float _health = 1f;
	public float health
	{
		get
		{
			return _health;
		}
		set
		{
			if(alive)
			{
				_health = Mathf.Clamp01(value);

				controller.SetVibration(1, 1, .2f);

				if(_health == 0)
				{
					//TODO: Handle player death
				}
			}
		}
	}

	public bool alive
	{
		get
		{
			return health > 0;
		}
	}

	private float _armorFactor = 0.1f;

	private Heartbeat _heartbeat;
	public PlayerRef player { get; private set; }

	void Start () {
		_heartbeat = transform.FindChild ("Heartbeat_indicator").GetComponent<Heartbeat>();
		_heartbeat.renderer.material.SetColor("playerColor", player.color);

		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Idle",
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Loop
			));
	}
	
	public void Initialize(PlayerRef player)
	{
		this.controller = player.controller;
		this.player = player;

		foreach(string skill in player.skills)
		{
			if(skill != null)
			{
				gameObject.AddComponent(skill);
			}
		}
	}

	public void ApplyDamage(Vector3 direction, float amount)
	{
		float dot = Vector3.Dot(direction.normalized, -_heartbeat.transform.forward);
		bool armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;
		
		if(armorHit)
		{
			amount = amount * _armorFactor;
		}
		
		health = health - amount;
	}

}
