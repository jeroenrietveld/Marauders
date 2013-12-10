using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System.Collections.Generic;

/// <summary>
/// Enum with the three different PlayerSkills.
/// </summary>
public enum PlayerSkills
{
    AttackSkills = 1,
    DefenseSkills = 2,
    UtilitySkills = 3
}

/// <summary>
/// With this class we can create a SkillMenu. THe user can navigate through the skills and select the skill they want.
/// In the Dictionary we keep the three PlayerSkills (attack, defense and utility) and a list with the different skills
/// per PlayerSkill.
/// The string activeSkill is used to know which string in PlayerSkill we have currently selected.
/// currenActiveSkill is so we know what PlayerSkill is active.
/// </summary>
public class SkillMenu
{
    public static Dictionary<PlayerSkills, List<string>> skillList;
    public static PlayerSkills currentActiveSkill = PlayerSkills.AttackSkills;
    public static string activeSkill;

    public static Menu Attach(GameObject gameObject)
    {
        if (menu == null)
        {
            skillList = new Dictionary<PlayerSkills, List<string>>();

            List<string> utilitySkills = new List<string>();
            List<string> defenseSkills = new List<string>();
            List<string> attackSkills = new List<string>();

			utilitySkills.Add ("Utility1");
            utilitySkills.Add ("Utility2");
            utilitySkills.Add ("Utility3");

			defenseSkills.Add ("Defense1");
			defenseSkills.Add ("Defense2");
			defenseSkills.Add ("Defense3");

			attackSkills.Add ("Attack1");
			attackSkills.Add ("Attack2");
			attackSkills.Add ("Attack3");

            skillList.Add(PlayerSkills.DefenseSkills, defenseSkills);
            skillList.Add(PlayerSkills.AttackSkills, attackSkills);
            skillList.Add(PlayerSkills.UtilitySkills, utilitySkills);

            activeSkill = skillList[PlayerSkills.AttackSkills][0];

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
