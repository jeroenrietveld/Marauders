using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;

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
    private int _gameModeIndex;
    private int _amountOfLivesIndex;
	private int _settingsIndex;
	private Level _level;
        
	public SettingsBlock()
	{    
        _levelDescription = GameObject.Find("LevelDescription");
        _gameMode = _levelDescription.transform.FindChild("GameModeSelection_TextBox/GameModeSelection_Text").GetComponent<TextMesh>();
        _amountOfLives = _levelDescription.transform.FindChild("AmountOfLives_TextBox/AmountOfLives_Text").GetComponent<TextMesh>();
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
		if(Input.GetKeyDown(KeyCode.I))
		{
			_currentIndex--;
		}
		if(Input.GetKeyDown(KeyCode.J))
		{
			_currentIndex++;
		}
		if(Input.GetKeyDown(KeyCode.A))
		{          
            if (_settingsIndex != _settings.Count-1)
            {
               _settingsIndex++;
               _currentIndex = _amountOfLivesIndex;
            }
            else
            {
                GameManager.Instance.matchSettings.level = LevelSelectionBlock.current.levelName;
                string gameModeID = LevelSelectionBlock.current.gameModes[_gameModeIndex];           

                //Get the selected game mode class by using the Activator
                var selectedGameMode = Activator.CreateInstance(null, gameModeID);
                GameManager.Instance.matchSettings.gameMode = (GameMode)selectedGameMode.Unwrap();    

                GameManager.Instance.Start();
            }	
		}
		if(Input.GetKeyDown(KeyCode.B))
		{
            if (_settingsIndex != 0)
            {
                _settingsIndex--;
                _currentIndex = _gameModeIndex;
            }
            else
            {
                LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
            }
		}
		if(index != _currentIndex)
		{
			if(_settings[_settingsIndex].Equals(_gameMode))
			{
                SetGameMode(_currentIndex);
			}
            if(_settings[_settingsIndex].Equals(_amountOfLives))
            {
                SetAmountOfLives(_currentIndex);
            }
		}
	}

    private void SetGameMode(int index)
    {
        if(index < 0)
        {
            _currentIndex = _level.gameModes.Count() - 1;
        }
        if (index > _level.gameModes.Count() - 1)
        {
            _currentIndex = 0;
        }
        _gameMode.text = _level.gameModes[_currentIndex];
        _gameModeIndex = _currentIndex;
    }

    private void SetAmountOfLives(int index)
    {
        if(index < 0)
        {
            _currentIndex = 5;
        } 
        if(index > 5)
        {
            _currentIndex = 0;
        }
        _amountOfLives.text = Convert.ToString(_currentIndex);
        _amountOfLivesIndex = _currentIndex;
    }

   
	
}