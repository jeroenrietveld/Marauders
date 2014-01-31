using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

	private GameManager _game;

	private Timer _introTimer = new Timer(2);

	public int initialTimeSync = 10;

	// Use this for initialization
	void Start () {
		_game = GameManager.Instance;
		ShowIntro ();
		SpawnMarauders ();
	}

	private void ShowIntro()
	{
		GetComponent<Announcer>().Announce(AnnouncementType.ShrineCapture, Locale.Current["level_intro_announcement"], Locale.Current["level_intro_subannouncement"]);
		_introTimer.AddPhaseCallback (.5f, delegate {
			foreach(var player in _game.playerRefs)
			{
				player.AddTimeSync(initialTimeSync);
			}
		});

		_introTimer.Start ();
	}

	private void SpawnMarauders()
	{
		foreach (Player player in _game.playerRefs)
		{
			player.StartSpawnProcedure(true);
		}
	}

	void Update()
	{
		_introTimer.Update ();
	}
}
