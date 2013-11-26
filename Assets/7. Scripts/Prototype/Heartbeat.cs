using UnityEngine;
using System.Collections;

/// <summary>
/// Prototype for the heart beat, this indicates the player health.
/// </summary>
public class Heartbeat : MonoBehaviour {
	public Decoratable<float> heartbeatSpeed = new Decoratable<float>(90);

	private Avatar avatar;

	// Use this for initialization
	void Start () {
		avatar = GetComponent<Avatar>();
	}
	
	// Update is called once per frame
	void Update () {
		//Rotate the health circle
		transform.Rotate(Vector3.up, heartbeatSpeed * Time.deltaTime);
		//Pass player health to shader
		renderer.material.SetFloat("health", avatar.health);
	}
}
