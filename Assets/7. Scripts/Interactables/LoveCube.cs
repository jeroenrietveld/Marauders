﻿using UnityEngine;
using System.Collections;

public class LoveCube : Interactable
{
	private string _message = "Get speed boost!" ;
	private bool _showMessage = false;
	private Vector2 _messageLocation;
	private Player _player;

	public override void OnInteractEnter(Player player)
	{
		//Lets show this message
		this._showMessage = true;
		this._player = player;
	}

	public override void OnInteractLeave(Player player)
	{
		this._showMessage = false;
		this._player = null;
	}

	public void Update()
	{
		//showMea
		if (_showMessage)
		{
			//TODO update controller input
			if (_player.controller.GetButtonDown (XboxButton.X) || Input.GetKeyDown("f"))
			{
				//Applying speedboost
				Speedboost speedboost = _player.gameObject.AddComponent<Speedboost>();
				speedboost.factor = 5f;
				speedboost.duration = 2f;

				//Destroy after pickup
				Destroy(this.gameObject);
			}
		}
	}

	public void OnGUI()
	{
		if (_showMessage)
		{
			//Getting the cube's location on screen and storing it		 
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);	
			GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), ControllerMapping.ButtonImages[_player.controller.PickupPowerup]);
			GUI.Label(new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), Locale.Current["speedboost_pickup"]);
		}
	}
}
