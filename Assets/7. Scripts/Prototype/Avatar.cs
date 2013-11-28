using UnityEngine;
using System.Collections;

public struct PlayerHitEvent
{

}

public struct PlayerDeathEvent
{

}

public class Avatar : MonoBehaviour {

	public float health
	{
		get {
			return _health;
		}

		set {
			_health = Mathf.Clamp01(value);

			Event.dispatch(new PlayerHitEvent());

			if(_health == 0f)
			{
				Event.dispatch(new PlayerDeathEvent());
			}
		}
	}

	public float armorFactor = 0.1f;

	private float _health = .75f;
	private Heartbeat _heartbeat;

	// Use this for initialization
	void Start () {
		_heartbeat = GetComponent<Heartbeat>();
		//_heartbeat.heartbeatSpeed.filters += ModulateHeartbeat;
	}
	
	// Update is called once per frame
	void Update () {
		if(Input.GetKeyDown(KeyCode.Space))
		{
			ApplyDamage(-Vector3.forward, 0.25f);
		}

		if(Input.GetKeyDown(KeyCode.LeftShift))
		{
			health = 1f;
		}
	}

	void ApplyDamage(Vector3 direction, float amount)
	{
		float dot = Vector3.Dot(direction, _heartbeat.transform.forward);
		bool armorHit = (Mathf.Acos(dot) / Mathf.PI) > health;

		if(armorHit) amount *= armorFactor;

		health = health - amount;
	}

	float ModulateHeartbeat(float f)
	{
		float primaryBeat = Mathf.Pow(Mathf.Abs(Mathf.Sin(Time.time * 4 * (health * .5f + .5f))), 25f) * 2.5f;
		float secondaryBeat = Mathf.Pow(Mathf.Abs(Mathf.Sin((Time.time - 0.25f) * 4 * (health * .5f + .5f))), 25f) * 1.5f;
		return 40 + 90 * secondaryBeat + 90 * primaryBeat;
		//return f * (health * .5f + .5f);
	}
}
