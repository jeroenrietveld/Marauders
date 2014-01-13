using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using UnityEngine;

public class Trophy : ITrophy
{
    public string Column { get; set; }
    public string TrophyName { get; set; }
    public string Title { get; set; }
    public string Condition { get; set; }

    private List<PlayerTest> playerTrophy;

    public List<PlayerTest> CalculateTrophy(ICollection<PlayerTest> players)
    {
        playerTrophy = new List<PlayerTest>();

        foreach (PlayerTest player in players)
        {
            if (Condition.Equals(">"))
            {
                switch(TrophyName)
                {
                    case "Kills":
                        GreatherThan(players, "Kills");
                        break;
                    case "Owned Shrines":
                        GreatherThan(players, "Owned Shrines");
                        break;
                    case "Eliminated":
                        GreatherThan(players, "Eliminated");
                        break;
                }
            }
            else if(Condition.Equals("<"))
            {
                switch (TrophyName)
                {
                    case "Kills":
                        LessThan(players, "Kills");
                        break;
                    case "Owned Shrines":
                        LessThan(players, "Owned Shrines");
                        break;
                    case "Eliminated":
                        LessThan(players, "Eliminated");
                        break;
                }
            }
            
        }

        return playerTrophy;
    }

    private void GreatherThan(ICollection<PlayerTest> players, string name)
    {
        int win = 0;

        foreach (PlayerTest player in players)
        {
            //int playerWin = (int)player.GetType().GetProperty(name).GetValue(player, null);
            
            /*
            if (playerWin > win)
            {
                // Clear list and add player with highest deaths
                // Set deaths variable to the current highest
                playerTrophy.Clear();
                playerTrophy.Add(player);
                win = playerWin;
            }
            else if (playerWin == win)
            {
                // If two player have the highest score add the second player to the list
                playerTrophy.Add(player);
            }
             */
        }
    }

    private List<PlayerTest> LessThan(ICollection<PlayerTest> players, string name)
    {
        return null;
    }
}
