using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// This class will create a block containing level selection methods.
/// </summary>
public class LevelSelectionBlock : LevelSelectionBlockBase
{
    public static Level current;

    private int _currentIndex;
	private GameObject _levelPreview;
	private GameObject _levelSelectUp;
	private GameObject _levelSelectDown;
	private GameObject _levelDescription;
    private GameObject _timeSync;
    private GameObject _timeSyncArrows;
    private GameObject _timeSyncBar;
    private TextMesh _levelName;
	private TextMesh _levelInfoText;
    private float _time = 0.2f;
    private float _resetTime = 0.2f;

	public LevelSelectionBlock()
	{
        _currentIndex = 0;
        _levelPreview = GameObject.Find("LevelPreview");
        _levelSelectUp = GameObject.Find("LevelSelectUp");
        _levelSelectDown = GameObject.Find("LevelSelectDown");
        _levelDescription = GameObject.Find("LevelDescription");
        _timeSync = GameObject.Find("TimeSyncText");
        _timeSyncArrows = GameObject.Find("ArrowsTimeSync");
        _timeSyncBar = GameObject.Find("TimeSyncBar");
        _levelName = _levelDescription.transform.FindChild("LevelInfo").gameObject.transform.FindChild("LevelInfo_Name").gameObject.GetComponent<TextMesh>();
        _levelInfoText = _levelDescription.transform.FindChild("LevelInfo").gameObject.transform.FindChild("LevelInfo_Text").gameObject.GetComponent<TextMesh>();
        SetLevel(_currentIndex);
	}

    public override void Update(GamePad controller)
	{
		int index = _currentIndex;
        _time -= Time.deltaTime;

        if (controller.JustPressed(Button.DPadLeft) || (controller.Axis(Axis.LeftHorizontal) >= 0.75f && _time <= 0f))
		{
			_currentIndex++;
            _time = _resetTime;
		}
        if (controller.JustPressed(Button.DPadRight) || (controller.Axis(Axis.LeftHorizontal) <= -0.75f && _time <= 0f))
		{
			_currentIndex--;
            _time = _resetTime;
		}
        if (controller.JustPressed(Button.A))
		{
            _timeSync.renderer.material.color = Color.black;

            MeshRenderer[] mesh = _timeSyncArrows.GetComponentsInChildren<MeshRenderer>();
            _timeSyncBar.renderer.enabled = true;
            for (int i = 0; i < mesh.Count(); i++)
            {
                mesh[i].enabled = true;
            }

            LevelSelectionManager.ChangeState(LevelSelectionState.SettingSelection);
		}
        if (controller.JustPressed(Button.B))
        {
            _timeSync.renderer.material.color = Color.gray;

            MeshRenderer[] mesh = _timeSyncArrows.GetComponentsInChildren<MeshRenderer>();
            _timeSyncBar.renderer.enabled = false;

            for (int i = 0; i < mesh.Count(); i++)
            {
                mesh[i].enabled = false;
            }

            LevelSelectionManager.ChangeState(LevelSelectionState.NotSelecting);
        }

		if(_currentIndex != index)
		{
			SetLevel(_currentIndex);
		}
	}

	public void SetLevel(int index)
	{
		if(index > (LevelSelectionManager.levels.Count - 1))
		{
			_currentIndex = 0;
		}
		if(index < 0)
		{
			_currentIndex = LevelSelectionManager.levels.Count - 1;
		}

		current = LevelSelectionManager.levels [_currentIndex];
		_levelPreview.transform.FindChild("LevelPreviewImage").renderer.material.mainTexture = current.previewImage;

        _levelName.text = current.levelName;
		_levelInfoText.text = current.levelInfo;
	}
}

