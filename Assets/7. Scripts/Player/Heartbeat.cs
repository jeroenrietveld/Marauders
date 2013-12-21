using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : MonoBehaviour {
	public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);
	public float groundOffset = 0.1f;
	private float _groundHeight;

	private Avatar _avatar;

	void Start ()
	{
		_avatar = transform.parent.GetComponent<Avatar>();
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
		position.y = Mathf.Min(_groundHeight, _avatar.transform.position.y) + groundOffset;
		transform.position = position;

		//Rotate the health circle
		transform.Rotate(Vector3.up, heartbeatSpeed * Time.deltaTime);
		//Pass player health to shader
		renderer.material.SetFloat("phase", 1 - _avatar.health);
	}
}
