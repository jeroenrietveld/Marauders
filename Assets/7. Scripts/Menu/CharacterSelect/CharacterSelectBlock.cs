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
    private bool isConnected = false;
    private bool selectedCharacter = false;

    private GameObject _bigCharacterSelectPlane;
    private GameObject _smallCharacterSelectPlane;
    private GameObject _skillSelectPlane;
    private GameObject _textJoin;

    private string _bigCharSelect = "BigCharacterSelect";
    private string _skillSelect = "SkillSelect";
    private string _smallCharSelect = "SmallCharacterSelect";

	// Use this for initialization
	void Start () 
	{
        playerIndex = (int)index + 1;
	}

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary>
	void Update () 
	{
		GamePadState _state = GamePad.GetState (index, GamePadDeadZone.IndependentAxes);

        if(!isConnected && _state.IsConnected)
        {
            OnControllerConnect();
        }
        else if(isConnected && _state.IsConnected && _state.Buttons.A == ButtonState.Pressed)
        {
            _bigCharacterSelectPlane.renderer.enabled = true;
            _bigCharacterSelectPlane.renderer.material = heroes[_count];
            _textJoin.GetComponent<TextMesh>().text = "Press A to select";

            selectedCharacter = true;
        }
        else if (isConnected && !_state.IsConnected)
        {
            OnControllerDisConnect();
        }
        else
        {
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
	}

    private void OnControllerDisConnect()
    {
        isConnected = false;
        _textJoin.GetComponent<TextMesh>().text = "Connect Controller";
        _bigCharacterSelectPlane.renderer.enabled = false;
        _count = 0;
    }

    private void OnControllerConnect()
    {
        isConnected = true;
        _skillSelectPlane = transform.FindChild(_skillSelect).gameObject;
        _smallCharacterSelectPlane = transform.FindChild(_smallCharSelect).gameObject;
        _bigCharacterSelectPlane = transform.FindChild(_bigCharSelect).gameObject;
        _textJoin = transform.FindChild("text_select").gameObject;
        _textJoin.GetComponent<TextMesh>().text = "Press A to join";
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