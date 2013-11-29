using UnityEngine;
using System.Collections;

public class DeathIndicator : MonoBehaviour {

	// Use this for initialization
	void Start () {
		Event.register<PlayerHitEvent>(OnPlayerHit);
		Event.register<PlayerDeathEvent>(OnPlayerDeath);
	}
	
	// Update is called once per frame
	void Update () {
	
	}

	public void OnPlayerHit(PlayerHitEvent evt)
	{
		transform.Translate(new Vector3(0, .125f, 0));
	}

	public void OnPlayerDeath(PlayerDeathEvent etv)
	{
		var pos = transform.position;
		pos.y = 0;
		transform.position = pos;
		transform.Translate(new Vector3(.5f, 0, 0));
	}
}
