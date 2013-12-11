using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoryState : MenuStateBase
{
	private List<CharacterSelect> _selectBlocks;

	public ArmoryState()
	{
		center = GameObject.Find ("ArmoryScreen").transform.position;

		_selectBlocks = new List<CharacterSelect> ();

		foreach(var block in MonoBehaviour.FindObjectsOfType<CharacterSelect>())
		{
			_selectBlocks.Add(block);
			block.enabled = false;
		}
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
	}

	public override void OnActive()
	{
		foreach(var block in _selectBlocks)
		{
			block.enabled = true;
		}
	}

	public override void OnInactive()
	{
		foreach(var block in _selectBlocks)
		{
			block.enabled = false;
		}
	}
}
