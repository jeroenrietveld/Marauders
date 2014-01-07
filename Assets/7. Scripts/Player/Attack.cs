using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Attack : ActionBase {

	//TODO: Knockback properties weapon / player dependent?
	public float standardKnockBackForce = 200;
	public float comboKnockBackForce = 700;

	private Weapon _weapon;
	private TrailRenderer[] _trailRenderers;

	// Current successive successful attacks
	private int _comboCount;
	// Maximum time between successful attacks until combo counter resets
	private Timer _comboReset;
	// Time until damage is applied after starting the attack
	private Timer _attackDelay;

	private Timer _trailTimer;

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
				var trailTime = (1 - Mathf.Pow(_trailTimer.Phase(), 3)) * .1f;
				foreach(var renderer in _trailRenderers)
				{
					renderer.time = trailTime;
				}
			}
		});

		_attackDelay = new Timer(.5f);
		_attackDelay.AddPhaseCallback (DoAttack);
		_attackDelay.AddPhaseCallback (_trailTimer.Start);
	}

	void Start () {
		ControllerMapping controllerMapping = GetComponent<ControllerMapping> ();
		controllerMapping.AddAction(Button.RightShoulder, this);
	}

	public override void PerformAction()
	{
		if (_weapon && !animation.IsPlaying(_weapon.attacks[_comboCount].animationName))
		{
			_attackDelay.endTime = _weapon.attacks[_comboCount].timing;
			_attackDelay.Start();
			_comboReset.Start();

			_trailTimer.endTime = animation.GetClip(_weapon.attacks[_comboCount].animationName).length - _attackDelay.endTime;

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
		_comboReset.Update ();
		_trailTimer.Update ();
	}

	private void DoAttack()
	{
		bool hasHit = false;
		Collider[] colls = Physics.OverlapSphere(transform.position, _weapon.range);

		foreach(Collider hit in colls) 
		{
			var avatar = hit.gameObject.GetComponent<Avatar>();
			
			if (avatar && avatar.gameObject != gameObject)
			{ 
				// Is this really neccessary? The physics check should suffice I think..
				float dst = Vector3.Distance(avatar.transform.position, transform.position);
				
				if (dst <= _weapon.range)
				{
					float angle = Mathf.Acos(Vector3.Dot (transform.forward, (avatar.transform.position - transform.position).normalized));
					
					if (Mathf.Abs(angle / 0.0174532925f) < 45)
					{
						hasHit = true;
						ApplyDamage(avatar, _comboCount < _weapon.attacks.Count - 1 ? standardKnockBackForce : comboKnockBackForce);
					}
				}
			}
		}

		if (hasHit)
		{
			_comboCount = (_comboCount + 1) % _weapon.attacks.Count;
		}
		else
		{
			_comboCount = 0;
		}
	}
	
	private void ApplyDamage(Avatar avatar, float knockBackForce)
	{
		Vector3 attackDirection = (avatar.transform.position - transform.position).normalized;

		avatar.ApplyDamage(-attackDirection, _weapon.damage, gameObject.GetComponent<Avatar>().player);
		avatar.rigidbody.AddForce(attackDirection * knockBackForce, ForceMode.Impulse);
	}
}
