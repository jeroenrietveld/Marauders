using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {

	private Timer _notificationTimer;
	private Vector3 _initialPosition;

	// Use this for initialization
	void Start () {
		_notificationTimer = new Timer (1.5f);
		_notificationTimer.Start ();

		_notificationTimer.AddCallback (delegate() {
			Destroy (this.gameObject);
		});

		_initialPosition = transform.position;
	}
	
	// Update is called once per frame
	void Update () {
		_notificationTimer.Update ();

		Vector3 pos = transform.position;
		transform.position = _initialPosition + Vector3.up * _notificationTimer.Phase () * 0.5f;
		Color color = renderer.material.GetColor ("_Color");
		color.a = 1 - _notificationTimer.Phase ();
		renderer.material.SetColor ("_Color", color);
	}
}
