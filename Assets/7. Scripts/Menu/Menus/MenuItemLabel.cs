using UnityEngine;
using XInputDotNetPure;
using System;
 
public class MenuItemLabel : MenuItem
{
	public event XboxPressedEventHandler xboxPressed;
	
	public override void Draw (int yLocation)
	{
		//Getting the label skin
		GUIStyle style = parent.skin.GetStyle("label");

		//Setting the default ocolor
		string color = "";

		//Getting the color
		if (this.hasFocus)
		{ 
			color = ToHex(style.focused.textColor);
		}
		else
		{
			color = ToHex(style.normal.textColor);
		}

		//Debug.Log(this.parent.scale);
		//Draws a label, style is handled by the skin
		string txt = "<size='"+ (int)( 48f * this.parent.scale ) + "'><color='" + color + "'>" + this.text + "</color></size>";
		GUI.Label(
			new Rect(
				0,
				yLocation,
				parent.region.width,
				(int)Math.Round(this.height * parent.scale)),
				txt,
			style);	
	
	}

	public override void HandleInput(GamePad controller)
	{
		if (controller.JustPressed(Button.Start))
		{
			this.xboxPressed(this, Button.Start);
		}

		if (controller.JustPressed(Button.A))
		{
			this.xboxPressed(this, Button.A);
		}

		if (controller.JustPressed(Button.B))
		{
			this.xboxPressed(this, Button.B);
		}
	}
}