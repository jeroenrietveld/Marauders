using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class ShrineMessage : Interactable
{
	public override void OnInteract (GameObject obj)
	{
	}	

	public override void ShowMessage ()
	{
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);
		GUI.Label (new Rect (screenPoint.x, (Screen.height - screenPoint.y), 200, 50), Locale.Current["shrine_capture"]);
	}
}
