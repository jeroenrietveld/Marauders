using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardGraphics : MonoBehaviour {

	private List<Timer> _lightBulbTimers;
	private List<Timer> _timeSyncSliceTimers;
	private List<Vector2[]> _timeSyncSliceOffsets;

	private static Vector2[] timeSyncLossOffsets = new []{ new Vector2(150, 150), new Vector2(-150, 150), new Vector2(150, -150), new Vector2(-150, -150) };

	public int ellipseRadius = 4;

	private GameUI _gameUI;

	void Start () {
		_lightBulbTimers = new List<Timer> ();
		_timeSyncSliceTimers = new List<Timer> ();
		_timeSyncSliceOffsets = new List<Vector2[]> ();

		_gameUI = GetComponent<GameUI> ();

		Color defaultColor = new Color (0.5f, 0.5f, 0.5f, 0.5f);

		foreach (Player _player in GameManager.Instance.playerRefs)
		{
			Timer timer = new Timer(1f);
			Player player = _player;
			
			Color playerColor = player.color;
			playerColor.a = 0.8f;

			timer.AddTickCallback(delegate(){
				float phase = Mathf.Clamp(timer.Phase(), 0, 1);
				float sine = Mathf.Sin(phase * Mathf.PI);

				float factor = Mathf.Clamp(halfEllipse(phase * (2 * ellipseRadius), ellipseRadius), 0, 1);

				_gameUI.lightBulbColors[player.indexInt] = sine * playerColor;
			});

			timer.AddCallback(timer.endTime, delegate() {
				_gameUI.lightBulbColors[player.indexInt] = defaultColor;
			});

			_lightBulbTimers.Add(timer);
		}

		foreach(var _player in GameManager.Instance.playerRefs)
		{
			var player = _player;
			var timer = new Timer(.5f);
			var idx = player.indexInt;

			timer.AddPhaseCallback(0, delegate
			{
				_gameUI.timeSyncSliceAlphas[idx] = 1;
			});
			timer.AddPhaseCallback(delegate
			{
				_gameUI.timeSyncSliceAlphas[idx] = 0;
				_gameUI.shownTimeSync[idx] = player.timeSync;
			});
			timer.AddTickCallback(delegate
			{
				_gameUI.timeSyncSliceOffsets[idx] = Vector2.Lerp(_timeSyncSliceOffsets[idx][0], _timeSyncSliceOffsets[idx][1], timer.Phase ());
			});


			_timeSyncSliceTimers.Add(timer);
			_timeSyncSliceOffsets.Add(new []{ new Vector2(0, 0), new Vector2(0, 0) });
		}

		Event.register<PlayerTimeSyncEvent> (LightBulbAnimation);
		Event.register<PlayerTimeSyncEvent> (TimeSyncSliceAnimation);
	}

	void OnDestroy()
	{
		Event.unregister<PlayerTimeSyncEvent> (LightBulbAnimation);
		Event.unregister<PlayerTimeSyncEvent> (TimeSyncSliceAnimation);
	}

	private float halfEllipse(float x, float radius)
	{
		//such math, many ellipse, wow
		//http://www.wolframalpha.com/input/?i=%28sqrt%28a%5E2+-+%28x+-+a%29%5E2%29%29%2Fa

		return (1/radius) * (Mathf.Sqrt (Mathf.Pow(radius, 2) - Mathf.Pow(x - radius, 2)));
	}

	void Update () {
		foreach(Timer timer in _lightBulbTimers)
		{
			timer.Update();
		}

		foreach(var timer in _timeSyncSliceTimers)
		{
			timer.Update();
		}
	}

	private void LightBulbAnimation(PlayerTimeSyncEvent evt)
	{
		if(evt.amount > 0)
		{
			Timer timer = _lightBulbTimers[evt.player.indexInt];
			timer.Start();
		}
	}

	private void TimeSyncSliceAnimation(PlayerTimeSyncEvent evt)
	{
		var idx = evt.player.indexInt;
		_timeSyncSliceTimers [idx].Start ();

		Vector2 srcOffset = new Vector2(), tgtOffset = new Vector2();

		if(evt.amount > 0 && evt.hasPosition)
		{
			srcOffset = _gameUI.ToPlayerLocalScreenSpace(Camera.main.WorldToScreenPoint(evt.worldSpacePosition), evt.player);
		}
		else
		{
			tgtOffset = timeSyncLossOffsets[idx];
		}

		if(evt.amount < 0)
		{
			_gameUI.shownTimeSync[idx] = evt.player.timeSync;
		}

		_timeSyncSliceOffsets [idx] [0] = srcOffset;
		_timeSyncSliceOffsets [idx] [1] = tgtOffset;

		float requiredSync = GameManager.Instance.matchSettings.timeSync;

		_gameUI.timeSyncSliceBounds [idx] = new Vector2 (
			Mathf.Min (evt.player.timeSync, evt.player.timeSync - evt.amount) / requiredSync, 
			Mathf.Max (evt.player.timeSync, evt.player.timeSync - evt.amount) / requiredSync);
	}
}
