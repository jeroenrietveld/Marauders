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
    private Level _current;
    private int _currentIndex;

    /// <summary>
    /// The constructor.
    /// </summary>
    public LevelSelectionBlock()
    {
        _currentIndex = 0;
        _current = LevelSelectionManager.levels[_currentIndex];
    }

    /// <summary>
    /// This method will be called when input has occured.
    /// </summary>
    public void onInput() 
    { 
        //Check for the input
        //if UP
        _currentIndex--;

        //if DOWN
        _currentIndex++;

        //Check if the current index is still within bounds. Else, switch to last/first level.
        if (_currentIndex < 0)
        {
            //Switch to last level.
            _currentIndex = LevelSelectionManager.levels.Count - 1;
        }

        if(_currentIndex == LevelSelectionManager.levels.Count)
        {
            //Switch to first level.
            _currentIndex = 0;
        }

        //Set the level.
        _current = LevelSelectionManager.levels[_currentIndex];

        //if A
        //TODO
        //Set the visual focus to the settingsblock
        LevelSelectionManager.currentState = LevelSelectionState.SettingSelection;

        //if B
        //TODO
        //Go back to character select
    }

    /// <summary>
    /// This method will be called every frame.
    /// </summary>
    public void Update()
    {
        //Only set visible if the LevelSelection state is active
        if(LevelSelectionManager.currentState == LevelSelectionState.LevelSelection)
        { 
            //Set the image of the current level
            Resources.Load(_current.previewImagePath);

            //TODO
            //Show it in unity
        }
    }

}

