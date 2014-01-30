using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;


public class LoadingState : MenuStateBase
{
    private GamePad _controller;
    private TextMesh _text;
    public LoadingState()
    {
        center = GameObject.Find("LoadingScreen").transform.position;
        Debug.Log("" + center.x + " " + center.y + " " + center.z);
        _text = GameObject.Find("LoadingScreen").transform.FindChild("LoadingScreenText").GetComponent<TextMesh>();

        _controller = ControllerInput.GetController(PlayerIndex.One);
        _controller.deadZone = GamePadDeadZone.IndependentAxes;
    }

    public override void Update(MenuManager manager)
    {
        _controller = GameManager.Instance.playerRefs.OrderBy(x => x.indexInt).First().controller;

        if (_controller.JustPressed(Button.B))
        {
            manager.ChangeState(MenuStates.LevelState);
            LevelSelectionManager.ChangeState(LevelSelectionState.SettingSelection);
        }
        if (_controller.JustPressed(Button.A))
        {
            MonoBehaviour.Destroy(GameObject.Find("LoadingScreen").transform.FindChild("AButton").transform.gameObject);
            _text.text = "Loading...";
            GameManager.Instance.Start();
        }
    }

    public override void OnActive()
    {
    }

    public override void OnInactive()
    {
    }
}

