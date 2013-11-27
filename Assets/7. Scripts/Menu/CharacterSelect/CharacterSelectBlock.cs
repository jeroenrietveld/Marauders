using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;


public class CharacterSelectBlock : MonoBehaviour {

	public PlayerIndex index;
    public List<Material> heroes;
    private int playerIndex;
    private int _count = 0;
    private float _defaultTimeValue = 0.15f;
    private float _timer = 0;

    private GameObject _bigCharacterSelectPlane;
    private GameObject _smallCharacterSelectPlane;
    private GameObject _skillSelectPlane;

    private string _bigCharSelect = "BigCharacterSelect";
    private string _skillSelect = "SkillSelect";
    private string _smallCharSelect = "SmallCharacterSelect";

	// Use this for initialization
	void Start () 
	{
        playerIndex = (int)index + 1;
        if(GamePad.GetState(index).IsConnected)
        {
            _skillSelectPlane = transform.FindChild(_skillSelect + playerIndex).gameObject;
            _smallCharacterSelectPlane = transform.FindChild(_smallCharSelect + playerIndex).gameObject;
            _bigCharacterSelectPlane = transform.FindChild(_bigCharSelect + playerIndex).gameObject;
            _bigCharacterSelectPlane.renderer.enabled = true;
            _bigCharacterSelectPlane.renderer.material = heroes[_count];
        }
	}

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary>
	void Update () 
	{
		GamePadState _state = GamePad.GetState (index, GamePadDeadZone.IndependentAxes);

        if (_state.IsConnected)
        {
            // get child text component and change text from "Connect Controller" to "Press A to join"
        }

        float x = _state.ThumbSticks.Left.X;

        if (x != 0 && GetTimer())
        {
            if (x > 0)
            {
                _count++;
            }
            else if (x < 0)
            {
                _count--;
            }

            _count = (_count + heroes.Count) % heroes.Count;

            _bigCharacterSelectPlane.renderer.material = heroes[_count];
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
