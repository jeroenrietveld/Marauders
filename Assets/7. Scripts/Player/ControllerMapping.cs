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

	private Player _player;

	void Start()
	{
		var avatar = GetComponent<Avatar> ();
		controller = avatar.controller;
		_player = avatar.player;

		Event.register<AvatarDeathEvent> (OnAvatarDeath);
	}

	void OnDestroy()
	{
		Event.unregister<AvatarDeathEvent> (OnAvatarDeath);
	}

	void Update () {
		foreach(ActionPair pair in _actions)
		{
			if(controller.JustPressed(pair.button) && pair.action.enabled)
			{
				pair.action.PerformAction();
			}
		}
	}

	public void AddAction(Button button, ActionBase action)
	{
		_actions.Add (new ActionPair (button, action));
	}

	private void OnAvatarDeath(AvatarDeathEvent evt)
	{
		if(evt.victim == _player)
		{
			enabled = false;
		}
	}
}
