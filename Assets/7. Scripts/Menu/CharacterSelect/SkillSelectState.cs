using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;
using XInputDotNetPure;

public class SkillSelection
{
    public string baseCategory { get; set; }
    public string category { get; set; }
    public List<string> skills { get; set; }
    public string active { get; set; }

    public SkillSelection(string _base, string _category, List<string> listSkills)
    {
        baseCategory = _base;
        category = _category;
        skills = listSkills;
        active = listSkills[0];
    }
}

public class SkillSelectState : SelectionBase
{
    private GameObject arrow;
    private GameObject bottom;

    private int currentSkillCategory;
    private Dictionary<int, SkillSelection> list;

    public SkillSelectState(CharacterSelectBlock block)
    {
        this.block = block;
        list = new Dictionary<int, SkillSelection>();
        list.Add(0, new SkillSelection("SkillAttack", "SkillSelectorAttack", new List<string>() {"Attack 1" }));
        list.Add(1, new SkillSelection("SkillDefense", "SkillSelectorDefense", new List<string>() { "Defense 1"}));
        list.Add(2, new SkillSelection("SkillUtility", "SkillSelectorUtility", new List<string>() { "Dash", "Windsweep" }));
        // temp
        currentSkillCategory = 2;
    }

    public override void OnUpdate(GamePad controller)
    {
        float vertical = controller.Axis(Axis.LeftVertical);
        float horizontal = controller.Axis(Axis.LeftHorizontal);
        int dPadVertical = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadUp))) + Convert.ToInt32(controller.JustPressed(Button.DPadDown));
        int dPadHorizontal = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadLeft))) + Convert.ToInt32(controller.JustPressed(Button.DPadRight));

        if(!block.isPlayerReady)
        {
            // temp because attack and defense skills are not available.
            arrow.gameObject.transform.position = bottom.transform.FindChild("SkillUtility").transform.FindChild("SkillSelectorUtility").transform.position;
            if (((vertical > 0.7f || vertical < -0.7f) && GetTimer()) || dPadVertical != 0)
            {
                //int newIndex = cal(vertical, dPadVertical, list.Count, currentSkillCategory, true);
                //currentSkillCategory = newIndex;
                //arrow.gameObject.transform.position = bottom.transform.FindChild(list[currentSkillCategory].baseCategory).transform.FindChild(list[currentSkillCategory].category).transform.position;
            }

            if (((horizontal > 0.7f || horizontal < -0.7f) && GetTimer()) || dPadHorizontal != 0)
            {
                int newIndex = cal(horizontal, dPadHorizontal, list[currentSkillCategory].skills.Count, list[currentSkillCategory].skills.IndexOf(list[currentSkillCategory].active), false);

                list[currentSkillCategory].active = list[currentSkillCategory].skills[newIndex];
                bottom.transform.FindChild(list[currentSkillCategory].baseCategory).transform.FindChild("CurrentSkillText").GetComponent<TextMesh>().text = list[currentSkillCategory].active;
            }
        }

        if(controller.JustPressed(Button.A))
        {
            if(!block.isPlayerReady)
            {
                block.isPlayerReady = true;
                block.SkillSelect.transform.FindChild("Ready").renderer.enabled = true;

                // Add selected marauder and skills to the gamemanager.
                Player playerRef = new Player(block.player);
                playerRef.marauder = block.marauderNames[block.marauderIndex];
				playerRef.skills[(int)SkillType.Offensive] = list[0].active;
				playerRef.skills[(int)SkillType.Defensive] = list[1].active;
				playerRef.skills[(int)SkillType.Utility] = list[2].active;
                GameManager.Instance.AddPlayerRef(playerRef);
            }
            else if(block.player == PlayerIndex.One)
            {
                bool canContinue = true;
                foreach (CharacterSelectBlock item in GameObject.FindObjectsOfType<CharacterSelectBlock>())
                {
                    if (!item.isPlayerReady && item.isJoined)
                    {
                        canContinue = false;
                        break;
                    }
                }

                if (canContinue)
                {
                    GameObject.Find("MenuManager").GetComponent<MenuManager>().ChangeState(MenuStates.LevelState);
                }
            }
        }
        if (controller.JustPressed(Button.B))
        {
            if(block.isPlayerReady)
            {
                block.SkillSelect.transform.FindChild("Ready").renderer.enabled = false;
                block.isPlayerReady = false;
            }
            else
            {
                block.ChangeState(CharacterSelectBlockStates.CharSelectState);
            }
        }
    }

    private int cal(float axis, int dpad, int count, int currentIndex, bool isCategory)
    {
        int dir;
        if (axis == 0)
        {
            dir = dpad;
        }
        else
        {
            if(isCategory)
            {
                dir = axis > 0 ? -1 : 1;
            }
            else
            {
                dir = axis > 0 ? 1 : -1;
            }
        }

        if (currentIndex + dir >= count)
        {
            currentIndex = count - 1;
        }
        else if (currentIndex + dir < 0)
        {
            currentIndex = 0;
        }
        else
        {
            currentIndex += dir;
        }

        return currentIndex;
    }

    public override void OnActive()
    {
        block.SkillSelect.SetActive(true);
        block.SkillSelect.transform.FindChild("Top").transform.FindChild("MarauderModelSkill").renderer.material = block.marauders[block.marauderIndex];
        block.SkillSelect.transform.FindChild("Top").transform.FindChild("MauraderNameSkillText").GetComponent<TextMesh>().text = block.marauderNames[block.marauderIndex];
        bottom = block.SkillSelect.transform.FindChild("Bottom").gameObject;
        arrow = bottom.transform.FindChild("SkillArrows").gameObject;
        arrow.transform.position = bottom.transform.FindChild("SkillAttack").transform.FindChild("SkillSelectorAttack").transform.position;

        foreach (KeyValuePair<int, SkillSelection>  item in list)
        {
            bottom.transform.FindChild(item.Value.baseCategory).transform.FindChild("CurrentSkillText").GetComponent<TextMesh>().text = item.Value.active;                
        }
    }

    public override void OnInActive()
    {
        block.SkillSelect.SetActive(false);
        foreach (KeyValuePair<int, SkillSelection>  item in list)
        {
            item.Value.active = item.Value.skills[0];
        }
        block.isPlayerReady = false;
    }
}