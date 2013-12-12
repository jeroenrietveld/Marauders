using UnityEngine;
using System.Collections;

public class TimeBubbleRipple : MonoBehaviour {

	public PlayerIndex player = PlayerIndex.One;
	public bool reactOnObjects = false;

	public float endPhase = 1;

	public Timer timer = new Timer(0, 1, Timer.WrapMode.NONE);

	// Use this for initialization
	void Start () {
		Event.register<TimeBubbleObjectExitEvent>(OnObjectExit);
		Event.register<TimeBubblePlayerExitEvent>(OnPlayerExit);

		timer.startPhase = -10;
	}

	void OnDestroy()
	{
		Event.unregister<TimeBubbleObjectExitEvent>(OnObjectExit);
		Event.unregister<TimeBubblePlayerExitEvent>(OnPlayerExit);
	}

	void OnPlayerExit(TimeBubblePlayerExitEvent evt)
	{
		if(!reactOnObjects && player == evt.player.playerIndex)
		{
			DoRipple(evt.player.transform.position, evt.respawnDelay);
		}
	}

	void OnObjectExit(TimeBubbleObjectExitEvent evt)
	{
		if(reactOnObjects)
		{
			DoRipple(evt.obj.transform.position, evt.respawnDelay);
		}
	}

	private void DoRipple(Vector3 position, float respawnDelay)
	{
		float rippleCompression = renderer.material.GetFloat("RippleCompression");
		
		timer.endTime = respawnDelay;
		timer.startPhase = -1 / rippleCompression;
		timer.endPhase = endPhase;
		timer.Start();
		
		transform.LookAt(position);
		transform.localRotation *= Quaternion.AngleAxis(Random.value * 360, Vector3.forward);
	}
	
	// Update is called once per frame
	void Update () {
		timer.Update();

		float phase = timer.Phase();
		renderer.material.SetFloat ("RipplePhase", timer.Phase());
		renderer.enabled = timer.running && phase >= timer.startPhase && phase <= 2;
	}
}
