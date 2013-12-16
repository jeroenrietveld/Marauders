using UnityEngine;
using System.Collections;

public class Portal : MonoBehaviour {

	public Timer portalTimer;

	// Use this for initialization
	void Awake () {
		Vector4 scale = Vector4.zero;
		scale.x = 1 / transform.localScale.x;
		scale.y = 1 / transform.localScale.y;
		scale.z = 1 / transform.localScale.z;

		transform.renderer.material.SetVector ("InverseScale", scale);

		portalTimer = new Timer (2f, Timer.WrapMode.ONCE);
		portalTimer.endPhase = 10f;
		portalTimer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		portalTimer.Update ();
		if(portalTimer.running)
		{
			renderer.material.SetFloat("Percentage", portalTimer.Phase());
		}

		renderer.material.SetVector("FractalOffset", new Vector4(0.1f * Time.time, 0.1f * Time.time));
	}
}
