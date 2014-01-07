using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeSyncCell : Cell
{
    public TimeSyncCell()
    {
        this.title = "Time Sync";
        this.content = "timesync";
        this.trophysEnabled = false;
        this.cellType = CellType.Static;
    }

    public override string GetContent()
    {
        return content.ToString();
    }
}
