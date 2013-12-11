using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class CharacterSelect : MonoBehaviour 
{
	public PlayerIndex playerIndex;
	
	private GamePad _controller;

	private List<MonoBehaviour> _steps;
	private int _stepIndex;

	// Use this for initialization
	void Start () {
		_controller = ControllerInput.GetController (playerIndex);
		_stepIndex = 0;
		_steps = new List<MonoBehaviour> ();

		ConnectControllerStep controllerConnect = transform.GetComponentInChildren<ConnectControllerStep> ();
		controllerConnect.controller = _controller;
		controllerConnect.characterSelect = this;
		_steps.Add (controllerConnect);

		CharacterSelectStep characterSelect = transform.GetComponentInChildren<CharacterSelectStep> ();
		characterSelect.controller = _controller;
		characterSelect.characterSelect = this;
		_steps.Add (characterSelect);

		_steps [_stepIndex].enabled = true;
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void NextStep()
	{
		if(_stepIndex + 1 < _steps.Count)
		{
			if(_stepIndex != 0)
			{
				_steps[_stepIndex].enabled = false;
			}

			_stepIndex++;
			_steps[_stepIndex].enabled = true;
		}
	}

	public void PreviousStep()
	{
		if(_stepIndex -1 > _steps.Count)
		{
			_steps[_stepIndex].enabled = false;
			_stepIndex--;
			_steps[_stepIndex].enabled = true;
		}
	}

	public void ControllerDisconnect()
	{
		_steps [_stepIndex].enabled = false;
		_stepIndex = 0;
	}
}
