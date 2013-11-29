using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class will make the changing of settings possible.
/// </summary>
class SettingsBlock : LevelSelectionBlockBase
{
    private int _currentLives;
	private bool _reloadPending;

	public SettingsBlock()
	{
		_currentLives = 3;
		_reloadPending = false;
	}

	public override void Update()
	{
		if(Input.GetKey(KeyCode.A))
		{
			_currentLives--;
		}
		if(Input.GetKey(KeyCode.B))
		{
			_currentLives++;
		}
		if(Input.GetKey (KeyCode.Escape))
		{
			LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
		}
		if(Input.GetKey(KeyCode.Space))
		{
			//TODO load scene
		}
	}
}

