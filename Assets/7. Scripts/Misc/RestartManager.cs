using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

public class RestartManager : MonoBehaviour
{
    public static bool restarted = false;

    void Update()
    {
        if(GameManager.Instance.gameEnded)
        {
            if (ControllerInput.GetController(GameManager.Instance.playerRefs[0].index).JustPressed(Button.A))
            {
                GameManager.scoreboard.Hide();
                GameManager.scoreboard.Clear();
                Application.LoadLevel("Menu");
                GameManager.Instance.gameEnded = false;
                GameManager.Instance.ResumeGame();
                restarted = true;
            }
        }
    }
}


