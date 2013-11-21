using UnityEngine;
using System.Collections;
using System;

[Flags]
public enum GameModeID
{
	CaptureTheFlag	= (1 <<  0),
	DeathMatch		= (1 <<  1)
}

/// <summary>
/// Game mode abstract class
/// </summary>
public abstract class GameMode
{
	public readonly GameModeID id;

	protected GameMode(GameModeID id)
	{
		this.id = id;

		//Disable all game mode objects that are not relevant to the game mode
		foreach(var description in MonoBehaviour.FindObjectsOfType<GameModeDescription>())
		{
			bool active = (description.gameModes & id) == id;
			description.gameObject.SetActive(active);
		}
	}
}
