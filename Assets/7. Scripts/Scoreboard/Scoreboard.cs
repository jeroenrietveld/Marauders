using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Scoreboard
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
}

