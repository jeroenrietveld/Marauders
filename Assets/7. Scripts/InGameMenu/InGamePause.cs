using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;

/// <summary>
/// Class that keeps track of the states and updates them.
/// </summary>
public class InGamePause : MonoBehaviour
{
    public List<GameObject> pauseTextObjects;
    public GameObject yes;
    public GameObject no;

    private Dictionary<InGameMenuStates, InGameMenuBase> _states;
    private InGameMenuBase _currentState;

    void Start()
    {
        _states = new Dictionary<InGameMenuStates, InGameMenuBase>();
        _states.Add(InGameMenuStates.PauseState, new PauseState(this));
        _states.Add(InGameMenuStates.ConfirmExitState, new ConfirmExitState(this));

        ChangeState(InGameMenuStates.PauseState);
    }

    /// <summary>
    /// Updates the states. When the state is null delete the prefab.
    /// </summary>
    void Update()
    {
        GamePad controller = ControllerInput.GetController(PlayerIndex.One);

        if (_currentState != null)
        {
            _currentState.OnUpdate(controller);
        }
        else 
        {
            Destroy(this.gameObject);
        }
    }

    public void ChangeState(InGameMenuStates state)
    {
        if (_currentState != null)
        {
            _currentState.OnInActive();
        }

        _currentState = _states[state];
        _currentState.OnActive();
    }

    /// <summary>
    /// Only call this method to delete the Menu Prefab. So this should
    /// only be called when the user clicks on the Resume game.
    /// </summary>
    public void SetCurrentStateNull()
    {
        _currentState = null;
    }
}

