using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class CharacterSelectBlock : MonoBehaviour {

	public PlayerIndex index;

	// Use this for initialization
	void Start () 
	{
	}
	
	// Update is called once per frame
	void Update () 
	{
		GamePadState gamePadState = GamePad.GetState (index);

		Debug.Log (gamePadState.Buttons.A);
	}
}
