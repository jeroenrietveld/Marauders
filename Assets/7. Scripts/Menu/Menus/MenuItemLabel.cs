using UnityEngine;
using XInputDotNetPure;

public class MenuItemLabel:MenuItem
{
	public event XboxPressedEventHandler XboxPressed;
    private GUIStyle style = new GUIStyle();

	public override void Draw (int yLocation)
	{
		//Swithcing to focused style if needed
		if (this.hasFocus)
		{
            style = focusedStyle;
		} 
		else 
		{
            style = normalStyle;
		}

        // draw a GUI.Label without a texture
        if (this.normalTexture == null)
        {
            GUI.Label(
                new Rect(parent.region.x,
                    yLocation,
                    parent.region.width,
                    this.height),
                    this.text,
                    style);
        }
        // draw a GUI.Label without a text
        else if (this.text == null)
        {
            GUI.Label(
                new Rect(parent.region.x,
                    yLocation,
                    parent.region.width,
                    this.height),
                    this.normalTexture,
                    style);
        }
        // draw a GUI.Label and GUI texture
        else
        {
            GUI.DrawTexture(new Rect(parent.region.x,
                            yLocation,
                            parent.region.width,
                            this.height), 
                            this.normalTexture);

            GUI.Label(
                    new Rect(parent.region.x,
                        yLocation,
                        parent.region.width,
                        this.height),
                        this.text,
                        style);
        }       
	}

	public override void HandleInput(GamePad controller)
	{
		if (controller.Pressed(Button.A))
		{
			this.OnXboxPressed(Button.A);
		}
	}
}

