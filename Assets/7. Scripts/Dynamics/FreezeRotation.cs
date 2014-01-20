using UnityEngine;
using System.Collections;

public class FreezeRotation : MonoBehaviour
{	
	void Update()
	{
		transform.rotation = Quaternion.identity;
	}
}
