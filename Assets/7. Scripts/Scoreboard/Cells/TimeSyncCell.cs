using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeSyncCell : Cell
{
    public Player player;
    public TimeSyncCell(Player player)
    {
        this.title = "Time Sync";
        this.content = 0;
        this.player = player;
        this.trophysEnabled = false;
        this.cellType = CellType.Static;
    }

    public override string GetContent()
    {
        return (int)content > 0 ? (Mathf.Clamp(GameManager.Instance.matchSettings.timeSync / (int)(content), 0, 100)).ToString() : "0"; 
    }
}
