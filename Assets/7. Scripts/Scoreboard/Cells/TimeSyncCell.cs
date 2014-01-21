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
        if ((int)content < 0 || (int)content > (float)GameManager.Instance.matchSettings.timeSync)
        {
            content = (int)Mathf.Clamp((int)content, 0, (float)GameManager.Instance.matchSettings.timeSync);
        }
        return ((int)content / (float)GameManager.Instance.matchSettings.timeSync*100f).ToString(); 
    }
}
