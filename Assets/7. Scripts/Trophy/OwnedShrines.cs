using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class OwnedShrines : Trophy
{
    public List<Player> CalculateTrophy(ICollection<Player> players)
    {
        // not implemented, no data yet
        return null;
    }

    public string TrophyName
    {
        get
        {
            return this.TrophyName;
        }
        set
        {
            this.TrophyName = "Owned Shrines";
        }
    }

    public string Title
    {
        get
        {
            return this.Title;
        }
        set
        {
            this.Title = "Gaian";
        }
    }
}
