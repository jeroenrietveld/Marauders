using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

public class SkillMenu
{
	public static List<string> utilitySkills;
	public static List<string> defenseSkills;
	public static List<string> attackSkills;

    public static Menu Attach(GameObject gameObject)
    {
        if (menu == null)
        {
			// creating the dictionary with the texts in the skillmenu
			utilitySkills = new List<string>();
			defenseSkills = new List<string>();
			attackSkills = new List<string>();

			utilitySkills.Add ("utility1");
			utilitySkills.Add ("utility2");
			utilitySkills.Add ("utility3");

			defenseSkills.Add ("defense1");
			defenseSkills.Add ("defense2");
			defenseSkills.Add ("defense3");

			attackSkills.Add ("attack1");
			attackSkills.Add ("attack2");
			attackSkills.Add ("attack3");

            // Making menu
            menu = (Menu)gameObject.AddComponent("Menu");
            menu.region = new Rect(Screen.width / 2 - 100, Screen.height / 2, 250, 100);
            menu.visible = true;

            // Adding a GUITexture to the menu
            GameObject background = new GameObject("Menubackground");
            background.transform.localScale = new Vector3(0.25f, 0.5f, 0.5f);
            background.transform.position = new Vector3(0.5f, 0.5f, 0.5f);

            Texture textureSkill = (Texture)Resources.Load("select_06");
            background.AddComponent<GUITexture>();
            background.guiTexture.texture = textureSkill;
            background.guiTexture.pixelInset = new Rect(0, -128, 128, 256);

            // Setting the normal and focused style for selecting items
            GUIStyle normal = new GUIStyle();
            GUIStyle focused = new GUIStyle();

            normal.normal.textColor = Color.black;
            normal.alignment = TextAnchor.MiddleCenter;
            normal.padding = new RectOffset(50, 0, 5, 5);
            focused.normal.textColor = Color.red;
            focused.alignment = TextAnchor.MiddleCenter;
            focused.padding = new RectOffset(50, 0, 5, 5);

            // Text above the skills selection (attack, defense, utility)
            GUIStyle staticText = new GUIStyle();
            staticText.normal.textColor = Color.black;
            staticText.fontSize = 26;
            staticText.alignment = TextAnchor.MiddleCenter;
            staticText.padding = new RectOffset(50, 0, 5, 5);

            // Adding the skill items
            MenuItem item = new MenuItemLabel();
            item.text = "Attack";
            item.height = 575;
            item.normalStyle = staticText;
            item.focusedStyle = staticText;
            item.isEnabled = false;
            menu.Add(item);

            item = new MenuItemLabel();
			item.text = attackSkills[0];
            item.height = -525;
            item.normalStyle = normal;
            item.focusedStyle = focused;
			item.isEnabled = true;
            menu.Add(item);

            item = new MenuItemLabel();
            item.text = "Defense";
            item.height = 600;
            item.normalStyle = staticText;
            item.focusedStyle = staticText;
            item.isEnabled = false;
            menu.Add(item);

            item = new MenuItemLabel();
			item.text = defenseSkills[0];
            item.height = -550;
            item.normalStyle = normal;
            item.focusedStyle = focused;
			item.isEnabled = true;
            menu.Add(item);

            item = new MenuItemLabel();
            item.text = "Utility";
            item.height = 625;
            item.normalStyle = staticText;
            item.focusedStyle = staticText;
            item.isEnabled = false;
            menu.Add(item);

            item = new MenuItemLabel();
			item.text = utilitySkills[0];
            item.height = -550;
            item.normalStyle = normal;
            item.focusedStyle = focused;
			item.isEnabled = true;
            menu.Add(item);
        }

        menu.visible = true;

        return menu;
    }

    private static Menu menu;

    private static void button_Resume(MenuItem sender, Button button)
    {
        //GameManager.Instance.ResumeGame();
        //menu.visible = false;
    }
}
