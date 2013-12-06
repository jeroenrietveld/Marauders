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
			menu.region = new Rect(Screen.width/2 - 200, Screen.height/2 - 300, 400, 600);
			menu.visible = true;
			
			MenuItem item = new MenuItemLabel();
			item.text = "Resume" ;
			item.height = 100;
			item.XboxPressed += new XboxPressedEventHandler(button_Resume);
			menu.Add (item);
			
			item = new MenuItemLabel();
			item.text = "Options" ;
			item.height = 100;
			menu.Add (item);
			
			item = new MenuItemLabel();
			item.text = "Quit";
			item.height = 100;
			menu.Add (item);
		}

		menu.visible = true;

		return menu;
	}

	private static Menu menu;

	private static void button_Resume(MenuItem sender, Button button)
	{
		GameManager.Instance.ResumeGame();
		Debug.Log ("button_Resume");

		menu.visible = false;
	}
}
