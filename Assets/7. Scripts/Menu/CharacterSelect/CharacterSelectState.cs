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
            bool timer = false;
			if (controller.Axis(Axis.LeftHorizantal) != 0)
			{
				direction = controller.Axis(Axis.LeftHorizantal) > 0 ? 1 : -1;
                timer = true;
            }
			else
			{
				direction = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadLeft))) + Convert.ToInt32(controller.JustPressed(Button.DPadRight));
            }

			//Selecting new marauder
			block.changeMarauder(GetMarauderIndex(block.marauderIndex, direction, block.marauders.Count, timer));
        }

        if(controller.JustPressed(Button.A))
        {
			block.ChangeState(CharacterSelectBlockStates.SkillSelectState);
        }
        else if (controller.JustPressed(Button.B))
        {
            block.OnLeave("Press A to join");
        }
    }

    public override void OnActive()
    {
        block.textHolder.gameObject.SetActive(true);
        block.bigCharacterSelectPlane.renderer.enabled = true;
        block.bigCharacterSelectPlane.renderer.material = block.marauders[block.marauderIndex];
        block.textHolder.text = "Press A to select";
    }

    public override void OnInActive()
    {
        block.textHolder.gameObject.SetActive(false);
        block.bigCharacterSelectPlane.renderer.enabled = false;
    }
}
