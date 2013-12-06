using UnityEngine;
using System;
using XInputDotNetPure;

public delegate void XboxPressedEventHandler(MenuItem sender, Button button);

public abstract class MenuItem
{
	public event XboxPressedEventHandler XboxPressed;

	protected virtual void OnXboxPressed(Button button) 
	{
		if (XboxPressed != null)
		{
			XboxPressed(this, button);
		}
	}

	public MenuItem()
	{
		this.normalStyle = new GUIStyle();
		this.focusedStyle = new GUIStyle();

		this.normalStyle.normal.textColor = Color.black;
		this.focusedStyle.normal.textColor = Color.white;
	}

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
    /// The GUIStyle that is beeing used when not selected
    /// </summary>
    public GUIStyle normalStyle
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
    /// The GUIStyle that is beeing used when selected
    /// </summary>
    public GUIStyle focusedStyle
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

