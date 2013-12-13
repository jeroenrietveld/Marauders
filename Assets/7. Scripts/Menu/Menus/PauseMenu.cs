using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class PauseMenu {
	public static Menu Attach(GameObject gameObject)
	{
		if (menu == null)
		{
			//Making menu
			menu = (Menu)gameObject.AddComponent("Menu");
            menu.region = new Rect((Screen.width / 2) - 100, (Screen.height / 2) - 150, 250, 350);
            menu.skin = MonoBehaviour.Instantiate(Resources.Load("UI/Skins/PauseMenuSkin")) as GUISkin;

            // Adding the items to the menu, resume, options and quit
			MenuItemLabel item = new MenuItemLabel();
            item.height = 90;
            item.text = "Resume";
			((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(button_Resume);
			menu.Add (item);
            menu.focusedItem = item;
			
			item = new MenuItemLabel();
            item.height = 90;
			item.text = "Options";
			menu.Add (item);
			
			item = new MenuItemLabel();
            item.height = 90;
            item.text = "Quit";
			menu.Add (item);
            ((MenuItemLabel)item).xboxPressed += new XboxPressedEventHandler(button_Exit);
		}

		menu.visible = true;

		return menu;
	}

	private static Menu menu;

	private static void button_Resume(MenuItem sender, Button button)
	{
		GameManager.Instance.ResumeGame();
		menu.visible = false;
	}

    private static void button_Exit(MenuItem sender, Button button)
    {
        GameManager.Instance.ResumeGame();
        Application.LoadLevel(0);
    }
}
