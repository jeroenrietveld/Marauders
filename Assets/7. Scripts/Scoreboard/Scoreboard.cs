using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
	private List<List<Cell>> _cells;
    public GUISkin scoreboardskin;

    public Scoreboard()
	{
		_cells = new List<List<Cell>> ();
	}

	public void AddCellList(List<Cell> list)
	{
		_cells.Add (list);
	}

    void Start()
    {
        List<Player> testlist = new List<Player>();
        testlist.Add(GameObject.Find("Player1").GetComponent<Player>());
        testlist.Add(GameObject.Find("Player2").GetComponent<Player>());
        Scoreboard scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();
        List<Cell> fieldNameList = new List<Cell>();
        scoreboard.AddCellList(fieldNameList);
        foreach (String name in new String[] { "Players", "Time Sync", "Eliminations", "Eliminated", "Suicides", "Hitratio" })
        {
            fieldNameList.Add(new StringCell(name));
        }

        foreach (Player player in testlist)
        {
            List<Cell> addition = new List<Cell>();
            scoreboard.AddCellList(addition);

            addition.Add(new PlayerCell(player));

            //Declare all cells
            ProgressbarCell timeSync = new ProgressbarCell(100);
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
                if (evt.victim == player)
                {
                    timeSync.Add(-20);
                    eliminated.amount += 1;
                    if (evt.offender == player)
                    {
                        suicides.amount += 1;
                        //Apply a suicide penalty to the timeSync
                        timeSync.Add(-10);
                    }
                }
                else if (evt.offender == player)
                {
                    timeSync.Add(20);
                    eliminations.amount += 1;
                }
            });
        }
        
       
    }
   void OnGUI()
    {
        GUI.skin = scoreboardskin;
        int rows = _cells.Count;
        int columns = _cells[0].Count;

        int width = Screen.width;
        int heigth = Screen.height;
        int cellwidth = 100;
        int cellheigth = 20;
        for (int i = 0; i < rows; i++)
        {
            for (int j = 0; j < columns; j++)
            {
                _cells[i][j].pos = new Vector2(j * cellwidth, i * cellheigth);
                _cells[i][j].size = new Vector2(cellwidth, cellheigth);
                GUI.Label(new Rect(j * cellwidth, i * cellheigth, cellwidth, cellheigth), _cells[i][j].GetContent());
            }
        }    
    }
}

