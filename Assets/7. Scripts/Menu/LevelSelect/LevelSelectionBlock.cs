using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class will create a block containing level selection methods.
/// </summary>
class LevelSelectionBlock : LevelSelectionBlockBase
{
    public Level current;
    private int _currentIndex;
	private bool _reloadPending;
	private GameObject _levelPreview;
	private GameObject _levelSelectUp;
	private GameObject _levelSelectDown;

	public LevelSelectionBlock()
	{
		_currentIndex = 0;
		_reloadPending = false;
		_levelPreview = GameObject.Find ("LevelPreview");
		_levelSelectUp = GameObject.Find ("LevelSelectUp");
		_levelSelectDown = GameObject.Find ("LevelSelectDown");

		SetLevel (_currentIndex);
	}

	public override void Update()
	{
		int index = _currentIndex;

		if(Input.GetKeyDown(KeyCode.I))
		{
			_currentIndex++;
		}
		if(Input.GetKeyDown(KeyCode.J))
		{
			_currentIndex--;
		}
		if(Input.GetKeyDown(KeyCode.Escape))
		{
			//TODO return to character select
		}
		if(Input.GetKeyDown(KeyCode.Space))
		{
			LevelSelectionManager.ChangeState(LevelSelectionState.SettingSelection);
		}

		if(_currentIndex != index)
		{
			SetLevel(_currentIndex);
		}
	}

	public void SetLevel(int index)
	{
		if(index > (LevelSelectionManager.levels.Count - 1))
		{
			_currentIndex = 0;
		}
		if(index < 0)
		{
			_currentIndex = LevelSelectionManager.levels.Count - 1;
		}

		current = LevelSelectionManager.levels [_currentIndex];
		_levelPreview.renderer.material.mainTexture = current.previewImage;
	}
}

