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
        block.skillSelectPlane.SetActive(true);
        block.smallCharacterSelectPlane.SetActive(true);
		//Show the selected Marauder
        block.smallCharacterSelectPlane.renderer.material = block.marauders[block.marauderIndex];
		block.textHolder = null;
    }

    public override void OnInActive()
    {
        block.skillSelectPlane.SetActive(false);
        block.smallCharacterSelectPlane.SetActive(false);
        block.smallCharacterSelectPlane.renderer.enabled = true;
    }
}
