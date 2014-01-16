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

    public static bool isPaused = false;

	//TODO: Remove Player list, rename playerRefs to players.
	public List<Player> playerRefs;

	public int timeSyncLimit = 1000;

	public struct MatchSettings
	{
		public string level;
		public GameMode gameMode;
        public float timeSync;
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
	}

	public void Start()
	{   
        if (matchSettings.level != null)
        {
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
}