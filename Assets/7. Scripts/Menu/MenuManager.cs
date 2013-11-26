using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using XInputDotNetPure;

public class MenuManager : MonoBehaviour {
	
	public MenuCameraMovement cameraMovement;
	public Dictionary<MenuStates, MenuStateBase> states;
	public MenuStateBase currentState;
	
    public static float distanceFromMenu = -30f;

    // Use this for initialization
	void Start ()
	{
		states = new Dictionary<MenuStates, MenuStateBase>();

		cameraMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuCameraMovement>();

		states.Add(MenuStates.SplashState, new SplashState());
		states.Add(MenuStates.ArmoryState, new ArmoryState());
        states.Add(MenuStates.LevelState, new LevelState());

		ChangeState(MenuStates.SplashState);
	}

	// Update is called once per frame
	void Update ()
	{
		if(!cameraMovement.isMoving)
		{
			currentState.Update(this);

            if (currentState == states[MenuStates.ArmoryState])
            {
                EnableArmoryStateText();
            }
		}
	}	

    private void EnableArmoryStateText()
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag("MenuText");

        foreach (GameObject go in gameObjects)
        {
            go.renderer.enabled = true;
        }
    }

	public void ChangeState(MenuStates state)
	{
		if (currentState != null) 
		{
			currentState.OnInactive();
		}

    	currentState = states[state];
		cameraMovement.targetPosition = currentState.center + new Vector3(0, 0, distanceFromMenu);

		currentState.OnActive();
	}
}
