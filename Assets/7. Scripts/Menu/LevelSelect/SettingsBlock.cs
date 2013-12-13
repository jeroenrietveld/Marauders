using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityEditor;
using XInputDotNetPure;

/// <summary>
/// This class will make the changing of settings possible.
/// </summary>
public class SettingsBlock : LevelSelectionBlockBase
{
    private int _currentIndex;
	private GameObject _levelDescription;
    private GUIProgressbar _timeSyncBar;
        
	public SettingsBlock()
	{    
        _levelDescription = GameObject.Find("LevelDescription");
        GameObject.Find("GUIProgressbar").GetComponent<GUIProgressbar>().enabled = true;
        _timeSyncBar = _levelDescription.transform.FindChild("GUIProgressbar").GetComponent<GUIProgressbar>();
        _timeSyncBar.max = 50;
        _timeSyncBar.pos = _timeSyncBar.transform.position;
        _timeSyncBar.size = new Vector3(200, 0, 40);
	}

	public override void Update(GamePad controller)
	{
		if(controller.JustPressed(Button.DPadRight))
		{
            _timeSyncBar.Add(10);
		}
		if(controller.JustPressed(Button.DPadLeft))
		{
            _timeSyncBar.Add(-10);
		}
		if(controller.JustPressed(Button.A))
		{
            // TODO set the timeSync in GameManager.Instance.matchsettings.timeSync
            GameManager.Instance.matchSettings.level = LevelSelectionBlock.current.levelName;
            GameManager.Instance.matchSettings.timeSync = _timeSyncBar.effective;        

            // Get the selected game mode class by using the Activator
            // Default Normal until new game modes are implemented.
            // var selectedGameMode = Activator.CreateInstance(null, "Normal");
            // GameManager.Instance.matchSettings.gameMode = (GameMode)selectedGameMode.Unwrap();    
            
            // temporary loading Gaia
            Application.LoadLevel(2);
            // GameManager.Instance.Start();
		}
        if(controller.JustPressed(Button.B))
        {
            LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
        }
	}   	
}