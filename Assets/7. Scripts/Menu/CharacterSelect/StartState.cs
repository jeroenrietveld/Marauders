using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class StartState : SelectionBase
{
    public StartState(CharacterSelectBlock block)
    {
        this.block = block;
    }

    public override void OnUpdate(GamePad controller)
    {
       
    }

    public override void OnActive()
    {
     
    }

    public override void OnInActive()
    {

    }
}
