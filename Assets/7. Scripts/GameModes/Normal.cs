using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Normal : GameMode
{
    public Normal() : base(GameModeID.Normal) 
    {
        GameManager.scoreboard.AddGameSpecificCell(new CustomCell("Owned Shrines", CellType.Integer, 0, true));
        //No addition to the list
        //No addition to the gamemode
    }
}
