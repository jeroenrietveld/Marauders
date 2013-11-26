using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoryState : MenuStateBase
{
	public List<GameObject> characterSelectBoxes;

	public ArmoryState()
	{
		center = GameObject.Find("ArmoryScreen").renderer.bounds.center;
        
		foreach(GameObject characterSelectBox in GameObject.FindGameObjectsWithTag("CharacterSelect"))
		{
			//characterSelectBoxes.Add(CharacterSelectBlock);
		}
	}
    
	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
			manager.ChangeState(MenuStates.LevelState);
		}
	}
}
