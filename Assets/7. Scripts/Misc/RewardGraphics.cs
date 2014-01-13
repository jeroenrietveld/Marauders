using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardGraphics : MonoBehaviour {

	private List<Timer> effectTimers;

	// Use this for initialization
	void Awake () {
		effectTimers = new List<Timer> ();
		GameUI gameUI = GetComponent<GameUI> ();

		Color mult = new Color (0.5f, 0.5f, 0.5f, 0.5f);

		foreach (Player _player in GameManager.Instance.playerRefs)
		{
			Timer timer = new Timer(0.2f);
			Player player = _player;

			timer.AddTickCallback(delegate(){
				float phase = timer.Phase();

				gameUI.lightBulbColors[player.indexInt] = Color.Lerp(Color.white, mult * player.color, phase);
			});

			effectTimers.Add(timer);
		}

		Event.register<AvatarDeathEvent> (AvatarDeath);
	}
	
	// Update is called once per frame
	void Update () {
		if (effectTimers != null)
		{
			foreach(Timer timer in effectTimers)
			{
				timer.Update();
			}
		}
	}

	private void AvatarDeath(AvatarDeathEvent evt)
	{
		Player offender = evt.offender;

		if(offender != null)
		{
			Timer timer = effectTimers[offender.indexInt];
			timer.Start();
		}
	}
}
