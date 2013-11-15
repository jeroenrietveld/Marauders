using UnityEngine;
using System.Collections;

public class TestFilter3 : MonoBehaviour
{
	public float multiplier = 2;
	
	private TestDecoratable testDecoratable;
	
	// Use this for initialization
	void Start ()
	{
		testDecoratable = GetComponent<TestDecoratable>();
	}
	
	// Update is called once per frame
	void Update ()
	{
		if (Input.GetKeyDown (KeyCode.LeftShift)) {
			testDecoratable.position.filters += heightModulation;
		} else if (Input.GetKeyUp (KeyCode.LeftShift)) {
			testDecoratable.position.filters -= heightModulation;
		}
	}
	
	Vector3 heightModulation(Vector3 v)
	{
		v.y *= multiplier;
		return v;
	}
}