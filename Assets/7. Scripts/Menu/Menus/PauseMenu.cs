using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;

public class PauseMenu : Menu {
	private void Build()
	{
		if (menu == null)
		{
			//Making menu
			menu = (Menu)gameObject.AddComponent("Menu");
			menu.region = new Rect((Screen.width / 2) - 125, (Screen.height / 2) - 175, 250, 350);
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
			((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(Resume);
			
			item = new MenuItemLabel();
			item.height = 115;
			item.text = "Quit";
			menu.Add (item);
			((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(button_Exit);
		} 
	}
	
	private static Menu menu;

	private static DateTime lastOpened = DateTime.Now;

	private void Update()
	{
		if (menu == null)
		{
			if ((DateTime.Now - lastOpened).TotalMilliseconds > 100)
			{
				for (int i = 0; i < 4; i++)
				{
					if (ControllerInput.GetController(i).JustPressed(Button.Start))
					{
						Debug.Log("Showing pause menu");
						Build();
						menu.controllers.Clear ();
						menu.controllers.Add (ControllerInput.GetController(i));
						Show ();
						return;
					}
				}
			}
		}
	}

	private static void Hide()
	{
		GameManager.Instance.ResumeGame();
		menu.visible = false;
		Destroy (menu);
		menu = null;
		lastOpened = DateTime.Now;
	}
	 
	private static void Show()
	{
		GameManager.Instance.PauseGame();
		menu.visible = true;
	}

	private static void Resume(MenuItem sender, Button button)
	{
		Hide();
	}

    private static void button_Exit(MenuItem sender, Button button)
    {
		if (button == Button.A)
		{
			GameManager.Instance.ResumeGame();
        	Application.LoadLevel(0);
			return;
		}

		Hide ();
    }
}
