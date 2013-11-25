using UnityEngine;
using System.Collections;
using System.Collections.Generic;

/// <summary>
/// Lets players join the characterscreen selection so they can select a character.
/// </summary>
public class JoinGame : MonoBehaviour 
{
    public List<Material> heroes;

	private Dictionary<ControllerMapping, string> controllers;
	private int _count = 0;

	// <summary>
	// Place all the controllers with selection box in the controllers Dictionary.
	// This code needs to change a bit when the new input is done.
	// </summary>
	void Awake()
	{
		controllers = new Dictionary<ControllerMapping, string> ();
		controllers.Add (InputWrapper.Instance.GetController (1), "hero_select_pl1");
		controllers.Add (InputWrapper.Instance.GetController (2), "hero_select_pl2");
		controllers.Add (InputWrapper.Instance.GetController (3), "hero_select_pl3");
		controllers.Add (InputWrapper.Instance.GetController (4), "hero_select_pl4");
	}

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary>
    void Update()
    {
        foreach(KeyValuePair<ControllerMapping, string> pair in controllers)
		{
			if(pair.Key.GetButtonDown(XboxButton.A))
			{
				GameObject hero = GameObject.Find (pair.Value).transform.GetChild(0).gameObject;
				hero.renderer.enabled = true;
				hero.renderer.material = heroes[_count];
				_count++;

				if(_count >= heroes.Count)
				{
					_count = 0;
				}
			}
		}
    }
}
