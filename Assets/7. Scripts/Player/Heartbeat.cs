using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : MonoBehaviour {
	public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);
	public float groundOffset = 0.1f;

	public float damageScale = 1f;
	public float damageAlphaScale = 4;

	private float _groundHeight;
	private float _playerOffset;
	private float _previousHealth;
	private float _currentRotation;
	
	private GameObject _damage;
	private Avatar _avatar;

	private Timer _damageTimer;

	void Start ()
	{
		_damage = transform.GetChild(0).gameObject;
		_avatar = transform.parent.GetComponent<Avatar>();
		_avatar.heartbeat = this;

		renderer.material.SetColor("playerColor", _avatar.player.color);
		_damage.renderer.material.SetColor("playerColor", _avatar.player.color);

		_playerOffset = (int)_avatar.player.index * 0.01f;
		_previousHealth = _avatar.health;

		_damageTimer = new Timer(.35f);
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

		float health = _avatar.health;
		renderer.material.SetFloat ("health", health);

		if(!Mathf.Approximately(_previousHealth, health))
		{
			var material = _damage.renderer.material;

			material.SetFloat("upperBound", _previousHealth);
			material.SetFloat("lowerBound", health);

			_previousHealth = health;
			_damageTimer.Start();
		}

		_damageTimer.Update();

		_damage.renderer.enabled = _damageTimer.running;
		_damage.transform.localScale = Vector3.one * (1 + _damageTimer.Phase() * damageScale);
		_damage.renderer.material.SetFloat("alpha", Mathf.Clamp01(damageAlphaScale - _damageTimer.Phase() * damageAlphaScale));
	}
}
