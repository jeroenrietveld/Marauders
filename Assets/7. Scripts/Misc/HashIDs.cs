using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour
{
	public int speedFloat;
	public int jumpBool;
	public int locomotionState;
	
	void Awake()
	{
		speedFloat = Animator.StringToHash("Speed");
		jumpBool = Animator.StringToHash("Jump");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
	}
}
