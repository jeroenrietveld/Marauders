using UnityEngine;
using System.Collections;

public class ArmoryState : IState
{
	public void onInput()
	{
		if(Input.GetKey(KeyCode.Space)) { 
			Debug.Log("armory");
		}
	}
}
