using UnityEngine;
using System.Collections;

public class Fader : MonoBehaviour {

	private Camera _camera;

	public float transparentDistance = 0f;
	public float opaqueDistance = 1f;
		
	// Use this for initialization
	void Start () {
		_camera = Camera.main;
	}
	
	// Update is called once per frame
	void Update () {
		Color color = renderer.material.color;

		float distance = Vector3.Distance (transform.position, _camera.transform.position);
		color.a = Mathf.Clamp01((distance - transparentDistance) / (opaqueDistance - transparentDistance));

		renderer.material.color = color;
	}
}
