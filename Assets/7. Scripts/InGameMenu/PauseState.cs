using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using System;

/// <summary>
/// When the player presses the Start button this class will
/// be called. This class represents the pause menu.
/// _ActiveText is used to set the active text hightlight.
/// _previousText is used to set the previous text back to the normal color.
/// </summary>
public class PauseState : InGameMenuBase 
{
    private GameObject _activeText;
    private GameObject _previousText;
    private int _currentTextIndex = 0;

    public PauseState(InGamePause _inGamePause)
    {
        this.pauseMenu = _inGamePause;
    }

    /// <summary>
    /// If ElapsedSeconds() returns true when timer > 0.25 seconds then
    /// we want to change the _activeText.
    /// If the user presses the A button we want to check the _activeText.
    /// </summary>
    public override void OnUpdate(GamePad controller)
    {
        if (ElapsedSeconds() > 0.25f)
        {
            if (controller.Axis(Axis.LeftVertical) >= 0.5f || controller.JustReleased(Button.DPadUp))
            {
                _previousText = _activeText;
                SetActive(-1);
            }
            else if (controller.Axis(Axis.LeftVertical) <= -0.5f || controller.JustReleased(Button.DPadDown))
            {
                _previousText = _activeText;
                SetActive(1);
            }
        }

        // pauseMenu.pauseTextObjects[0] is the Resume Text
        // pauseMenu.pauseTextObjects[1] is the Options Text
        // pauseMenu.pauseTextObjects[2] is the Quit Text
        if (controller.JustPressed(Button.A))
        {
            if (_activeText == pauseMenu.pauseTextObjects[0])
            {
                GameManager.Instance.ResumeGame();

                // calling this method will destroy the prefab because _currentState becomes null
                pauseMenu.SetCurrentStateNull();
            } 
            else if (_activeText == pauseMenu.pauseTextObjects[1]) 
            {
                // Change state to OptionsState. There is currently not an options class and implementations. 
            }
            else if (_activeText == pauseMenu.pauseTextObjects[2])
            {
                pauseMenu.ChangeState(InGameMenuStates.ConfirmExitState);                
            }
        }
        else if (controller.JustPressed(Button.B))
        {
            GameManager.Instance.ResumeGame();

            // calling this method will destroy the prefab because _currentState becomes null
            pauseMenu.SetCurrentStateNull();
        }
    }

    /// <summary>
    /// Enable the guiTexture for the background of the menu.
    /// Loop through all pauseTexts to enable text in menu.
    /// Set the _activeText to the first text which is Resume Game.
    /// Sets the color of _activetext and sets previousText for changing
    /// highlight of selected text when navigating through menu.
    /// </summary>
    public override void OnActive()
    {
        GameObject.Find("BackgroundPause").gameObject.guiTexture.enabled = true;

        foreach (GameObject pauseMenuText in pauseMenu.pauseTextObjects)
        {
            pauseMenuText.guiText.enabled = true;
        }
       
        _activeText = pauseMenu.pauseTextObjects[0];
        _activeText.guiText.color = Color.yellow;
        _previousText = _activeText;
    }

    /// <summary>
    /// Destroy the menu.
    /// </summary>
    public override void OnInActive()
    {
        GameObject.Find("BackgroundPause").gameObject.guiTexture.enabled = false;
        _activeText.guiText.color = Color.white;

        foreach (GameObject pauseMenuText in pauseMenu.pauseTextObjects)
        {
            pauseMenuText.guiText.enabled = false;
        }

        _currentTextIndex = 0;
    }

    /// <summary>
    /// Changes the activeText to a new text depending
    /// if the users clicks up or down (+1 or -1)
    /// Sets the new text to blue.
    /// Sets the previous text back to white.
    /// </summary>
    private void SetActive(int changeActiveText)
    {
        _currentTextIndex += changeActiveText;

        if (_currentTextIndex < 0)
        {
            _currentTextIndex = pauseMenu.pauseTextObjects.Count - 1;
        }
        else if (_currentTextIndex >= pauseMenu.pauseTextObjects.Count)
        {
            _currentTextIndex = 0;
        }

        _activeText = pauseMenu.pauseTextObjects[_currentTextIndex];
        _previousText.guiText.color = Color.white;
        _activeText.guiText.color = Color.yellow;

        ResetTime();
    }
}