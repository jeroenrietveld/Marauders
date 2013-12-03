using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public partial class Player : MonoBehaviour
{
	public Weapon primaryWeapon;
	public Weapon secondaryWeapon;

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

		//Setting our primary weapon
		primaryWeapon = weapon;

		//Hiding the weapon
		weapon.gameObject.SetActive(false);

		//We're the owner
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
    /// Check is game is paused or not and sets the timeScale in the GameManager.
    /// </summary>
	public void Update()
	{
        if (controller.JustPressed(Button.Start) && !GameManager.isPaused)
        {
            GameManager.PauseGame();
        }
        else if (controller.JustPressed(Button.Start) && GameManager.isPaused)
        {
            GameManager.ResumeGame();
        }
	}
}