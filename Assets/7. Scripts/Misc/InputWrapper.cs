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
	private int _controllerId;
	private string _rightHorizontal;
	private string _rightVertical;
	private string _leftHorizontal;
	private string _leftVertical;
	private string _numHorizontal;
	private string _numVertical;
	private string _buttonLeftStick;
	private string _buttonRightStick;
	private string _buttonStart;
	private string _buttonBack;
	private string _buttonA;
	private string _buttonB;
	private string _buttonX;
	private string _buttonY;
	private string _buttonLeftBumper;
	private string _buttonRightBumper;
	private string _rightTrigger;
	private string _leftTrigger;
	
    /// <summary>
    /// The constructor which creates all of the strings needed to get the input.
    /// </summary>
    /// <param name="ControllerId">The controlllerId corresponds with player one, two, three and four. 
    /// In order to get the correct input from all of the controllers.</param>
	public ControllerMapping(int controllerId)
	{
		this._controllerId = controllerId;
		this._rightHorizontal = "360_pl" + controllerId +"_HorizontalRight";
		this._rightVertical = "360_pl" + controllerId +"_VerticalRight";
		this._leftHorizontal = "360_pl" + controllerId +"_HorizontalLeft";
		this._leftVertical = "360_pl" + controllerId +"_VerticalLeft";
		this._buttonA = "360_pl" + controllerId + "_A";
		this._buttonB = "360_pl" + controllerId + "_B";
		this._buttonX = "360_pl" + controllerId + "_X";
		this._buttonY = "360_pl" + controllerId + "_Y";
		this._buttonStart = "360_pl" + controllerId + "_Start";
		this._buttonBack = "360_pl" + controllerId + "_Back";
		this._numHorizontal = "360_pl" + controllerId +"_NumHorizontal";
		this._numVertical = "360_pl" + controllerId + "_NumVertical";
		this._buttonLeftBumper = "360_pl" + controllerId + "_LeftBumper";
		this._buttonRightBumper = "360_pl" + controllerId + "_RightBumper";
		this._buttonLeftStick = "360_pl" + controllerId + "_LeftStick";
		this._buttonRightStick = "360_pl" + controllerId + "_RightStick";
		this._rightTrigger = "360_pl" + controllerId + "_RightTrigger";
		this._leftTrigger = "360_pl" + controllerId + "_LeftTrigger";
	}
	
    /// <summary>
    /// Returns the right trigger float values.
    /// </summary>
    /// <returns>Float value of the right trigger axis. Range: 0 - 1.</returns>
	public float GetRightTrigger()
	{
		return Input.GetAxis(_rightTrigger);
	}

    /// <summary>
    /// Returns the left trigger float values.
    /// </summary>
    /// <returns>Float value of the left trigger axis. Range: -1 - 0.</returns>
	public float GetLeftTrigger()
	{
		return Input.GetAxis(_leftTrigger);
	}

    /// <summary>
    /// Returns the horizontal value of the numpad.
    /// </summary>
    /// <returns>Returns the horizontal float value of the numpad.</returns>
	public float GetNumHorizontal()
	{
		return Input.GetAxis(_numHorizontal);
	}

    /// <summary>
    /// Returns the vertical value of the numpad.
    /// </summary>
    /// <returns>Returns the vertical float value of the numpad.</returns>
	public float GetNumVertical()
	{
		return Input.GetAxis(_numVertical);
	}

    /// <summary>
    /// Returns the right horizontal thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the right thumbstick horizontal float value.</returns>
	public float GetRightHorizontal()
	{
		return Input.GetAxis(_rightHorizontal);
	}

    /// <summary>
    /// Returns the right vertical thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the right thumbstick vertical float value.</returns>
	public float GetRightVertical()
	{
		return Input.GetAxis(_rightVertical);
	}

    /// <summary>
    /// Returns the left horizontal thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the left thumbstick horizontal float value.</returns>
	public float GetLeftHorizontal()
	{
		return Input.GetAxis(_leftHorizontal);
	}

    /// <summary>
    /// Returns the left vertical thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the left thumbstick vertical float value.</returns>
	public float GetLeftVertical()
	{
		return Input.GetAxis(_leftVertical);
	}

	
    /// <summary>
    /// Returns a boolean whether ButtonA is released.
    /// </summary>
    /// <returns>Boolean when ButtonA is released.</returns>
	public bool GetButtonAUp()
	{
		return Input.GetButtonUp(_buttonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonA is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonA is pressed.</returns>
	public bool GetButtonADown()
	{
		return Input.GetButtonDown(_buttonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonA is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonA is pressed or held down.</returns>
	public bool GetButtonA()
	{
		return Input.GetButton(_buttonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is released.
    /// </summary>
    /// <returns>Boolean when ButtonB is released.</returns>
	public bool GetButtonBUp()
	{
		return Input.GetButtonUp(_buttonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonB is pressed.</returns>
	public bool GetButtonBDown()
	{
		return Input.GetButtonDown(_buttonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonB is pressed or held down.</returns>
	public bool GetButtonB()
	{
		return Input.GetButton(_buttonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is released.
    /// </summary>
    /// <returns>Boolean when ButtonX is released.</returns>
	public bool GetButtonXUp()
	{
		return Input.GetButtonUp(_buttonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonX is pressed.</returns>
	public bool GetButtonXDown()
	{
		return Input.GetButtonDown(_buttonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonX is pressed or held down.</returns>
	public bool GetButtonX()
	{
		return Input.GetButton(_buttonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is released.
    /// </summary>
    /// <returns>Boolean when ButtonY is released.</returns>
	public bool GetButtonYUp()
	{
		return Input.GetButtonUp(_buttonY);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonY is pressed.</returns>
	public bool GetButtonYDown()
	{
		return Input.GetButtonDown(_buttonY);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonY is pressed or held down.</returns>
	public bool GetButtonY()
	{
		return Input.GetButton(_buttonY);
	}

    /// <summary>
    /// Returns a boolean whether the start button is released.
    /// </summary>
    /// <returns>Boolean when start start button is released.</returns>
	public bool GetButtonStartUp()
	{
		return Input.GetButtonUp(_buttonStart);
	}

    /// <summary>
    /// Returns a boolean whether start button is pressed.
    /// </summary>
    /// <returns>Boolean when start button is pressed.</returns>
	public bool GetButtonStartDown()
	{
		return Input.GetButtonDown(_buttonStart);
	}

    /// <summary>
    /// Returns a boolean whether the start button is being pressed.
    /// </summary>
    /// <returns>Boolean when the start button is pressed or held down.</returns>
	public bool GetButtonStart()
	{
		return Input.GetButton(_buttonStart);
	}

    /// <summary>
    /// Returns a boolean whether the back button is released.
    /// </summary>
    /// <returns>Boolean when the back button is released.</returns>
	public bool GetButtonBackUp()
	{
		return Input.GetButtonUp(_buttonBack);
	}

    /// <summary>
    /// Returns a boolean whether back button is pressed.
    /// </summary>
    /// <returns>Boolean when back button is pressed.</returns>
	public bool GetButtonBackDown()
	{
		return Input.GetButtonDown(_buttonBack);
	}

    /// <summary>
    /// Returns a boolean whether back button is being pressed.
    /// </summary>
    /// <returns>Boolean when back button is pressed or held down.</returns>
	public bool GetButtonBack()
	{
		return Input.GetButton(_buttonBack);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper button is released.
    /// </summary>
    /// <returns>Boolean when the left bumper button is released.</returns>
	public bool GetButtonLeftBumperUp()
	{
		return Input.GetButtonUp(_buttonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper button is pressed.
    /// </summary>
    /// <returns>Boolean when the left bumper button is pressed.</returns>
	public bool GetButtonLeftBumperDown()
	{
		return Input.GetButtonDown(_buttonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper is being pressed.
    /// </summary>
    /// <returns>Boolean when the left bumper is pressed or held down.</returns>
	public bool GetButtonLeftBumper()
	{
		return Input.GetButton(_buttonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper button is released.
    /// </summary>
    /// <returns>Boolean when the right bumper button is released.</returns>
	public bool GetButtonRightBumperUp()
	{
		return Input.GetButtonUp(_buttonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper button is pressed.
    /// </summary>
    /// <returns>Boolean when the right bumper button is pressed.</returns>
	public bool GetButtonRightBumperDown()
	{
		return Input.GetButtonDown(_buttonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper is being pressed.
    /// </summary>
    /// <returns>Boolean when the right bumper is pressed or held down.</returns>
	public bool GetButtonRightBumper()
	{
		return Input.GetButton(_buttonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether left thumbstick is released.
    /// </summary>
    /// <returns>Boolean when left thumbstick is released.</returns>
	public bool GetButtonLeftStickUp()
	{
		return Input.GetButtonUp(_buttonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the left thumbstick is pressed.
    /// </summary>
    /// <returns>Boolean when the left thumbstick is pressed.</returns>
	public bool GetButtonLeftStickDown()
	{
		return Input.GetButtonDown(_buttonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the left thumb stick is being pressed.
    /// </summary>
    /// <returns>Boolean when the left thumb stick is pressed or held down.</returns>
	public bool GetButtonLeftStick()
	{
		return Input.GetButton(_buttonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumbstick is released.
    /// </summary>
    /// <returns>Boolean when the right thumbstick is released.</returns>
	public bool GetButtonRightStickUp()
	{
		return Input.GetButtonUp(_buttonRightStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumbstick is pressed.
    /// </summary>
    /// <returns>Boolean when the right thumbstick is pressed.</returns>
	public bool GetButtonRightStickDown()
	{
		return Input.GetButtonDown(_buttonRightStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumb stick is being pressed.
    /// </summary>
    /// <returns>Boolean when the right thumb stick is pressed or held down.</returns>
	public bool GetButtonRightStick()
	{
		return Input.GetButton(_buttonRightStick);
	}	
}