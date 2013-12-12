using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class SkillSelectState : SelectionBase
{
    public SkillSelectState(CharacterSelectBlock block)
    {
        this.block = block;
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
        block.SkillSelect.SetActive(true);
    }

    public override void OnInActive()
    {
        block.SkillSelect.SetActive(false);
    }
}
