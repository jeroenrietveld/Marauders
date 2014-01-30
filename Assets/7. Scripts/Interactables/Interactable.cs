using UnityEngine;
using System.Collections;
using XInputDotNetPure;

public abstract class Interactable : MonoBehaviour {

	void OnTriggerEnter (Collider collider) {
		//Looking if the one colliding with us is a player
		var interactor = collider.gameObject.GetComponent<Interactor>();

		//Are we a player
		if (interactor)
		{
			interactor.currentInteractable = this;
		}
	}

	void OnTriggerExit (Collider collider) {
		//Looking if the one colliding with us is a player
		var interactor = collider.gameObject.GetComponent<Interactor>();
		
		//Are we a player
		if (interactor && interactor.currentInteractable == this)
		{
			interactor.currentInteractable = null;
		}
	}

	public abstract void OnInteract(GameObject obj);

	public abstract void ShowMessage ();
}
