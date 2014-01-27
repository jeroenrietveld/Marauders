using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Stun : MonoBehaviour {

	private List<MonoBehaviour> _disabledComponents = new List<MonoBehaviour>();
	private float _stunTimeRemaining;
    private float _disabledStunTimeRemaining;

	// Use this for initialization
	void Start ()
	{
		_disabledComponents.Add(GetComponent<Movement>());
		_disabledComponents.Add(GetComponent<Jump>());
		_disabledComponents.Add(GetComponent<Attack>());
	}

	void Update()
	{
        if(_disabledStunTimeRemaining > 0)
        {
            _disabledStunTimeRemaining -= Time.deltaTime;
        }
		else
        {
            if (_stunTimeRemaining > 0)
            {
                _stunTimeRemaining -= Time.deltaTime;

                if (_stunTimeRemaining <= 0)
                {
                    EnableComponents();
                }
            }
        }    
	}

	public void SetStunTime(float time)
	{
        // Make sure that the stun can only be set when the disabledstuntime is active.
        if(_disabledStunTimeRemaining <= 0)
        {
            _stunTimeRemaining = Mathf.Max(_stunTimeRemaining, time);

            DisableComponents();
        }	
	}

    public void DisableStunTime(float time)
    {
        _disabledStunTimeRemaining = Mathf.Max(_disabledStunTimeRemaining, time);
        // Enable all components and reset the stuntimeremaining.
        _stunTimeRemaining = 0;
        EnableComponents();
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
