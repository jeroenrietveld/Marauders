using UnityEngine;
using System.Collections;

public class LevelState : MenuStateBase
{
	public LevelState()
	{
		center = GameObject.Find("LevelScreen").renderer.bounds.center;
		LevelSelectionManager.currentState = LevelSelectionState.LevelSelection;
	}

	public override void Update(MenuManager manager)
	{
	}
}
