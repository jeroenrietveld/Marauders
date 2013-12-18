using UnityEngine;
using System.Collections;

public class Interactor : ActionBase {

	public Interactable currentInteractable;

	void Start()
	{
		GetComponent<ControllerMapping>().AddAction(XInputDotNetPure.Button.LeftShoulder, this);
	}

	public override void PerformAction()
	{
		if (currentInteractable)
		{
			currentInteractable.OnInteract(gameObject);
		}
	}

	void OnGUI()
	{
		if (currentInteractable)
		{
			currentInteractable.ShowMessage();
		}
	}
}
