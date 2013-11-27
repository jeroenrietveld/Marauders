using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoryState : MenuStateBase
{
    private bool _characterText = false;

	public ArmoryState()
	{
		center = GameObject.Find("ArmoryScreen").renderer.bounds.center;
        
		foreach(GameObject characterSelectBox in GameObject.FindGameObjectsWithTag("CharacterSelect"))
		{

        }
	}
    
	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
			manager.ChangeState(MenuStates.LevelState);
            LevelSelectionManager.currentState = LevelSelectionState.LevelSelection;
		}
	}

	public override void OnActive()
	{
        _characterText = true;
        ArmoryStateText();

		foreach(var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
		{
			block.enabled = true;
		}
	}

	public override void OnInactive()
	{
        _characterText = false;
        ArmoryStateText();

		foreach(var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
		{
			block.enabled = false;
		}
	}

    private void ArmoryStateText()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MenuText");

        foreach (GameObject go in gameObjects)
        {
            go.renderer.enabled = _characterText;
        }
    }
}
