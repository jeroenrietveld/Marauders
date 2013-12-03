using UnityEngine;
using System.Collections;

public class LevelState : MenuStateBase
{
	public LevelState()
	{
		center = GameObject.Find("LevelPreview").renderer.bounds.center;
        LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
	}

	public override void Update(MenuManager manager)
	{
	}
}
