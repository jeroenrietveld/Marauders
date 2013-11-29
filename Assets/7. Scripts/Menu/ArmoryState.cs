using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class ArmoryState : MenuStateBase
{
    private bool _characterText = false;

	public ArmoryState()
	{
		center = GameObject.Find ("ArmoryScreen").transform.position;
        
		foreach(GameObject characterSelectBox in GameObject.FindGameObjectsWithTag("CharacterSelect"))
		{

        }
	}
    
	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
			manager.ChangeState(MenuStates.LevelState);
		}
		if (Input.GetKey(KeyCode.B))
		{
			manager.ChangeState(MenuStates.SplashState);
		}

		EnableArmoryStateText();
	}
	
	private void EnableArmoryStateText()
	{
		GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MenuText");
		
		foreach (GameObject go in gameObjects)
		{
			go.renderer.enabled = true;
		}
	}

	public override void OnActive()
	{
        //_characterText = true;
        ArmoryStateText();

		foreach(var block in MonoBehaviour.FindObjectsOfType<CharacterSelectBlock>())
		{
			block.enabled = true;
		}
	}

	public override void OnInactive()
	{
        //_characterText = false;
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
            //go.renderer.enabled = _characterText;
        }
    }
}
