using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    public void Calculate()
    {
        // List holding a list with players who should get a Trophy
        // Two lists because multiple players can get a trophy
        List<List<Player>> playerTrophyList = new List<List<Player>>();

        foreach (Trophy t in GameManager.Instance.trophies)
        {
            playerTrophyList.Add(t.CalculateTrophy(GameManager.Instance.playerRefs));
        }
    }
}