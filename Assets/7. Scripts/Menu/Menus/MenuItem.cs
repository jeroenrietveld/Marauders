using UnityEngine;
using System;
using XInputDotNetPure;

public delegate void XboxPressedEventHandler(MenuItem sender, Button button);

public abstract class MenuItem
{

    /// <summary>
    /// Location of the MenuItem
    /// </summary>
    public Vector2 location
    {
		get; 
		set;
    }

    /// <summary>
    /// The text to draw
    /// </summary>
    public string text
    {
		get;
		set;
    }

    /// <summary>
    /// Sets a texture for this menu item when not focused
    /// </summary>
    public Texture normalTexture
    {
        get;
        set;
    }

    /// <summary>
    /// Sets a texture for this menu item when focused
    /// </summary>
    public Texture focusedTexture
    {
        get;
        set;
    }

    /// <summary>
    /// Sets a normalGUITexture for this menu item when not focused
    /// </summary>
    public GUITexture normalGUITexture
    {
        get;
        set;
    }

    /// <summary>
    /// Sets a focusedGUITexture for this menu item when focused
    /// </summary>
    public GUITexture focusedGUITexture
    {
        get;
        set;
    }

    /// <summary>
    /// The GUIStyle that is beeing used when not focused
    /// </summary>
    public GUIStyle normalStyle
    {
		get;
		set;
    }

    /// <summary>
    /// The GUIStyle that is beeing used when focused
    /// </summary>
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

    /// <summary>
    /// The parent
    /// </summary>
    public Menu parent
    {
		get;
		set;
    }

    /// <summary>
    /// The height of this menuItem
    /// </summary>
    public int height
    {
        get;
		set;
    }

    /// <summary>
    /// Can we be selected
    /// </summary>
    public bool isEnabled
    {
		get;
		set;
    }

	public abstract void Draw(int locationY);

	public abstract void HandleInput(GamePad controller);
}

