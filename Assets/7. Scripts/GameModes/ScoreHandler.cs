using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

	public int killScore = 50;
	public int deathPenalty = -25;

	// Use this for initialization
	void Start () {
		Event.register<AvatarDeathEvent>(OnAvaterDeath);
		Event.register<TimeBubbleAvatarExitEvent>(OnTimeBubbleExit);
	}

	void OnDisable()
	{
		Event.unregister<AvatarDeathEvent> (OnAvaterDeath);
		Event.unregister<TimeBubbleAvatarExitEvent>(OnTimeBubbleExit);
	}

	void OnAvaterDeath(AvatarDeathEvent evt)
	{
		if(evt.offender != null) 
		{
			evt.offender.AddTimeSync (killScore);
            GameManager.scoreboard.AddContent(evt.offender.index, "Eliminations", 1);
            GameManager.scoreboard.AddContent(evt.offender.index, "Time Sync", killScore);
		}

		evt.victim.AddTimeSync (deathPenalty);
        GameManager.scoreboard.AddContent(evt.victim.index, "Time Sync", deathPenalty);
	}

	void OnTimeBubbleExit(TimeBubbleAvatarExitEvent evt)
	{
		evt.avatar.player.AddTimeSync (deathPenalty);
        GameManager.scoreboard.AddContent(evt.avatar.player.index, "Time Sync", deathPenalty);
	}
}
