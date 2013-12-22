using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : MonoBehaviour {
	public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);
	public float groundOffset = 0.1f;
	public float maxDamagePerSecond = .5f;

	private float _groundHeight;
	private float _playerOffset;
	private float _currentHealtIndication;
	private float _previousHealthIndication;

	private Avatar _avatar;

	void Start ()
	{
		_avatar = transform.parent.GetComponent<Avatar>();
		_playerOffset = (int)_avatar.player.index * 0.01f;
		_currentHealtIndication = _previousHealthIndication = _avatar.health;
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

		//Rotate the health circle
		transform.Rotate(Vector3.up, heartbeatSpeed * Time.deltaTime);

		float health = _avatar.health;

		_currentHealtIndication = Mathf.MoveTowards(_currentHealtIndication, health, maxDamagePerSecond * Time.deltaTime);

		if (!Mathf.Approximately(health, _currentHealtIndication))
		{
			renderer.material.SetFloat ("health", 1 - _currentHealtIndication);
		}
		else if(!Mathf.Approximately(_previousHealthIndication, health))
		{
			_previousHealthIndication = Mathf.MoveTowards(_previousHealthIndication, health, maxDamagePerSecond * Time.deltaTime);
			renderer.material.SetFloat("previousHealth", 1 - _previousHealthIndication);
		}

	}
}
