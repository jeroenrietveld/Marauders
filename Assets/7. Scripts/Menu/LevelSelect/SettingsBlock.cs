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
    private int _currentIndex;
	private GameObject _levelDescription;
	private TextMesh _gameMode;
	private TextMesh _amountOfLives;
	private List<TextMesh> _settings;
	private int _settingsIndex;
	private Level _level;

	public SettingsBlock()
	{
        _gameMode = _levelDescription.transform.FindChild("GameModeSelection_Text").GetComponent<TextMesh>();
        _amountOfLives = _levelDescription.transform.FindChild("AmountOfLives_Text").GetComponent<TextMesh>();
		_settings = new List<TextMesh>();
		_settings.Add(_gameMode);
		_settings.Add(_amountOfLives);
		_currentIndex = 0;
		_settingsIndex = 0;
		_level = LevelSelectionBlock.current;
	}

	public override void Update()
	{
		int index = _currentIndex;
		if(Input.GetKey(KeyCode.I))
		{
			_currentIndex--;
		}
		if(Input.GetKey(KeyCode.J))
		{
			_currentIndex++;
		}
		if(Input.GetKey(KeyCode.A))
		{
			//_settingsIndex++;			
		}
		if(Input.GetKey(KeyCode.B))
		{
			//_settingsIndex--;
		}
		if(Input.GetKey (KeyCode.Escape))
		{
			LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
		}
		if(Input.GetKey(KeyCode.Space))
		{
			//TODO load scene
		}
		if(index != _currentIndex)
		{
			if(_settings[_settingsIndex].Equals(_gameMode))
			{
				_gameMode.text = _level.gameModes[_currentIndex];
			}
		}
	}
	
}