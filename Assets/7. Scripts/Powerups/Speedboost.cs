using UnityEngine;
using System.Collections;

public class Speedboost : MonoBehaviour {
	/// <summary>
	/// The speed multiplyer
	/// </summary>
	public float factor = 1;

	/// <summary>
	/// The speed additive
	/// </summary>
	public float additive = 0;

	/// <summary>
	/// The duration of the (de)buff in seconds
	/// </summary>
	public float duration = 0;

	/// <summary>
	/// Applys the speedboost
	/// </summary>
	/// <param name="inputSpeed">Input speed.</param>
	public float Apply(float inputSpeed)
	{
		return inputSpeed * this.factor + this.additive;
	}

	void Update()
	{
		duration -= Time.deltaTime;

		//Removing the buff
		if (duration <= 0)
		{
			Destroy(this);
		}
	}

	void OnEnable()
	{
		GetComponent<Movement>().movementSpeed.AddFilter(Apply);
	}

	void OnDisable()
	{
		GetComponent<Movement>().movementSpeed.RemoveFilter(Apply);
	}

}
