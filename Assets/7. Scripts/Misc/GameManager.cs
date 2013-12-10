using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager {

	/// <summary>
	/// Instance of the game manager (singleton)
	/// </summary>
	private static GameManager _instance;

    public static bool isPaused = false;

    public List<Player> players;
	public List<PlayerModel> playerModels;

	public struct MatchSettings
	{
		public string level;
		public GameMode gameMode;
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
		players = new List<Player>();
		playerModels = new List<PlayerModel>();
	}

	public void Start()
	{   
        if (matchSettings.level != null)
        {
			Application.LoadLevel(matchSettings.level);

			foreach (PlayerModel model in playerModels)
			{
				GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/Marauders/" + model.marauder)) as GameObject;
				prefab.AddComponent("Player");
				prefab.GetComponent<Player>().LoadModel(model);

				//add the players to the player list, this way they are always easily accessible
				players.Add(prefab.GetComponent<Player>());
			}
        } 
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
}