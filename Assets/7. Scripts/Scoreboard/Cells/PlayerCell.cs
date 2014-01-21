using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public class PlayerCell : Cell 
{
    public Player player;
	public PlayerCell(Player player)
    {
        this.player = player;
        this.title = player.index.ToString();
        this.content = Resources.Load("Textures/Defensive_icon") as Texture;
        this.initialContent = null;
        this.trophysEnabled = false;
        this.cellType = CellType.Static;
    }

    public override string GetContent()
    {
        return content.ToString();
    }
}
