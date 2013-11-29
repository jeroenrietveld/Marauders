using UnityEngine;
using System.Collections;

public abstract class Interactable : MonoBehaviour {

	/// <summary>
	/// <para>Gets or sets a value indicating whether this <see cref="Interactable"/> is interactable.</para><para>Can be disabled if ( for example ) a weapon is picked up</para>
	/// </summary>
	/// <value><c>true</c> if is interactable; can be used by a player<c>false</c>.</value>
	public bool isInteractable = true;

	void OnTriggerEnter (Collider collider) {
		//Looking if the one colliding with us is a player
		Player player = collider.gameObject.GetComponent<Player>();

		//Are we a player
		if (player != null)
		{
			//Are we allowed to interact
			if (isInteractable)
			{
				OnInteractEnter(player);
			}
		}
	}

	void OnTriggerExit (Collider collider) {
		//Looking if the one colliding with us is a player
		Player player = collider.gameObject.GetComponent<Player>();
		
		//Are we a player
		if (player != null)
		{
			//Are we allowed to interact
			if (isInteractable)
			{
				OnInteractLeave(player);
			}
		}
	}
	
	public abstract void OnInteractEnter(Player player);

	public abstract void OnInteractLeave(Player player);
}
