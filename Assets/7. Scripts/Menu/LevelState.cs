using UnityEngine;
using System.Collections;

public class LevelState : IState
{
    private GameObject LevelScreen = GameObject.Find("LevelSelect");

    public void onInput()
    {
        
    }

    public bool CenterCamera()
    {
        if (Mathf.Abs(Camera.main.transform.position.x) != Mathf.Abs(LevelScreen.renderer.bounds.center.x))
        {
            Vector3 pos = new Vector3(LevelScreen.renderer.bounds.center.x, LevelScreen.renderer.bounds.center.y, LevelScreen.renderer.bounds.center.z - Mathf.Abs(MenuManager.distanceFromMenu));
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
        return MenuStates.ArmoryState;
    }
}
