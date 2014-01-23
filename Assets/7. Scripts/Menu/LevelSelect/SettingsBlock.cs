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
    private GameObject _timeSync;
    private GameObject _timeSyncArrows;
    private float _timer = 0f;
    private float _defaultTimeValue = 0.15f;

	public SettingsBlock()
	{
        _levelDescription = GameObject.Find("LevelScreen/LevelDescription");
        _timeSyncBar = GameObject.Find("TimeSyncBar").GetComponent<TimeSyncBar>();
        _timeSyncBar.Add(0.5f);
        _timeSync = GameObject.Find("TimeSyncText");
        _timeSyncArrows = GameObject.Find("ArrowsTimeSync");
	}

	public override void Update(GamePad controller)
	{
        if (controller.JustPressed(Button.DPadRight) || (controller.Axis(Axis.LeftHorizontal) > 0.7f && GetTimer()) || Input.GetKeyDown(KeyCode.RightArrow))
		{
            _timeSyncBar.Add(0.1f);
		}
        else if (controller.JustPressed(Button.DPadLeft) || (controller.Axis(Axis.LeftHorizontal) < -0.7f && GetTimer()) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
            _timeSyncBar.Add(-0.1f);
		}
        /// For BETA version no settings
            if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.A))
            {              
                // TODO set the timeSync in GameManager.Instance.matchsettings.timeSync
                GameManager.Instance.matchSettings.level = LevelSelectionBlock.current.levelName;
                GameManager.Instance.matchSettings.timeSync = (int)(_timeSyncBar.GetPercentage() * 100);

                // Get the selected game mode class by using the Activator
                // Default Normal until new game modes are implemented.
                 var selectedGameMode = Activator.CreateInstance(null, "Normal");
                 GameManager.Instance.matchSettings.gameMode = (GameMode)selectedGameMode.Unwrap();
                 GameManager.Instance.matchSettings.groundType = LevelSelectionBlock.current.groundType;
                 GameManager.Instance.Start();
            }
        if (controller.JustPressed(Button.B) || Input.GetKeyDown(KeyCode.B))
        {
            _timeSync.renderer.material.color = Color.gray;

            MeshRenderer[] mesh = _timeSyncArrows.GetComponentsInChildren<MeshRenderer>();
            _timeSyncBar.renderer.enabled = false;
            for (int i = 0; i < mesh.Count(); i++)
            {
                mesh[i].enabled = false;
            }

            LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
        }
	}

    public bool GetTimer()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = _defaultTimeValue;
            return true;
        }
        return false;
    }
}