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
    public List<string> description { get; set; }
    public string active { get; set; }
    public string activeDescription { get; set; }

    public SkillSelection(string _base, string _category, List<string> skillName, List<string> skillDescription)
    {
        this.baseCategory = _base;
        this.category = _category;
        this.skills = skillName;
        this.active = skillName[0];
        this.description = skillDescription;
        this.activeDescription = skillDescription[0];
    }
}

public class SkillSelectState : SelectionBase
{
    private GameObject arrow;
    private GameObject bottom;
    private Dictionary<int, SkillSelection> list;

    private List<string> offensiveSkills;
    private List<string> defensiveSkills;
    private List<string> offensiveSkillsDescription;
    private List<string> defensiveSkillsDescription;
    private List<Material> xBoxImages = new List<Material>();

    private int currentCategory = 0;

    public SkillSelectState(CharacterSelectBlock block)
    {
        this.block = block;
        list = new Dictionary<int, SkillSelection>();

        AddSkills();

        list.Add(0, new SkillSelection("SkillAttack", "SkillSelectorAttack", offensiveSkills, offensiveSkillsDescription));
        list.Add(1, new SkillSelection("SkillDefense", "SkillSelectorDefense", defensiveSkills, defensiveSkillsDescription));

        // Set the text to the first item
        this.block.gameObject.transform.FindInChildren("SkillDescriptionText").GetComponent<TextMesh>().text = list[0].description[0];
    }


