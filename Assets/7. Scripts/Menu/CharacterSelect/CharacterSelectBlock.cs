using UnityEngine;
using System.Collections;
using XInputDotNetPure;
using System;
using System.Linq;
using System.Collections.Generic;

public class CharacterSelectBlock : MonoBehaviour 
{
    public GameObject StartScreen;
    public GameObject MarauderSelect;
    public GameObject SkillSelect;
    public SelectionBase _currentState;
    public CharacterSelectBlockStates _currentEnum;
    public PlayerIndex player;
    
    // These list should later be filled dynamicly.
    public List<Material> marauders;
    public List<Material> maraudersSmall;
    public List<string> marauderNames;
	public int marauderIndex { get; set; }
	public bool isConnected { get; set; }
    public bool isPlayerReady { get; set; }
    public bool isJoined { get; set; }
    public bool isInSelection { get; set; }
    public Dictionary<string, string> marauderTexts;

	private GamePad _controller;

    private IDictionary<CharacterSelectBlockStates, SelectionBase> _list;
    private MenuCameraMovement cameraMovement;
    private bool zoomedIn = true;
    private TextMesh _textCharselect;
    public AudioSource audioSourceArmory;
    public AudioSource menuSelectSounds;

    void Start()
	{
        menuSelectSounds = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
        audioSourceArmory = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);

        marauderIndex = 0;
        cameraMovement = GameObject.FindGameObjectWithTag("MainCamera").GetComponent<MenuCameraMovement>();

        _list = new Dictionary<CharacterSelectBlockStates, SelectionBase>();
        _list.Add(CharacterSelectBlockStates.CharSelectState, new CharacterSelectState(this));
        _list.Add(CharacterSelectBlockStates.SkillSelectState, new SkillSelectState(this));
        _list.Add(CharacterSelectBlockStates.StartState, new StartState(this));
        marauderTexts = new Dictionary<string, string>();
        FillMarauderTextsFromJSON();

		_currentState = null;
		_controller = ControllerInput.GetController (player);
        _controller.deadZone = GamePadDeadZone.IndependentAxes;

        StartScreen = transform.FindChild("Start").gameObject;
        MarauderSelect = transform.FindChild("MarauderSelect").gameObject;
        SkillSelect = transform.FindChild("SkillSelect").gameObject;
        _textCharselect = MarauderSelect.transform.FindChild("TextCharselect").gameObject.GetComponent<TextMesh>();

        ChangeState(CharacterSelectBlockStates.StartState);
    }

    private void FillMarauderTextsFromJSON()
    {
        var resources = Resources.LoadAll("Marauders/");

        foreach (object resource in resources)
        {
            var node = SimpleJSON.JSON.Parse(((TextAsset)resource).text);
            string name = node["name"].Value;
            string text = node["text_charselect"].Value;

            marauderTexts.Add(name, text);
        }
    }

    public void ChangeState(CharacterSelectBlockStates state)
    {
        _currentEnum = state;

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
        if(!cameraMovement.isMoving)
        {
            // Show text only when zooming in is done
            if (zoomedIn)
            {
                StartScreen.transform.FindChild("ControllerText").gameObject.renderer.enabled = true;
                zoomedIn = false;
            }

            if (!isConnected && _controller.connected)
            {
                OnControllerConnect();
            }

            else if (isConnected && !_controller.connected)
            {
                OnControllerDisConnect();
            }
            
            if (isConnected && _controller.connected)
            {
                if (_currentState != null)
                {
                    _currentState.OnUpdate(_controller);
                }
            }
        }
	}

    private void OnControllerDisConnect()
    {
        isJoined = false;
        isConnected = false;
        isInSelection = false;
        _currentState = null;
        SkillSelect.SetActive(false);
        MarauderSelect.SetActive(false);
        StartScreen.SetActive(true);
        StartScreen.transform.FindChild("ControllerText").gameObject.GetComponent<TextMesh>().text = "Connect \n Controller";
        GameManager manager = GameManager.Instance;
        Player p = manager.playerRefs.FirstOrDefault(x => x.index == player);
        StartScreen.transform.FindChild("AButton").gameObject.renderer.enabled = false;
        if (p != null)
        {
            manager.playerRefs.Remove(p);
        }
    }

    private void OnControllerConnect()
    {
        isConnected = true;
        StartScreen.transform.FindChild("ControllerText").gameObject.GetComponent<TextMesh>().text = "Press \n to Join";
        StartScreen.transform.FindChild("AButton").gameObject.renderer.enabled = true;
    }

	public void changeMarauder(int index)
	{
		marauderIndex = index;
		MarauderSelect.transform.FindChild("MarauderModel").gameObject.renderer.material = marauders[marauderIndex];
        MarauderSelect.transform.FindChild("MauraderSelectNameText").GetComponent<TextMesh>().text = marauderNames[marauderIndex];

        _textCharselect.text = marauderTexts[marauderNames[marauderIndex]];
	}
}