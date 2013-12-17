using UnityEngine;
using System.Collections;

public class WeaponPrototype : MonoBehaviour {

	// Use this for initialization
	void Start () {
		GameObject katana1 = WeaponFactory.create ("Katana");
		PickupSpawner.SpawnWeapon (katana1, new Vector3(0, 20, 0));

		GameObject daggers1 = WeaponFactory.create ("Daggers");
		PickupSpawner.SpawnWeapon (daggers1, new Vector3(0, 20, 25));

		GameObject katana2 = WeaponFactory.create ("Katana");
		PickupSpawner.SpawnWeapon (katana2, new Vector3(-5, 20, 0));
	
		GameObject daggers = WeaponFactory.create ("Daggers");
		PickupSpawner.SpawnWeapon (daggers, new Vector3(0, 20, 25));

		PlayerRef player = new PlayerRef (PlayerIndex.One);
		player.skills.utilitySkill = "Dash";
		player.CreateAvatar ();
	}
}
