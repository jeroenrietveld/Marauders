using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
	
	public GameObject camera;
	public Dictionary<MenuStates, IState> states;
	public IState currentState;

    public static int cameraMenuSpeed = 60;
    public static int distanceFromMenu = 30;
    public static bool isIntroMoving = true;

    public bool isCameraMoving { get; set; }
	
    // Use this for initialization
	void Start ()
	{
		states = new Dictionary<MenuStates, IState>();

		camera = GameObject.FindGameObjectWithTag("MainCamera");

		states.Add(MenuStates.SplashState, new SplashState());
		states.Add(MenuStates.ArmoryState, new ArmoryState());
        states.Add(MenuStates.LevelState, new LevelState());

		currentState = states[MenuStates.SplashState];
        isCameraMoving = true;
	}
	
    void FixedUpdate()
    {
        if(isCameraMoving)
        {
            isCameraMoving = !currentState.CenterCamera();
        }
    }

	// Update is called once per frame
	void Update ()
	{
        if(!isIntroMoving)
        {
            if (Input.GetKeyDown(KeyCode.A))
            {
                changeState(currentState.GetNextState());
            }
            else if (Input.GetKeyDown(KeyCode.B))
            {
                changeState(currentState.GetPreviousState());
            }

            currentState.onInput();
        }
	}

	public void changeState(MenuStates state)
	{
        if (currentState != states[state])
        {
            currentState = states[state];
            isCameraMoving = true;
        }
	}
}
