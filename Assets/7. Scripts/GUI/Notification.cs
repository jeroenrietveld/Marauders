using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {

	private Timer _notificationTimer;

	// Use this for initialization
	void Start () {
		_notificationTimer = new Timer (1.5f);
		_notificationTimer.Start ();

		_notificationTimer.AddCallback (delegate() {
			Destroy (this.gameObject);
		});
	}
	
	// Update is called once per frame
	void Update () {
		_notificationTimer.Update ();

		Vector3 pos = transform.position;
		pos.y = _notificationTimer.Phase () * 0.05;
		transform.position = pos;
	}
}
