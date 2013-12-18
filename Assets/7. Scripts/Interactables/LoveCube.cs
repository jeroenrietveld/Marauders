using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class LoveCube : Interactable
{
	public override void OnInteract(GameObject obj)
	{
		//Applying speedboost
		Speedboost speedboost = obj.AddComponent<Speedboost>();
		speedboost.factor = 5f;
		speedboost.duration = 2f;

		//Destroy after pickup
		Destroy(this.gameObject);
	}

	public override void ShowMessage(Button interactButton)
	{
		//Getting the cube's location on screen and storing it		 
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);	
		//GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), ControllerMapping.ButtonImages[_player.controller.PickupPowerup]);
		GUI.Label(new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), Locale.Current["speedboost_pickup"]);
	}
}
