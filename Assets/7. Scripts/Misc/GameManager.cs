using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameManager : ScriptableObject {

	private static GameManager _Instance;

	private struct MatchSettings
	{
		public string Level;
		public string GameMode;
	}

	private List<MatchSettings> Matches;

	//private List

	/// <summary>
	/// Gets the instance.
	/// </summary>
	/// <value>The instance.</value>
	public static GameManager Instance
	{
		get
		{
			if(_Instance == null)
			{
				_Instance = GameManager.CreateInstance<GameManager>();
				matches = new List<MatchSettings>();
			}
			return _Instance;
		}
	}

	private void NextLevel()
	{
		//TODO implementation
		//Application.LoadLevel();
	}
}