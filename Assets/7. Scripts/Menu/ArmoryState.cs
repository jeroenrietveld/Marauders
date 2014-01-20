﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ArmoryState : MenuStateBase
{
    public ArmoryState()
    {
        center = GameObject.Find("ArmoryScreen").transform.position;
        GameObject.Find("TextButtonA").gameObject.GetComponent<TextMesh>().text = locale["text_select"];
        GameObject.Find("TextButtonB").gameObject.GetComponent<TextMesh>().text = locale["text_back"];
        GameObject.Find("MarauderSelectText").gameObject.GetComponent<TextMesh>().text = locale["select_your_marauder"];
    }

    public override void Update(MenuManager manager)
    {
        if (Input.GetKey(KeyCode.A))
        {
            manager.ChangeState(MenuStates.LevelState);
            LevelSelectionManager.currentState = LevelSelectionManager.selectionBlocks[LevelSelectionState.LevelSelection];
        }
    }

    public override void OnActive()
    {
        foreach (var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
        {
            block.enabled = true;
        }
    }

    public override void OnInactive()
    {
        foreach (var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
        {
            block.enabled = false;
        }
    }
}
