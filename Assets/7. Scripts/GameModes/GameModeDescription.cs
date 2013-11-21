using UnityEngine;
using System.Collections;

/// <summary>
/// Defines the game mode(s) and team an object belongs to
/// </summary>
public class GameModeDescription : MonoBehaviour {
	public static readonly GameModeID DEFAULT_GAME_MODES =
			GameModeID.CaptureTheFlag |
			GameModeID.DeathMatch;

	public static readonly TeamColor DEFAULT_TEAM_COLOR =
			TeamColor.Neutral;

	//TODO Build nice inspector
	public GameModeID gameModes = DEFAULT_GAME_MODES;

	public TeamColor team = DEFAULT_TEAM_COLOR;
}
