using UnityEngine;
using XInputDotNetPure;
 
public class MenuItemLabel:MenuItem
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

		//Draws a label, style is handled by the skin
		GUI.Label(
			new Rect(
				0,
				yLocation,
				parent.region.width,
				this.height),
			"<color='" + color + "'>" + this.text + "</color>",
			style);	
	
	}

	public override void HandleInput(GamePad controller)
	{
		if (controller.Pressed(Button.A))
		{
			this.xboxPressed(this, Button.A);
		}
	}
}