
using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : Attackable
{
    public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);

    public float damageScale = 1f;
    public float damageAlphaScale = 4;
    private float _currentRotation;
    private float _armorFactor = 0.5f;

    private AudioSource heartbeatSource;

    private GameObject _damage;
    private Avatar _avatar;
	private float _regen = 0.01f;

    private Player _lastAttacker;
	public Player lastAttacker { get { return _lastAttacker; } }

	private Timer _lastAttackerTimer;

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

		_lastAttackerTimer = new Timer(4);
		_lastAttackerTimer.AddPhaseCallback (delegate
		{
			_lastAttacker = null;
		});
    }

    void FixedUpdate()
	{
		if(alive) health += _regen * Time.deltaTime;
	}

    void Update()
    {
        _currentRotation = (_currentRotation + heartbeatSpeed * Time.deltaTime) % 360;
        transform.rotation = Quaternion.AngleAxis(_currentRotation, Vector3.up);

        _damageTimer.Update();

		_lastAttackerTimer.Update ();
    }

	protected override void ApplyAttack(Attack attacker)
    {
		var direction = attacker.transform.position - transform.position;
		direction.y = 0;
		direction.Normalize();

		var source = new DamageSource (
			attacker.GetComponent<Avatar>().player,
			direction,
			-direction * (attacker.isCombo ? attacker.comboKnockBackForce : attacker.standardKnockBackForce),
			attacker.weapon.damage,
			attacker.GetStunTime(),
			false
		);

		ApplyDamage (source);
    }

	public void ApplyDamage(DamageSource source)
	{
		_lastAttacker = source.inflicter;
		_lastAttackerTimer.Start ();

		if(source.amount > 0)
		{
			bool armorHit = false;
			
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

			if (!alive)
			{
				Event.dispatch(new AvatarDeathEvent(_avatar.player, source.inflicter));
			}
		}

		_avatar.rigidbody.AddForce (source.force, ForceMode.Impulse);
		
		if(source.stunTime > 0)
		{
			var stun = _avatar.gameObject.AddComponent<Stun>();
			Destroy(stun, source.stunTime);
		}
	}
}