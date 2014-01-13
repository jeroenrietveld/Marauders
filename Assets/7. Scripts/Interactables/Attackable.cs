using UnityEngine;
using System.Collections;

public abstract class Attackable : MonoBehaviour {

	public abstract void OnAttack(Attack attacker);

}
