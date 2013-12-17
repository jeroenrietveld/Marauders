using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Pickup : ActionBase {
	
	void Start () {
		ControllerMapping controllerMapping = GetComponent<ControllerMapping> ();
		controllerMapping.AddAction(Button.LeftShoulder, this);
	}
	
	public override void PerformAction()
	{
	}
	
	void Update()
	{
		
	}
}
