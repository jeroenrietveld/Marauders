using UnityEngine;
using System.Collections;

public class HashIDs : MonoBehaviour
{
	public int speedFloat;
	public int jumpBool;
	public int fallBool;
	public int attack1Bool;
	public int attack2Bool;
	public int attack3Bool;
	public int locomotionState;
	
	void Awake()
	{
		speedFloat = Animator.StringToHash("Speed");
		jumpBool = Animator.StringToHash("Jump");
		attack1Bool = Animator.StringToHash("Attack1");
		attack2Bool = Animator.StringToHash("Attack2");
		attack3Bool = Animator.StringToHash("Attack3");
		locomotionState = Animator.StringToHash("Base Layer.Locomotion");
	}
}
