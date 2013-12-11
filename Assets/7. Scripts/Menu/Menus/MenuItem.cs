using UnityEngine;
using System;
using XInputDotNetPure;
using System.Text;
using System.Linq;

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

    public string text
    {
		get;
		set;
    }

    /*public Texture normalTexture
    {
        get;
        set;
    }

    public Texture focusedTexture
    {
        get;
        set;
    }
    
    public ScaleMode textureScaleMode 
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
    }*/

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
   /* public RectOffset padding 
    {
	    get;
	    set;
    }*/

	public abstract void Draw(int locationY);

	public abstract void HandleInput(GamePad controller);


	//Fastest for converting colors to hex
	protected static string[] HexTbl = Enumerable.Range(0, 256).Select(v => v.ToString("X2")).ToArray();

	protected static string ToHex(byte[] array)
	{
		StringBuilder s = new StringBuilder(array.Length*2);
		foreach (var v in array)
			s.Append(HexTbl[v]);
		return s.ToString();
	}

	protected static string ToHex(Color c)
	{

		return "#" + ToHex (new byte[] 
		         {
					(byte)(c.r * 255f),
					(byte)(c.g * 255f),
					(byte)(c.b * 255f),
					(byte)(c.a * 255f)
				});
	}

	
}
