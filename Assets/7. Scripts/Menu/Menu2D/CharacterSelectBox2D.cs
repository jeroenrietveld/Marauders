using System;
using System.Collections.Generic;
using UnityEngine;
using XInputDotNetPure;

public class CharacterSelectBox2D : MonoBehaviour
{
    private int _progress;
    public GamePad controller;

    public List<Menu2DComponent> components;
    void Start()   
    {
        _progress = 0;
    }

    void OnGUI() 
    { 
        switch(_progress)
        {
            case 0: 
                //Not connected
                break;
            case 1:
                //Connected controller - "Press A to join"
                break;
            case 2: 
                //Pressed A - Select marauder
                break;
            case 3:
                //Selected marauder - Select skill
                break;
            case 4:
                //Selected skill - change to "Ready"-screen
                break;
            default: break;
        }
    }

    public void Update()
    {
        float vertical = controller.Axis(Axis.LeftVertical);
        float horizontal = controller.Axis(Axis.LeftHorizontal);

        switch (_progress)
        {
            case 0:
                //Not connected - do nothing
                if (controller.connected)
                {
                    _progress++;
                }
                break;
            case 1:
                //Connected controller - "Press A to join"
                if (controller.JustPressed(Button.A))
                {
                    _progress++;
                }
                break;
            case 2:
                //Pressed A - Select marauder
                if(controller.JustPressed(Button.DPadLeft) || horizontal < -0.7)
                {
                    //Change marauder to 1 down
                }

                if(controller.JustPressed(Button.DPadRight) || horizontal > 0.7)
                {
                    //change marauder to 1 up
                }
                if (controller.JustPressed(Button.A))
                {
                    //Lock the marauder
                    _progress++;
                }
                break;
            case 3:
                //Selected marauder - Select skill
                break;
            case 4:
                //Selected skill - change to "Ready"-screen
                break;
            default: break;
        }
    }


}
