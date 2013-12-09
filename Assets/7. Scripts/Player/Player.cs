using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public partial class Player : MonoBehaviour
{
	public Weapon primaryWeapon;
	public Weapon secondaryWeapon;
	
	public SkillBase bSkill { get; set; }
	public SkillBase xSkill { get; set; }
	public SkillBase ySkill { get; set; }
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
		anim = GetComponent<Animator>();
		hash = GetComponent<HashIDs>();
		_camera = Camera.main;
		_controller = ControllerInput.GetController (playerIndex);
		//_heartbeat = transform.FindChild ("Heartbeat_indicator").GetComponent<Heartbeat>();


		InitializeAnimations();
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
	
	private void SetWeapon(Weapon weapon)
	{
		primaryWeapon = weapon;
		
		Transform mainHand = transform.FindChild("Character1_Reference/Character1_Hips/Character1_Spine/Character1_Spine1/Character1_Spine2/Character1_RightShoulder/Character1_RightArm/Character1_RightForeArm/Character1_RightHand/MainHand");
		
		weapon.transform.rotation = mainHand.rotation;
		weapon.transform.parent = mainHand;
		weapon.transform.position = mainHand.position;
		weapon.owner = this;
	}
	
	public void DropPrimaryWeapon()
	{
		//Moving secondary to primary
		//primaryWeapon = secondaryWeapon;

		//Secondary is definately null now
		//secondaryWeapon = null;

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
		if (controller.JustPressed(Button.Start) && !GameManager.isPaused)
		{
			GameManager.Instance.PauseGame();
			
			Menu menu = PauseMenu.Attach(this.gameObject);
			menu.controllers.Add (controller);
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
			Attack();
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
		
		if(armorHit) amount *= armorFactor;
		
		health = health - amount;
	}
	
	public void AddModel(PlayerModel model)
	{
		this.xSkill = (SkillBase)Activator.CreateInstance(null, model.xAction).Unwrap();
		this.bSkill = (SkillBase)Activator.CreateInstance(null, model.bAction).Unwrap();
		this.ySkill = (SkillBase)Activator.CreateInstance(null, model.yAction).Unwrap();
		this.playerIndex = model.index;
	}
}