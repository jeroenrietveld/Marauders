using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class StartState : SelectionBase
{
    private GamePad controllerOne;

    public StartState(CharacterSelectBlock block)
    {
        this.block = block;
        controllerOne = ControllerInput.GetController(PlayerIndex.One);
    }

    public override void OnUpdate(GamePad controller)
    {
        if (controllerOne.JustPressed(Button.B))
        {
            GameObject.Find("MenuManager").GetComponent<MenuManager>().ChangeState(MenuStates.SplashState);
        }
        else if (controller.JustPressed(Button.A))
        {
            block.isJoined = true;
            block.ChangeState(CharacterSelectBlockStates.CharSelectState);
        }
    }

    public override void OnActive()
    {
     
    }

    public override void OnInActive()
    {

    }
}
