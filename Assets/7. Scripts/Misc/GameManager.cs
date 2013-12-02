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

	private struct MatchSettings
	{
		public string level;
		public GameMode gameMode;
	}

	/// <summary>
	/// Playlist of matches. (level, game mode combination)
	/// </summary>
	private List<MatchSettings> _matches;

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
		_matches = new List<MatchSettings>();
	}

	/// <summary>
	/// Loads the next scene.
	/// </summary>
	private void NextLevel()
	{
		//TODO implementation
		//Application.LoadLevel();
	}

	private void StartLevel(int unitySceneId)
	{
		Application.LoadLevel(unitySceneId);
	}

    /// <summary>
    /// Pause the game by setting the timeScale to 0f
    /// </summary>
    public static void PauseGame()
    {
        GameManager.isPaused = true;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resume  the game by setting the timeScale to 1f
    /// </summary>
    public static void ResumeGame()
    {
        GameManager.isPaused = false;
        Time.timeScale = 1f;
    }
}