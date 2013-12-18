using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Interactor : ActionBase {

	public Interactable currentInteractable;
	private Button interactButton = Button.LeftShoulder;


	void Start()
	{
		GetComponent<ControllerMapping>().AddAction(interactButton, this);
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
			currentInteractable.ShowMessage(interactButton);
		}
	}
}
