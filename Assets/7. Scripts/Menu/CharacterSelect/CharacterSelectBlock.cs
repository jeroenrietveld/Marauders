using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;


public class CharacterSelectBlock : MonoBehaviour {

	public PlayerIndex index;
    public List<Material> heroes;

    private IList<string> _heroSelects;
    private int _count = 0;
    private float _defaultTimeValue = 0.15f;
    private float _timer = 0;
    private float deadZone = -0.7f;
    private GamePadState _state;
    private GameObject _hero;

	// Use this for initialization
	void Start () 
	{
        _heroSelects = new List<string>();
        _heroSelects.Add("hero_select_pl1");
        _heroSelects.Add("hero_select_pl2");
        _heroSelects.Add("hero_select_pl3");
        _heroSelects.Add("hero_select_pl4");

        _hero = GameObject.Find(_heroSelects[(int)index]).transform.GetChild(0).gameObject;
        
        if (_state.IsConnected)
        {
            _hero.renderer.enabled = true;
            _hero.renderer.material = heroes[_count];
        }
	}

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary>
	void Update () 
	{
		GamePadState _state = GamePad.GetState (index);

        float x = _state.ThumbSticks.Left.X;
        if ((x < deadZone || x > Mathf.Abs(deadZone)) && GetTimer())
        {
            if (x > Mathf.Abs(deadZone))
            {
                _count++;
            }
            else if (x < deadZone)
            {
                _count--;
            }

            if (_count >= heroes.Count)
            {
                _count = 0;
            }
            else if (_count < 0)
            {
                _count = heroes.Count - 1;
            }
            _hero.renderer.material = heroes[_count];
        }
	}

    /// <summary>
    /// Runs a timer and returns true wether the user can select the next character.
    /// </summary>
    /// <returns></returns>
    private bool GetTimer()
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f)
        {
            _timer = _defaultTimeValue;
            return true;
        }
        return false;
    }
}
