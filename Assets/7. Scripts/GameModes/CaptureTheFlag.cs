using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
public class CaptureTheFlag : GameMode
{
    public CaptureTheFlag() : base(GameModeID.CaptureTheFlag)
    {
        List<Cell> additives = new List<Cell>();
        scoreboard.AddCellList(additives);
    }
}
