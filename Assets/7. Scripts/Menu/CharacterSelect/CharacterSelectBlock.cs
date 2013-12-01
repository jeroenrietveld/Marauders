using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Collections.Generic;

public enum CharacterSelectBlockStates
{
    CharSelectState,
    CharConfirmState,
    SkillSelectState
}

public class CharacterSelectBlock : MonoBehaviour {

    /*
     * to-do list voor character select:
         * prefabs integreren in nieuwe menu.unity scene
         * In ArmoryState staan nog code voor de text in de armory. Dit had jij volgens mij gedaan. Kan je nalopen of deze nog nodig zijn? En in de OnActive() methode
             * heb ik de code gezet om de splashstate te activeren. Hadden we eerst in de SplashState staan. Is netter hier. Maar er stond daar al een soort gelijke methode. Kan je kijken 
             * of beide werken en mocht nodig zijn 1 weg halen.
         * beter uitlijnen in unity
         * skills inladen en selecteren (misschien moet dat via de json bestanden.)
         * nieuwe input gebruiken
         * gegevens (gekozen char en skills per speler) opslaan in gamemanager of o.i.d.
         * alle gameobjects in variable zetten die nodig zijn. Zie hieronder (alleen de 3 textboxes volgens mij.)
         * Refactoren en opschonen van code.
     * 
     * mocht het nodig zijn (denk niet) characterselectblock class integreren in ArmoryState.cs 
     */

    public GameObject _bigCharacterSelectPlane;
    public GameObject _textJoin;
    public GameObject _skillSelectPlane;
    public GameObject _smallCharacterSelectPlane;
	
    public PlayerIndex index;
    public List<Material> heroes;
    public int _count { get; set; }
    public bool isConnected { get; set; }
    private bool isJoined = false;

    private CharacterSelectBase _currentState;
    private IDictionary<CharacterSelectBlockStates, CharacterSelectBase> list;

    void Start()
    {
        _count = 0;
        list = new Dictionary<CharacterSelectBlockStates, CharacterSelectBase>();
        list.Add(CharacterSelectBlockStates.CharSelectState, new CharSelectState(this));
        list.Add(CharacterSelectBlockStates.SkillSelectState, new SkillSelectState(this));
    }

    public void ChangeState(CharacterSelectBlockStates state)
    {
        if(_currentState != null) _currentState.OnInActive();
        _currentState = list[state];
        _currentState.OnActive();
    }

    /// <summary>
    ///  Check every frame if a players wants to join the game. If they press their "A" button
    ///  the first hero will be shown. The List heroes has all the materials for the characters so
    ///  we can switch between them.
    /// </summary> 
	void Update () 
	{
        GamePad controller = ControllerInput.GetController(index);

        Debug.Log(controller.connected);
        Debug.Log(controller.deadZone);
        

        if (!isConnected && controller.connected)
        {
            OnControllerConnect();
        }
        else if (isConnected && !controller.connected)
        {
            OnControllerDisConnect();
        }

        if (isConnected && controller.connected)
        {
            if(isJoined)
            {
                _currentState.OnUpdate(controller);
            }
            else if (controller.JustPressed(Button.A))
            {
                isJoined = true;
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

        _bigCharacterSelectPlane = transform.FindChild("BigCharacterSelect").gameObject;
        _textJoin = _bigCharacterSelectPlane.transform.FindChild("text_select").gameObject;
        _skillSelectPlane = transform.FindChild("SkillSelect").gameObject;
        _smallCharacterSelectPlane = transform.FindChild("SmallCharacterSelect").gameObject;

        _textJoin.GetComponent<TextMesh>().text = "Press Start to join";
    }

    public void OnLeave(string s)
    {
        _textJoin.GetComponent<TextMesh>().text = s;
        _bigCharacterSelectPlane.renderer.enabled = false;
        isJoined = false;
        _count = 0;
    }
}