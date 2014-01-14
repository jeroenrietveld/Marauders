using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RewardGraphics : MonoBehaviour {

	private List<Timer> effectTimers;

	public int ellipseRadius = 4;

	// Use this for initialization
	void Awake () {
		effectTimers = new List<Timer> ();
		GameUI gameUI = GetComponent<GameUI> ();

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

				gameUI.lightBulbColors[player.indexInt] = sine * playerColor;
			});

			timer.AddCallback(timer.endTime, delegate() {
				gameUI.lightBulbColors[player.indexInt] = defaultColor;
			});

			effectTimers.Add(timer);
		}

		Event.register<AvatarDeathEvent> (AvatarDeath);
	}

	private float halfEllipse(float x, float radius)
	{
		//such math, many ellipse, wow
		//http://www.wolframalpha.com/input/?i=%28sqrt%28a%5E2+-+%28x+-+a%29%5E2%29%29%2Fa

		return (1/radius) * (Mathf.Sqrt (Mathf.Pow(radius, 2) - Mathf.Pow(x - radius, 2)));
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
