using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System;

/// <summary>
/// InputWrapper is a singleton which holds the 4 controllers. The controllers use lazy loading.
/// </summary>
public class InputWrapper : ScriptableObject {

	private static InputWrapper instance;

    private static ControllerMapping controllerOne = null;
    private static ControllerMapping controllerTwo = null;
    private static ControllerMapping controllerThree = null;
    private static ControllerMapping controllerFour = null;

    /// <summary>
    /// Get the InputWrapper singleton instance.
    /// </summary>
	public static InputWrapper Instance
	{
		get
        {
			if(instance == null)
			{
				instance = InputWrapper.CreateInstance<InputWrapper>();
			}
			return instance;
		}
	}

    /// <summary>
    /// Returns the controller for player one.
    /// </summary>
    /// <returns>The ControllerMapping object which holds all the methods for controller input.</returns>
    public ControllerMapping GetControllerOne()
    {
        if (controllerOne == null) return (controllerOne = new ControllerMapping(1));
        else return controllerOne;
    }

    /// <summary>
    /// Returns the controller for player two.
    /// </summary>
    /// <returns>The ControllerMapping object which holds all the methods for controller input.</returns>
    public ControllerMapping GetControllerTwo()
    {
        if (controllerTwo == null) return (controllerTwo = new ControllerMapping(2));
        else return controllerTwo;
    }

    /// <summary>
    /// Returns the controller for player three.
    /// </summary>
    /// <returns>The ControllerMapping object which holds all the methods for controller input.</returns>
    public ControllerMapping GetControllerThree()
    {
        if (controllerThree == null) return (controllerThree = new ControllerMapping(3));
        else return controllerThree;
    }

