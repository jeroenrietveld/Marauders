using UnityEngine;
using System.Collections;

public class ScoreHandler : MonoBehaviour {

	public int killReward = 100;
	public int suicidePenalty = -100;
	public int smackedOutPenalty = -100;
	public int smackOutReward = 100;

    public Scoreboard scoreboard;

	// Use this for initialization
	void Start () {
		Event.register<AvatarDeathEvent>(OnAvatarDeath);
		Event.register<TimeBubbleAvatarExitEvent>(OnTimeBubbleExit);
        Event.register<PlayerTimeSyncedEvent>(OnTimeSynced);

        scoreboard = GameObject.Find("_GLOBAL").GetComponent<Scoreboard>();
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
            scoreboard.AddContent(evt.offender.index, Locale.Current["scoreboard_eliminations"], 1);
		}
       
        scoreboard.AddContent(evt.victim.index, Locale.Current["scoreboard_eliminated"], 1);
	}

	void OnTimeBubbleExit(TimeBubbleAvatarExitEvent evt)
	{
		var heartbeat = evt.avatar.GetComponentsInChildren<Heartbeat> (true)[0];

		if(heartbeat.lastAttacker != null)
		{
			evt.avatar.player.AddTimeSync (smackedOutPenalty);
            scoreboard.AddContent(evt.avatar.player.index, Locale.Current["scoreboard_eliminated"], 1);

			heartbeat.lastAttacker.AddTimeSync(smackOutReward, evt.exitPosition);
            scoreboard.AddContent(heartbeat.lastAttacker.index, Locale.Current["scoreboard_eliminations"], 1);
		}
		else
		{
            scoreboard.AddContent(evt.avatar.player.index, Locale.Current["scoreboard_suicides"], 1);
			evt.avatar.player.AddTimeSync (suicidePenalty);
		}
	}

    void OnTimeSynced(PlayerTimeSyncedEvent evt)
    {
        foreach(Player player in GameManager.Instance.playerRefs)
        {
            scoreboard.AddContent(player.index, Locale.Current["scoreboard_timesync"], player.timeSync);
        }
        if (!GameManager.Instance.gameEnded)
        {
            GameManager.Instance.StopGame();
        }
    }
}