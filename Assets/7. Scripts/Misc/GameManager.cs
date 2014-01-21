using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

public class GameManager {

	/// <summary>
	/// Instance of the game manager (singleton)
	/// </summary>
	private static GameManager _instance;

    /// <summary>
    /// The instance of the scoreboard
    /// </summary>
    public static Scoreboard scoreboard;

    public static bool isPaused = false;

	//TODO: Remove Player list, rename playerRefs to players.
	public List<Player> playerRefs;
	public SoundIngame soundInGame;

    public bool gameEnded = false;

	public struct MatchSettings
	{
		public string level;
		public GameMode gameMode;
        public int timeSync;
	}
	public MatchSettings matchSettings;

	public static GameManager Instance
	{
		get
		{
			if(_instance == null)
			{
				_instance = new GameManager();
			}
			return _instance;
		}
	}

	private GameManager()
	{
        matchSettings = new MatchSettings();
		playerRefs = new List<Player> ();
		soundInGame = new SoundIngame();
        scoreboard = GameObject.Find("_MENUGLOBAL").GetComponent<Scoreboard>();
	}

	public void Start()
	{
        if (matchSettings.level != null)
        {
            gameEnded = false;
			Application.LoadLevel(matchSettings.level);
        } 
	}

    public void AddPlayerRef(Player model)
    {
        playerRefs.RemoveAll(x => x.index.Equals(model.index));
		
		int index = 0;
		while(index < playerRefs.Count && playerRefs[index].indexInt < model.indexInt) 
		{
			index++;
		}
		
		playerRefs.Insert (index, model);
    }

    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }

	public List<Player> playersByTimeSync()
	{
		return playerRefs.OrderBy (player => player.timeSync).ToList ();
	}

    public void StopGame()
    {
        scoreboard.Show();
        PauseGame();
        gameEnded = true;
    }
}