    /// <summary>
    /// Returns the controller for player four.
    /// </summary>
    /// <returns>The ControllerMapping object which holds all the methods for controller input.</returns>
    public ControllerMapping GetControllerFour()
    {
        if (controllerFour == null) return (controllerFour = new ControllerMapping(4));
        else return controllerFour;
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
	private int controllerId;
	private string rightHorizontal;
	private string rightVertical;
	private string leftHorizontal;
	private string leftVertical;
	private string numHorizontal;
	private string numVertical;
	private string buttonLeftStick;
	private string buttonRightStick;
	private string buttonStart;
	private string buttonBack;
	private string buttonA;
	private string buttonB;
	private string buttonX;
	private string buttonY;
	private string buttonLeftBumper;
	private string buttonRightBumper;
	private string rightTrigger;
	private string leftTrigger;
	
    /// <summary>
    /// The constructor which creates all of the strings needed to get the input.
    /// </summary>
    /// <param name="_controllerId">The controlllerId corresponds with player one, two, three and four. 
    /// In order to get the correct input from all of the controllers.</param>
	public ControllerMapping(int _controllerId)
	{
		this.controllerId = _controllerId;
		this.rightHorizontal = "360_pl" + _controllerId +"_HorizontalRight";
		this.rightVertical = "360_pl" + _controllerId +"_VerticalRight";
		this.leftHorizontal = "360_pl" + _controllerId +"_HorizontalLeft";
		this.leftVertical = "360_pl" + _controllerId +"_VerticalLeft";
		this.buttonA = "360_pl" + _controllerId + "_A";
		this.buttonB = "360_pl" + _controllerId + "_B";
		this.buttonX = "360_pl" + _controllerId + "_X";
		this.buttonY = "360_pl" + _controllerId + "_Y";
		this.buttonStart = "360_pl" + _controllerId + "_Start";
		this.buttonBack = "360_pl" + _controllerId + "_Back";
		this.numHorizontal = "360_pl" + _controllerId +"_NumHorizontal";
		this.numVertical = "360_pl" + _controllerId + "_NumVertical";
		this.buttonLeftBumper = "360_pl" + _controllerId + "_LeftBumper";
		this.buttonRightBumper = "360_pl" + _controllerId + "_RightBumper";
		this.buttonLeftStick = "360_pl" + _controllerId + "_LeftStick";
		this.buttonRightStick = "360_pl" + _controllerId + "_RightStick";
		this.rightTrigger = "360_pl" + _controllerId + "_RightTrigger";
		this.leftTrigger = "360_pl" + _controllerId + "_LeftTrigger";
	}
	
    /// <summary>
    /// Returns the right trigger float values.
    /// </summary>
    /// <returns>Float value of the right trigger axis. Range: 0 - 1.</returns>
	public float GetRightTrigger()
	{
		return Input.GetAxis(rightTrigger);
	}

    /// <summary>
    /// Returns the left trigger float values.
    /// </summary>
    /// <returns>Float value of the left trigger axis. Range: -1 - 0.</returns>
	public float GetLeftTrigger()
	{
		return Input.GetAxis(leftTrigger);
	}

    /// <summary>
    /// Returns the horizontal value of the numpad.
    /// </summary>
    /// <returns>Returns the horizontal float value of the numpad.</returns>
	public float GetNumHorizontal()
	{
		return Input.GetAxis(numHorizontal);
	}

    /// <summary>
    /// Returns the vertical value of the numpad.
    /// </summary>
    /// <returns>Returns the vertical float value of the numpad.</returns>
	public float GetNumVertical()
	{
		return Input.GetAxis(numVertical);
	}

    /// <summary>
    /// Returns the right horizontal thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the right thumbstick horizontal float value.</returns>
	public float GetRightHorizontal()
	{
		return Input.GetAxis(rightHorizontal);
	}

    /// <summary>
    /// Returns the right vertical thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the right thumbstick vertical float value.</returns>
	public float GetRightVertical()
	{
		return Input.GetAxis(rightVertical);
	}

    /// <summary>
    /// Returns the left horizontal thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the left thumbstick horizontal float value.</returns>
	public float GetLeftHorizontal()
	{
		return Input.GetAxis(leftHorizontal);
	}

    /// <summary>
    /// Returns the left vertical thumbstick value of the numpad.
    /// </summary>
    /// <returns>Returns the left thumbstick vertical float value.</returns>
	public float GetLeftVertical()
	{
		return Input.GetAxis(leftVertical);
	}	
	
    /// <summary>
    /// Returns a boolean whether ButtonA is released.
    /// </summary>
    /// <returns>Boolean when ButtonA is released.</returns>
	public bool GetButtonAUp()
	{
		return Input.GetButtonUp(buttonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonA is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonA is pressed.</returns>
	public bool GetButtonADown()
	{
		return Input.GetButtonDown(buttonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonA is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonA is pressed or held down.</returns>
	public bool GetButtonA()
	{
		return Input.GetButton(buttonA);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is released.
    /// </summary>
    /// <returns>Boolean when ButtonB is released.</returns>
	public bool GetButtonBUp()
	{
		return Input.GetButtonUp(buttonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonB is pressed.</returns>
	public bool GetButtonBDown()
	{
		return Input.GetButtonDown(buttonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonB is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonB is pressed or held down.</returns>
	public bool GetButtonB()
	{
		return Input.GetButton(buttonB);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is released.
    /// </summary>
    /// <returns>Boolean when ButtonX is released.</returns>
	public bool GetButtonXUp()
	{
		return Input.GetButtonUp(buttonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonX is pressed.</returns>
	public bool GetButtonXDown()
	{
		return Input.GetButtonDown(buttonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonX is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonX is pressed or held down.</returns>
	public bool GetButtonX()
	{
		return Input.GetButton(buttonX);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is released.
    /// </summary>
    /// <returns>Boolean when ButtonY is released.</returns>
	public bool GetButtonYUp()
	{
		return Input.GetButtonUp(buttonY);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is pressed.
    /// </summary>
    /// <returns>Boolean when ButtonY is pressed.</returns>
	public bool GetButtonYDown()
	{
		return Input.GetButtonDown(buttonY);
	}

    /// <summary>
    /// Returns a boolean whether ButtonY is being pressed.
    /// </summary>
    /// <returns>Boolean when ButtonY is pressed or held down.</returns>
	public bool GetButtonY()
	{
		return Input.GetButton(buttonY);
	}

    /// <summary>
    /// Returns a boolean whether the start button is released.
    /// </summary>
    /// <returns>Boolean when start start button is released.</returns>
	public bool GetButtonStartUp()
	{
		return Input.GetButtonUp(buttonStart);
	}

    /// <summary>
    /// Returns a boolean whether start button is pressed.
    /// </summary>
    /// <returns>Boolean when start button is pressed.</returns>
	public bool GetButtonStartDown()
	{
		return Input.GetButtonDown(buttonStart);
	}

    /// <summary>
    /// Returns a boolean whether the start button is being pressed.
    /// </summary>
    /// <returns>Boolean when the start button is pressed or held down.</returns>
	public bool GetButtonStart()
	{
		return Input.GetButton(buttonStart);
	}

    /// <summary>
    /// Returns a boolean whether the back button is released.
    /// </summary>
    /// <returns>Boolean when the back button is released.</returns>
	public bool GetButtonBackUp()
	{
		return Input.GetButtonUp(buttonBack);
	}

    /// <summary>
    /// Returns a boolean whether back button is pressed.
    /// </summary>
    /// <returns>Boolean when back button is pressed.</returns>
	public bool GetButtonBackDown()
	{
		return Input.GetButtonDown(buttonBack);
	}

    /// <summary>
    /// Returns a boolean whether back button is being pressed.
    /// </summary>
    /// <returns>Boolean when back button is pressed or held down.</returns>
	public bool GetButtonBack()
	{
		return Input.GetButton(buttonBack);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper button is released.
    /// </summary>
    /// <returns>Boolean when the left bumper button is released.</returns>
	public bool GetButtonLeftBumperUp()
	{
		return Input.GetButtonUp(buttonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper button is pressed.
    /// </summary>
    /// <returns>Boolean when the left bumper button is pressed.</returns>
	public bool GetButtonLeftBumperDown()
	{
		return Input.GetButtonDown(buttonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the left bumper is being pressed.
    /// </summary>
    /// <returns>Boolean when the left bumper is pressed or held down.</returns>
	public bool GetButtonLeftBumper()
	{
		return Input.GetButton(buttonLeftBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper button is released.
    /// </summary>
    /// <returns>Boolean when the right bumper button is released.</returns>
	public bool GetButtonRightBumperUp()
	{
		return Input.GetButtonUp(buttonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper button is pressed.
    /// </summary>
    /// <returns>Boolean when the right bumper button is pressed.</returns>
	public bool GetButtonRightBumperDown()
	{
		return Input.GetButtonDown(buttonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether the right bumper is being pressed.
    /// </summary>
    /// <returns>Boolean when the right bumper is pressed or held down.</returns>
	public bool GetButtonRightBumper()
	{
		return Input.GetButton(buttonRightBumper);
	}

    /// <summary>
    /// Returns a boolean whether left thumbstick is released.
    /// </summary>
    /// <returns>Boolean when left thumbstick is released.</returns>
	public bool GetButtonLeftStickUp()
	{
		return Input.GetButtonUp(buttonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the left thumbstick is pressed.
    /// </summary>
    /// <returns>Boolean when the left thumbstick is pressed.</returns>
	public bool GetButtonLeftStickDown()
	{
		return Input.GetButtonDown(buttonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the left thumb stick is being pressed.
    /// </summary>
    /// <returns>Boolean when the left thumb stick is pressed or held down.</returns>
	public bool GetButtonLeftStick()
	{
		return Input.GetButton(buttonLeftStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumbstick is released.
    /// </summary>
    /// <returns>Boolean when the right thumbstick is released.</returns>
	public bool GetButtonRightStickUp()
	{
		return Input.GetButtonUp(buttonRightStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumbstick is pressed.
    /// </summary>
    /// <returns>Boolean when the right thumbstick is pressed.</returns>
	public bool GetButtonRightStickDown()
	{
		return Input.GetButtonDown(buttonRightStick);
	}

    /// <summary>
    /// Returns a boolean whether the right thumb stick is being pressed.
    /// </summary>
    /// <returns>Boolean when the right thumb stick is pressed or held down.</returns>
	public bool GetButtonRightStick()
	{
		return Input.GetButton(buttonRightStick);
	}	
}