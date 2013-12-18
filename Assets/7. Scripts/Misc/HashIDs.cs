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
		/*speedFloat = Animation.StringToHash("Speed");
		jumpBool = Animation.StringToHash("Jump");
		attack1Bool = Animation.StringToHash("Attack1");
		attack2Bool = Animation.StringToHash("Attack2");
		attack3Bool = Animation.StringToHash("Attack3");
		locomotionState = Animation.StringToHash("Base Layer.Locomotion");*/
	}
}
