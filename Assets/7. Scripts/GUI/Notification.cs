using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {

	private Timer _notificationTimer;
	private Vector3 _initialPosition;
	private Vector3 _translation;

	// Use this for initialization
	public Notification () {
		_notificationTimer = new Timer ();

		_notificationTimer.AddPhaseCallback (delegate() {
			Destroy (this.gameObject);
		});

		_notificationTimer.AddTickCallback (delegate() {
			Vector3 pos = transform.position;
			transform.position = _initialPosition + _translation * _notificationTimer.Phase ();
			
			Color color = renderer.material.GetColor ("_Color");
			color.a = 1 - _notificationTimer.Phase ();
			renderer.material.SetColor ("_Color", color);
		});
	}

	public void Initialize(float duration, Vector3 translation)
	{
		_notificationTimer.endTime = duration;
		_translation = translation;
		
		_notificationTimer.Start ();

		_initialPosition = transform.position;
	}

	// Update is called once per frame
	void Update () {
		_notificationTimer.Update ();
	}
}
