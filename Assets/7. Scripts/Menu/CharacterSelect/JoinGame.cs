using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class JoinGame : MonoBehaviour 
{
    public List<Material> heroes;

    private int countOne = 0;
    private int countTwo = 0;
    private int countThree = 0;
    private int countFour = 0;

    void Update()
    {
        if (InputWrapper.Instance.GetController(1) != null) 
        {
            if (InputWrapper.Instance.GetController(1).GetButtonADown())
            {
                GameObject aButton = GameObject.Find("a_button_pl1");
                Destroy(aButton);

                GameObject hero = GameObject.Find("hero_select_pl1").transform.GetChild(0).gameObject;

                hero.renderer.enabled = true;
                hero.renderer.material = heroes[countOne];
                countOne++;

                if (countOne >= heroes.Count)
                {
                    countOne = 0;
                }
            }
        }
        if (InputWrapper.Instance.GetController(2) != null) 
        {
            if (InputWrapper.Instance.GetController(2).GetButtonADown())
            {
                GameObject aButton = GameObject.Find("a_button_pl2");
                Destroy(aButton);

                GameObject hero = GameObject.Find("hero_select_pl2").transform.GetChild(0).gameObject;

                hero.renderer.enabled = true;
                hero.renderer.material = heroes[countTwo];
                countTwo++;

                if (countTwo >= heroes.Count)
                {
                    countTwo = 0;
                }
            }
        }
        if (InputWrapper.Instance.GetController(3) != null) 
        {
            if (InputWrapper.Instance.GetController(3).GetButtonADown())
            {
                GameObject aButton = GameObject.Find("a_button_pl3");
                Destroy(aButton);

                GameObject hero = GameObject.Find("hero_select_pl3").transform.GetChild(0).gameObject;

                hero.renderer.enabled = true;
                hero.renderer.material = heroes[countTwo];
                countThree++;

                if (countThree >= heroes.Count)
                {
                    countThree = 0;
                }
            }
        }
        if (InputWrapper.Instance.GetController(4) != null) 
        {
            if (InputWrapper.Instance.GetController(4).GetButtonADown())
            {
                GameObject aButton = GameObject.Find("a_button_pl4");
                Destroy(aButton);

                GameObject hero = GameObject.Find("hero_select_pl4").transform.GetChild(0).gameObject;

                hero.renderer.enabled = true;
                hero.renderer.material = heroes[countTwo];
                countFour++;

                if (countFour >= heroes.Count)
                {
                    countFour = 0;
                }
            }
        }
    }
}
