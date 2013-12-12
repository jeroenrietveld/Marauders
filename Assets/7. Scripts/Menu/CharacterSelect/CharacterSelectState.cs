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
            block._currentState = null;
            block.MarauderSelect.SetActive(false);
            block.StartScreen.SetActive(true);
        }
    }

    public override void OnActive()
    {
        block.StartScreen.SetActive(false);
        block.MarauderSelect.SetActive(true);
        block.changeMarauder(0);
    }

    public override void OnInActive()
    {
        block.MarauderSelect.SetActive(false);

        if (block._currentEnum == CharacterSelectBlockStates.CharSelectState)
        {
            block.StartScreen.SetActive(true);
        }
    }
}
