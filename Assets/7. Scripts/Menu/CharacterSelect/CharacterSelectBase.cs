using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

public abstract class CharacterSelectBase
{
    private float _defaultTimeValue = 0.2f;
    private float _timer = 0;
    protected CharacterSelectBlock block;

    public abstract void OnUpdate(GamePad controller);
    public abstract void OnActive();
    public abstract void OnInActive();
    public abstract void OnControllerConnect();
    public abstract void OnControllerDisconnect();

    public int GetID(int _count, float x, int total)
    {
        if (x != 0 && GetTimer())
        {
            if (x > 0)
            {
                _count++;
            }
            else if (x < 0)
            {
                _count--;
            }

            _count = (_count + total) % total;
            return _count;
        }
        return _count;
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
