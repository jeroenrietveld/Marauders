using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class SplashState : MenuStateBase
{
	public SplashState()
	{
		center = GameObject.Find("SplashScreen").renderer.bounds.center;
	}

	public override void Update(MenuManager manager)
	{
       // if(GamePad.GetState(PlayerIndex.One).Buttons.A == ButtonState.Pressed)
        if(Input.GetKey(KeyCode.A))
        {
            manager.ChangeState(MenuStates.ArmoryState);
        }
	}
}
