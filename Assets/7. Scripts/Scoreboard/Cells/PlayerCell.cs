using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerCell : Cell 
{
	public PlayerCell(Player player)
    {
        this.title = player.index.ToString();
        this.content = /*Resources.Load(player.marauder) as Texture*/"image";
        this.trophysEnabled = false;
        this.cellType = CellType.Static;
    }

    public override string GetContent()
    {
        return content.ToString();
    }
}
