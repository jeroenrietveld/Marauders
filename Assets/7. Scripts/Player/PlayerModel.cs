using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public struct PlayerModel
{
    public string offensiveSkill { get; set; }

    public string defensiveSkill { get; set; }

    public string utilitySkill { get; set; }

    public PlayerIndex index { get; set; }

    public string marauder { get; set; }

    public TeamColor color { 
        get 
        {
            switch (index)
            {
                case PlayerIndex.One:
                    return TeamColor.Red;
                case PlayerIndex.Two:
                    return TeamColor.Blue;
                case PlayerIndex.Three:
                    return TeamColor.Green;
                case PlayerIndex.Four:
                    return TeamColor.Purple;
                default:
                    return TeamColor.Neutral;
            }
        } 
    }
}
