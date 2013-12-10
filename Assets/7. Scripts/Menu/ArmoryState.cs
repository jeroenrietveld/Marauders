﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoryState : MenuStateBase
{
	public ArmoryState()
	{
		center = GameObject.Find ("ArmoryScreen").transform.position;
	}
    
	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
			manager.ChangeState(MenuStates.LevelState);
		}
		if (Input.GetKey(KeyCode.B))
		{
			manager.ChangeState(MenuStates.SplashState);
		}

		EnableArmoryStateText();
	}

	private void EnableArmoryStateText()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MenuText");
		
		foreach (GameObject go in gameObjects)
		{
			go.renderer.enabled = true;
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
