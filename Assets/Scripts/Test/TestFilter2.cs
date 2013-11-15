using UnityEngine;
using System.Collections;

public class TestFilter2 : MonoBehaviour
{
	public float speed = 1;
	public float xAmplitude = 1;
	public float zAmplitude = 1;
	
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
		if (Input.GetKeyDown (KeyCode.LeftControl))
		{
			testDecoratable.position.filters += positionModulation;
			timeOffset = Time.time;
		}
		else if (Input.GetKeyUp (KeyCode.LeftControl))
		{
			testDecoratable.position.filters -= positionModulation;
		}
	}
	
	Vector3 positionModulation(Vector3 v)
	{
		v.x += Mathf.Sin ((Time.time - timeOffset) * speed) * xAmplitude;
		v.z += Mathf.Cos ((Time.time - timeOffset) * speed) * zAmplitude;
		return v;
	}
}