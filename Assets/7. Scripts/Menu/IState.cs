using UnityEngine;
using System.Collections;

public interface IState
{
    MenuStates GetNextState();
    MenuStates GetPreviousState();
	void onInput();
    bool CenterCamera();
}
