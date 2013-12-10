using UnityEngine;
using System;
using System.Collections.Generic;

public class Timer
{
	public float startTime;
	public float endTime;

	public enum WrapMode
	{
		ONCE,
		LOOP
	}
	public WrapMode wrapMode;

	public delegate void Callback();

	private bool _running;

	private float _startTimeStamp;

	private List<CallbackPair> _callbacks;
	private int _callbackIndex;
	private struct CallbackPair
	{
		public float time;
		public Callback callback;

		public CallbackPair(float time, Callback callback)
		{
			this.time = time;
			this.callback = callback;
		}
	}

	public Timer(float endTime) : this(endTime, WrapMode.ONCE) {}

	public Timer(float endTime, Timer.WrapMode wrapMode) : this(0f, endTime, wrapMode) {}

	public Timer(float startTime, float endTime, Timer.WrapMode wrapMode)
	{
		this.startTime = startTime;
		this.endTime = endTime;
		this.wrapMode = wrapMode;

		this._running = false;
		this._startTimeStamp = 0f;
		this._callbackIndex = 0;
		this._callbacks = new List<CallbackPair> ();
	}

	public void Start()
	{
		Start(0f);
	}

	private void Start(float remainingTime)
	{
		_running = true;
		_startTimeStamp = Time.time - remainingTime;
		_callbackIndex = 0;
	}

	public void Stop()
	{
		_running = false;
	}

	public void Update()
	{
		if(_running)
		{
			float currentTime = GetCurrentTime();

			while(_callbackIndex < _callbacks.Count && _callbacks[_callbackIndex].time < currentTime)
			{
				_callbacks[_callbackIndex].callback();

				_callbackIndex++;
			}


			if(currentTime > endTime)
			{
				switch(wrapMode)
				{
					case WrapMode.ONCE:
						Stop();
					break;
					case WrapMode.LOOP:
						Start(currentTime - endTime);
					break;
					default:
						throw new NotImplementedException();
					break;
				}
			}
		}
	}

	private float GetCurrentTime()
	{
		float currentTime = Time.time - _startTimeStamp;
		return currentTime + startTime;
	}

	public float Phase()
	{
		return (GetCurrentTime - startTime) / (endTime - startTime);
	}

	public void AddCallback(float time, Callback callback)
	{
		_callbacks.Add (new CallbackPair (time, callback));
	}
}
