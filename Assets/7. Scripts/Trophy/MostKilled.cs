using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class MostKilled : Trophy
{
    public List<Player> CalculateTrophy(ICollection<Player> players)
    {
        List<Player> mostKills = null;
        int kills = 0;

        foreach (Player player in players)
        {
            if (player.kills > kills)
            {
                // Clear list and add player with highest kills
                // Set kills variable to the current highest
                mostKills.Clear();
                mostKills.Add(player);
                kills = player.kills;
            }
            else if (player.kills == kills)
            {
                // If two player have the highest score add the second player to the list
                mostKills.Add(player);
            }
        }

        return mostKills;
    }

    public string TrophyName
    {
        get
        {
            return this.TrophyName;
        }
        set
        {
            this.TrophyName = "Kills";
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
            this.Title = "The Eliminator";
        }
    }
}

