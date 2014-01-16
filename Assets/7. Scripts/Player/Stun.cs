using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stun : MonoBehaviour {

	private List<MonoBehaviour> _disabledComponents = new List<MonoBehaviour>();

	// Use this for initialization
	void Start ()
	{
		_disabledComponents.Add(GetComponent<Movement>());
		_disabledComponents.Add(GetComponent<Jump>());
		_disabledComponents.Add(GetComponent<Attack>());

		foreach (var c in _disabledComponents)
		{
			c.enabled = false;
		}
	}

	void OnDestroy()
	{
		foreach(var c in _disabledComponents)
		{
			c.enabled = true;
		}
	}
}
