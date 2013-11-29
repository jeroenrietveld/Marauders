using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class SplashState : MenuStateBase
{
	public SplashState()
	{
		center = GameObject.Find("SplashScreen").renderer.bounds.center;
		cameraSpeed = 20f;
	}

	public override void Update(MenuManager manager)
	{
		if(Input.GetKey(KeyCode.A))
		{
            manager.ChangeState(MenuStates.ArmoryState);
        }
	}
}
