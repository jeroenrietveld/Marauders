using UnityEngine;
using System.Collections;

public class LevelState : MenuStateBase
{
	public LevelState()
	{
		center = GameObject.Find("LevelScreen").transform.position;
	}

	public override void Update(MenuManager manager)
	{
	    if (Input.GetKey(KeyCode.B))
	    {
	        manager.ChangeState(MenuStates.ArmoryState);
	    }
	}
}
