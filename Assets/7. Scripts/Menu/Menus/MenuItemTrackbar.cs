using UnityEngine;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public delegate void ValueChangedEventHandler(MenuItem sender);

public class MenuItemTrackbar : MenuItem
{
	public Dictionary<GamePad, bool> readInput { get; set; }

	public ValueChangedEventHandler valueChanged;

	/// <summary>
	/// The minimum value
	/// </summary>
	public int min { get; set; }
	
	/// <summary>
	/// The maximum value
	/// </summary>
	public int max { get; set; }
	
	/// <summary>
	/// The step value
	/// </summary>
	public int step { get; set; }
	
	/// <summary>
	/// The current value
	/// </summary>
	public int currentValue { get; set; }
	
	public MenuItemTrackbar()
	{
		this.readInput = new Dictionary<GamePad, bool>();
		
		min = 0;
		max = 10;
		currentValue = 5;
		step = 1;
	}
	
	
	public override void HandleInput(GamePad controller)
	{
		if ((controller.Axis(Axis.LeftHorizontal) >= Menu.axisThreshhold) || controller.Pressed(Button.DPadRight))
		{
			if (readInput[controller])
			{
				//Next item
				currentValue = ((currentValue >= max) ? max : (int)((currentValue + step) / step));
				readInput[controller] = false;

				valueChanged(this);
			}
			
			return;
		} 
		
		if ((controller.Axis(Axis.LeftHorizontal) <= -Menu.axisThreshhold) || controller.Pressed(Button.DPadLeft))
		{
			if (readInput[controller])
			{
				//Next item
				currentValue = ((currentValue <= min) ? min : (int)((currentValue - step) / step));
				readInput[controller] = false;

				valueChanged(this);
			}
			
			return;
		}
		
		readInput[controller] = true;
		
	}
	
	public override void Draw(int yLocation)
	{
		//Drawing the About
		//Getting the label skin
		GUIStyle style = parent.skin.GetStyle("label");

		//Setting the default ocolor
		string colorStr = "";
		Color color;
		
		//Getting the color
		if (this.hasFocus)
		{ 
			colorStr = ToHex(style.focused.textColor);
			color = style.focused.textColor;
		}
		else
		{
			colorStr = ToHex(style.normal.textColor);
			color = style.normal.textColor;
		}

		//Draws a label, style is handled by the skin
		string txt = "<size='" + (int)(30f * parent.scale) + "'><color='" + colorStr + "'>" + this.text + "</color></size>";
		Debug.Log (txt);
		GUI.Label(
			new Rect(
			this.parent.region.width * 0.2f,
			yLocation,
			parent.region.width * 0.6f,
			(this.height * parent.scale) * 0.5f),
			txt,
			style);	

		Texture2D texture = new Texture2D(1, 1);
		texture.SetPixel(0,0, color);
		texture.Apply();


		GUI.Box(new Rect(0, yLocation + ((this.height * parent.scale) * 0.5f), this.parent.region.width, (this.height * parent.scale) * 0.5f), texture);
		
		//Left right indicatorrs
		/*if (this.hasFocus)
		{
			GUI.DrawTexture(
				new Rect(
				this.parent.region.width * 0.08f,
				yLocation + this.height * 0.45f,
				this.parent.region.width * 0.84f,
				this.height * 0.4f),
				parent.skin.box.focused.background,
				ScaleMode.StretchToFill);
			
		}
		
		//Option box
		GUI.DrawTexture(
			new Rect(
			this.parent.region.width * 0.225f,
			yLocation + this.height * 0.45f,
			this.parent.region.width * 0.55f,
			this.height * 0.4f),
			parent.skin.box.normal.background,
			ScaleMode.StretchToFill);*/
		
		
		//Getting the current options
		/*string Option = "";
		if (Options.Count > 0)
		{
			Option = Options[SelectedOptionIndex];
		}

		//
		GUI.Label(
			new Rect(
				this.parent.region.width * 0.25f,
				yLocation + this.height * 0.45f,
				parent.region.width * 0.5f,
				this.height * 0.4f),
			"<color='" + ToHex(style.normal.textColor) + "'>" + Option+ "</color>",
			style);	*/
	}
}

