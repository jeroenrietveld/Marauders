using UnityEngine;
using System.Collections;

public class ShrineManager : MonoBehaviour {

	public int timeSyncPerShrine;
	public float activationDelay = 20;

	private Shrine[] _shrines;
	private Timer _activationTimer;

	// Use this for initialization
	void Start () {
		_shrines = GetComponentsInChildren<Shrine> ();

		_activationTimer = new Timer (activationDelay, Timer.WrapMode.LOOP);
		_activationTimer.AddPhaseCallback (ActivateRandomShrine);

		_activationTimer.Start ();
	}

	void OnEnable()
	{
		Event.register<ShrineCapturedEvent> (OnShrineCaptured);
	}

	void OnDisable()
	{
		Event.unregister<ShrineCapturedEvent> (OnShrineCaptured);
	}
	
	// Update is called once per frame
	void Update () {
		_activationTimer.Update ();
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
			ApplyReward();
		}
	}

	private void ApplyReward()
	{


		foreach (var shrine in _shrines)
		{
			shrine.owner.AddTimeSync(timeSyncPerShrine);
			shrine.Reset();
		}
	}

	private void ActivateRandomShrine()
	{
		//TODO: make random
		foreach(var shrine in _shrines)
		{
			if(!shrine.capturable)
			{
				shrine.Activate();
				break;
			}
		}
	}
}
