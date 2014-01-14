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

        // testing data
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
            string title = node["title"].Value;
            string condition = node["condition"].Value;

            Trophy t = new Trophy();
            t.Column = column;
            t.Title = title;
            t.Condition = condition;          
            
            trophyList.Add(t);
        }
    }

    public void Calculate()
    {
        // Dictionary containing the players list as a key and the related Trophy as a value
        Dictionary<Trophy, List<PlayerTest>> trophyDictionary = new Dictionary<Trophy, List<PlayerTest>>();

        // Filling the Dicationary with the trophys
        foreach (Trophy trophy in this.trophyList)
        {
            trophyDictionary.Add(trophy, trophy.CalculateTrophy(playerList));
        }

        // showing the data for testing purposes
        // Multiple players can get the same trophy
        foreach(KeyValuePair<Trophy, List<PlayerTest>> trophy in trophyDictionary)
        {
            Debug.Log("Tropyname :" + trophy.Key.Column);
            Debug.Log("Title: " + trophy.Key.Title);

            foreach (PlayerTest p in trophy.Value)
            {
                Debug.Log("Name: " + p.Name);
                Debug.Log("Deaths: " + p.Deaths);
                Debug.Log("Kills: " + p.Kills);
            }
            Debug.Log("");
        }
    }
}
