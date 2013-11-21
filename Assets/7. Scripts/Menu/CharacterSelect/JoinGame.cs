using UnityEngine;
using System.Collections;

public class JoinGame : MonoBehaviour 
{

    void Update()
    {
        if (InputWrapper.Instance.GetController(1) != null) 
        {
            if (InputWrapper.Instance.GetController(1).GetButtonADown())
            { 
                Debug.Log("Player 1 selects");
            }
        }
        if (InputWrapper.Instance.GetController(2) != null) 
        {
            if (InputWrapper.Instance.GetController(2).GetButtonADown())
            { 
                Debug.Log("Player 2 selects");
            }
        }
        if (InputWrapper.Instance.GetController(3) != null) 
        {
            if (InputWrapper.Instance.GetController(3).GetButtonADown())
            { 
                Debug.Log("Player 3 selects");
            }
        }
        if (InputWrapper.Instance.GetController(4) != null) 
        {
            if (InputWrapper.Instance.GetController(4).GetButtonADown())
            {
                Debug.Log("Player 4 selects");
            }
        }
    }
}
