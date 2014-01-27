using UnityEngine;
using System.Collections.Generic;
using System;

[Flags]
public enum GameModeID
{
	CaptureTheFlag	= (1 <<  0),
	DeathMatch		= (1 <<  1),
    Normal          = (1 <<  2)
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

        InitializeScoreboard();
	}

    public void InitializeScoreboard()
    {
        foreach(var player in GameManager.Instance.playerRefs)
        {
            List<Cell> addition = new List<Cell>();
            GameManager.scoreboard.AddCellList(addition);

            addition.Add(new PlayerCell(player));

            //Declare all cells
            TimeSyncCell timeSync = new TimeSyncCell(player);
            TitlesCell titles = new TitlesCell();
            CustomCell eliminations = new CustomCell("Eliminations", CellType.Integer, 0, true);
            CustomCell eliminated = new CustomCell("Eliminated", CellType.Integer, 0, true);
            CustomCell suicides = new CustomCell("Suicides", CellType.Integer, 0, true);

            //Add all cells to the list of cells
            addition.Add(timeSync);
            addition.Add(titles);
            addition.Add(eliminations);
            addition.Add(eliminated);
            addition.Add(suicides);
        }

        var resources = Resources.LoadAll("JSON/Trophy");

        foreach (object resource in resources)
        {
            var node = SimpleJSON.JSON.Parse(((TextAsset)resource).text);
            string column = node["column"].Value;
            string title = node["title"].Value;
            string condition = node["condition"].Value;

            Trophy t = new Trophy();
            t.Column = column;
            t.Title = title;
            t.Condition = condition;

            GameManager.scoreboard.trophyList.Add(t);
        }
    }
   
}
