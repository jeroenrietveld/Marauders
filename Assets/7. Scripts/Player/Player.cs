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

		while(weaponHolder.transform.childCount > 0)
		{	
			Transform weapon = weaponHolder.transform.GetChild(0);
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

	Menu pauseMenu;

    /// <summary>
    /// Check is game is paused and sets the timeScale in the GameManager.
    /// Create the menu from the prefabMenu.
    /// </summary>
    public void Update()
    {
		//We want to calculate this only once, so filling it up here
		RaycastHit hit;
		//Vector3 vector = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
		Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity);
		this.distanceToGround = hit.distance;
		//Transform heartbeat = transform.FindChild ("Heartbeat_indicator");
		//heartbeat.position = new Vector3(heartbeat.position.x, (distanceToGround) + 0.02f, heartbeat.position.z);

		//Dropping weapon / Doing skill
		if(controller.JustPressed(Button.Y))
		{
			DropPrimaryWeapon();
		}

		//Pausing/resuming game
        if (controller.JustPressed(Button.Start))
        {
			if (!GameManager.isPaused)
			{
            	//Showing the menu
				pauseMenu = PauseMenu.Attach(this.gameObject);
				pauseMenu.controllers.Add(controller);
				pauseMenu.visible = true;

				//Pausing the game
				GameManager.Instance.PauseGame();
			} else
			{
				//Hiding the menu
				pauseMenu.visible = false;

				//Resming the game
				GameManager.Instance.ResumeGame();
			}
        }

		//Attacking
		if (controller.JustPressed(Button.B))
		{
			AnimationAttack();
		}

		//Utility skill
		if(controller.JustPressed(Button.X) && !utilitySkill.cooldown.running)
		{
			utilitySkill.PerformAction();
			animation.Play(utilitySkill.animationName, PlayMode.StopSameLayer);
		}

		//Calculating the movement, relative to the camera
		Vector3 camDirection = _camera.transform.forward + _camera.transform.up;
		camDirection.y = 0;
		camDirection.Normalize();
		
		Vector3 camRight = _camera.transform.right;
		camDirection.y = 0;
		camDirection.Normalize();
		
		//Xbox Controls:
		float h = _controller.Axis (Axis.LeftHorizantal);
		float v = _controller.Axis (Axis.LeftVertical);
		Vector3 moveSpeed = camDirection * v + camRight * h;

		//Applying the movement
		MovementManagement(moveSpeed);

		//Jumping
		if (controller.JustPressed(Button.A) && this.isGrounded)
		{
			Jump();
			/*if (AgainstWall(moveSpeed))
			{
				
			}*/
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