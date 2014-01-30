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

	private float _currentTime;

	private List<CallbackPair> _callbacks;
	private List<CallbackPair> _phaseCallbacks;
	private List<Callback> _tickCallbacks;
	private int _callbackIndex;
	private int _phaseCallbackIndex;
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

	public Timer () : this(0f) {}

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
		this._currentTime = startTime;
		this._callbacks = new List<CallbackPair> ();
		this._phaseCallbacks = new List<CallbackPair> ();
		this._tickCallbacks = new List<Callback> ();
		this._callbackIndex = 0;
		this._phaseCallbackIndex = 0;
	}

	public void Start()
	{
		Start(0f);
	}

	private void Start(float remainingTime)
	{
		_running = true;
		_currentTime = startTime + remainingTime;
		_callbackIndex = 0;
		_phaseCallbackIndex = 0;
	}

	public void Stop()
	{
		_running = false;
	}

	public void Continue()
	{
		_running = true;
	}

	public void Update()
	{
		if(_running)
		{
			_currentTime += Time.deltaTime;
			var referenceTime = _currentTime; // Check for Timer restarting from a callback

			while(_callbackIndex < _callbacks.Count && _callbacks[_callbackIndex].time < _currentTime && _currentTime == referenceTime)
			{
				_callbacks[_callbackIndex].callback();

				_callbackIndex++;
			}

			var phase = Phase();
			while(_phaseCallbackIndex < _phaseCallbacks.Count && _phaseCallbacks[_phaseCallbackIndex].time < phase && _currentTime == referenceTime)
			{
				_phaseCallbacks[_phaseCallbackIndex].callback();
				_phaseCallbackIndex++;
			}

			foreach(var callback in _tickCallbacks)
			{
				callback();
			}


			if(_currentTime > endTime)
			{
				switch(wrapMode)
				{
					case WrapMode.NONE:
					break;
					case WrapMode.ONCE:
						Stop();
					break;
					case WrapMode.LOOP:
						Start(_currentTime - endTime);
					break;
				}
			}
		}
	}

	public float GetCurrentTime()
	{
		return _currentTime;
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
		SortedInsert(_callbacks, time, callback);
	}

	public void AddPhaseCallback(Callback callback)
	{
		AddPhaseCallback (endPhase, callback);
	}

	public void AddPhaseCallback(float phase, Callback callback)
	{
		SortedInsert (_phaseCallbacks, phase, callback);
	}

	public void AddTickCallback(Callback callback)
	{
		_tickCallbacks.Add (callback);
	}

	private void SortedInsert(List<CallbackPair> callbacks, float time, Callback callback)
	{
		int index = 0;
		while(index < callbacks.Count && callbacks[index].time < time) 
		{
			index++;
		}
		
		callbacks.Insert (index, new CallbackPair (time, callback));
	}
}
