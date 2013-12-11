using System;
using System.Collections.Generic;
using XInputDotNetPure;
using UnityEngine;

public class Menu: MonoBehaviour
{
	/// <summary>
	/// Gets or sets a value indicating whether this <see cref="Menu"/> is showing.
	/// </summary>
	/// <value><c>true</c> if showing; otherwise, <c>false</c>.</value>
	public bool visible { get;set; }

	/// <summary>
	/// Gets or sets the style.
	/// </summary>
	/// <value>The style.</value>
	public GUISkin skin { get;set; }

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
	/// Gets or sets the index of the window.
	/// </summary>
	/// <value>The index of the window.</value>
	public int windowIndex {get;set;}

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
	/// A list of controllers that can navigate this menu.
	/// </summary>
	/// <value>The controllers.</value>
	public List<GamePad> controllers {get;set;}
	public Dictionary<GamePad, bool> readInput {get;set;}

	public Menu()
	{
		this.controllers = new List<GamePad>();
		this.menuItems = new List<MenuItem>();
		this.readInput = new Dictionary<GamePad, bool>(); 
		this.windowIndex = 1;
	}

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
    public void RemoveItem(MenuItem item)
    {
		menuItems.Remove(item);
		item = null;
	}

    /// <summary>
    /// Removes an item at a specific index
    /// </summary>
    public void RemoveAtIndex(int itemIndex)
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
    /// <param name="Index">The index to insert the menu item at</param>
    /// <param name="Item">The item to insert</param>
    public void InsertAtIndex(int index, MenuItem item)
    {
		menuItems.Insert(index, item);
    }

	private void Update()
	{
        if (visible)
        {
            if (controllers != null)
            {
                //Looping each controller
                foreach (GamePad controller in controllers)
                {
                    if ((controller.Axis(Axis.LeftVertical) <= -axisThreshhold) || controller.Pressed(Button.DPadDown))
                    {
                        if (readInput[controller])
                        {
                            readInput[controller] = false;
                            NextItem();
                        }

                        return;
                    }

                    if ((controller.Axis(Axis.LeftVertical) >= axisThreshhold) || controller.Pressed(Button.DPadUp))
                    {
                        if (readInput[controller])
                        {
                            readInput[controller] = false;
                            PreviousItem();
                        }

                        return;
                    }
					
					readInput[controller] = true;
					
					if (focusedItem != null)
					{
						focusedItem.HandleInput(controller);
					}
				}
			}
		}
	}

	public void Remove()
	{
		Destroy(GameObject.Find("Menubackground"));
		Destroy(this);
	}
	
	public void OnGUI()
	{
		GUI.Window(windowIndex, this.region, MakeMenu, GUIContent.none, skin.window);
	}

	private void MakeMenu(int windowID)
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
	/// Focusses the Next item
	/// </summary>
	public void NextItem()
	{
		if (count == 0)
		{
			return;
		}

		//Gets the focused item to do some checks
		int focusedItemIndex = menuItems.IndexOf(focusedItem) + 1;

		//Checking if next item exists and can be selected
		if (focusedItemIndex >= this.count)
		{
			//Its out of range...
			focusedItemIndex = this.count - 1;
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
		int focusedItemIndex = menuItems.IndexOf(focusedItem) - 1;

		//Checking if next item exists and can be selected
		if (focusedItemIndex <= 0)
		{
			//Its out of range...
			focusedItemIndex = 0;
		} 

		//Changing focused item
		this.focusedItem = this[focusedItemIndex];
	}
	
}