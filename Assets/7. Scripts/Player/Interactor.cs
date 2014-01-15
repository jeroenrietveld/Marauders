using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public class Interactor : ActionBase {

	public Interactable currentInteractable;
	private Button interactButton = Button.LeftShoulder;

	void Update()
	{
		if(currentInteractable)
		{
			currentInteractable.OnInteract(gameObject);
		}
	}

	public override void PerformAction()
	{
		if (currentInteractable)
		{
			currentInteractable.OnInteract(gameObject);
		}
	}
}
