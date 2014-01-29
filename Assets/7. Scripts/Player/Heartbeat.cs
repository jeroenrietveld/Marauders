﻿using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : Attackable
{
    public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);
    // Used for Bulwark.
    public DecoratableFloat damageMultiplier = new DecoratableFloat(1);

    public float damageScale = 1f;
    public float damageAlphaScale = 4;
    private float _currentRotation;
    private float _armorFactor = 0.30f;

    private AudioSource heartbeatSource;

    private GameObject _damage;
    private Avatar _avatar;
	private ParticleSystem _hitEmitter;
	
	private float _regen = 0.05f;
	private Timer _regenDelay;

	public Player lastAttacker {get;set;}
	public Timer lastAttackerTimer;

    private Timer _damageTimer;

    private float _health = 1f;
    public float health
    {
        get
        {
            return _health;
        }
        set
        {
            if (alive)
            {
                _health = Mathf.Clamp01(value);
				renderer.material.SetFloat("_phase", health);
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

    void Start()
    {
        heartbeatSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
        _damage = transform.GetChild(0).gameObject;
        _avatar = transform.root.GetComponent<Avatar>();

		_hitEmitter = ResourceCache.GameObject("Prefabs/ParticleEmitters/HitEmitter").GetComponent<ParticleSystem>();
		_hitEmitter.transform.SetParentKeepLocal (_avatar.transform);

        renderer.material.SetColor("_color", _avatar.player.color);
        _damage.renderer.material.SetColor("playerColor", _avatar.player.color);

        _damageTimer = new Timer(.35f);
        _damageTimer.AddPhaseCallback(0, delegate
        {
            _damage.renderer.enabled = true;
        });
        _damageTimer.AddPhaseCallback(delegate
        {
            _damage.renderer.enabled = false;
        });
        _damageTimer.AddTickCallback(delegate
        {
            _damage.transform.localScale = Vector3.one * (1 + _damageTimer.Phase() * damageScale);
            _damage.renderer.material.SetFloat("alpha", Mathf.Clamp01(damageAlphaScale - _damageTimer.Phase() * damageAlphaScale));
        });

		//Initializing the last attack timer
		lastAttackerTimer = new Timer(2.0f);
		lastAttackerTimer.AddPhaseCallback (delegate
		{		
			//Resetting has changed and moved to Jump, this still needs to be there to check if 2 seconds have past.
		});

		_regenDelay = new Timer (5);
    }

    void FixedUpdate()
	{
		_regenDelay.Update ();
		if(alive && !_regenDelay.running) health += _regen * Time.deltaTime;
	}

    void Update()
    {
        _currentRotation = (_currentRotation + heartbeatSpeed * Time.deltaTime) % 360;
        transform.rotation = Quaternion.AngleAxis(_currentRotation, Vector3.up);

        _damageTimer.Update();

		lastAttackerTimer.Update ();
    }

	protected override void ApplyAttack(DamageSource source)
	{
		this.lastAttacker = source.inflicter;
		this.lastAttackerTimer.Start ();

		if(source.amount > 0)
		{
			bool armorHit = false;

            source.amount *= damageMultiplier;

			if(!source.piercing)
			{
				float dot = Vector3.Dot(source.direction, transform.forward);
				armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;
			}
			
			if (armorHit)
			{
				source.amount *= _armorFactor;
				GameManager.Instance.soundInGame.PlaySoundRandom(heartbeatSource, "armorhit", false);
			}
			else
			{
				GameManager.Instance.soundInGame.PlaySoundRandom(heartbeatSource, "heartbeathit", false);
			}
            
			var previousHealth = health;
			health = health - source.amount;

			var material = _damage.renderer.material;
			material.SetFloat("upperBound", previousHealth);
			material.SetFloat("lowerBound", health);
			_damageTimer.Start();

			if(source.inflicter != null)
			{
				_hitEmitter.startColor = source.inflicter.color;
				_hitEmitter.transform.rotation = Quaternion.LookRotation(-source.direction);
				_hitEmitter.Play ();
			}

			if (!alive)
			{
				Event.dispatch(new AvatarDeathEvent(_avatar.player, source.inflicter));
			}

			_regenDelay.Start();
		}

		_avatar.rigidbody.AddForce (source.force, ForceMode.Impulse);
		
		if(source.stunTime > 0)
		{
			_avatar.gameObject.GetComponent<Stun>().SetStunTime(source.stunTime);
		}
	}
}