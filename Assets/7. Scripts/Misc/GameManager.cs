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
    public static float _timeResume = 3f;
    public static bool resumeTimer = false;

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
        isPaused = true;
        resumeTimer = false;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resume  the game. Set isPaused to false and resumeTimer to true;
    /// </summary>
    public static void ResumeGame()
    {
        isPaused = false;
        resumeTimer = true;
    }

    /// <summary>
    /// Start the timer at 3 seconds. Every update call it
    /// changes _timeResume minus 0.01f. After three seconds
    /// the game restarts and the timeScale will be reset to
    /// 3 seconds.
    /// </summary>
    public static void Timer()
    {
        GameManager._timeResume -= 0.01f;

        if (GameManager._timeResume <= 0f)
        {
            Time.timeScale = 1f;
            GameManager._timeResume = 3f;
        }
    }
}