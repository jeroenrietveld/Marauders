using UnityEngine;
using System.Collections;

public class SplashState : IState
{
    private GameObject introScreen = GameObject.Find("IntroScreen");

	public void onInput()
	{

	}

    public bool CenterCamera()
    {
        if (MenuManager.isIntroMoving)
        {
            if (Mathf.Abs(Camera.main.transform.position.z) > Mathf.Abs(MenuManager.distanceFromMenu))
            {
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, introScreen.renderer.bounds.center, MenuManager.cameraMenuSpeed * Time.deltaTime);
                return false;
            }
            MenuManager.isIntroMoving = false;
            return true;
        }
        else
        {
            if (Mathf.Abs(Camera.main.transform.position.x) != Mathf.Abs(introScreen.renderer.bounds.center.x))
            {
                Vector3 pos = new Vector3(introScreen.renderer.bounds.center.x, introScreen.renderer.bounds.center.y, introScreen.renderer.bounds.center.z - Mathf.Abs(MenuManager.distanceFromMenu));
                Camera.main.transform.position = Vector3.MoveTowards(Camera.main.transform.position, pos, MenuManager.cameraMenuSpeed * Time.deltaTime);
                return false;
            }

            return true;
        }
    }

    public MenuStates GetNextState()
    {
        return MenuStates.ArmoryState;
    }

    public MenuStates GetPreviousState()
    {
        return MenuStates.SplashState;
    }
}
