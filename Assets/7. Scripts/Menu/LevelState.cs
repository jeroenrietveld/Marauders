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
        if (LevelSelectionManager.currentState != null)
        {
            LevelSelectionManager.currentState.Update();
        }

        if (Input.GetKey(KeyCode.B))
	    {
            if(LevelSelectionManager.currentState == LevelSelectionManager.selectionBlocks[LevelSelectionState.NotSelecting])
            { 
                manager.ChangeState(MenuStates.ArmoryState);
            }
	    }

		

	}
}
