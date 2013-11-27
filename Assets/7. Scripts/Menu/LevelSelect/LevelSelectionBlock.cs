using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class will create a block containing level selection methods.
/// </summary>
class LevelSelectionBlock : MonoBehaviour
{
    public Level current;
    private int _currentIndex;
	private bool _reloadPending;

    /// <summary>
    /// The constructor.
    /// </summary>
    public void Awake()
	{
        _currentIndex = 0;
		_reloadPending = false;
		current = LevelSelectionManager.levels[_currentIndex];
    }
	
    /// <summary>
    /// This method will be called every frame.
    /// </summary>
    public void Update()
    {
		//Only set visible if the LevelSelection state is active
		if(LevelSelectionManager.currentState == LevelSelectionState.LevelSelection)
		{
			//Check for the input
			//if UP
			if(Input.GetKey(KeyCode.W))
			{
				_currentIndex--;
				_reloadPending = true;
			}
			
			//if DOWN
			else if(Input.GetKey(KeyCode.S))
			{
				_currentIndex++;
				_reloadPending = true;
			}

			//if A
			//TODO
			//Set the visual focus to the settingsblock
			else if(Input.GetKey (KeyCode.Space))
			{
				LevelSelectionManager.currentState = LevelSelectionState.SettingSelection;
			}

			//We should only load if a switch of levels happened,
			//therefore;
			if(_reloadPending)
			{
				//Check if the current index is still within bounds. Else, switch to last/first level.
				if (_currentIndex < 0)
				{
					//Switch to last level.
					_currentIndex = LevelSelectionManager.levels.Count - 1;
				}
					
				if(_currentIndex >= LevelSelectionManager.levels.Count)
				{
					//Switch to first level.
					_currentIndex = 0;
				}
					
				//Set the level.
				current = LevelSelectionManager.levels[_currentIndex];
                

	            //Set the image of the current level
	            //Resources.Load(current.previewImagePath);
			
	            //TODO
	            //Show it in unity

				_reloadPending = false;
	        }
		}
	}
}

