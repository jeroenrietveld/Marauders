using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Collections;
using XInputDotNetPure;

public class Attack : ActionBase {

	//TODO: Knockback properties weapon / player dependent?
	public float standardKnockBackForce = 200;
	public float comboKnockBackForce = 700;

	private Weapon _weapon;
	public Weapon weapon { get { return _weapon; } }
	private TrailRenderer[] _trailRenderers;

	// Current successive successful attacks
	private int _comboCount;
	public int comboCount { get { return _comboCount; }}
	public int nextComboCount { get { return (_comboCount + 1) % _weapon.attacks.Count; } }

	public bool isCombo { get { return _comboCount == _weapon.attacks.Count - 1; } }

	// Maximum time between successful attacks until combo counter resets
	private Timer _comboReset;
	// Time until damage is applied after starting the attack
	private Timer _attackDelay;
	// Time the current attack takes to perform
	private Timer _attackCooldown;

	private Timer _trailTimer;

	private AudioSource swingSource;
	
	public Attack()
	{
		//TODO: Combo reset weapon / player dependent?
		_comboReset = new Timer (1);
		_comboReset.AddCallback (delegate {
			_comboCount = 0;
		});
		
		_trailTimer = new Timer(0);
		_trailTimer.AddTickCallback(delegate ()
		{
			if(_trailRenderers != null)
			{
				var trailTime = (1 - Mathf.Pow(_trailTimer.Phase(), 3)) * .2f;
				foreach(var renderer in _trailRenderers)
				{
					renderer.time = trailTime;
				}
			}
		});

		_attackDelay = new Timer(.5f);
		_attackDelay.AddPhaseCallback (DoAttack);
		_attackDelay.AddPhaseCallback (_trailTimer.Start);

		_attackCooldown = new Timer (0);
	}

	void Start () {
		ControllerMapping controllerMapping = GetComponent<ControllerMapping> ();
		controllerMapping.AddAction(Button.B, this);

        swingSource = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
	}

	public override void PerformAction()
	{
		if (_weapon && !_attackCooldown.running)
		{
			_attackDelay.endTime = _weapon.attacks[_comboCount].timing;
			_attackDelay.Start();
			_comboReset.Start();

			_attackCooldown.endTime = animation.GetClip(_weapon.attacks[_comboCount].animationName).length;
			_attackCooldown.Start();

			_trailTimer.endTime = _attackCooldown.endTime - _attackDelay.endTime;

			animation.CrossFade(_weapon.attacks[_comboCount].animationName, 0.1f, PlayMode.StopSameLayer);
		}
	}

	public void SetWeapon(Weapon weaponHolder)
	{
		//Dropping the weapon
		if (_weapon != null)
		{
			GameObject dropWeapon = WeaponFactory.create (_weapon.name);
			PickupSpawner.SpawnWeapon (dropWeapon, transform.position);

			//Deleting the first grip
			Transform hand = transform.FindInChildren("Grip_0");
			int i = 1;

			//Deleting all the other hands
			while(hand != null)
			{
				hand.DestroyChildren();
				hand = transform.FindInChildren("Grip_" + i);
				i++;
			}
		}

		_weapon = weaponHolder;
		_trailRenderers = weaponHolder.GetComponentsInChildren<TrailRenderer> ();

		while(weaponHolder.transform.childCount > 0)
		{	
			Transform weapon = weaponHolder.transform.GetChild(0);
			Transform hand = transform.FindInChildren(weapon.gameObject.name);

			weapon.rotation = hand.rotation;
			weapon.parent = hand;
			weapon.position = hand.position;
		}

		weaponHolder.transform.parent = transform;

		AnimationHandler animationHandler = GetComponent<AnimationHandler> ();
		
		foreach(AttackInfo attackInfo in _weapon.attacks)
		{	
			animationHandler.AddAnimation(
				new AnimationHandler.AnimationSettings(
				attackInfo.animationName,
				AnimationHandler.MixTransforms.Upperbody,
				3,
				WrapMode.Once
			));
		}
	}

	void Update()
	{
		_attackDelay.Update ();
		_attackCooldown.Update ();
		_comboReset.Update ();
		_trailTimer.Update ();
	}

	private void DoAttack()
	{
		bool hasHit = false;
		Collider[] colls = Physics.OverlapSphere(transform.position, _weapon.range);
		
		GameManager.Instance.soundInGame.PlaySoundIndex(swingSource, "Swing", _comboCount, false);
		
		foreach(Collider hit in colls) 
		{
			if (hit.gameObject != gameObject)
			{
				// Is this really neccessary? The physics check should suffice I think..
				float dst = Vector3.Distance(hit.transform.position, transform.position);
				
				if (dst <= _weapon.range)
				{
					float angle = Mathf.Acos(Vector3.Dot (transform.forward, (hit.transform.position - transform.position).normalized));
					
					if (Mathf.Abs(angle / 0.0174532925f) < 70)
					{
						var attackables = hit.gameObject.GetComponentsInChildren<Attackable>();

						if(attackables.Length > 0)
						{
							foreach(var attackable in attackables)
							{
								attackable.DoAttack(this);
							}
							hasHit = true;
						}
					}
				}
			}
		}

		if (hasHit)
		{
			_comboCount = nextComboCount;
		}
		else
		{
			_comboCount = 0;
		}
	}
	
	public float GetStunTime()
	{
		return _trailTimer.endTime + _weapon.attacks[nextComboCount].timing;
	}
}
