using UnityEngine;
using System.Collections;

public class TimeBubbleRipple : MonoBehaviour {

	private float _phase = 2f;
	public float speed = 2f;
	public float start = 0;
	public float end = 2;

	// Use this for initialization
	void Start () {
		Event.register<TimeBubbleExitEvent>(OnExit);
	}

	void OnExit(TimeBubbleExitEvent evt)
	{

		transform.LookAt(evt.collisionPosition);
		transform.localRotation *= Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
		_phase = start;
	}
	
	// Update is called once per frame
	void Update () {
		_phase += Time.deltaTime * speed;
		//_phase = ((_phase - start) % (end - start)) + start;

		renderer.material.SetFloat ("RipplePhase", _phase);
	}
}
