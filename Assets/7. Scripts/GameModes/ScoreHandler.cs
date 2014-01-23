using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

	public int killReward = 100;
	public int suicidePenalty = -100;
	public int smackedOutPenalty = -100;
	public int smackOutReward = 100;

	// Use this for initialization
	void Start () {
		Event.register<AvatarDeathEvent>(OnAvatarDeath);
		Event.register<TimeBubbleAvatarExitEvent>(OnTimeBubbleExit);
        Event.register<PlayerTimeSyncedEvent>(OnTimeSynced);
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
			evt.offender.AddTimeSync (killReward, evt.victim.avatar.transform.position);
            GameManager.scoreboard.AddContent(evt.offender.index, "Kills", 1);
		}
	}

	void OnTimeBubbleExit(TimeBubbleAvatarExitEvent evt)
	{
		var heartbeat = evt.avatar.GetComponentsInChildren<Heartbeat> (true)[0];

		heartbeat.health = 1f;

		if(heartbeat.lastAttacker != null)
		{
			evt.avatar.player.AddTimeSync (smackedOutPenalty);
            GameManager.scoreboard.AddContent(evt.avatar.player.index, "Eliminated", 1);

			heartbeat.lastAttacker.AddTimeSync(smackOutReward, evt.exitPosition);
            GameManager.scoreboard.AddContent(heartbeat.lastAttacker.index, "Eliminations", 1);
		}
		else
		{
            GameManager.scoreboard.AddContent(evt.avatar.player.index, "Suicides", 1);
			evt.avatar.player.AddTimeSync (suicidePenalty);
		}
	}

    void OnTimeSynced(PlayerTimeSyncedEvent evt)
    {
        foreach(Player player in GameManager.Instance.playerRefs)
        {
            GameManager.scoreboard.AddContent(player.index, "Time Sync", player.timeSync);
        }
        GameManager.Instance.StopGame();
    }
}