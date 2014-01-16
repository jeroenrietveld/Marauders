using UnityEngine;
using System.Collections.Generic;

public class ShrineManager : MonoBehaviour {

	private static System.Random rng = new System.Random();

	public int timeSyncPerShrine;
	public float activationDelay = 20;

	private Shrine[] _shrines;
	private List<Shrine> _inactiveShrines;
	private Timer _activationTimer;
	private Timer _rewardDelay;

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
		}
	}

	private void ApplyReward()
	{
		foreach (var shrine in _shrines)
		{
			shrine.owner.AddTimeSync(timeSyncPerShrine);
			shrine.Reset();
		}

		_activationTimer.Start ();
		_inactiveShrines.AddRange (_shrines);
	}

	private void ActivateRandomShrine()
	{
		if(_inactiveShrines.Count == 0) return;

		Shrine shrine = _inactiveShrines[rng.Next(_inactiveShrines.Count)];
		_inactiveShrines.Remove (shrine);

		shrine.Activate ();
	}
}
