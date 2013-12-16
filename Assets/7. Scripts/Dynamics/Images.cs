using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;
using System;

public static class Images
{
	private static IDictionary<Button, Texture> btnImages = new Dictionary<Button, Texture>();

	static Images ()
	{
		foreach (Button button in Enum.GetValues(typeof(Button)))
		{
			btnImages.Add(button, (Texture)Resources.Load("UI/Buttons/" + button.ToString(), typeof(Texture2D))); 
		}
	}

	public static Texture Get (Button button)
	{
		return btnImages[button];
	}

}
