using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using System;

public class ConfirmExitState : InGameMenuBase
{
    private DateTime _startTime;
    private GameObject _activeText;

    public ConfirmExitState(InGamePause _inGamePause)
    {
        this.pauseMenu = _inGamePause;
    }

    public override void OnUpdate(GamePad controller)
    {
        if (ElapsedSeconds() > 0.25f)
        {
            if (controller.Axis(Axis.LeftHorizantal) >= 0.75f || controller.JustReleased(Button.DPadLeft))
            {
                ChangeActive();
            }
            else if (controller.Axis(Axis.LeftHorizantal) <= -0.75f || controller.JustReleased(Button.DPadRight))
            {
                ChangeActive();
            }
        }

        if (controller.JustPressed(Button.A))
        {
            if (_activeText == pauseMenu.yes)
            {
                Application.LoadLevel("Menu");
            }
            else if (_activeText == pauseMenu.no)
            {
                pauseMenu.ChangeState(InGameMenuStates.PauseState);
            }
        }
        else if (controller.JustPressed(Button.B))
        {
            pauseMenu.ChangeState(InGameMenuStates.PauseState);
        }
    }

    /// <summary>
    /// Enable everything from the confirm exit menu.
    /// </summary>
    public override void OnActive()
    {
        GameObject.Find("AreYouSure").guiText.enabled = true;

        pauseMenu.yes.guiText.enabled = true;
        pauseMenu.no.guiText.enabled = true;
        pauseMenu.no.guiText.color = Color.yellow;

        _activeText = pauseMenu.no;
    }

    /// <summary>
    /// Disable everything from the confirm exit menu.
    /// </summary>
    public override void OnInActive()
    {
        GameObject.Find("AreYouSure").guiText.enabled = false;
        pauseMenu.yes.guiText.enabled = false;
        pauseMenu.no.guiText.enabled = false;
    }

    /// <summary>
    /// Changes color to hightlight selected menu item.
    /// </summary>
    private void ChangeActive()
    {
        if (_activeText == pauseMenu.no)
        {
            _activeText = pauseMenu.yes;
            _activeText.guiText.color = Color.yellow;
            pauseMenu.no.guiText.color = Color.white;
        }
        else
        {
            _activeText = pauseMenu.no;
            _activeText.guiText.color = Color.yellow;
            pauseMenu.yes.guiText.color = Color.white;
        }

        ResetTime();
    }
}
