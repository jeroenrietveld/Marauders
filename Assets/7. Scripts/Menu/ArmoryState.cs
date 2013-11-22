using UnityEngine;
using System.Collections;

public class ArmoryState : MenuStateBase
{
	public ArmoryState()
	{
		center = GameObject.Find("ArmoryScreen").renderer.bounds.center;
	}
    
	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
			manager.ChangeState(MenuStates.LevelState);
		}
	}
}
