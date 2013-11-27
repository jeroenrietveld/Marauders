using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// This class will make the changing of settings possible.
/// </summary>
class SettingsBlock : MonoBehaviour
{
    private int _currentLives;
	private bool _reloadPending;

    /// <summary>
    /// The constructor.
    /// </summary>
   public void Awake()
    {
        _currentLives = 3;
		_reloadPending = false;
    }

    /// <summary>
    /// This method will be called every frame.
    /// </summary>
    public void Update()
    {
		//Only proceed if we are in the right state
		if (LevelSelectionManager.currentState == LevelSelectionState.SettingSelection)
		{
				//Check for the input
			//if LEFT
			if(Input.GetKey(KeyCode.A))
			{
				_currentLives--;
				_reloadPending = true;
			}
			
			//if RIGHT
			else if(Input.GetKey(KeyCode.D))
			{
				_currentLives++;
				_reloadPending = true;
			}

			//if A
			//Load the scene with the level
			else if(Input.GetKey(KeyCode.Space))
			{
				//LevelSelectionManager.currentState = LevelSelectionState.NotSelecting;
				//Application.LoadLevel(LevelSelectionBlock.current);
			}
			
			//if B
			//TODO
			//Set the visual focus back to levelselectionblock       
			else if(Input.GetKey(KeyCode.B))
			{
				LevelSelectionManager.currentState = LevelSelectionState.LevelSelection;
			}

			//We should only update if a change occured,
			//therefore;
			if(_reloadPending)
			{        	
	            //show amount of lives using _currentLives

				_reloadPending = false;
	        }
		}
    }
}

