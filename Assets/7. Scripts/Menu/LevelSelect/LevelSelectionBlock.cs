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
    private TextMesh _levelName;
	private TextMesh _levelInfoText;
    private float _time = 0.2f;
    private float _resetTime = 0.2f;
    private bool enableGUIProgress = true;

	public LevelSelectionBlock()
	{
        _currentIndex = 0;
        _levelPreview = GameObject.Find("LevelPreview");
        _levelSelectUp = GameObject.Find("LevelSelectUp");
        _levelSelectDown = GameObject.Find("LevelSelectDown");
        _levelDescription = GameObject.Find("LevelDescription");
        _levelName = _levelDescription.transform.FindChild("LevelInfo").gameObject.transform.FindChild("LevelInfo_Name").gameObject.GetComponent<TextMesh>();
        _levelInfoText = _levelDescription.transform.FindChild("LevelInfo").gameObject.transform.FindChild("LevelInfo_Text").gameObject.GetComponent<TextMesh>();
        SetLevel(_currentIndex);
	}

    public override void Update(GamePad controller)
	{
        if (enableGUIProgress)
        {
            GameObject.Find("GUIProgressbar").GetComponent<GUIProgressbar>().enabled = true;
            enableGUIProgress = false;
        }

		int index = _currentIndex;
		SetAlpha(_levelSelectUp, .9f);
		SetAlpha(_levelSelectDown, .9f);
        _time -= Time.deltaTime;

        if (controller.JustPressed(Button.DPadUp) || (controller.Axis(Axis.LeftVertical) >= 0.75f && _time <= 0f) || Input.GetKeyDown(KeyCode.UpArrow))
		{
			_currentIndex++;
			SetAlpha(_levelSelectUp, 1f);
            _time = _resetTime;
		}
        if (controller.JustPressed(Button.DPadDown) || (controller.Axis(Axis.LeftVertical) <= -0.75f && _time <= 0f) || Input.GetKeyDown(KeyCode.DownArrow))
		{
			_currentIndex--;
			SetAlpha(_levelSelectDown, 1f);
            _time = _resetTime;
		}
        if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.A))
		{
            GameObject.Find("TempConfirmText").renderer.enabled = true;
			LevelSelectionManager.ChangeState(LevelSelectionState.SettingSelection);
		}
        if (controller.JustPressed(Button.B) || Input.GetKeyDown(KeyCode.B))
        {
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

	public void SetAlpha(GameObject gameObject, float alpha)
	{
		//Color color = gameObject.renderer.material.color;
		//color.a = alpha;
		//gameObject.renderer.material.color = color;
	}
}

