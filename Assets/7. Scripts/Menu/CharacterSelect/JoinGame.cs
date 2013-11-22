using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Lets players join the characterscreen selection so they can select a character.
/// </summary>
public class JoinGame : MonoBehaviour 
{
    public List<Material> heroes;

    private int _countOne = 0;
    private int _countTwo = 0;
    private int _countThree = 0;
    private int _countFour = 0;

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary>
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
                hero.renderer.material = heroes[_countOne];
                _countOne++;

                if (_countOne >= heroes.Count)
                {
                    _countOne = 0;
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
                hero.renderer.material = heroes[_countTwo];
                _countTwo++;

                if (_countTwo >= heroes.Count)
                {
                    _countTwo = 0;
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
				hero.renderer.material = heroes[_countTwo];
                _countThree++;

                if (_countThree >= heroes.Count)
                {
                    _countThree = 0;
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
                hero.renderer.material = heroes[_countTwo];
                _countFour++;

                if (_countFour >= heroes.Count)
                {
                    _countFour = 0;
                }
            }
        }
    }
}