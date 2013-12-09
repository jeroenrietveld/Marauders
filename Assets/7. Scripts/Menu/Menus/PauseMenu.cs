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
			menu.region = new Rect(Screen.width / 2, Screen.height / 2, 100, 100);
			menu.visible = true;

            // adding a GUITexture to the menu
            GameObject background = new GameObject("Menubackground");
            background.AddComponent<GUITexture>();
            background.transform.localScale = new Vector3(0.5f, 0.5f, 0.5f);

            background.guiTexture.texture = (Texture)Resources.Load("testMenu");
            background.guiTexture.pixelInset = menu.region;

            Texture texture = (Texture)Resources.Load("testMenu");

            // setting normal and focused style. This can be changed for every menu or menu item.
            GUIStyle normal = new GUIStyle();
            GUIStyle focused = new GUIStyle();
            normal.normal.textColor = Color.yellow;
            normal.alignment = TextAnchor.MiddleCenter;
            
            focused.normal.textColor = Color.blue;
            focused.alignment = TextAnchor.MiddleCenter;
            focused.margin = new RectOffset(11, 22, 33, 44);

            MenuItem item = new MenuItemLabel();
            item.text = "Resume" ;
			item.height = 100;
            item.normalStyle = normal;
            item.normalStyle.padding = new RectOffset(100, 100, 10, 10);

            item.focusedStyle = focused;
           
			((MenuItemLabel)item).XboxPressed += new XboxPressedEventHandler(button_Resume);
			menu.Add (item);
			
			item = new MenuItemLabel();
			//item.text = "Options" ;
            item.normalTexture = (Texture)Resources.Load("testOptions");
			item.height = 100;
			menu.Add (item);
            item.normalStyle = normal;
            item.focusedStyle = focused;
			
			item = new MenuItemLabel();
            item.normalTexture = (Texture)Resources.Load("testQuit");
			item.height = 100;
			menu.Add (item);
            item.normalStyle = normal;
            item.focusedStyle = focused;
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
}
