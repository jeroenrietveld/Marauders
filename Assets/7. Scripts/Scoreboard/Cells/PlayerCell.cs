using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayerCell : Cell 
{
    private Player _player;
    public PlayerCell(Player player)
    {
        _player = player;
    }

    public override String GetContent()
    {
        return _player.name;
    }
}
