using UnityEngine;
using System.Collections;

public class GroundProjection : MonoBehaviour {
	
	public float groundOffset = 0.1f;
	
	private float _groundHeight;
	private float _playerOffset;
	
	private Avatar _avatar;

	void Start()
	{
		_avatar = transform.root.GetComponent<Avatar> ();
		_playerOffset = (int)_avatar.player.index * 0.01f;
	}

	void FixedUpdate()
	{
		var collisionResult = Physics.RaycastAll(_avatar.transform.position + Vector3.up, Vector3.down);
		
		float maxHeight = float.NegativeInfinity;
		foreach (var result in collisionResult)
		{
			maxHeight = Mathf.Max(maxHeight, result.point.y);
		}
		
		if (!float.IsInfinity(maxHeight))
		{
			_groundHeight = maxHeight;
		}
	}

	void Update ()
	{
		var position = transform.position;
		position.y = Mathf.Min(_groundHeight, _avatar.transform.position.y) + groundOffset + _playerOffset;
		transform.position = position;
	}
}
