using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
 
public class Scoreboard : MonoBehaviour
{
	private List<List<Cell>> _cells;
    public GUISkin scoreboardskin;

    public float horizontalOffset = 10f;
    public float verticalOffset = 5f;

    public float fontScale = 0.01f;

    private Rect _scoreboardRect;

    public Scoreboard()
	{
		_cells = new List<List<Cell>> ();
	}

    /// <summary>
    /// This method will add a cell list to the end of the list
    /// </summary>
    /// <param name="list"></param>
	public void AddCellList(List<Cell> list)
	{
		_cells.Add (list);
	}   

    /// <summary>
    /// This method will add a custom cell just after the static cells.
    /// </summary>
    /// <param name="gameSpecificCell"></param>
    public void AddGameSpecificCell(CustomCell gameSpecificCell)
    {
        //Iterate over the cells
        for (int i = 0; i < _cells.Count; i++)
        {
            for (int j = 0; j < _cells[i].Count; j++)
            {
                if (_cells[i][j].cellType != CellType.Static)
                {
                    //This spot is the first spot after the static cells
                    _cells[i].Insert(j, gameSpecificCell);
                    break;
                }
            }
        }
    }

    void Start()
    {      
        Scoreboard scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();
        var player = new Player(PlayerIndex.One);
        List<Cell> addition = new List<Cell>();
        scoreboard.AddCellList(addition);

        addition.Add(new PlayerCell(player));

        //Declare all cells
        TimeSyncCell timeSync = new TimeSyncCell();
        TitlesCell titleCell = new TitlesCell();
        CustomCell eliminations = new CustomCell("Eliminations", CellType.Integer, 0, true);
        CustomCell eliminated = new CustomCell("Eliminated", CellType.Integer, 0, true);
        CustomCell kills = new CustomCell("Kills", CellType.Integer, 0, true);
        CustomCell hitratio = new CustomCell("Hitratio", CellType.Percentage, 0, true);
        CustomCell heartstops = new CustomCell("Heartstops", CellType.Integer, 0, true);   

        //Testing titles
        titleCell.AddTitle("TITLE2");
        titleCell.AddTitle("TITLE3");

        //Add all cells to the list of cells
        addition.Add(timeSync);
        addition.Add(titleCell);
        addition.Add(eliminations);
        addition.Add(eliminated);
        addition.Add(kills);
        addition.Add(heartstops);
        addition.Add(hitratio);

        scoreboard.AddGameSpecificCell(new CustomCell("Owned Shrines", CellType.Integer, 0, true));    
    }

   void OnGUI()
    {
       _scoreboardRect = new Rect(20, 20, Screen.width - 40, Screen.height - 40);
       GUI.skin = scoreboardskin;
       _scoreboardRect = GUI.Window(0, _scoreboardRect, DrawScoreboard, "");                 
    }

   void DrawScoreboard(int windowID)
   {
       float boxWidth = _scoreboardRect.width - horizontalOffset*2;
       float boxHeight = _scoreboardRect.height / 4 - verticalOffset*2;

       int rows = _cells.Count;
       int columns = _cells[0].Count;

       float cellwidth = boxWidth/columns;
       float cellTop = boxHeight*0.1f;
       float cellHeight = boxHeight * 0.8f;
              
       for (int i = 0; i < rows; i++)
       {
           GUI.Box(new Rect(horizontalOffset, verticalOffset + i * (boxHeight + verticalOffset * 2), boxWidth, boxHeight), "");
           for (int j = 0; j < columns; j++)
           {            
               //Draw the title
               GUI.Label(new Rect(j * cellwidth, i * (boxHeight + verticalOffset*2) + cellTop, cellwidth, cellHeight), _cells[i][j].title);

               //Draw the content
               if (_cells[i][j] is TitlesCell)
               {
                   scoreboardskin.label.alignment = TextAnchor.UpperLeft;
                   //GUI.contentColor = Player.color;
                   GUI.Label(new Rect(j * cellwidth, i * (boxHeight + verticalOffset * 2) + cellTop + 25, cellwidth, cellHeight), _cells[i][j].GetContent());
                   scoreboardskin.label.alignment = TextAnchor.UpperCenter;
               } 
               else if(_cells[i][j] is PlayerCell)
               {
                   GUI.Box(new Rect(j * cellwidth + cellTop, i * (boxHeight + verticalOffset * 2) + cellTop + 25, cellwidth - 20, cellHeight - 20), "Texture");
               } 
               else if(_cells[i][j] is TimeSyncCell)
               {
                   //TimeSync texture here
                   scoreboardskin.label.alignment = TextAnchor.MiddleCenter;
                   GUI.Label(new Rect(j * cellwidth, i * (boxHeight + verticalOffset * 2) + cellTop + 25, cellwidth, cellHeight - 20), "0%");
                   scoreboardskin.label.alignment = TextAnchor.UpperCenter;
               }
               else 
               {
                   GUI.Label(new Rect(j * cellwidth, i * (boxHeight + verticalOffset * 2) + cellTop + 25, cellwidth, cellHeight), _cells[i][j].GetContent());
               }
               
           }
       }  
   }
}

