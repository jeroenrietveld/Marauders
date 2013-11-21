using UnityEngine;
using System.Collections;

public class SplashState : IState
{
	public void onInput()
	{
		if(Input.GetKey(KeyCode.Space)) { 
			Debug.Log("splash");
		}
	}
}
