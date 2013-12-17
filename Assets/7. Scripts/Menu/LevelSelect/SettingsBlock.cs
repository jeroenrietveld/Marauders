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
    private GUIProgressbar _timeSyncBar;
    private float _progress;
        
	public SettingsBlock()
	{
        _levelDescription = GameObject.Find("LevelDescription");
        _timeSyncBar = _levelDescription.transform.FindChild("GUIProgressbar").GetComponent<GUIProgressbar>();
        _timeSyncBar.max = 30;
        _timeSyncBar.pos = _timeSyncBar.transform.position;
        _timeSyncBar.size = new Vector3(200, 0, 40);
	}

	public override void Update(GamePad controller)
	{
        if (controller.JustPressed(Button.DPadRight) || Input.GetKeyDown(KeyCode.RightArrow))
		{
            _timeSyncBar.Add(10);
            _progress += 1;
		}
        if (controller.JustPressed(Button.DPadLeft) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
            _timeSyncBar.Add(-10);
            _progress -= 1;
		}
        /// For BETA version no settings
        ///if (_progress >= 3f)
        ///{
            if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.A))
            {
                // TODO set the timeSync in GameManager.Instance.matchsettings.timeSync
                GameManager.Instance.matchSettings.level = LevelSelectionBlock.current.levelName;
                GameManager.Instance.matchSettings.timeSync = _timeSyncBar.effective;

                // Get the selected game mode class by using the Activator
                // Default Normal until new game modes are implemented.
                // var selectedGameMode = Activator.CreateInstance(null, "Normal");
                // GameManager.Instance.matchSettings.gameMode = (GameMode)selectedGameMode.Unwrap();    

                // Just start the Gaia level without setting settings for the BETA version.
                Application.LoadLevel(2);
                // GameManager.Instance.Start();
            }
        ///}
        if (controller.JustPressed(Button.B) || Input.GetKeyDown(KeyCode.B))
        {
            LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
        }
	}   	
}