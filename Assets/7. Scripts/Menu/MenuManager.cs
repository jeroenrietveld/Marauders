using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class MenuManager : MonoBehaviour {
	
	public GameObject mainCamera;
	public Dictionary<MenuStates, IState> states;
	public IState currentState;

	// Use this for initialization
	void Start ()
	{
		states = new Dictionary<MenuStates, IState>();

		mainCamera = GameObject.FindGameObjectWithTag("MainCamera");

		states.Add(MenuStates.SplashState, new SplashState());
		states.Add(MenuStates.ArmoryState, new ArmoryState());

		currentState = states[MenuStates.SplashState];
	}
	
	// Update is called once per frame
	void Update ()
	{
		if(Input.GetKey(KeyCode.A)) {
			changeState(MenuStates.ArmoryState);
		}
		currentState.onInput();
	}

	public void changeState(MenuStates state)
	{
		currentState = states[state];
		//camera move
	}
}
