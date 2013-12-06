using UnityEngine;
using XInputDotNetPure;

public class MenuItemLabel:MenuItem
{
	public event XboxPressedEventHandler XboxPressed;

	public override void Draw (int yLocation)
	{
		//Drawing this menu item
		string t ;

		//Centering the text
		GUIStyle style = new GUIStyle();
		style.alignment = TextAnchor.MiddleCenter;

		//Swithcing to focused style if needed
		if (this.hasFocus)
		{
			style.normal.textColor = Color.white;
		} 
		else 
		{
			style.normal.textColor = Color.black;
		}
		
		GUI.Label (
			new Rect (parent.region.x,
		                 yLocation,
		                 parent.region.width,
		                 this.height),      
			this.text, 
			style);
	}

	public override void HandleInput(GamePad controller)
	{
		if (controller.Pressed(Button.A))
		{
			this.OnXboxPressed(Button.A);
		}
	}

}

