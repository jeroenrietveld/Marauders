using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class Normal : GameMode
{
    public Normal() : base(GameModeID.Normal) 
    {
        for (int i = 0; i < GameManager.Instance.playerRefs.Count; i++)
        {
            
        }
        /*
        var resources = Resources.LoadAll("JSON/Trophy/Normal");

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

            //GameManager.scoreboard.trophyList.Add(t);
        }
         */
        //No addition to the list
        //No addition to the gamemode
    }
}
