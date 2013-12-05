using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : MonoBehaviour {
	public DecoratableFloat heartbeatSpeed = new DecoratableFloat(90);

	private Player player;

	// Use this for initialization
	void Start () {
		player = transform.parent.gameObject.GetComponent<Player>();
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate the health circle
		transform.Rotate(Vector3.up, heartbeatSpeed * Time.deltaTime);
		//Pass player health to shader
		renderer.material.SetFloat("health", player.health);
	}
}
