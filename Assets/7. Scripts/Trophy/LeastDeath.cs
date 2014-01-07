using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class LeastDeath : Trophy
{

    public List<Player> CalculateTrophy(ICollection<Player> players)
    {
        List<Player> mostDeaths = null;
        int deaths = 0;

        foreach (Player player in players)
        {
            if (player.deaths > deaths)
            {
                // Clear list and add player with highest deaths
                // Set deaths variable to the current highest
                mostDeaths.Clear();
                mostDeaths.Add(player);
                deaths = player.deaths;
            }
            else if (player.deaths == deaths) 
            {
                // If two player have the highest score add the second player to the list
                mostDeaths.Add(player);
            }
        }

        return mostDeaths;
    }

    public string TrophyName
    {
        get
        {
            return this.TrophyName;
        }
        set
        {
            TrophyName = "Eliminated";
        }
    }

    public string Title
    {
        get
        {
            return this.TrophyName;
        }
        set
        {
            TrophyName = "The Survivor";
        }
    }
}
