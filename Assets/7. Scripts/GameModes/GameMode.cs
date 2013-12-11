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

    public Scoreboard scoreboard;

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
        scoreboard = new Scoreboard();
        List<Cell> fieldNameList = new List<Cell>();
        scoreboard.AddCellList(fieldNameList);
        foreach(String name in new String[] {"Players", "Time Sync", "Eliminations", "Eliminated", "Suicides", "Hitratio"})
        {
            fieldNameList.Add(new StringCell(name));
        }

        foreach(Player player in GameManager.Instance.players)
        {
            List<Cell> addition = new List<Cell>();
            scoreboard.AddCellList(addition);

            addition.Add(new PlayerCell(player));

            //Declare all cells
            ProgressbarCell timeSync = new ProgressbarCell(GameManager.Instance.matchSettings.timeSync);
            IntegerCell eliminations = new IntegerCell();
            IntegerCell eliminated = new IntegerCell();
            IntegerCell suicides = new IntegerCell();
            PercentageCell hitratio = new PercentageCell();

            //Add all cells to the list of cells
            addition.Add(timeSync);
            addition.Add(eliminations);
            addition.Add(eliminated);
            addition.Add(suicides);
            addition.Add(hitratio);

            //Register the events that are always needed
            Event.register<PlayerDeathEvent>(delegate(PlayerDeathEvent evt) 
            { 
                if(evt.victim == player) 
                {                  
                    timeSync.Add(-20);
                    eliminated.amount += 1;
                    if(evt.offender == player)
                    {
                        suicides.amount += 1;
                        //Apply a suicide penalty to the timeSync
                        timeSync.Add(-10);
                    }
                }
                else if(evt.offender == player)
                {
                    timeSync.Add(20);
                    eliminations.amount += 1;
                }
            });
        }
    }
   
}
