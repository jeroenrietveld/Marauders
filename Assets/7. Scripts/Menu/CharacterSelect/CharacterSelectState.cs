﻿using UnityEngine;
using System;
using System.Collections.Generic;
using System.Linq;
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
		if (controller.Axis(Axis.LeftHorizontal) != 0 || controller.JustPressed(Button.DPadLeft) || controller.JustPressed(Button.DPadRight))
        {
			int direction = 0;
            bool timer = false;
			if (controller.Axis(Axis.LeftHorizontal) != 0)
			{
				direction = controller.Axis(Axis.LeftHorizontal) > 0 ? 1 : -1;
                timer = true;
            }
			else
			{
				direction = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadLeft))) + Convert.ToInt32(controller.JustPressed(Button.DPadRight));
            }

			//Selecting new marauder
			block.changeMarauder(GetMarauderIndex(block.marauderIndex, direction, block.marauders.Count, timer));
        }

        if(controller.JustPressed(Button.A) || Input.GetKeyDown(KeyCode.W))
        {
			//Disabled due to missing juggernaut sound
            //GameManager.Instance.soundInGame.PlaySound(block.audioSourceArmory, block.marauderNames[block.marauderIndex] + "-selected", true);
            block.ChangeState(CharacterSelectBlockStates.SkillSelectState);
        }
        else if (controller.JustPressed(Button.B))
        {
            block._currentState = null;
            block.ChangeState(CharacterSelectBlockStates.StartState);
            OnInActive();
        }
    }

    public override void OnActive()
    {
        block.StartScreen.SetActive(false);
        block.MarauderSelect.SetActive(true);
        block.changeMarauder(block.marauderIndex);
    }

    public override void OnInActive()
    {
        block.MarauderSelect.SetActive(false);

        if (block._currentEnum == CharacterSelectBlockStates.StartState)
        {
            block.isInSelection = false;
            block.isJoined = false;
            block.StartScreen.SetActive(true);
            block.marauderIndex = 0;
        }
    }
}