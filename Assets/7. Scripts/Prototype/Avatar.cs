using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public class Avatar : MonoBehaviour 
{	
	public GamePad controller;
	public Weapon primaryWeapon;

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

	void Update () {
	}

	private void SetWeapon(Weapon weaponHolder)
	{
		primaryWeapon = weaponHolder;
		weaponHolder.owner = this;
		
		while(weaponHolder.transform.childCount > 0)
		{	
			Transform weapon = weaponHolder.transform.GetChild(0);
			Transform hand = FindInChildren(transform, weapon.gameObject.name);
			
			weapon.rotation = hand.rotation;
			weapon.parent = hand;
			weapon.position = hand.position;
		}

		GetComponent<Attack> ().UpdateAttackAnimations (weaponHolder);
	}
	
	private static Transform FindInChildren(Transform transform, string name)
	{
		if(transform.gameObject.name == name)
		{
			return transform;
		}
		
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform result = FindInChildren(transform.GetChild(i), name);
			if(result)
			{
				return result;
			}
		}
		
		return null;
	}
}
