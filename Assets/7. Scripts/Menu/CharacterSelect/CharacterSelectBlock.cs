using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public class CharacterSelectBlock : MonoBehaviour {

    /*
    * to-do list voor character select:
    * skills inladen en selecteren (misschien moet dat via de json bestanden.)
    * gegevens (gekozen char en skills per speler) opslaan in gamemanager of o.i.d.
    * Refactoren en opschonen van code.
    * mocht het nodig zijn (denk niet) characterselectblock class integreren in ArmoryState.cs 
    */

    public GameObject bigCharacterSelectPlane;
	public TextMesh textHolder;
    public GameObject skillSelectPlane;
    public GameObject smallCharacterSelectPlane;
	
    public PlayerIndex player;
    public List<Material> marauders;
	public int marauderIndex { get; set; }
	public bool isConnected { get; set; }
	private GamePad _controller;

    private SelectionBase _currentState;
    private IDictionary<CharacterSelectBlockStates, SelectionBase> _list;

    void Start()
	{
        marauderIndex = 0;
        _list = new Dictionary<CharacterSelectBlockStates, SelectionBase>();
        _list.Add(CharacterSelectBlockStates.CharSelectState, new CharacterSelectState(this));
        _list.Add(CharacterSelectBlockStates.SkillSelectState, new SkillSelectState(this));

		_currentState = null;
		_controller = ControllerInput.GetController (player);
        _controller.deadZone = GamePadDeadZone.IndependentAxes;

        bigCharacterSelectPlane = transform.FindChild("BigCharacterSelect").gameObject;
        skillSelectPlane = transform.FindChild("SkillSelect").gameObject;
        smallCharacterSelectPlane = transform.FindChild("SmallCharacterSelect").gameObject;
		
		// Change the text of all text properties to the current locale.
        skillSelectPlane.transform.FindChild("text_defense").GetComponent<TextMesh>().text = Locale.Current["text_defense"];
        skillSelectPlane.transform.FindChild("text_offense").GetComponent<TextMesh>().text = Locale.Current["text_offense"];
        skillSelectPlane.transform.FindChild("text_utility").GetComponent<TextMesh>().text = Locale.Current["text_utility"];
        smallCharacterSelectPlane.transform.FindChild("text_select_small").GetComponent<TextMesh>().text = Locale.Current["press_reselect"];
    }

    public void ChangeState(CharacterSelectBlockStates state)
    {
        if(_currentState != null && _currentState != _list[state]) 
		{
			_currentState.OnInActive();
		}

        _currentState = _list[state];
        _currentState.OnActive();
    }

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first marauder will be shown. The List marauders has all the materials for the characters so
    ///  we can switch between them.
    /// </summary> 
	void Update () 
	{
        if (!isConnected && _controller.connected)
        {
			//when a controller was not previously connected, but it is now, call a controller connect function
            OnControllerConnect();
        }
        else if (isConnected && !_controller.connected)
        {
            OnControllerDisConnect();
        }

        if (isConnected && _controller.connected)
        {
            if(_currentState != null)
            {
                _currentState.OnUpdate(_controller);
            }
            else if (_controller.JustPressed(Button.A))
            {
                ChangeState(CharacterSelectBlockStates.CharSelectState);
            }
        }
	}

    private void OnControllerDisConnect()
    {
        isConnected = false;
        OnLeave(Locale.Current["connect_controller"]);
    }

    private void OnControllerConnect()
    {
        isConnected = true;
        textHolder.text = Locale.Current["press_join"];
    }

    public void OnLeave(string s)
    {
        _currentState = null;
        textHolder.text = s;
        bigCharacterSelectPlane.renderer.enabled = false;
        //TODO: Why is the index reset?
		marauderIndex = 0;
    }

	public void changeMarauder(int index)
	{
		marauderIndex = index;
		bigCharacterSelectPlane.renderer.material = marauders[marauderIndex];
	}
}