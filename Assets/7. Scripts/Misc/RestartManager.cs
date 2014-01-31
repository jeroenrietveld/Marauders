using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

public class RestartManager : MonoBehaviour
{
    public static bool restarted = false;
    private Timer _timer;

    void Awake()
    {
        DontDestroyOnLoad(this);
        _timer = new Timer(5f);
    }

    void Update()
    {
        _timer.Update();
        if(GameManager.Instance.gameEnded)
        {
            _timer.Start();
            if (ControllerInput.GetController(GameManager.Instance.playerRefs[0].index).JustPressed(Button.A) && !_timer.running)
            {
                Restart();
            }
        }
    }

    public static void Restart()
    {      
        Application.LoadLevel("Menu");
        GameManager.Instance.gameEnded = false;
        GameManager.Instance.playerRefs.Clear();
        GameManager.Instance.ResumeGame();
        restarted = true;
    }
}


