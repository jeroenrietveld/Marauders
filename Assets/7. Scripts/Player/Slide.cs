using UnityEngine;
using System.Collections;

public class Slide : MonoBehaviour {

	private Jump _jump;
	private Movement _movement;
	private Avatar _avatar;

	public float slideTreshold = 4;

	// Use this for initialization
	void Start () {
		_jump = GetComponent<Jump> ();
		_movement = GetComponent<Movement> ();
		_avatar = GetComponent<Avatar> ();
	}
	
	// Update is called once per frame
	void FixedUpdate () {
		if (_jump.onGround && Vector3.Distance (_movement.targetVelocity, Vector3.Scale(_avatar.calculatedVelocity, new Vector3(1, 0, 1))) >= slideTreshold)
		{
			_avatar.controller.SetVibration(1, 1, .1f);
		}
	}
}
