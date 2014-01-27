using UnityEngine;
using System.Linq;
using System.Collections;
using XInputDotNetPure;

public class StartState : SelectionBase
{
    public StartState(CharacterSelectBlock block)
    {
        this.block = block;
    }

    public override void OnUpdate(GamePad controller)
    {
        if (controller.JustPressed(Button.B) || Input.GetKeyDown(KeyCode.B))
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
            
        else if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.W))
        {
            block.isJoined = true;
            block.isInSelection = true;
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
