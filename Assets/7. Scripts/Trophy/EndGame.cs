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

    private PlayerTest p1 = new PlayerTest();
    private PlayerTest p2 = new PlayerTest();

    public EndGame()
    {
        var resources = Resources.LoadAll(_resourcePath);
        this.playerList = new List<PlayerTest>();
        this.trophyList = new List<Trophy>();

        p1.Deaths = 2;
        p1.Kills = 18;
        p1.TimeSync = 97;

        p2.Deaths = 18;
        p2.Kills = 2;
        p2.TimeSync = 47;

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

        // showing the data for testing purposes
        // Multiple players can get the same trophy
        foreach(KeyValuePair<List<PlayerTest>, Trophy> trophy in trophyDictionary)
        {
            
            foreach (PlayerTest p in trophy.Key)
            {
                Debug.Log("Name: " + p.Name);
                Debug.Log("Deaths: " + p.Deaths);
                Debug.Log("Kills: " + p.Kills);
            }
            Debug.Log("Tropyname :" + trophy.Value.TrophyName);
            Debug.Log("Title: " + trophy.Value.Title);
            Debug.Log("");
        }
         
    }
}
