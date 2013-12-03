using UnityEngine;
using System;
using System.Collections;
using XInputDotNetPure;

public class CharacterSelectState : SelectionBase
{
    public CharacterSelectState(CharacterSelectBlock block)
    {
        this.block = block;
    }

    public override void OnUpdate(GamePad controller)
    {
		if (controller.Axis(Axis.LeftHorizantal) != 0 || controller.JustPressed(Button.DPadLeft) || controller.JustPressed(Button.DPadRight))
        {
			int direction = 0;

			if (controller.Axis(Axis.LeftHorizantal) != 0)
			{
				direction = (int) controller.Axis(Axis.LeftHorizantal);
			}
			else
			{
				direction = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadLeft))) + Convert.ToInt32(controller.JustPressed(Button.DPadRight));
			}

			//Selecting new marauder
            block.marauderIndex = GetMarauderIndex(block.marauderIndex, direction, block.marauders.Count);
        }

        if(controller.JustPressed(Button.A))
        {
			block.ChangeState(CharacterSelectBlockStates.SkillSelectState);
        }
        else if (controller.JustPressed(Button.B))
        {
            block.OnLeave("Press Start to join");
        }
    }

    public override void OnActive()
    {
        block.bigCharacterSelectPlane.renderer.enabled = true;
        block.bigCharacterSelectPlane.renderer.material = block.marauders[block.marauderIndex];
        block.textHolder.text = "Press A to select";
    }

    public override void OnInActive()
    {
        block.bigCharacterSelectPlane.renderer.enabled = false;
        block.skillSelectPlane.SetActive(false);
        block.smallCharacterSelectPlane.SetActive(false);
    }
}
