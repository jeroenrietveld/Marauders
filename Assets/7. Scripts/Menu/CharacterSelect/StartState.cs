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
            bool check = true;

            foreach (CharacterSelectBlock item in GameObject.FindObjectsOfType<CharacterSelectBlock>())
            {
                if (item.isInSelection)
                {
                    check = false;        
                }
            }

            if (check)
            {
                GameObject.Find("MenuManager").GetComponent<MenuManager>().ChangeState(MenuStates.SplashState);
            }
        }
        else if (controller.JustPressed(Button.A))
        {
            block.isJoined = true;
            block.ChangeState(CharacterSelectBlockStates.CharSelectState);
            block.isInSelection = true;
        }
    }

    public override void OnActive()
    {
    }

    public override void OnInActive()
    {
    }
}
