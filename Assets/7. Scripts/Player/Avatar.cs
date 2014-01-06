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

	public Heartbeat heartbeat { set { _heartbeat = value; }}
	private Heartbeat _heartbeat;
	public Player player { get; private set; }

	void Start () {
		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Idle",
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Loop
			));
	}
	
	public void Initialize(Player player)
	{
		this.player = player;
		this.controller = player.controller;
	}

	public void ApplyDamage(Vector3 direction, float amount, Player offender)
	{
		float dot = Vector3.Dot(direction.normalized, _heartbeat.transform.forward);
		bool armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;
		
		if(armorHit)
		{
			amount = amount * _armorFactor;
		}
		
		health = health - amount;

		if(!alive) 
		{
			Event.dispatch(new AvatarDeathEvent(this.player, offender));

			player.StartSpawnProcedure();
		}
	}
}
