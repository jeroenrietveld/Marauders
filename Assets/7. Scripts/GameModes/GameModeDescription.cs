using UnityEngine;
using System.Collections;

public enum TeamID
{
	Team1,
	Team2,
	Team3,
	Team4,
	Neutral
}

public class GameModeDescription : MonoBehaviour {
	public static readonly GameModeID DEFAULT_GAME_MODES =
			GameModeID.CaptureTheFlag |
			GameModeID.DeathMatch;

	public static readonly TeamID DEFAULT_TEAM_ID =
			TeamID.Neutral;

	//TODO Build nice inspector
	public GameModeID gameModes = DEFAULT_GAME_MODES;

	public TeamID teamID = DEFAULT_TEAM_ID;
}
