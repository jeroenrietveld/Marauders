using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public class Avatar : MonoBehaviour 
{	
	public GamePad controller;

	public Player player { get; private set; }

	void Start () {
		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Idle",
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Loop
			));
	}
	
	public void Initialize(Player player)
	{
		this.player = player;
		this.controller = player.controller;
	}

	//TODO: remove this schmuck when avatar is moved using forces
	private Vector3 _previousPosition;
	public Vector3 calculatedVelocity;
	void FixedUpdate()
	{
		calculatedVelocity = (transform.position - _previousPosition) / Time.deltaTime;
		_previousPosition = transform.position;
	}
}
