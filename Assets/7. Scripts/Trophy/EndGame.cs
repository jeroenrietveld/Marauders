using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EndGame
{
    private static string _resourcePath = "JSON/Trophy";
    private ICollection<PlayerTest> playerList;
    private List<Trophy> trophyList;

    public EndGame()
    {
        var resources = Resources.LoadAll(_resourcePath);
        this.playerList = new List<PlayerTest>();
        this.trophyList = new List<Trophy>();

        PlayerTest p1 = new PlayerTest();
        PlayerTest p2 = new PlayerTest();
        p1.Deaths = 1;
        p1.Kills = 22;
        p2.Deaths = 18;
        p2.Kills = 3;

        p1.Name = "Ronald";
        p2.Name = "Noob";
        playerList.Add(p1);
        playerList.Add(p2);

        foreach (object resource in resources)
        {
            var node = SimpleJSON.JSON.Parse(((TextAsset)resource).text);
            string column = node["column"].Value;
            string trophy = node["trophy"].Value;
            string title = node["title"].Value;
            string condition = node["condition"].Value;

            Trophy t = new Trophy();
            t.Column = column;
            t.TrophyName = trophy;
            t.Title = title;
            t.Condition = condition;
            
            
            trophyList.Add(t);
        }
    }

    public void Calculate()
    {
        // Dictionary containing the players list as a key and the related Trophy as a value
        Dictionary<List<PlayerTest>, Trophy> trophyDictionary = new Dictionary<List<PlayerTest>, Trophy>();

        // Filling the Dicationary with the trophys
        foreach (Trophy trophy in this.trophyList)
        {
            trophyDictionary.Add(trophy.CalculateTrophy(playerList), trophy);
        }
        
        // Read the dictionary with players and trophies for testing purpose
        foreach(KeyValuePair<List<PlayerTest>, Trophy> trophy in trophyDictionary)
        {
            foreach (PlayerTest p in trophy.Key)
            {
                //Debug.Log("Name: " + p.Name);
                //Debug.Log("Kills: " + p.Kills);
                //Debug.Log("Deaths: " +  p.Deaths);
            }
            
            //Debug.Log("TrophyTitle: " + trophy.Value.Title);
            //Debug.Log("" + trophy.Value.TrophyName);
        }
    }
}
