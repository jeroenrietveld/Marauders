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

	public void AddCellList(List<Cell> list)
	{
		_cells.Add (list);
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

        //Add all cells to the list of cells
        addition.Add(timeSync);
        addition.Add(titleCell);
        addition.Add(eliminations);
        addition.Add(eliminated);
        addition.Add(kills);
        addition.Add(heartstops);
        addition.Add(hitratio);                          
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

       int rows = 1;
       int columns = _cells[0].Count;

       float cellwidth = boxWidth/columns;
       float cellheigth = boxHeight*0.2f + 10;
              
       for (int i = 0; i < rows; i++)
       {
           GUI.Box(new Rect(horizontalOffset, verticalOffset + i * (boxHeight + verticalOffset * 2), boxWidth, boxHeight), "");
           for (int j = 0; j < columns; j++)
           {         
               //_cells[i][j].pos = new Vector2(j * cellwidth, i * cellheigth);
               //_cells[i][j].size = new Vector2(cellwidth, cellheigth);
               GUI.Label(new Rect(j * cellwidth, i * cellheigth, cellwidth, cellheigth), _cells[i][j].title);
               GUI.Label(new Rect(j * cellwidth, (float)(i + 0.5f) * cellheigth, cellwidth, cellheigth), _cells[i][j].GetContent());
           }
       }  
   }
}

