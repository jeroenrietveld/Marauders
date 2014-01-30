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
                GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuback", false);
                GameObject.Find("MenuManager").GetComponent<MenuManager>().ChangeState(MenuStates.SplashState);
            }
        }
            
        else if (controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.W))
        {
            block.isJoined = true;
            block.isInSelection = true;
            block.ChangeState(CharacterSelectBlockStates.CharSelectState);
            GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuselect", false);
        }
    }

    public override void OnActive()
    {
    }

    public override void OnInActive()
    {
    }
}
