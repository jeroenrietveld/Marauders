using UnityEngine;
using System.Collections;

public class ConnectControllerStep : BaseStep 
{
	private bool _isConnected = false;

	void Start () {
	}

	void Update () {
		if(controller.connected && !_isConnected)
		{
			_isConnected = true;
			characterSelect.NextStep();
		}
		else if(!controller.connected && _isConnected)
		{
			_isConnected = false;
			characterSelect.ControllerDisconnect();
		}
	}

	void OnEnable()
	{
		Debug.Log ("ASD");
		GetComponent<MeshRenderer>().enabled = true;
	}

	void OnDisable()
	{
		GetComponent<MeshRenderer> ().enabled = false;
	}
}
