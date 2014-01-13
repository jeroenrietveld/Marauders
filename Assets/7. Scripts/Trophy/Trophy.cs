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

        if (Condition.Equals(">"))
        {
            GreatherThan(players);
        }
        else if(Condition.Equals("<"))
        {
            LessThan(players);
        }

        return playerTrophy;
    }

    private void LessThan(ICollection<PlayerTest> players)
    {
        if (Column == "LeastDeath")
        {
            int win = players.First().Deaths;

            foreach (PlayerTest player in players)
            {
                if (player.Deaths < win)
                {
                    win = player.Deaths;

                    playerTrophy.Clear();
                    playerTrophy.Add(player);
                }
                else if (player.Deaths == win)
                {
                    playerTrophy.Add(player);
                }
            }
        }
    }

    private void GreatherThan(ICollection<PlayerTest> players)
    {
        if (Column == "OwnedShrines")
        {
            int win = 0;

            foreach(PlayerTest player in players)
            {
                if (player.TimeSync > win)
                {
                    win = player.TimeSync;

                    playerTrophy.Clear();
                    playerTrophy.Add(player);
                }
                else if (player.TimeSync == win)
                {
                    playerTrophy.Add(player);
                }
            }
        }
        else if (Column == "MostKills")
        {
            int win = 0;

            foreach (PlayerTest player in players)
            {
                if (player.Kills > win)
                {
                    win = player.Kills;

                    playerTrophy.Clear();
                    playerTrophy.Add(player);
                }
                else if (player.Kills == win)
                {
                    playerTrophy.Add(player);
                }
            }
        }
    }
}
