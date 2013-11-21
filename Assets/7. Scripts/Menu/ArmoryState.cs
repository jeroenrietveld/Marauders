using UnityEngine;
using System.Collections;

public class ArmoryState : IState
{
    private GameObject characterScreen = GameObject.Find("CharacterSelect");
    
	public void onInput()
	{
		
	}

    public bool CenterCamera()
    {
        if (Mathf.Abs(Camera.main.transform.position.x) != Mathf.Abs(characterScreen.renderer.bounds.center.x))
        {
            Vector3 pos = new Vector3(characterScreen.renderer.bounds.center.x, characterScreen.renderer.bounds.center.y, characterScreen.renderer.bounds.center.z - Mathf.Abs(MenuManager.distanceFromMenu));
            Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, pos, MenuManager.cameraMenuSpeed * Time.deltaTime);
            return false;
        }

        return true;
    }

    public MenuStates GetNextState()
    {
        return MenuStates.LevelState;
    }

    public MenuStates GetPreviousState()
    {
        return MenuStates.SplashState;
    }
}
