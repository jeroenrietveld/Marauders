using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoryState : MenuStateBase
{
	public ArmoryState()
	{
		center = GameObject.Find("ArmoryScreen").renderer.bounds.center;
<<<<<<< HEAD
        
		foreach(GameObject characterSelectBox in GameObject.FindGameObjectsWithTag("CharacterSelect"))
		{
			//characterSelectBoxes.Add(CharacterSelectBlock);
		}
=======
>>>>>>> 1dde2c66b02a80a405f093b01e7abd2afb3b2ab8
	}
    
	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
			manager.ChangeState(MenuStates.LevelState);
		}
	}

	public override void OnActive()
	{
		foreach(var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
		{
			block.enabled = true;
		}
	}

	public override void OnInactive()
	{	
		foreach(var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
		{
			block.enabled = false;
		}
	}
}
