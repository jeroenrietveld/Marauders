using UnityEngine;
using System.Collections;

public class WeaponPrototype : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject pickup = GameObject.Instantiate (Resources.Load ("Prefabs/Interactables/Pickup")) as GameObject;
		GameObject weapon = WeaponFactory.create ("Katana");
		weapon.transform.parent = pickup.transform;
		pickup.transform.position = new Vector3 (0, 15, 0);
		pickup.GetComponent<WeaponInteractable> ().weaponObject = weapon;
	}
}
