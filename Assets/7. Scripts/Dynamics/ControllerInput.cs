using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public enum PlayerIndex
{
	One,
	Two,
	Three,
	Four
}

public class ControllerInput : MonoBehaviour {

	private static GamePad[] controllers;

	static ControllerInput()
	{
		controllers = new GamePad[4];

		foreach(XInputDotNetPure.PlayerIndex index in System.Enum.GetValues(typeof(XInputDotNetPure.PlayerIndex)))
		{
			controllers[(int)index] = new GamePad(index);
		}
	}
		
	void Update () {
		foreach(GamePad controller in controllers)
		{
			controller.Update();
		}
	}

	public static GamePad GetController(PlayerIndex index)
	{
		return GetController ((int)index);
	}

	public static GamePad GetController(int index)
	{
		return controllers [index];
	}
}
