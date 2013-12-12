using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public partial class Player : MonoBehaviour
{
	public Weapon primaryWeapon;
	public Weapon secondaryWeapon;
	
	public SkillBase offensiveSkill { get; set; }
	public SkillBase utilitySkill { get; set; }
	public SkillBase defensiveSkill { get; set; }

	public float health
	{
		get {
			return _health;
		}
		
		set {
			_health = Mathf.Clamp01(value);

			//Event.dispatch(new PlayerHitEvent());
			
			if(_health == 0f)
			{
				//Event.dispatch(new PlayerDeathEvent());
			}
		}
	}
	public float armorFactor = 0.5f;
	private float _health = 0.6f;
	private Heartbeat _heartbeat;


	void Awake()
	{
		_camera = Camera.main;
		_controller = ControllerInput.GetController (playerIndex);
		_heartbeat = transform.FindChild ("Heartbeat_indicator").GetComponent<Heartbeat>();

		InitializeAnimations();

		//TODO: remove (testing purposes)
		utilitySkill = gameObject.AddComponent<Dash> ();
	}

	public Player()
	{
	}
	
	/// <summary>
	/// Picks up weapon and puts in in 'primaryWeapon' 
	/// </summary>
	/// <param name="weapon">The weapon that's beeing picked up</param>
	/// <param name="gametypeSpecificObj">If true, moves primary to secondary, else; drops primary weapon</param>
	public void PickUpWeapon(Weapon weapon, bool gametypeSpecificObj)
	{
		if (gametypeSpecificObj)
		{
			secondaryWeapon = primaryWeapon;
		}
		
		SetWeapon (weapon);
	}
	
	private void SetWeapon(Weapon weaponHolder)
	{
		primaryWeapon = weaponHolder;
		weaponHolder.owner = this;

		for(int i = 0; i < weaponHolder.transform.childCount; i++)
		{
			Transform weapon = weaponHolder.transform.GetChild(i);
			Transform hand = FindInChildren(transform, weapon.gameObject.name);

			weapon.rotation = hand.rotation;
			weapon.parent = hand;
			weapon.position = hand.position;
		}
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
	
	public void DropPrimaryWeapon()
	{
		//Manually set the position a little higher so it wont fall through the ground
		Vector3 position = transform.position;
		position.y += 2;

		PickupSpawner.SpawnWeapon (primaryWeapon.gameObject, primaryWeapon.transform.position);
		primaryWeapon.owner = null;
	}

    /// <summary>
    /// Check is game is paused and sets the timeScale in the GameManager.
    /// Create the menu from the prefabMenu.
    /// </summary>
    public void Update()
    {
		if (Input.GetMouseButtonDown(0) || controller.JustPressed(Button.Start) && !GameManager.isPaused)
		{
			//GameManager.Instance.PauseGame();
		

            Menu skillMenu = SkillSelectMenu.Attach(this.gameObject);
            skillMenu.controllers.Add(controller);
		} 
		
		if (controller.JustPressed(Button.X))
		{
			//xSkill.performAction(this);
		}

		if(controller.JustPressed(Button.Y))
		{
			DropPrimaryWeapon();
		}

        if (controller.JustPressed(Button.Start) && !GameManager.isPaused)
        {
            GameManager.Instance.PauseGame();
            //Instantiate(prefabMenu);
        }

		if (controller.JustPressed(Button.B))
		{
			AnimationAttack();
		}

		if(controller.JustPressed(Button.X) && !utilitySkill.cooldown.running)
		{
			utilitySkill.PerformAction();
			animation.Play(utilitySkill.animationName, PlayMode.StopSameLayer);
		}
    }

	/// <summary>
	/// This method is called by the attack animation
	/// </summary>
	public void Attack()
	{
		if(primaryWeapon)
		{
			primaryWeapon.Attack ();
		}
	}
	
	public void ApplyDamage(Vector3 direction, float amount)
	{
		float dot = Vector3.Dot(direction.normalized, _heartbeat.transform.forward);
		bool armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;
		
		if(armorHit)
		{
			amount = amount * armorFactor;
		}
		
		health = health - amount;
	}
	
	public void LoadModel(PlayerModel model)
	{
		this.utilitySkill = (SkillBase)Activator.CreateInstance(null, model.utilitySkill).Unwrap();
		this.offensiveSkill = (SkillBase)Activator.CreateInstance(null, model.offensiveSkill).Unwrap();
		this.defensiveSkill = (SkillBase)Activator.CreateInstance(null, model.defensiveSkill).Unwrap();
		this.playerIndex = model.index;
	}
}