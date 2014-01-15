using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : Attackable {
	public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);
	public float groundOffset = 0.1f;

	public float damageScale = 1f;
	public float damageAlphaScale = 4;

	private float _groundHeight;
	private float _playerOffset;
	private float _currentRotation;
	
	private GameObject _damage;
	private Avatar _avatar;

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
			if(alive)
			{
				_health = Mathf.Clamp01(value);
				
				//controller.SetVibration(1, 1, .2f);
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

	void Start ()
	{
		_damage = transform.GetChild(0).gameObject;
		_avatar = transform.parent.GetComponent<Avatar>();

		renderer.material.SetColor("playerColor", _avatar.player.color);
		_damage.renderer.material.SetColor("playerColor", _avatar.player.color);

		_playerOffset = (int)_avatar.player.index * 0.01f;

		_damageTimer = new Timer(.35f);
		_damageTimer.AddPhaseCallback (0, delegate {
			_damage.renderer.enabled = true;
		});
		_damageTimer.AddPhaseCallback (delegate {
			_damage.renderer.enabled = false;
		});
		_damageTimer.AddTickCallback (delegate {
			_damage.transform.localScale = Vector3.one * (1 + _damageTimer.Phase() * damageScale);
			_damage.renderer.material.SetFloat("alpha", Mathf.Clamp01(damageAlphaScale - _damageTimer.Phase() * damageAlphaScale));
		});
	}

	void FixedUpdate()
	{
		var collisionResult = Physics.RaycastAll(_avatar.transform.position + Vector3.up, Vector3.down);

		float maxHeight = float.NegativeInfinity;
		foreach (var result in collisionResult)
		{
			maxHeight = Mathf.Max(maxHeight, result.point.y);
		}

		if (!float.IsInfinity (maxHeight))
		{
			_groundHeight = maxHeight;
		}
	}

	void Update ()
	{
		var position = transform.position;
		position.y = Mathf.Min(_groundHeight, _avatar.transform.position.y) + groundOffset + _playerOffset;
		transform.position = position;

		_currentRotation = (_currentRotation + heartbeatSpeed * Time.deltaTime) % 360;
		transform.rotation = Quaternion.AngleAxis(_currentRotation, Vector3.up);

		_damageTimer.Update();
	}

	public override void OnAttack(Attack attacker)
	{
		var direction = attacker.transform.position - transform.position;
		direction.y = 0;
		direction.Normalize ();

		float dot = Vector3.Dot(direction, transform.forward);
		bool armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;

		var amount = attacker.weapon.damage;
		
		if(armorHit)
		{
			amount = amount * _armorFactor;
		}

		var previousHealth = health;
		health = health - amount;

		_avatar.rigidbody.AddForce(-direction * (attacker.isCombo ? attacker.comboKnockBackForce : attacker.standardKnockBackForce), ForceMode.Impulse);

		renderer.material.SetFloat ("health", health);

		var material = _damage.renderer.material;
		material.SetFloat("upperBound", previousHealth);
		material.SetFloat("lowerBound", health);
		_damageTimer.Start();
		
		if(!alive) 
		{
			Event.dispatch(new AvatarDeathEvent(_avatar.player, attacker.GetComponent<Avatar>().player));
		}
		else
		{
			var stun = _avatar.gameObject.AddComponent<Stun>();
			Destroy(stun, attacker.GetStunTime());
		}
	}
}
