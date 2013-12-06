using System;
using System.Collections.Generic;
using XInputDotNetPure;
using UnityEngine;


public class Menu: MonoBehaviour
{
	public Menu()
	{
		this.controllers = new List<GamePad>();
		this.menuItems = new List<MenuItem>();
		this._startTime = DateTime.Now;
	}

	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Menu"/> is showing.
	/// </summary>
	/// <value><c>true</c> if showing; otherwise, <c>false</c>.</value>
	public bool visible { get;set; }

	/// <summary>
	/// Sets the <see cref="Menu"/> with the specified i.
	/// </summary>
	/// <param name="i">The menuItem's index</param>
	public MenuItem this[int i]
	{
		get
		{
			return menuItems[i];
		}
		set
		{
			menuItems[i] = value;
			value.parent = this;
		}
	}

    /// <summary>
    /// A list of menu items
    /// </summary>
	private List<MenuItem> menuItems;

    /// <summary>
    /// The currently selected item
    /// </summary>
    public MenuItem focusedItem
    {
        get
        {
			return _focusedItem;
        }
        set
        {
			_focusedItem = value;
        }
    }
	private MenuItem _focusedItem;

    /// <summary>
    /// Count of menuItems
    /// </summary>
    public int count
    {
        get
        {
			return menuItems.Count;
        }
    }

	/// <summary>
	/// The axis threshhold.
	/// </summary>
	public static float axisThreshhold 
	{
		get
		{
			return _axisThreshold;
		}
		set
		{
			_axisThreshold = value;
		}
	}
	private static float _axisThreshold = 0.75f; 

	/// <summary>
	/// Gets or sets the region of the menu
	/// </summary>
	public Rect region { get; set; }

    /// <summary>
    /// Adds an item to the menu
    /// </summary>
    public void Add(MenuItem item)
    {
		item.parent = this;

		if (this.focusedItem == null)
		{
			this.focusedItem = item;
		}

		menuItems.Add (item);


    }

    /// <summary>
    /// Removes an item
    /// </summary>
    public void Remove(MenuItem item)
    {
		menuItems.Remove(item);
    }

    /// <summary>
    /// Removes an item at a specific index
    /// </summary>
    public void RemoteAt(int itemIndex)
    {
		menuItems.RemoveAt (itemIndex);
    }

    /// <summary>
    /// Removes all items
    /// </summary>
    public void Clear()
    {
		menuItems.Clear ();
    }

    /// <summary>
    /// Inserts a menu item
    /// </summary>
    /// <param name="Index">The index to nsert the menu item at</param>
    /// <param name="Item">The item to insert</param>
    public void InsertAt(int index, MenuItem item)
    {
		menuItems.Insert(index, item);
    }

	/// <summary>
	/// Tthe start time of the 'timer'  for axis contro
	/// </summary>
	private DateTime _startTime;

	/// <summary>
	/// Resets the timer Axis controls
	/// </summary>
	public void ResetTime()
	{
		_startTime = DateTime.Now;
	}
	
	/// <summary>
	/// We need to use this method when the game is paused because
	/// most unity methods are not working after setting
	/// timeScale to 0.f.
	/// </summary>
	public float ElapsedSeconds()
	{
		TimeSpan span = DateTime.Now.Subtract(_startTime);
		
		return ((float)span.Ticks / (float)TimeSpan.TicksPerSecond);
	}

	/// <summary>
	/// A list of controllers that can navigate this menu.
	/// </summary>
	/// <value>The controllers.</value>
	public List<GamePad> controllers {get;set;}
	public Dictionary<GamePad, bool> canPress {get;set;}

	private void Update()
	{
        if (visible)
        {
            if (controllers != null)
            {
                //Keeping track of the controllers
                if (canPress == null) { canPress = new Dictionary<GamePad, bool>(); }

                //Looping each controller
                foreach (GamePad controller in controllers)
                {

                    if ((controller.Axis(Axis.LeftVertical) <= -axisThreshhold) || controller.Pressed(Button.DPadDown))
                    {
                        if (canPress[controller])
                        {
                            canPress[controller] = false;
                            NextItem();
                        }

                        return;
                    }

                    if ((controller.Axis(Axis.LeftVertical) >= axisThreshhold) || controller.Pressed(Button.DPadUp))
                    {
                        if (canPress[controller])
                        {
                            canPress[controller] = false;
                            PreviousItem();
                        }

                        return;
                    }

                    canPress[controller] = true;

                    if (focusedItem != null)
                    {
                        focusedItem.HandleInput(controller);
                    }
                }
            }
        }
        else 
        {
            Destroy(GameObject.Find("Menubackground"));
            Destroy(this);
        }
	}
	
	public void OnGUI()
	{
		if (visible)
		{
			int offset = 0;
			foreach (MenuItem item in menuItems)
			{
				item.Draw (offset);
				offset += item.height;
			}
		}
	}

	/// <summary>
	/// Focusses the Nexts item
	/// </summary>
	public void NextItem()
	{
		if (count == 0)
		{
			return;
		}

		//Gets the focused item to do some checks
		int focusedItemIndex = menuItems.IndexOf(focusedItem) + 1;

		//Checks if below range
		if (focusedItemIndex  < 0)
		{
			focusedItemIndex = 0;
		}

		//Checks if above range
		if (focusedItemIndex >= this.count)
		{
			focusedItemIndex = this.count-1 ;
		}

		//Changing focused item
		this.focusedItem = this[focusedItemIndex];
	}

	/// <summary>
	/// Focusses the Previous item.
	/// </summary>
	public void PreviousItem()
	{
		if (count == 0)
		{
			return;
		}
		
		//Gets the focused item to do some checks
		int focusedItemIndex = menuItems.IndexOf (focusedItem) - 1;

		//Checks if below range
		if (focusedItemIndex  < 0)
		{
			focusedItemIndex = 0;
		}
		
		//Checks if above range
		if (focusedItemIndex >= this.count)
		{
			focusedItemIndex = this.count - 1 ;
		}
		
		//Changing focused item
		this.focusedItem = this[focusedItemIndex];
	}
}