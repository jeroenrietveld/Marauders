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
	private float _health = 1f;
	private Heartbeat _heartbeat;

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
		//Dropping at our current location
		primaryWeapon.gameObject.SetActive(true);
		primaryWeapon.transform.position = transform.position;
		//Rigidbody FlagClone  = (Rigidbody)Inistantiate (Flag, transform.position, transform.rotation);

		primaryWeapon.owner = null;

		//Moving secondary to primary
		primaryWeapon = secondaryWeapon;

		//Secondary is definately null now
		secondaryWeapon = null;
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
            //Instantiate(prefabMenu);
        }
        if (controller.JustPressed(Button.X))
        {
            xSkill.performAction(this);
        }
    }

	public void AttackStart()
	{
		if(primaryWeapon)
		{
			primaryWeapon.AttackStart ();
		}
	}

	public void AttackEnd()
	{
		if(primaryWeapon)
		{
			primaryWeapon.AttackEnd ();
		}
	}

	public void ApplyDamage(Vector3 direction, float amount)
	{
		float dot = Vector3.Dot(direction, _heartbeat.transform.forward);
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