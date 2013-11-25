using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// InputWrapper is a singleton which holds the 4 controllers. The controllers use lazy loading.
/// </summary>
public class InputWrapper : ScriptableObject {

	private static InputWrapper _instance;

	private static Dictionary<int, ControllerMapping> _controllerMappings;

    /// <summary>
    /// Get the InputWrapper singleton instance.
    /// </summary>
	public static InputWrapper Instance
	{
		get
        {
			if(_instance == null)
			{
				_instance = InputWrapper.CreateInstance<InputWrapper>();
				_controllerMappings = new Dictionary<int, ControllerMapping>();
			}
			return _instance;
		}
	}

	/// <summary>
	/// Gets the controller.
	/// </summary>
	/// <returns>The controller.</returns>
	/// <param name="controllerId">Controller identifier. (1-4)</param> 
	public ControllerMapping GetController(int controllerId)
	{
		if(controllerId < 1 || controllerId > 4)
		{
			throw new ArgumentOutOfRangeException("Invalid argument.");
		}

		if(!_controllerMappings.ContainsKey(controllerId))
		{
			ControllerMapping controller = new ControllerMapping(controllerId);
			_controllerMappings[controllerId] = controller;
		}

		return _controllerMappings[controllerId];
	}
}


public enum XboxButton
{
	NumHorizontal,
	NumVertical,
	A,
	B,
	X,
	Y,
	LeftBumper,
	RightBumper,
	Start,
	Back,
	RightStick,
	LeftStick
}

public enum XboxAxis
{
	HorizontalRight,
	VerticalRight,
	HorizontalLeft,
	VerticalLeft,
	LeftTrigger,
	RightTrigger,
}

/// <summary>
/// This class contains all of the strings to get the input from the controllers.
/// Including the down, pressed and up events of buttons.
/// The left and right thumbstick, keypad and left and right triggers use float values. 
/// All of the buttons return booleans.
/// 
/// In the following link there is a picture which shows the buttons and trigger ids:
/// http://wiki.unity3d.com/index.php?title=Xbox360Controller
/// </summary>
public class ControllerMapping
{
	/// <summary>
	/// The default button for picking up a weapon
	/// </summary>
	public XboxButton PickupWeapon = XboxButton.X;

	/// <summary>
	/// The default button for picking up a powerup
	/// </summary>
	public XboxButton PickupPowerup = XboxButton.X;

	/// <summary>
	/// The default button for picking up a gametype object.
	/// </summary>
	public XboxButton PickupGametypeObject = XboxButton.X;

	/// <summary>
	/// The index of the controller (1,2,3,4)
	/// </summary>
	private int _controllerId;

	/// <summary>
	/// A dictionary of xboxAxis and their Names
	/// </summary>
	private Dictionary<XboxAxis, string> AxisNames = new Dictionary<XboxAxis, string>();

	/// <summary>
	/// A dictionary of xboxButtons and their Names
	/// </summary>
	private Dictionary<XboxButton, string> ButtonNames = new Dictionary<XboxButton, string>();

	/// <summary>
	/// A dictionary of xboxButtons and their Images
	/// </summary>
	public static Dictionary<XboxButton, Texture2D> ButtonImages =  new Dictionary<XboxButton, Texture2D>();

	static ControllerMapping()
	{
		foreach (XboxButton button in Enum.GetValues(typeof(XboxButton)))
		{
			if ((button != XboxButton.NumHorizontal) && (button != XboxButton.NumVertical))
			{
				Texture2D resource = (Texture2D)Resources.Load(button.ToString());

				if (resource != null)
				{
					ButtonImages.Add (button, resource);
				}
			}
		}
	}

    /// <summary>
    /// The constructor which creates all of the strings needed to get the input.
    /// </summary>
    /// <param name="ControllerId">The controlllerId corresponds with player one, two, three and four. 
    /// In order to get the correct input from all of the controllers.</param>
	public ControllerMapping(int controllerId)
	{
		this._controllerId = controllerId;

		//Adding axis names
		foreach (XboxAxis axis in Enum.GetValues(typeof(XboxAxis)))
		{
			this.AxisNames.Add(axis, "360_pl" + controllerId + "_" + axis.ToString());
		}

		//Adding button names
		foreach (XboxButton button in Enum.GetValues(typeof(XboxButton)))
		{
			//Adding names
			this.ButtonNames.Add(button, "360_pl" + controllerId + "_" + button.ToString());
		}
	}

	/// <summary>
	/// Returns the float value of an Axis
	/// </summary>
	/// <returns>The axis.</returns>
	/// <param name="axis">Axis.</param>
	public float GetAxis(XboxAxis axis)
	{
		return Input.GetAxis(AxisNames[axis]);
	}

	/// <summary>
	/// Gets wether the button just got pressed
	/// </summary>
	/// <returns><c>true</c>, if button was just pressed, <c>false</c> otherwise.</returns>
	/// <param name="button">Button.</param>
	public bool GetButtonDown(XboxButton button)
	{
		return Input.GetButtonDown(ButtonNames[button]);
	}

	/// <summary>
	/// Gets wether the button is pressed
	/// </summary>
	/// <returns><c>true</c>, if button is pressed, <c>false</c> otherwise.</returns>
	/// <param name="button">Button.</param>
	public bool GetButton(XboxButton button)
	{
		return Input.GetButton(ButtonNames[button]);
	}

	/// <summary>
	/// Gets wether the button just got released
	/// </summary>
	/// <returns><c>true</c>, if button was just released, <c>false</c> otherwise.</returns>
	/// <param name="button">Button.</param>
	public bool GetButtonUp(XboxButton button)
	{
		return Input.GetButtonUp(ButtonNames[button]);
	}
}