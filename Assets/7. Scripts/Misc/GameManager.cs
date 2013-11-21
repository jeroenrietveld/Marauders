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

	private struct MatchSettings
	{
		public string level;
		public GameMode gameMode;
	}

	/// <summary>
	/// Playlist of matches. (level, game mode combination)
	/// </summary>
	private List<MatchSettings> matches;

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
		matches = new List<MatchSettings>();
	}

	/// <summary>
	/// Loads the next scene.
	/// </summary>
	private void NextLevel()
	{
		//TODO implementation
		//Application.LoadLevel();
	}
}