using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Base class for menu states.
/// </summary>
public abstract class InGameMenuBase
{
    public abstract void OnUpdate(GamePad controller);
    public abstract void OnActive();
    public abstract void OnInActive();

    private DateTime _startTime;
    protected InGamePause pauseMenu;

    /// <summary>
    /// Resets the time.
    /// </summary>
    public void ResetTime()
    {
        _startTime = DateTime.Now;
    }

    /// <summary>
    /// We need to use this method when the game is paused because
    /// most unity methods are not working after setting
    /// timeScale to 0.f.
    /// </summary>
    public float ElapsedSeconds()
    {
        TimeSpan span = DateTime.Now.Subtract(_startTime);

        return ((float)span.Ticks / (float)TimeSpan.TicksPerSecond);
    }
}
