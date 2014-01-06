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

	public int timeSyncLimit;

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
		Debug.Log ("Start()");
        if (matchSettings.level != null)
        {
			Application.LoadLevel(matchSettings.level);
        } 
	}

    /// <summary>
    /// Adds the player ref to the list. If the player already has a playerref in the list remove that one 
    /// and add the new one.
    /// </summary>
    /// <param name="model"></param>
    public void AddPlayerRef(Player model)
    {
        playerRefs.RemoveAll(x => x.index.Equals(model.index));
        playerRefs.Add(model);
    }

    public void PauseGame()
    {
		Debug.Log ("Paused");
        isPaused = true;
        Time.timeScale = 0f;
    }

    public void ResumeGame()
    {
		Debug.Log ("Resume");
        isPaused = false;
        Time.timeScale = 1f;
    }
}