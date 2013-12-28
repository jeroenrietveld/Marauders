using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

public class ControllerMapping : MonoBehaviour
{
	public GamePad controller;

	private struct ActionPair
	{
		public Button button;
		public ActionBase action;

		public ActionPair(Button button, ActionBase action)
		{
			this.button = button;
			this.action = action;
		}
	}
	private List<ActionPair> _actions = new List<ActionPair> ();

	void Start()
	{
		controller = GetComponent<Avatar> ().controller;
	}

	void Update () {
		foreach(ActionPair pair in _actions)
		{
			if(controller.JustPressed(pair.button))
			{
				pair.action.PerformAction();
			}
		}
	}

	public void AddAction(Button button, ActionBase action)
	{
		_actions.Add (new ActionPair (button, action));
	}
}
