using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public class CharacterSelectBlock : MonoBehaviour {

    /*
    * to-do list voor character select:
    * skills inladen en selecteren (misschien moet dat via de json bestanden.)
    * nieuwe input gebruiken (controller.connected geeft alleen false terug?, Maandag even vragen aan Rene / Jeroen).
    * gegevens (gekozen char en skills per speler) opslaan in gamemanager of o.i.d.
    * alle gameobjects in variable zetten die nodig zijn. Zie hieronder (alleen de 3 textboxes volgens mij.)
    * Refactoren en opschonen van code.
    * mocht het nodig zijn (denk niet) characterselectblock class integreren in ArmoryState.cs 
    */

    public GameObject bigCharacterSelectPlane;
	public TextMesh textHolder;
    public GameObject skillSelectPlane;
    public GameObject smallCharacterSelectPlane;
	
    public PlayerIndex player;
    public List<Material> marauders;
    public int marauderIndex 
	{ 
		get
		{
			return marauderIndex;
		}
		set
		{
			marauderIndex = value;
			bigCharacterSelectPlane.renderer.material = marauders[marauderIndex];
		}
	}
	public bool isConnected { get; set; }
	private GamePad _controller;

    private SelectionBase _currentState;
    private IDictionary<CharacterSelectBlockStates, SelectionBase> list;

    void Start()
	{
        marauderIndex = 0;
        list = new Dictionary<CharacterSelectBlockStates, SelectionBase>();
        list.Add(CharacterSelectBlockStates.CharSelectState, new CharacterSelectState(this));
        list.Add(CharacterSelectBlockStates.SkillSelectState, new SkillSelectState(this));

		_currentState = null;
		_controller = ControllerInput.GetController (player);

        bigCharacterSelectPlane = transform.FindChild("BigCharacterSelect").gameObject;
		textHolder = bigCharacterSelectPlane.transform.FindChild("text_select").gameObject.GetComponent<TextMesh>();
        skillSelectPlane = transform.FindChild("SkillSelect").gameObject;
        smallCharacterSelectPlane = transform.FindChild("SmallCharacterSelect").gameObject;
    }

    public void ChangeState(CharacterSelectBlockStates state)
    {
        if(_currentState != null) 
		{
			_currentState.OnInActive();
		}

        _currentState = list[state];
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
        OnLeave("Connect Controller");
    }

    private void OnControllerConnect()
    {
        isConnected = true;
		
		//TODO: Translate string
        textHolder.text = "Press Start to join";
    }

    public void OnLeave(string s)
    {
		//TODO: Translate string
        textHolder.text = s;
        bigCharacterSelectPlane.renderer.enabled = false;
        //TODO: Why is the index reset?
		marauderIndex = 0;
    }
}