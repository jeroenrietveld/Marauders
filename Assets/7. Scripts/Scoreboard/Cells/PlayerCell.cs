using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PlayerCell : Cell 
{
    private PlayerRef _player;
	public PlayerCell(PlayerRef player)
    {
        _player = player;
    }

    public override String GetContent()
    {
        return _player.marauder;
    }
}
