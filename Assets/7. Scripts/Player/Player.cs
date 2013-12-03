using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public partial class Player : MonoBehaviour
{
	public Weapon primaryWeapon;
	public Weapon secondaryWeapon;
    public GameObject prefabMenu;

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
		weapon.Owner = this;
	}

	public void DropPrimaryWeapon()
	{
		//Dropping at our current location
		primaryWeapon.gameObject.SetActive(true);
		primaryWeapon.transform.position = transform.position;
		//Rigidbody FlagClone  = (Rigidbody)Inistantiate (Flag, transform.position, transform.rotation);

		primaryWeapon.Owner = null;

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
            Instantiate(prefabMenu);
        }
    }
}