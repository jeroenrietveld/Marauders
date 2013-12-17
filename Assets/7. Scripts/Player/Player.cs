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

	public Heartbeat heartbeat { get; set; }
	public float armorFactor = 0.5f;
	public float health
	{
		get {
			return _health;
		}
		
		set {
			_health = Mathf.Clamp01(value);

			_controller.SetVibration(1, 1, damagedControllerPulse);

			//Event.dispatch(new PlayerHitEvent());
			
			if(_health == 0f)
			{
				//TODO: respan
				_health = 1;
				//Event.dispatch(new PlayerDeathEvent());
			}
		}
	}

	private float _health = 1f;
	private Timer _deadTimer;
	private DateTime _attackStart;
	public bool canJump = true;
	
	private Menu _pauseMenu;

	private Material _cooldownMat;
	private Texture _cooldownTex;

	public float damagedControllerPulse = 0.25f;

	//Move states to a better location?
	public bool frozen = false;

	public Player()
	{
	}

	void Awake()
	{
		_camera = Camera.main;
		_controller = ControllerInput.GetController (playerIndex);
		heartbeat = transform.FindChild ("Heartbeat_indicator").GetComponent<Heartbeat>();

		InitializeAnimations();

		//TODO: remove (testing purposes)
		utilitySkill = gameObject.AddComponent<Dash> ();
		//utilitySkill = gameObject.AddComponent<Timeshift> ();

		_cooldownMat = Resources.Load ("Materials/Cooldown", typeof(Material)) as Material;
		_cooldownTex = Resources.Load ("Textures/Cooldown", typeof(Texture)) as Texture;
	}

	void OnGUI()
	{
		Graphics.DrawTexture (new Rect (10, 10, 100, 100), _cooldownTex, _cooldownMat);
		_cooldownMat.SetFloat ("health", (utilitySkill.cooldown.running) ? 1 - utilitySkill.cooldown.Phase() : 0);
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

		if (primaryWeapon != null)
		{
			//Dropping primary weapon
			DropPrimaryWeapon();
		}

		//Setting our new weapon
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

		InitializeAnimationEvents(weaponHolder);
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
		//well that sucks
		if(frozen)
		{
			return;
		}

		//We want to calculate this only once, so filling it up here
		RaycastHit hit;
		//Vector3 vector = new Vector3(transform.position.x, transform.position.y - 0.1f, transform.position.z);
		Physics.Raycast(transform.position, -Vector3.up, out hit, Mathf.Infinity);

		if (hit.collider != null)
		{
			this.distanceToGround = hit.distance;
		} else
		{
			this.distanceToGround = 100;
		}

		//Pausing/resuming game
        if (controller.JustPressed(Button.Start))
        {
			if (!GameManager.isPaused)
			{
            	//Showing the menu
				_pauseMenu = PauseMenu.Attach(this.gameObject);
				_pauseMenu.controllers.Add(controller);
				_pauseMenu.visible = true;

				//Pausing the game
				GameManager.Instance.PauseGame();
			} else
			{
				//Hiding the menu
				_pauseMenu.visible = false;

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
		if(controller.JustPressed(Button.X) && !utilitySkill.cooldown.running || Input.GetKeyDown(KeyCode.X) && !utilitySkill.cooldown.running)
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
		float h = _controller.Axis (Axis.LeftHorizontal);
		float v = _controller.Axis (Axis.LeftVertical);
		Vector3 moveSpeed = camDirection * v + camRight * h;

		//Applying the movement
		MovementManagement(moveSpeed);

		//To jump when falling off
		if (distanceToGround < 0.1f)
		{
			canJump = true;
		}

		//Jumping
		if (controller.JustPressed(Button.A))
		{
			if (canJump)
			{
				Jump ();
			}
			canJump = false;
		}

		if(Input.GetKeyDown(KeyCode.D) && playerIndex == PlayerIndex.One)
		{
			Die ();
		}
		/*if(_isDeath)
		{
			Vector3 scale = new Vector3(
				transform.localScale.x * 0.5f * Time.deltaTime,
				transform.localScale.y * 0.5f * Time.deltaTime,
				transform.localScale.z * 0.5f * Time.deltaTime);
			transform.localScale = scale;
		}*/
    }
	
	public void DetectPlayerHit()
	{
		primaryWeapon.DetectPlayerHit();
	}

	public void LoadModel(PlayerModel model)
	{
		this.utilitySkill = (SkillBase)Activator.CreateInstance(null, model.utilitySkill).Unwrap();
		this.offensiveSkill = (SkillBase)Activator.CreateInstance(null, model.offensiveSkill).Unwrap();
		this.defensiveSkill = (SkillBase)Activator.CreateInstance(null, model.defensiveSkill).Unwrap();
		this.playerIndex = model.index;
	}

	public void ApplyDamage(Vector3 direction, float amount)
	{

		float dot = Vector3.Dot(direction.normalized, heartbeat.transform.forward);
		bool armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;
		
		if(armorHit)
		{
			amount = amount * armorFactor;
		}
		
		this.health = this.health - amount;

		if(this.health <= 0)
		{
			Die();
		}
	}

	private void Die()
	{
		GameObject portal = GameObject.Instantiate(Resources.Load("Prefabs/Portal")) as GameObject;
		portal.transform.position = transform.position + new Vector3 (0, 1.5f, -2);

		GameObject body = transform.FindChild ("Body").gameObject;
		body.renderer.material.SetFloat ("CutoffHeight", portal.transform.position.z);

		//Get the direction from the player to the portal
		Vector3 direction = (portal.transform.position - transform.position).normalized;

		//apply a force to the player when the timer ends
		Timer portalTimer = portal.GetComponent<Portal> ().portalTimer;

		portalTimer.AddCallback (portalTimer.endTime - 0.5f, delegate {
			rigidbody.velocity = direction * 10;
			transform.localScale = Vector3.one;
		});
	}
}