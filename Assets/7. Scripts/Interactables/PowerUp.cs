using UnityEngine;
using System.Collections;
using System;
using SimpleJSON;

public class PowerUp : MonoBehaviour
{
	public string component;
	public string field;
	public float factor;

	public Timer timer;

	private Avatar avatar;

	void Start()
	{
		JSONNode node = ResourceCache.json ("PowerUps/PowerUp");

		component = node ["component"].Value;
		field = node ["field"].Value;
		factor = node ["factor"].AsFloat;

		this.avatar = GetComponent<Avatar>();

		timer = new Timer(node ["time"].AsFloat);
		timer.AddCallback (timer.startTime, applyPowerUp);
		timer.AddCallback (timer.endTime, removePowerUp);

		timer.Start ();
	}

	void Update()
	{
		timer.Update ();
	}

	void applyPowerUp()
	{
		Component buffedComponent = avatar.GetComponent(component);
		DecoratableFloat buffedField = (DecoratableFloat) buffedComponent.GetType().GetField(field).GetValue(buffedComponent);
		buffedField.AddFilter (ModulateDR);
	}

	void removePowerUp()
	{
		Component buffedComponent = avatar.GetComponent(component);
		DecoratableFloat buffedField = (DecoratableFloat) buffedComponent.GetType().GetField(field).GetValue(buffedComponent);
		buffedField.RemoveFilter (ModulateDR);
	}

	float ModulateDR(float dr)
	{
		return dr * factor;
	}
}
