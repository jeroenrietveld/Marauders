﻿using UnityEngine;
using System.Collections.Generic;

public class ShrineManager : MonoBehaviour {

	private static System.Random rng = new System.Random();

	public int timeSyncPerShrine;
	public float activationDelay = 20;

	private Shrine[] _shrines;
	private List<Shrine> _inactiveShrines;
	private Timer _activationTimer;
	private Timer _rewardDelay;
	private Announcer _announcer;

	void Start () {
		_shrines = GetComponentsInChildren<Shrine> ();
		_inactiveShrines = new List<Shrine> ();
		_inactiveShrines.AddRange (_shrines);

		_activationTimer = new Timer (activationDelay, Timer.WrapMode.LOOP);
		_activationTimer.AddPhaseCallback (ActivateRandomShrine);
		_activationTimer.Start ();

		_rewardDelay = new Timer (2);
		_rewardDelay.AddPhaseCallback (0, _activationTimer.Stop);
		_rewardDelay.AddPhaseCallback (ApplyReward);

		GameObject Global = GameObject.Find("_GLOBAL");
		_announcer = Global.GetComponent<Announcer>();
	}

	void OnEnable()
	{
		Event.register<ShrineCapturedEvent> (OnShrineCaptured);
	}

	void OnDisable()
	{
		Event.unregister<ShrineCapturedEvent> (OnShrineCaptured);
	}

	void Update () {
		_activationTimer.Update ();
		_rewardDelay.Update ();
	}

	void OnShrineCaptured(ShrineCapturedEvent evt)
	{
		bool allCaptured = true;
		foreach(var shrine in _shrines)
		{
			allCaptured = allCaptured && shrine.captured;
		}

		if (allCaptured)
		{
			_rewardDelay.Start();
		} else
		{
			//Player X has captured a shrine
			_announcer.Announce(AnnouncementType.ShrineCapture, Locale.Current["shrine_announcement_captured"].Replace("{0}", (evt.newOwner.indexInt + 1).ToString()), Locale.Current["shrine_subannouncement_captured"]);
		}


	}

	private void ApplyReward()
	{
		foreach(var shrine in _shrines)
		{
			if(shrine.underAttack)
			{
				_rewardDelay.Start();
				return;
			}
		}

		foreach (var shrine in _shrines)
		{
			shrine.owner.AddTimeSync(timeSyncPerShrine, shrine.transform.position);
            GameObject.Find("_GLOBAL").GetComponent<Scoreboard>().AddContent(shrine.owner.index, Locale.Current["scoreboard_ownedshrines"], 1);
			shrine.Reset();
		}

		_announcer.Announce(AnnouncementType.ShrineCapture, Locale.Current["shrine_announcement_rewards"], Locale.Current["shrine_subannouncement_rewards"]);

		_activationTimer.Start ();
		_inactiveShrines.AddRange (_shrines);
	}

	private void ActivateRandomShrine()
	{
		if(_inactiveShrines.Count == 0) return;

		Shrine shrine = _inactiveShrines[rng.Next(_inactiveShrines.Count)];
		_inactiveShrines.Remove (shrine);

		shrine.Activate ();


		if (_inactiveShrines.Count == 0)
		{
			_announcer.Announce(AnnouncementType.ShrineCapture, Locale.Current["shrine_announcement_capturable_last"], Locale.Current["shrine_subannouncement_capturable_last"]);
		} 
		else
		{
			_announcer.Announce(AnnouncementType.ShrineCapture, Locale.Current["shrine_announcement_capturable"], Locale.Current["shrine_subannouncement_capturable"]);
		}		
	}
}
