using UnityEngine;
using System.Collections;

public class Heartbeat : MonoBehaviour {
	public Decoratable<float> heartbeatSpeed = new Decoratable<float>(90);

	private Avatar avatar;

	// Use this for initialization
	void Start () {
		avatar = GetComponent<Avatar>();
		renderer.material.color = new Color(0, 1, 1, 1);
	}
	
	// Update is called once per frame
	void Update () {
		transform.Rotate(Vector3.up, heartbeatSpeed * Time.deltaTime);
		renderer.material.SetFloat("health", avatar.health);
	}
}
