using UnityEngine;
using System.Collections.Generic;
using System;
using XInputDotNetPure;

public class MenuItemSelection : MenuItem
{
    /// <summary>
    /// The list of options to cicle through
    /// </summary>
    public List<string> Options
    {
		get;
		set;
    }

	/// <summary>
	/// The currently selected option
	/// </summary>
	public int SelectedOptionIndex;

	public Dictionary<GamePad, bool> readInput {get;set;}

	public MenuItemSelection()
	{
		this.Options = new List<string>();
		this.readInput = new Dictionary<GamePad, bool>();
	}


	public override void HandleInput(GamePad controller)
	{
		if ((controller.Axis(Axis.LeftHorizontal) >= Menu.axisThreshhold) || controller.Pressed(Button.DPadRight))
		{
			if (readInput[controller])
			{
				//Next item
				NextItem();
				readInput[controller] = false;
			}

			return;
		} 

		if ((controller.Axis(Axis.LeftHorizontal) <= -Menu.axisThreshhold) || controller.Pressed(Button.DPadLeft))
		{
			if (readInput[controller])
			{
				//Next item
				PreviousItem();
				readInput[controller] = false;
			}
			
			return;
		}

		readInput[controller] = true;

	}

	private void NextItem()
	{
		int i = SelectedOptionIndex + 1;
		
		//Checking bounds
		if (i < 0) { i = 0; }
		if (i >= Options.Count) { i = Options.Count - 1; }
		
		//All should be well and i wil now be the new SelectedOptioNItem
		SelectedOptionIndex = i;
	}

	private void PreviousItem()
	{
		int i = SelectedOptionIndex - 1;

		//Checking bounds
		if (i < 0) { i = 0; }
		if (i >= Options.Count) { i = Options.Count - 1; }
		
		//All should be well and i wil now be the new SelectedOptioNItem
		SelectedOptionIndex = i;
	}
	
	public override void Draw(int yLocation)
	{
		//Drawing the About
		//Getting the label skin
		GUIStyle style = parent.skin.GetStyle("label");

		//Draws a label, style is handled by the skin
		GUI.Label(
			new Rect(
				this.parent.region.width * 0.25f,
				yLocation,
				parent.region.width * 0.5f,
				this.height * 0.4f),
			"<color='" + ToHex(style.focused.textColor) + "'>" + this.text + "</color>",
			style);	

		//Left right indicatorrs
		if (this.hasFocus)
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
			ScaleMode.StretchToFill);


		//Getting the current options
		string Option = "";
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
			style);	
	}
}

