using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharSelectState : CharacterSelectBase
{
    public CharSelectState(CharacterSelectBlock _block)
    {
        this.block = _block;
    }

    public override void OnUpdate(GamePad controller)
    {
        if (controller.Axis(Axis.LeftHorizantal) != 0)
        {
            int temp = block._count;
            block._count = GetID(block._count, controller.Axis(Axis.LeftHorizantal), block.heroes.Count);

            if (temp != block._count)
            {
                block._bigCharacterSelectPlane.renderer.material = block.heroes[block._count];
            }
        }

        if(controller.JustPressed(Button.A))
        {
            // Save hero in gameManager
            block.ChangeState(CharacterSelectBlockStates.SkillSelectState);
        }
        else if (controller.JustPressed(Button.B))
        {
            block.OnLeave("Press Start to join");
        }
    }

    public override void OnActive()
    {
        block._bigCharacterSelectPlane.renderer.enabled = true;
        block._bigCharacterSelectPlane.renderer.material = block.heroes[block._count];
        block._textJoin.SetActive(true);
        block._textJoin.GetComponent<TextMesh>().text = "Press A to select";
    }

    public override void OnInActive()
    {
        block._bigCharacterSelectPlane.renderer.enabled = false;
        block._skillSelectPlane.SetActive(false);
        block._smallCharacterSelectPlane.SetActive(false);
    }

    public override void OnControllerConnect()
    {
        
    }

    public override void OnControllerDisconnect()
    {
        
    }
}
