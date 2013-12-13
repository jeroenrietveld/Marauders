using UnityEngine;
using System.Collections;

public class AttackInfo
{
	/// <summary>
	/// Gets or sets the name of the animation.
	/// </summary>
	/// <value>The name of the animation.</value>
	public string animationName { get; set; }

	/// <summary>
	/// Gets or sets the speed.
	/// </summary>
	/// <value>The speed.</value>
	public float speed { get; set; }

	/// <summary>
	/// Gets or sets the timing of firing the damage event ( in seconds ), will scale off of speed
	/// </summary>
	/// <value>The timing.</value>
	public float timing { get; set; }

	public AttackInfo(string name, float speed, float timing)
	{
		this.animationName = name;
		this.speed = speed;
		this.timing = timing;
	}

}
