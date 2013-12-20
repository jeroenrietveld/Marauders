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
	public List<PlayerRef> playerRefs;

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
		playerRefs = new List<PlayerRef> ();
	}

	public void Start()
	{   
		Debug.Log ("Start()");
        if (matchSettings.level != null)
        {
			Application.LoadLevel(matchSettings.level);
        } 
	}

    public void LoadMarauders()
    {
        foreach (PlayerRef player in playerRefs)
        {
            // GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/Marauders/" + player.marauder)) as GameObject;
            //prefab.AddComponent("Player");
            //prefab.GetComponent<Player>().LoadModel(player);

            //add the players to the player list, this way they are always easily accessible
            //players.Add(prefab.GetComponent<Player>());
            player.CreateAvatar();
        }
    }

    /// <summary>
    /// Adds the player ref to the list. If the player already has a playerref in the list remove that one 
    /// and add the new one.
    /// </summary>
    /// <param name="model"></param>
    public void AddPlayerRef(PlayerRef model)
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