using UnityEngine;
using System;
using XInputDotNetPure;

public delegate void XboxPressedEventHandler(MenuItem sender, Button button);

public abstract class MenuItem
{
    public Vector2 location
    {
		get; 
		set;
    }

    public string text
    {
		get;
		set;
    }

    public Texture normalTexture
    {
        get;
        set;
    }

    public Texture focusedTexture
    {
        get;
        set;
    }

    public GUITexture normalGUITexture
    {
        get;
        set;
    }

    public GUITexture focusedGUITexture
    {
        get;
        set;
    }

    public GUIStyle normalStyle
    {
		get;
		set;
    }

    public GUIStyle focusedStyle
    {
        get;
        set;
    }

    /// <summary>
    /// Returns true if selected; false otherwise
    /// </summary>
    public bool hasFocus
    {
		get
		{
			if (parent.focusedItem == this)
			{
				return true;
			}
			return false;
		}
    }

    public Menu parent
    {
		get;
		set;
    }

    public int height
    {
        get;
		set;
    }

    public bool isEnabled
    {
		get;
		set;
    }

	public abstract void Draw(int locationY);

	public abstract void HandleInput(GamePad controller);
}
