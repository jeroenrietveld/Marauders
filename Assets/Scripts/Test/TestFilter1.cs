using UnityEngine;
using System.Collections;

public class TestFilter1 : MonoBehaviour
{
	public float speed = 1;
	public float amplitude = 1;
	
	private TestDecoratable testDecoratable;
	private float timeOffset = 0;

	// Use this for initialization
	void Start ()
	{
		testDecoratable = GetComponent<TestDecoratable>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.Space))
		{
			testDecoratable.position.filters += heightModulation;
			timeOffset = Time.time;
		}
		else if (Input.GetKeyUp (KeyCode.Space))
		{
			testDecoratable.position.filters -= heightModulation;
		}
	}

	Vector3 heightModulation(Vector3 v)
	{
		v.y += Mathf.Sin ((Time.time - timeOffset) * speed) * amplitude;
		return v;
	}
}