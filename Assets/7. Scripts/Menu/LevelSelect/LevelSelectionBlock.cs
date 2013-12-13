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
	private TextMesh _levelInfoText;

	public LevelSelectionBlock()
	{
        _currentIndex = 0;
        _levelPreview = GameObject.Find("LevelPreview");
        _levelSelectUp = GameObject.Find("LevelSelectUp");
        _levelSelectDown = GameObject.Find("LevelSelectDown");
        _levelDescription = GameObject.Find("LevelDescription");
        _levelInfoText = _levelDescription.transform.FindChild("LevelInfo").gameObject.GetComponent<TextMesh>();
        SetLevel(_currentIndex);
	}

    public override void Update(GamePad controller)
	{
		int index = _currentIndex;
		SetAlpha(_levelSelectUp, .9f);
		SetAlpha(_levelSelectDown, .9f);

		if(controller.JustPressed(Button.DPadLeft))
		{
			_currentIndex++;
			SetAlpha(_levelSelectUp, 1f);
		}
        if (controller.JustPressed(Button.DPadRight))
		{
			_currentIndex--;
			SetAlpha(_levelSelectDown, 1f);
		}
        if (controller.JustPressed(Button.A))
		{
			LevelSelectionManager.ChangeState(LevelSelectionState.SettingSelection);
		}
        if (controller.JustPressed(Button.B))
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
		_levelInfoText.text = current.levelInfo;
	}

	public void SetAlpha(GameObject gameObject, float alpha)
	{
		Color color = gameObject.renderer.material.color;
		color.a = alpha;
		gameObject.renderer.material.color = color;
	}
}

