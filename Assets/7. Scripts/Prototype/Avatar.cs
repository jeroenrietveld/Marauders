using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public class Avatar : MonoBehaviour 
{	
	public GamePad controller;

	private float _health = 1f;
	private Heartbeat _heartbeat;
	private PlayerRef _player;

	void Start () {
		_heartbeat = transform.FindChild ("Heartbeat_indicator").GetComponent<Heartbeat>();
		_heartbeat.renderer.material.SetColor("playerColor", _player.color);

		GetComponent<AnimationHandler>().AddAnimation(
			new AnimationHandler.AnimationSettings(
				"Idle",
				AnimationHandler.MixTransforms.Lowerbody,
				1,
				WrapMode.Loop
			));
	}
	
	public void Initialize(PlayerRef player)
	{
		this.controller = player.controller;
		this._player = player;
		
		AddSkill (player.skills.utilitySkill);
		AddSkill (player.skills.defensiveSkill);
		AddSkill (player.skills.offensiveSkill);
	}
	
	private void AddSkill(string skill)
	{
		if(skill != null)
		{
			gameObject.AddComponent(skill);
		}
	}

	public void ApplyDamage(Vector3 direction, float damage)
	{

	}

}
