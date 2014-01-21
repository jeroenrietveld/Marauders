using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class Normal : GameMode
{
    public Normal() : base(GameModeID.Normal) 
    {
        for (int i = 0; i < GameManager.Instance.playerRefs.Count; i++)
        {
            GameManager.scoreboard.AddGameSpecificCell(i, new CustomCell("Owned Shrines", CellType.Integer, 0, true));
        }
        //No addition to the list
        //No addition to the gamemode
    }
}
