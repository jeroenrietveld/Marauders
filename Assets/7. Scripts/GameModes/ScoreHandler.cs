using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

	public int killReward = 5;
	public int suicidePenalty = -5;
	public int smackedOutPenalty = -5;
	public int smackOutReward = 5;

	// Use this for initialization
	void Start () {
		Event.register<AvatarDeathEvent>(OnAvatarDeath);
		Event.register<TimeBubbleAvatarExitEvent>(OnTimeBubbleExit);
	}

	void OnDisable()
	{
		Event.unregister<AvatarDeathEvent> (OnAvatarDeath);
		Event.unregister<TimeBubbleAvatarExitEvent>(OnTimeBubbleExit);
	}

	void OnAvatarDeath(AvatarDeathEvent evt)
	{
		if(evt.offender != null) 
		{
			evt.offender.AddTimeSync (killReward);
            GameManager.scoreboard.AddContent(evt.offender.index, "Eliminations", 1);
            GameManager.scoreboard.AddContent(evt.offender.index, "Time Sync", killReward);
		}

		evt.victim.AddTimeSync (suicidePenalty);
        GameManager.scoreboard.AddContent(evt.victim.index, "Time Sync", suicidePenalty);
	}

	void OnTimeBubbleExit(TimeBubbleAvatarExitEvent evt)
	{
		var heartbeat = evt.avatar.GetComponentInChildren<Heartbeat> ();

		if(heartbeat.lastAttacker != null)
		{
			evt.avatar.player.AddTimeSync (smackedOutPenalty);
			GameManager.scoreboard.AddContent(evt.avatar.player.index, "Time Sync", smackedOutPenalty);

			heartbeat.lastAttacker.AddTimeSync(smackOutReward);
			GameManager.scoreboard.AddContent(heartbeat.lastAttacker.index, "Time Sync", smackOutReward);
		}
		else
		{
			evt.avatar.player.AddTimeSync (suicidePenalty);
			GameManager.scoreboard.AddContent(evt.avatar.player.index, "Time Sync", suicidePenalty);
		}
	}
}