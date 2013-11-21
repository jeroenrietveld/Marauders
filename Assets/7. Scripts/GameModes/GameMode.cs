using UnityEngine;
using System.Collections;

[System.Flags]
public enum GameModeID
{
	CaptureTheFlag	= (1 <<  0),
	DeathMatch		= (1 <<  1)
}

public abstract class GameMode
{
	public readonly GameModeID id;

	protected GameMode(GameModeID id)
	{
		this.id = id;

		foreach(var description in MonoBehaviour.FindObjectsOfType<GameModeDescription>())
		{
			bool active = (description.gameModes | id) == id;
			description.gameObject.SetActive(active);
		}
	}
}
