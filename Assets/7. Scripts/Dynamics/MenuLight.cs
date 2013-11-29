using UnityEngine;
using System.Collections;

public class MenuLight : MonoBehaviour {

	// Use this for initialization
	void Start () {
	}
	
	// Update is called once per frame
	void Update () {
		GetComponent<Light> ().intensity = Random.Range (0.3f, 0.6f);
	}
}
