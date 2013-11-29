using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SkillSelectState : CharacterSelectBase
{
    public SkillSelectState(CharacterSelectBlock _block)
    {
        this.block = _block;
    }

    public override void OnUpdate(GamePad controller)
    {
        if (controller.JustPressed(Button.B))
        {
            block.ChangeState(CharacterSelectBlockStates.CharSelectState);
        }
    }

    public override void OnActive()
    {
        block._skillSelectPlane.SetActive(true);
        block._smallCharacterSelectPlane.SetActive(true);
        // veranderen naar het gameobject wat in gamemanager staat
        block._smallCharacterSelectPlane.renderer.material = block.heroes[block._count];
        block._textJoin.SetActive(false);
    }

    public override void OnInActive()
    {
        block._skillSelectPlane.SetActive(false);
        block._smallCharacterSelectPlane.SetActive(false);
        block._smallCharacterSelectPlane.renderer.enabled = true;
    }

    public override void OnControllerConnect()
    {
        
    }

    public override void OnControllerDisconnect()
    {
        
    }
}
