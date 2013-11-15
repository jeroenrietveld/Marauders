using UnityEngine;
using System.Collections;

public class MenuManager : MonoBehaviour {

	public GameObject camera;

	// Use this for initialization
	void Start () {
		camera = GameObject.FindGameObjectWithTag("MainCamera");

		camera.transform.position = Vector3.MoveTowards(camera.transform.position, new Vector3(10, 10, 10), Time.deltaTime * 10);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
