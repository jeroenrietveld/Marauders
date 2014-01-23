using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stun : MonoBehaviour {

	private List<MonoBehaviour> _disabledComponents = new List<MonoBehaviour>();
	private float _stunTimeRemaining;

	// Use this for initialization
	void Start ()
	{
		_disabledComponents.Add(GetComponent<Movement>());
		_disabledComponents.Add(GetComponent<Jump>());
		_disabledComponents.Add(GetComponent<Attack>());
	}

	void Update()
	{
		if(_stunTimeRemaining > 0)
		{
			_stunTimeRemaining -= Time.deltaTime;

			if(_stunTimeRemaining <= 0)
			{
				EnableComponents();
			}
		}
	}

	public void SetStunTime(float time)
	{
		_stunTimeRemaining = Mathf.Max (_stunTimeRemaining, time);

		DisableComponents ();
	}

	private void DisableComponents()
	{
		foreach (var c in _disabledComponents)
		{
			c.enabled = false;
		}
	}

	private void EnableComponents()
	{
		foreach(var c in _disabledComponents)
		{
			c.enabled = true;
		}
	}

	void OnDestroy()
	{
		EnableComponents ();
	}
}
