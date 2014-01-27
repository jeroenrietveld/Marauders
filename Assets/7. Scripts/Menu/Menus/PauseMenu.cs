using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public class PauseMenu : Menu {

	public Menu menu;
	private DateTime lastOpened = DateTime.Now;

	public void BuildMain()
	{
		if (menu != null)
		{
			DestroyMenu();
		}

		//Making menu
		menu = (Menu)gameObject.AddComponent("Menu");
		
		//Calculating the new size of the pause menu
		menu.scale = (float)Screen.height / 768f;
		int menuHeight = (int)Math.Round(menu.scale * 350f);
		int menuWidth = (int)Math.Round(menu.scale * 250f);
		
		//Positioning the menu
		menu.region = new Rect((Screen.width - menuWidth) / 2, (Screen.height - menuHeight) / 2, menuWidth, menuHeight);
		menu.skin = MonoBehaviour.Instantiate(Resources.Load("UI/Skins/PauseMenuSkin")) as GUISkin;
		
		// Adding the items to the menu, resume, options and quit
		MenuItemLabel item = new MenuItemLabel();
		item.height = 115;
		item.text = "Resume";
		((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(Resume);
		menu.Add (item);
		menu.focusedItem = item;
		
		item = new MenuItemLabel();
		item.height = 115;
		item.text = "Options";
		menu.Add (item);
		((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(button_Options);
		
		item = new MenuItemLabel();
		item.height = 115;
		item.text = "Quit";
		menu.Add (item);
		((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(button_Exit);

	}

	public void BuildOption()
	{
		if (menu != null)
		{
			DestroyMenu();
		}

		//Making menu
		menu = (Menu)gameObject.AddComponent("Menu");
		
		//Calculating the new size of the pause menu
		menu.scale = (float)Screen.height / 768f;
		int menuHeight = (int)Math.Round(menu.scale * 350f);
		int menuWidth = (int)Math.Round(menu.scale * 250f);
		
		//Positioning the menu
		menu.region = new Rect((Screen.width - menuWidth) / 2, (Screen.height - menuHeight) / 2, menuWidth, menuHeight);
		menu.skin = MonoBehaviour.Instantiate(Resources.Load("UI/Skins/PauseMenuSkin")) as GUISkin;

		// Adding the items to the menu, resume, options and quit
		MenuItemTrackbar item = new MenuItemTrackbar();
		item.height = 115;
		item.text = "Music";
		menu.Add (item);
		menu.focusedItem = item;
		
		item = new MenuItemTrackbar();
		item.height = 115;
		item.text = "Sound";
		menu.Add (item);
	}
	
	private void Update()
	{
		if (menu == null)
		{
			if ((DateTime.Now - lastOpened).TotalMilliseconds > 100)
			{
                if (!GameManager.isPaused)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (ControllerInput.GetController(i).JustPressed(Button.Start))
                        {
                            BuildMain();
                            menu.controllers.Clear();
                            menu.controllers.Add(ControllerInput.GetController(i));
                            Show();
                            return;
                        }
                    }
                }
			}
		}
	}

	private void DestroyMenu()
	{
		menu.visible = false;
		Component.Destroy(menu);
		menu = null;
	}

	private void Hide()
	{
		GameManager.Instance.ResumeGame();
		DestroyMenu();
		lastOpened = DateTime.Now;
	}
	 
	private void Show()
	{
		GameManager.Instance.PauseGame();
		menu.visible = true;
	}

	private void Resume(MenuItem sender, Button button)
	{
		if (button == Button.A || button == Button.B || button == Button.Start)
		{
			Hide();
		}
	}

	private void button_Options(MenuItem sender, Button button)
	{
		if (button == Button.A)
		{
			//Building options menu
			List<GamePad> c = new List<GamePad>();
			c.AddRange(menu.controllers);
			BuildOption();
			menu.visible = true;
			menu.controllers.AddRange(c);
		}

		if (button == Button.B || button == Button.Start)
		{
			Hide();
		}

	}

    private void button_Exit(MenuItem sender, Button button)
    {
		if (button == Button.A)
		{
            RestartManager.Restart();
			return;
		}

		if (button == Button.B || button == Button.Start)
		{
			this.Hide();
		}
    }
}
