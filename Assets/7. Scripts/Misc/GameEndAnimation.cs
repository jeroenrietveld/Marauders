using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameEndAnimation : MonoBehaviour {

	private Timer _animationTimer;
	private List<Timer> _deathEventTimer;

	private float _deathOffset = 1f;

	private DamageSource _damageSource;

	void Start () {
		_damageSource = new DamageSource (null, Vector3.zero, Vector3.zero, float.MaxValue, 100f, true);

		_deathEventTimer = new List<Timer> ();

		_animationTimer = new Timer ((GameManager.Instance.playerRefs.Count) * _deathOffset + 5f);
		_animationTimer.AddCallback(GameManager.Instance.StopGame);

		foreach (Player _player in GameManager.Instance.playerRefs)
		{
			Player player = _player;

			Timer timer = new Timer(2f + _deathOffset * player.indexInt);

			timer.AddCallback(delegate() {
				if(!player.isTimeSynced)
				{
					Heartbeat hb = player.avatar.GetComponentsInChildren<Heartbeat>(true)[0];
					hb.DoAttack(_damageSource);
				}
			});

			_deathEventTimer.Add(timer);
		}

		Event.register<PlayerTimeSyncedEvent> (OnPlayerTimeSyncedEvent);
	}

	void Update () {
		foreach(Timer timer in _deathEventTimer)
		{
			timer.Update();
		}

		_animationTimer.Update ();
	}

	private void OnPlayerTimeSyncedEvent(PlayerTimeSyncedEvent evt)
	{
		foreach(Timer timer in _deathEventTimer)
		{
			timer.Start();
		}
		
		_animationTimer.Start ();
	}

	void OnDestroy()
	{
		Event.unregister<PlayerTimeSyncedEvent> (OnPlayerTimeSyncedEvent);
	}
}
