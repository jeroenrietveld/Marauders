using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

public abstract class SelectionBase
{
    private float _defaultTimeValue = 0.2f;
    private float _timer = 0;
    protected CharacterSelectBlock block;

    public abstract void OnUpdate(GamePad controller);
    public abstract void OnActive();
    public abstract void OnInActive();
    
    public int GetMarauderIndex(int currentIndex, int direction, int marauderCount, bool useTimer)
    {
        if (direction != 0 && (useTimer && GetTimer()) || !useTimer)
        {
            GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuswitch", true);
            currentIndex += direction;

			//Use modulo so the value will be inside the range of the array (math magic)
            currentIndex = (currentIndex + marauderCount) % marauderCount;
            return currentIndex;
        }
        return currentIndex;
    }

	/// <summary>
	/// makes sure you don't switch a character each frame when pressing a thumbstick
	/// </summary>
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
