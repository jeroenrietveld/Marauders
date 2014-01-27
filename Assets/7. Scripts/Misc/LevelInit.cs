using UnityEngine;
using System.Collections;

public class LevelInit : MonoBehaviour {

	private GameManager _game;

	// Use this for initialization
	void Start () {
		_game = GameManager.Instance;
        GameManager.scoreboard = GameObject.Find("_GLOBAL").GetComponent<Scoreboard>();
		SpawnMarauders ();
	}

	private void SpawnMarauders()
	{
		foreach (Player player in _game.playerRefs)
		{
			player.StartSpawnProcedure(true);
		}
	}
}
