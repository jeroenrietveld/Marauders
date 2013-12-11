using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Scoreboard : MonoBehaviour
{
	private List<List<Cell>> _cells;

    public Scoreboard()
	{
		_cells = new List<List<Cell>> ();
	}

	public void AddCellList(List<Cell> list)
	{
		_cells.Add (list);
	}

   void OnGUI()
    {
       int rows = _cells.Count;
       int columns = _cells[0].Count;
       Rect scoreboardRect = new Rect(Screen.width - 20, Screen.height - 20, rows * 100, columns *20);
       for(int i = 0; i < rows; i++)
       {
           for(int j = 0; j < columns; j++)
           {
               //Draw the cell
               Rect cell = new Rect(j * 100, i * 20, 100, 20);
           }
       }
    }
}

