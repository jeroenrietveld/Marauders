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

		/*PlayerRef player1 = new PlayerRef (PlayerIndex.One);
		player1.marauder = "Thief";
		player1.skills = new []{"Dash", "Dash", "Dash"};
		player1.CreateAvatar ();
		GameManager.Instance.AddPlayerRef(player1);

		/*PlayerRef player2 = new PlayerRef (PlayerIndex.Two);
		player2.marauder = "Samurai";
		player2.skills = new []{"Dash", "Dash", "Dash"};
		player2.CreateAvatar ();

		PlayerRef player3 = new PlayerRef (PlayerIndex.Three);
		player3.marauder = "Samurai";
		player3.skills = new []{"Dash", "Dash", "Dash"};
		player3.CreateAvatar ();

		PlayerRef player4 = new PlayerRef (PlayerIndex.Four);
		player4.marauder = "Samurai";
		player4.skills = new []{"Dash", "Dash", "Dash"};
		player4.CreateAvatar ();*/
	}
}
