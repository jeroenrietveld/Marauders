using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	private Timer _portalTimer;

	// Use this for initialization
	void Start () {
		Vector4 scale = Vector4.zero;
		scale.x = 1 / transform.localScale.x;
		scale.y = 1 / transform.localScale.y;
		scale.z = 1 / transform.localScale.z;

		transform.renderer.material.SetVector ("InverseScale", scale);

		_portalTimer = new Timer (2f, Timer.WrapMode.ONCE);
		_portalTimer.endPhase = 10f;
		_portalTimer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		_portalTimer.Update ();
		if(_portalTimer.running)
		{
			renderer.material.SetFloat("Percentage", _portalTimer.Phase());
		}

		renderer.material.SetVector("FractalOffset", new Vector4(0.1f * Time.time, 0.1f * Time.time));
	}
}