    public override void OnUpdate(GamePad controller)
    {
        float vertical = controller.Axis(Axis.LeftVertical);
        float horizontal = controller.Axis(Axis.LeftHorizontal);
        int dPadVertical = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadUp))) + Convert.ToInt32(controller.JustPressed(Button.DPadDown));
        int dPadHorizontal = (-1 * Convert.ToInt32(controller.JustPressed(Button.DPadLeft))) + Convert.ToInt32(controller.JustPressed(Button.DPadRight));

        if (!block.isPlayerReady)
        {
            if (((horizontal > 0.7f || horizontal < -0.7f) && GetTimer()) || dPadHorizontal != 0)
            {
                int newIndex = cal(horizontal, dPadHorizontal, list[currentCategory].skills.Count, list[currentCategory].skills.IndexOf(list[currentCategory].active), false);

                list[currentCategory].active = list[currentCategory].skills[newIndex];
                list[currentCategory].activeDescription = list[currentCategory].description[newIndex];

                bottom.transform.FindChild(list[currentCategory].baseCategory).transform.FindChild("CurrentSkillText").GetComponent<TextMesh>().text = list[currentCategory].active;
                this.block.gameObject.transform.FindInChildren("SkillDescriptionText").GetComponent<TextMesh>().text = list[currentCategory].activeDescription;
                GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuswitch", false);
            }

            if (currentCategory == 0)
            {
                if (controller.JustPressed(Button.A))
                {
                    currentCategory++;

                    arrow.gameObject.transform.position = bottom.transform.FindChild(list[currentCategory].baseCategory).transform.FindChild(list[currentCategory].category).transform.position;
                    this.block.gameObject.transform.FindInChildren("SkillDescriptionText").GetComponent<TextMesh>().text = list[currentCategory].activeDescription;
                    this.block.gameObject.transform.FindInChildren("SkillButton").gameObject.renderer.material = xBoxImages[currentCategory];
                    GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuselect", false);
                }
                else if (controller.JustPressed(Button.B))
                {
                    if (block.isPlayerReady)
                    {
                        block.SkillSelect.transform.FindChild("Ready").renderer.enabled = false;
                        block.isPlayerReady = false;
                    }
                    else
                    {
                        block.ChangeState(CharacterSelectBlockStates.CharSelectState);
                    }
                    GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuback", false);
                }
            }
            else if (currentCategory == 1)
            {
                if (controller.JustPressed(Button.A))
                {
                    if (!block.isPlayerReady)
                    {
                        block.isPlayerReady = true;
                        block.SkillSelect.transform.FindChild("Ready").renderer.enabled = true;
                        GameManager.Instance.soundInGame.PlaySoundRandom(block.audioSourceArmory, block.marauderNames[block.marauderIndex] + "-selected", true);

                        // Add selected marauder and skills to the gamemanager.
                        Player playerRef = new Player(block.player);
                        playerRef.marauder = block.marauderNames[block.marauderIndex];
                        playerRef.skills[(int)SkillType.Offensive] = list[0].active;
                        playerRef.skills[(int)SkillType.Defensive] = list[1].active;
                        GameManager.Instance.AddPlayerRef(playerRef);
                        GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuselect", false);
                    }

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
                        GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuselect", false);
                    }
                }
                else if (controller.JustPressed(Button.B))
                {
                    currentCategory--;

                    arrow.gameObject.transform.position = bottom.transform.FindChild(list[currentCategory].baseCategory).transform.FindChild(list[currentCategory].category).transform.position;
                    this.block.gameObject.transform.FindInChildren("SkillDescriptionText").GetComponent<TextMesh>().text = list[currentCategory].activeDescription;
                    this.block.gameObject.transform.FindInChildren("SkillButton").gameObject.renderer.material = xBoxImages[currentCategory];
                    this.block.SkillSelect.transform.FindChild("Ready").renderer.enabled = false;
                    this.block.isPlayerReady = false;
                    GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuback", false);
                }
            }
        }
        if(block.isPlayerReady)
        {
            if (controller.JustPressed(Button.B))
            {
                arrow.gameObject.transform.position = bottom.transform.FindChild(list[currentCategory].baseCategory).transform.FindChild(list[currentCategory].category).transform.position;
                this.block.gameObject.transform.FindInChildren("SkillDescriptionText").GetComponent<TextMesh>().text = list[currentCategory].activeDescription;
                this.block.gameObject.transform.FindInChildren("SkillButton").gameObject.renderer.material = xBoxImages[currentCategory];
                this.block.SkillSelect.transform.FindChild("Ready").renderer.enabled = false;
                this.block.isPlayerReady = false;
                GameManager.Instance.soundInGame.PlaySound(block.menuSelectSounds, "menuback", false);
            }
        }
    }

    public void AddSkills()
    {
        offensiveSkills = new List<string>();
        defensiveSkills = new List<string>();
        offensiveSkillsDescription = new List<string>();
        defensiveSkillsDescription = new List<string>();

        Locale locale = new en();
        offensiveSkillsDescription.Add(locale["ability_windsweep"]);
		offensiveSkills.Add("Windsweep");
		
        offensiveSkillsDescription.Add(locale["ability_sunderstrike"]);
		offensiveSkills.Add("Sunderstrike");
        
        offensiveSkillsDescription.Add(locale["ability_obliterate"]);
        offensiveSkills.Add("Obliterate");
        
        defensiveSkillsDescription.Add(locale["ability_destabilize"]);
        defensiveSkills.Add("Destabilize");
        /*
        defensiveSkillsDescription.Add(locale["ability_riposte"]);
        defensiveSkills.Add("Riposte");
         * */
        defensiveSkillsDescription.Add(locale["ability_bulwark"]);
        defensiveSkills.Add("Bulwark");
        
        xBoxImages.Add(Resources.Load<Material>("UI/Buttons/Materials/xButton"));
        xBoxImages.Add(Resources.Load<Material>("UI/Buttons/Materials/yButton"));
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
            if (isCategory)
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
            currentIndex = 0;
        }
        else if (currentIndex + dir < 0)
        {
            currentIndex = count - 1;
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
        block.SkillSelect.transform.FindChild("Top").transform.FindChild("MarauderModelSkill").renderer.material = block.maraudersSmall[block.marauderIndex];
        block.SkillSelect.transform.FindChild("Top").transform.FindChild("MauraderNameSkillText").GetComponent<TextMesh>().text = block.marauderNames[block.marauderIndex];
        bottom = block.SkillSelect.transform.FindChild("Bottom").gameObject;
        arrow = bottom.transform.FindChild("SkillArrows").gameObject;
        arrow.transform.position = bottom.transform.FindChild("SkillAttack").transform.FindChild("SkillSelectorAttack").transform.position;

        foreach (KeyValuePair<int, SkillSelection> item in list)
        {
            bottom.transform.FindChild(item.Value.baseCategory).transform.FindChild("CurrentSkillText").GetComponent<TextMesh>().text = item.Value.active;
        }
    }

    public override void OnInActive()
    {
        block.SkillSelect.SetActive(false);
        foreach (KeyValuePair<int, SkillSelection> item in list)
        {
            item.Value.active = item.Value.skills[0];
        }
        block.isPlayerReady = false;
    }
}