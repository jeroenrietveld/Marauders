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
    public static bool resumeTimer = false;
    public static float _timeResume = 3f;
    public static GameObject guiResumeText;

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
    /// Pause the game by setting the timeScale to 0f.
    /// Set the text ro paused. isPaused is set to true
    /// and the resumeTimer t0 false.
    /// </summary>
    public static void PauseGame()
    {
        guiResumeText = GameObject.Find("ResumeTimer");
        guiResumeText.guiText.text = "Paused";

        isPaused = true;
        resumeTimer = false;
        Time.timeScale = 0f;
    }

    /// <summary>
    /// Resume  the game. Set isPaused to false and resumeTimer to true;
    /// By setting the resumeTimer to true the timer will start to
    /// countdown from 3 seconds to -1. At 0 players can fight, at -1 the text
    /// 'Fight!' will dissapear.
    /// </summary>
    public static void ResumeGame()
    {
        isPaused = false;
        resumeTimer = true;
    }

    /// <summary>
    /// Start the timer at 3 seconds. Every update call it
    /// changes _timeResume minus 0.01f because when the timeScale
    /// is set to zero we cannot use Time.Deltatime etc. After three seconds
    /// the game restarts and the timeScale will be reset to
    /// 3 seconds. When the timer hits -1f the text 'fight' will dissapear.
    /// </summary>
    public static void Timer()
    {
        GameManager._timeResume -= 0.01f;

        if (GameManager._timeResume <= 3f)
        {
            guiResumeText.guiText.text = "3";
        }
        if (GameManager._timeResume <= 2f)
        {
            guiResumeText.guiText.text = "2";
        }
        if (GameManager._timeResume <= 1f)
        {
            guiResumeText.guiText.text = "1";
        } 
        if (GameManager._timeResume <= 0f)
        {
            guiResumeText.guiText.text = "Fight!";
            Time.timeScale = 1f;
        }
        if (GameManager._timeResume <= -1f)
        {
            guiResumeText.guiText.text = "";
            _timeResume = 3f;
            resumeTimer = false;
        }
    }
}