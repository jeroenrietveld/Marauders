using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// InputWrapper is a singleton which holds the 4 controllers. The controllers use lazy loading.
/// </summary>
public class InputWrapper : ScriptableObject {

	private static InputWrapper _Instance;

	private static Dictionary<int, ControllerMapping> controllerMappings;

    /// <summary>
    /// Get the InputWrapper singleton instance.
    /// </summary>
	public static InputWrapper Instance
	{
		get
        {
			if(_Instance == null)
			{
				_Instance = InputWrapper.CreateInstance<InputWrapper>();
				controllerMappings = new Dictionary<int, ControllerMapping>();
			}
			return _Instance;
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

		if(!controllerMappings.ContainsKey(controllerId))
		{
			ControllerMapping controller = new ControllerMapping(controllerId);
			controllerMappings[controllerId] = controller;
		}

		return controllerMappings[controllerId];
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
	private int _ControllerId;
	private string _RightHorizontal;
	private string _RightVertical;
	private string _LeftHorizontal;
	private string _LeftVertical;
	private string _NumHorizontal;
	private string _NumVertical;
	private string _ButtonLeftStick;
	private string _ButtonRightStick;
	private string _ButtonStart;
	private string _ButtonBack;
	private string _ButtonA;
	private string _ButtonB;
	private string _ButtonX;
	private string _ButtonY;
	private string _ButtonLeftBumper;
	private string _ButtonRightBumper;
	private string _RightTrigger;
	private string _LeftTrigger;
	
    /// <summary>
    /// The constructor which creates all of the strings needed to get the input.
    /// </summary>
    /// <param name="ControllerId">The controlllerId corresponds with player one, two, three and four. 
    /// In order to get the correct input from all of the controllers.</param>
	public ControllerMapping(int ControllerId)
	{
		this._ControllerId = ControllerId;
		this._RightHorizontal = "360_pl" + ControllerId +"_HorizontalRight";
		this._RightVertical = "360_pl" + ControllerId +"_VerticalRight";
		this._LeftHorizontal = "360_pl" + ControllerId +"_HorizontalLeft";
		this._LeftVertical = "360_pl" + ControllerId +"_VerticalLeft";
		this._ButtonA = "360_pl" + ControllerId + "_A";
		this._ButtonB = "360_pl" + ControllerId + "_B";
		this._ButtonX = "360_pl" + ControllerId + "_X";
		this._ButtonY = "360_pl" + ControllerId + "_Y";
		this._ButtonStart = "360_pl" + ControllerId + "_Start";
		this._ButtonBack = "360_pl" + ControllerId + "_Back";
		this._NumHorizontal = "360_pl" + ControllerId +"_NumHorizontal";
		this._NumVertical = "360_pl" + ControllerId + "_NumVertical";
		this._ButtonLeftBumper = "360_pl" + ControllerId + "_LeftBumper";
		this._ButtonRightBumper = "360_pl" + ControllerId + "_RightBumper";
		this._ButtonLeftStick = "360_pl" + ControllerId + "_LeftStick";
		this._ButtonRightStick = "360_pl" + ControllerId + "_RightStick";
		this._RightTrigger = "360_pl" + ControllerId + "_RightTrigger";
		this._LeftTrigger = "360_pl" + ControllerId + "_LeftTrigger";
	}
	
    /// <summary>
    /// Returns the right trigger float values.
    /// </summary>
    /// <returns>Float value of the right trigger axis. Range: 0 - 1.</returns>
	public float GetRightTrigger()
	{
		return Input.GetAxis(_RightTrigger);
	}

    /// <summary>
    /// Returns the left trigger float values.
    /// </summary>
    /// <returns>Float value of the left trigger axis. Range: -1 - 0.</returns>
	public float GetLeftTrigger()
	{
		return Input.GetAxis(_LeftTrigger);
	}

    /// <summary>
    /// Returns the horizontal value of the numpad.
    /// </summary>
    /// <returns>Returns the horizontal float value of the numpad.</returns>
	public float GetNumHorizontal()
	{
		return Input.GetAxis(_NumHorizontal);
	}

    /// <summary>
    /// Returns the vertical value of the numpad.
    /// </summary>
    /// <returns>Returns the vertical float value of the numpad.</returns>
	public float GetNumVertical()
	{
		return Input.GetAxis(_NumVertical);
	}

    /// <summary>
    /// Returns the right horizontal thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the right thumbstick horizontal float value.</returns>
	public float GetRightHorizontal()
	{
		return Input.GetAxis(_RightHorizontal);
	}

    /// <summary>
    /// Returns the right vertical thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the right thumbstick vertical float value.</returns>
	public float GetRightVertical()
	{
		return Input.GetAxis(_RightVertical);
	}

    /// <summary>
    /// Returns the left horizontal thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the left thumbstick horizontal float value.</returns>
	public float GetLeftHorizontal()
	{
		return Input.GetAxis(_LeftHorizontal);
	}

    /// <summary>
    /// Returns the left vertical thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the left thumbstick vertical float value.</returns>
	public float GetLeftVertical()
	{
		return Input.GetAxis(_LeftVertical);
	}	
	
    /// <summary>
    /// Returns a boolean whether ButtonA is released.
    /// </summary>
    /// <returns>Boolean when ButtonA is released.</returns>
	public bool GetButtonAUp()
	{
		return Input.GetButtonUp(_ButtonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonA is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonA is pressed.</returns>
	public bool GetButtonADown()
	{
		return Input.GetButtonDown(_ButtonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonA is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonA is pressed or held down.</returns>
	public bool GetButtonA()
	{
		return Input.GetButton(_ButtonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is released.
    /// </summary>
    /// <returns>Boolean when ButtonB is released.</returns>
	public bool GetButtonBUp()
	{
		return Input.GetButtonUp(_ButtonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonB is pressed.</returns>
	public bool GetButtonBDown()
	{
		return Input.GetButtonDown(_ButtonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonB is pressed or held down.</returns>
	public bool GetButtonB()
	{
		return Input.GetButton(_ButtonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is released.
    /// </summary>
    /// <returns>Boolean when ButtonX is released.</returns>
	public bool GetButtonXUp()
	{
		return Input.GetButtonUp(_ButtonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonX is pressed.</returns>
	public bool GetButtonXDown()
	{
		return Input.GetButtonDown(_ButtonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonX is pressed or held down.</returns>
	public bool GetButtonX()
	{
		return Input.GetButton(_ButtonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is released.
    /// </summary>
    /// <returns>Boolean when ButtonY is released.</returns>
	public bool GetButtonYUp()
	{
		return Input.GetButtonUp(_ButtonY);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonY is pressed.</returns>
	public bool GetButtonYDown()
	{
		return Input.GetButtonDown(_ButtonY);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonY is pressed or held down.</returns>
	public bool GetButtonY()
	{
		return Input.GetButton(_ButtonY);
	}

    /// <summary>
    /// Returns a boolean whether the start button is released.
    /// </summary>
    /// <returns>Boolean when start start button is released.</returns>
	public bool GetButtonStartUp()
	{
		return Input.GetButtonUp(_ButtonStart);
	}

    /// <summary>
    /// Returns a boolean whether start button is pressed.
    /// </summary>
    /// <returns>Boolean when start button is pressed.</returns>
	public bool GetButtonStartDown()
	{
		return Input.GetButtonDown(_ButtonStart);
	}

    /// <summary>
    /// Returns a boolean whether the start button is being pressed.
    /// </summary>
    /// <returns>Boolean when the start button is pressed or held down.</returns>
	public bool GetButtonStart()
	{
		return Input.GetButton(_ButtonStart);
	}

    /// <summary>
    /// Returns a boolean whether the back button is released.
    /// </summary>
    /// <returns>Boolean when the back button is released.</returns>
	public bool GetButtonBackUp()
	{
		return Input.GetButtonUp(_ButtonBack);
	}

    /// <summary>
    /// Returns a boolean whether back button is pressed.
    /// </summary>
    /// <returns>Boolean when back button is pressed.</returns>
	public bool GetButtonBackDown()
	{
		return Input.GetButtonDown(_ButtonBack);
	}

    /// <summary>
    /// Returns a boolean whether back button is being pressed.
    /// </summary>
    /// <returns>Boolean when back button is pressed or held down.</returns>
	public bool GetButtonBack()
	{
		return Input.GetButton(_ButtonBack);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper button is released.
    /// </summary>
    /// <returns>Boolean when the left bumper button is released.</returns>
	public bool GetButtonLeftBumperUp()
	{
		return Input.GetButtonUp(_ButtonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper button is pressed.
    /// </summary>
    /// <returns>Boolean when the left bumper button is pressed.</returns>
	public bool GetButtonLeftBumperDown()
	{
		return Input.GetButtonDown(_ButtonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper is being pressed.
    /// </summary>
    /// <returns>Boolean when the left bumper is pressed or held down.</returns>
	public bool GetButtonLeftBumper()
	{
		return Input.GetButton(_ButtonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper button is released.
    /// </summary>
    /// <returns>Boolean when the right bumper button is released.</returns>
	public bool GetButtonRightBumperUp()
	{
		return Input.GetButtonUp(_ButtonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper button is pressed.
    /// </summary>
    /// <returns>Boolean when the right bumper button is pressed.</returns>
	public bool GetButtonRightBumperDown()
	{
		return Input.GetButtonDown(_ButtonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper is being pressed.
    /// </summary>
    /// <returns>Boolean when the right bumper is pressed or held down.</returns>
	public bool GetButtonRightBumper()
	{
		return Input.GetButton(_ButtonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether left thumbstick is released.
    /// </summary>
    /// <returns>Boolean when left thumbstick is released.</returns>
	public bool GetButtonLeftStickUp()
	{
		return Input.GetButtonUp(_ButtonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the left thumbstick is pressed.
    /// </summary>
    /// <returns>Boolean when the left thumbstick is pressed.</returns>
	public bool GetButtonLeftStickDown()
	{
		return Input.GetButtonDown(_ButtonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the left thumb stick is being pressed.
    /// </summary>
    /// <returns>Boolean when the left thumb stick is pressed or held down.</returns>
	public bool GetButtonLeftStick()
	{
		return Input.GetButton(_ButtonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumbstick is released.
    /// </summary>
    /// <returns>Boolean when the right thumbstick is released.</returns>
	public bool GetButtonRightStickUp()
	{
		return Input.GetButtonUp(_ButtonRightStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumbstick is pressed.
    /// </summary>
    /// <returns>Boolean when the right thumbstick is pressed.</returns>
	public bool GetButtonRightStickDown()
	{
		return Input.GetButtonDown(_ButtonRightStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumb stick is being pressed.
    /// </summary>
    /// <returns>Boolean when the right thumb stick is pressed or held down.</returns>
	public bool GetButtonRightStick()
	{
		return Input.GetButton(_ButtonRightStick);
	}	
}