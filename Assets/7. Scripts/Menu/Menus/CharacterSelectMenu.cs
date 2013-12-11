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
public class CharacterSelectMenu
{
    public static Menu Attach(GameObject gameObject)
    {
        if (menu == null)
        {
            // Making menu
            menu = (Menu)gameObject.AddComponent("Menu");
			menu.region = new Rect(( Screen.width - 173)/ 3, (Screen.height - 403) / 2 , 173, 403);
			menu.visible = true;
			menu.skin = MonoBehaviour.Instantiate(Resources.Load("UI/Skins/CharacterSelectSkin")) as GUISkin;

			MenuItem item = new MenuItemLabel();
			item.height = 50;
			item.text = "Test not focused";
			menu.Add(item);

			item = new MenuItemLabel();
			item.height = 50;
			item.text = "Test focused";
			menu.Add(item);

			menu.focusedItem = item;
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
