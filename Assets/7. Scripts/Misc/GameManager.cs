using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Keeps track of the game.
/// </summary>
public class GameManager {

	/// <summary>
	/// Instance of the game manager (singleton)
	/// </summary>
	private static GameManager _instance;
    public static bool isPaused = false;
    public MatchSettings matchSettings;

    public List<PlayerModel> players;

	public struct MatchSettings
	{
		public string level;
		public GameMode gameMode;
	}

	/// <summary>
	/// Playlist of matches. (level, game mode combination)
	/// </summary>
	//private List<MatchSettings> _matches;

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
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
		//_matches = new List<MatchSettings>();
        matchSettings = new MatchSettings();
        players = new List<PlayerModel>();
	}

	/// <summary>
	/// Loads the next scene.
	/// </summary>
	private void NextLevel()
	{
		// TODO implementation
		// Application.LoadLevel();
	}

	public void Start()
	{   
        if (matchSettings.level != null)
        {
            Debug.Log(Application.loadedLevelName);
            Application.LoadLevel(matchSettings.level);  
            foreach (PlayerModel model in players)
            {
                GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/Marauders/" + model.character)) as GameObject;
                prefab.AddComponent("Player");
                prefab.GetComponent<Player>().AddModel(model);
            }
        }
	}

    /// <summary>
    /// Pause the game by setting the timeScale to 0f.
    /// </summary>
    public void PauseGame()
    {
        isPaused = true;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resume  the game by setting timeScale to 1f.
    /// </summary>
    public void ResumeGame()
    {
        isPaused = false;
        Time.timeScale = 1f;
    }
}