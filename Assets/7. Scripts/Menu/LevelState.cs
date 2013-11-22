using UnityEngine;
using System.Collections;

public class LevelState : MenuStateBase
{
	public LevelState()
	{
		center = GameObject.Find("LevelScreen").renderer.bounds.center;
	}

	public override void Update(MenuManager manager)
	{
	}
}
