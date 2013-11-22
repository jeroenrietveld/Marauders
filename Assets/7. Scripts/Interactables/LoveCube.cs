using UnityEngine;
using System.Collections;

public class LoveCube : Interactable
{
	private string message = "Pickup LoveCube" ;
	private bool showMessage = false;
	private Vector2 messageLocation;
	

	public override void OnInteractEnter(Player player)
	{

		//Lets show this message
		this.showMessage = true;
	}

	public override void OnInteractLeave(Player player)
	{
		this.showMessage = false;
	}

	public void OnGUI()
	{
		if (showMessage)
		{
			//Getting the cube's location on screen and storing it		 
			Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);	
			GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), ControllerMapping.ButtonImages[XboxButton.X]);
			GUI.Label (new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), message);
		}
	}
}
