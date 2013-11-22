using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour
{
	public int speedFloat;
	public int locomotionState;
	
	void Awake()
	{
		speedFloat = Animator.StringToHash("Speed");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
	}
}
