using UnityEngine;
using System;
using System.Collections.Generic;

public class Timer
{
	public float startTime;
	public float endTime;

	public float startPhase;
	public float endPhase;

	public enum WrapMode
	{
		NONE,
		ONCE,
		LOOP
	}
	public WrapMode wrapMode;

	public delegate void Callback();

	public bool running
	{
		get
		{
			return _running;
		}
	}
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

		this.startPhase = 0;
		this.endPhase = 1;

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
		Debug.Log (running);
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
					case WrapMode.NONE:
					break;
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

	public float GetCurrentTime()
	{
		float currentTime = _running ? Time.time - _startTimeStamp : 0;
		return currentTime + startTime;
	}

	public float Phase()
	{
		return startPhase + (GetCurrentTime() - startTime) / (endTime - startTime) * (endPhase - startPhase);
	}

	public void AddCallback(Callback callback)
	{
		AddCallback(endTime, callback);
	}

	public void AddCallback(float time, Callback callback)
	{
		int index = 0;
		while(index < _callbacks.Count && _callbacks[index].time < time) 
		{
			index++;
		}

		_callbacks.Insert (index, new CallbackPair (time, callback));
	}
}
