using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class MenuManager : MonoBehaviour {
	
	public MenuCameraMovement cameraMovement;
	public Dictionary<MenuStates, MenuStateBase> states;
	public MenuStateBase currentState;
	
    public static float distanceFromMenu = -30f;

    public GamePad primaryController { get; private set; }
    
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
	
    void FixedUpdate()
    {
    }

	// Update is called once per frame
	void Update ()
	{
		if(!cameraMovement.isMoving)
		{
			currentState.Update(this);
		}
	}

	public void ChangeState(MenuStates state)
	{
    	currentState = states[state];
		cameraMovement.SetTarget(currentState.center + new Vector3(0, 0, distanceFromMenu), 1f);
	}
}
