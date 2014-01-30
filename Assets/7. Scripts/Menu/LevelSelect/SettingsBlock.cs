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
    private GameObject _levelSelectArrows;
    private GameObject _levelOverlay;
    private List<string> timesyncTexts = new List<string> { "Shortest", "Shorter", "Short", "Average", "Long", "Longer", "Longest" };
    private TextMesh _timeSyncText;
    private float _timer = 0f;
    private float _defaultTimeValue = 0.15f;
    // Start with average timeSync
    private int currentSync = 3;

	public SettingsBlock()
	{
        _levelDescription = GameObject.Find("LevelScreen/LevelDescription");
        _timeSyncBar = GameObject.Find("TimeSyncBar").GetComponent<TimeSyncBar>();
        _timeSyncBar.Add(0.5f);
        _timeSync = GameObject.Find("TimeSyncText");
        _timeSyncArrows = GameObject.Find("ArrowsTimeSync");
        _levelSelectArrows = GameObject.Find("LevelSelectArrows");
        _levelOverlay = GameObject.Find("LevelOverlay");
        _timeSyncText = GameObject.Find("TimeSyncText").gameObject.GetComponent<TextMesh>();
	}

	public override void Update(GamePad controller)
	{
        if (controller.JustPressed(Button.DPadRight) || (controller.Axis(Axis.LeftHorizontal) > 0.7f && GetTimer()) || Input.GetKeyDown(KeyCode.RightArrow))
		{
            ChangeTimeSyncText(true);
            _timeSyncBar.Add(0.17f);
		}
        else if (controller.JustPressed(Button.DPadLeft) || (controller.Axis(Axis.LeftHorizontal) < -0.7f && GetTimer()) || Input.GetKeyDown(KeyCode.LeftArrow))
		{
            ChangeTimeSyncText(false);
            _timeSyncBar.Add(-0.17f);
		}
        /// For BETA version no settings
            if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.A))
            {       
                
                // TODO set the timeSync in GameManager.Instance.matchsettings.timeSync
                GameManager.Instance.matchSettings.level = LevelSelectionBlock.current.levelName;
                GameManager.Instance.matchSettings.timeSync = (int)(_timeSyncBar.GetPercentage() * 600);

                // Get the selected game mode class by using the Activator
                // Default Normal until new game modes are implemented.
                 var selectedGameMode = Activator.CreateInstance(null, "Normal");
                 GameManager.Instance.matchSettings.gameMode = (GameMode)selectedGameMode.Unwrap();
                 GameManager.Instance.matchSettings.groundType = LevelSelectionBlock.current.groundType;
                 LevelSelectionManager.ChangeState(LevelSelectionState.Done);
            }
        if (controller.JustPressed(Button.B) || Input.GetKeyDown(KeyCode.B))
        {
            _timeSync.renderer.material.color = Color.gray;
            _levelSelectArrows.SetActive(true);
            _levelOverlay.renderer.enabled = false;
            _timeSyncText.renderer.enabled = false;

            MeshRenderer[] mesh = _timeSyncArrows.GetComponentsInChildren<MeshRenderer>();
            _timeSyncBar.renderer.enabled = false;
            for (int i = 0; i < mesh.Count(); i++)
            {
                mesh[i].enabled = false;
            }

            LevelSelectionManager.ChangeState(LevelSelectionState.LevelSelection);
        }
	}

    void ChangeTimeSyncText(bool forward)
    {
        if (forward)
        {
            currentSync++;

            if (currentSync >= timesyncTexts.Count - 1)
            {
                currentSync = timesyncTexts.Count - 1;
            }

            _timeSyncText.text = timesyncTexts[currentSync];
        }
        else
        {
            currentSync--;

            if (currentSync < 0)
            {
                currentSync = 0;
            }
            _timeSyncText.text = timesyncTexts[currentSync];
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