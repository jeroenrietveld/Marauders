using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using XInputDotNetPure;

/// <summary>
/// Lets players join the characterscreen selection so they can select a character.
/// </summary>
public class JoinGame : MonoBehaviour 
{
    public List<Material> heroes;

    private IList<string> _heroSelects;
    public int _playerId;
	private int _count = 0;
    private float _defaultTimeValue = 0.15f;
    private float _timer = 0;
    private float deadZone = -0.7f;
    //private GamePadState _state;
    private GameObject _hero;

	// <summary>
	// Place all the controllers with selection box in the controllers Dictionary.
	// This code needs to change a bit when the new input is done.
	// </summary>
	void Awake()
	{
        _heroSelects = new List<string>();
        _heroSelects.Add("hero_select_pl1");
        _heroSelects.Add("hero_select_pl2");
        _heroSelects.Add("hero_select_pl3");
        _heroSelects.Add("hero_select_pl4");

        _hero = GameObject.Find(_heroSelects[_playerId - 1]).transform.GetChild(0).gameObject;
        setGamePad();
        if(_state.IsConnected)
        {
            _hero.renderer.enabled = true;
            _hero.renderer.material = heroes[_count];
        }
	}

    private void setGamePad()
    {
        if (_playerId == 1) _state = GamePad.GetState(PlayerIndex.One);
        else if (_playerId == 2) _state = GamePad.GetState(PlayerIndex.Two);
        else if (_playerId == 3) _state = GamePad.GetState(PlayerIndex.Three);
        else if (_playerId == 4) _state = GamePad.GetState(PlayerIndex.Four);
    }

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary>
    void Update()
    {
        setGamePad();
        float x = _state.ThumbSticks.Left.X;
        if ((x < deadZone || x > Mathf.Abs(deadZone)) && GetTimer())
        {
            if (x > Mathf.Abs(deadZone))
            {
                _count++;
            }
            else if(x < deadZone)
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
