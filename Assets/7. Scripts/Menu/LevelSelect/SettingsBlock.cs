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

    /// <summary>
    /// The constructor.
    /// </summary>
    public SettingsBlock()
    {
        _currentLives = 3;
    }

    /// <summary>
    /// This method will be called when input has occured.
    /// </summary>
    public void onInput()
    {
        //Check for the input
        //if LEFT
        _currentLives--;

        //if RIGHT
        _currentLives++;

        //if A
        //Load the level

        //if B
        //TODO
        //Set the visual focus back to levelselectionblock       
        LevelSelectionManager.currentState = LevelSelectionState.LevelSelection;
    }

    /// <summary>
    /// This method will be called every frame.
    /// </summary>
    public void Update()
    {
        //Only proceed if we are in the right state
        if (LevelSelectionManager.currentState == LevelSelectionState.SettingSelection)
        {
            //show amount of lives using _currentLives
        }
       
    }
}

