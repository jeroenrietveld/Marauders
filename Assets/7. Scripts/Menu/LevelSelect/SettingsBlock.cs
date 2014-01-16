using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// This class will make the changing of settings possible.
/// </summary>
public class SettingsBlock : LevelSelectionBlockBase
{
    private int _currentIndex;
	private GameObject _levelDescription;
    private TimeSyncBar _timeSyncBar;

	public SettingsBlock()
	{
        _levelDescription = GameObject.Find("LevelScreen/LevelDescription");
        _timeSyncBar = GameObject.Find("TimeSyncBar").GetComponent<TimeSyncBar>();
        _timeSyncBar.Add(0.5f);
	}

	public override void Update(GamePad controller)
	{
        if (controller.JustPressed(Button.DPadRight) || Input.GetKeyDown(KeyCode.RightArrow))
		{
            _timeSyncBar.Add(0.1f);
		}
        if (controller.JustPressed(Button.DPadLeft) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
            _timeSyncBar.Add(-0.1f);
		}
        /// For BETA version no settings
            if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.A))
            {
                // TODO set the timeSync in GameManager.Instance.matchsettings.timeSync
                GameManager.Instance.matchSettings.level = LevelSelectionBlock.current.levelName;
                GameManager.Instance.matchSettings.timeSync = _timeSyncBar.GetPercentage();

                // Get the selected game mode class by using the Activator
                // Default Normal until new game modes are implemented.
                 var selectedGameMode = Activator.CreateInstance(null, "Normal");
                 GameManager.Instance.matchSettings.gameMode = (GameMode)selectedGameMode.Unwrap();    

				GameManager.Instance.timeSyncLimit = 100;
                GameManager.Instance.Start();
            }
        if (controller.JustPressed(Button.B) || Input.GetKeyDown(KeyCode.B))
        {
            LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
        }
	}   	
}