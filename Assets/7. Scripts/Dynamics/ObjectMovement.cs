using UnityEngine;
using System.Collections;

public class ObjectMovement : MonoBehaviour {

	public Timer timer = new Timer(0);
	public Vector3 targetPosition;
	public Vector3 targetScale;
	private Vector3 _initialPosition;
	private Vector3 _initialScale;
	private bool _destroyObject;

	public static ObjectMovement Create(GameObject go, Vector3 targetPosition, Vector3 targetScale, float time, bool destroyObject = true)
	{
		var mv = go.AddComponent<ObjectMovement> ();
		mv.targetPosition = targetPosition;
		mv.targetScale = targetScale;
		mv.timer.endTime = time;
		mv._destroyObject = destroyObject;
		return mv;
	}

	// Use this for initialization
	void Start () {
		_initialPosition = transform.position;
		_initialScale = transform.localScale;

		timer.AddPhaseCallback (delegate {
			if(_destroyObject) Destroy (this.gameObject);
			else Destroy (this);
		});

		timer.Start ();
	}
	
	// Update is called once per frame
	void Update () {
		timer.Update ();

		transform.position = _initialPosition + (targetPosition - _initialPosition) * Mathf.SmoothStep (0, 1, timer.Phase ());
		transform.localScale = _initialScale + (targetScale - _initialScale) * Mathf.SmoothStep (0, 1, timer.Phase ());
	}
}
