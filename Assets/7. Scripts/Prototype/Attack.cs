using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Attack : ActionBase {

	//TODO: Knockback properties weapon / player dependent?
	public float standardKnockBackForce = 200;
	public float comboKnockBackForce = 700;

	private Weapon _weapon;
	// Current successive successful attacks
	private int _comboCount;
	// Maximum time between successful attacks until combo counter resets
	private Timer _comboReset;
	// Time until damage is applied after starting the attack
	private Timer _attackDelay;

	public Attack()
	{
		//TODO: Combo reset weapon / player dependent?
		_comboReset = new Timer (1);
		_comboReset.AddCallback (delegate {
			_comboCount = 0;
		});

		_attackDelay = new Timer(.5f);
		_attackDelay.AddPhaseCallback (DoAttack);
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

			animation.CrossFade(_weapon.attacks[_comboCount].animationName, 0.1f, PlayMode.StopSameLayer);
		}
	}

	public void SetWeapon(Weapon weaponHolder)
	{
		_weapon = weaponHolder;
		
		while(weaponHolder.transform.childCount > 0)
		{	
			Transform weapon = weaponHolder.transform.GetChild(0);
			Transform hand = Util.FindInChildren(transform, weapon.gameObject.name);
			
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
				AnimationHandler.MixTransforms.Lowerbody | AnimationHandler.MixTransforms.Upperbody,
				3,
				WrapMode.Once
			));
		}
	}

	void Update()
	{
		_attackDelay.Update ();
		_comboReset.Update ();
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
		
		avatar.ApplyDamage(-attackDirection, _weapon.damage);
		avatar.rigidbody.AddForce(attackDirection * knockBackForce, ForceMode.Impulse);
	}
}
