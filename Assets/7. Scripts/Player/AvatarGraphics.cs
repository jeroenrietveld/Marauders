using UnityEngine;
using System.Collections.Generic;

public class AvatarGraphics : MonoBehaviour {

	private List<Material> _materials = new List<Material>();

	private Timer _deathTimer;

	private Player _player;

	// Use this for initialization
	void Start () {
		FillMaterials(transform);

		var avatar = GetComponent<Avatar> ();
		var playerColor = avatar.player.color;
		_player = avatar.player;

		foreach (var m in _materials)
		{
			m.SetColor("_PlayerColor", playerColor);
		}

		_deathTimer = new Timer (.4f);
		_deathTimer.AddPhaseCallback (_player.StartSpawnProcedure);
		_deathTimer.AddTickCallback(delegate
		{
			foreach(var m in _materials)
			{
				m.SetFloat("_DeathPhase", _deathTimer.Phase());
			}
		});

		Event.register<AvatarDeathEvent> (OnAvatarDeath);
	}

	void OnDestroy()
	{
		Event.unregister<AvatarDeathEvent> (OnAvatarDeath);
	}

	void Update()
	{
		_deathTimer.Update ();
	}

	private void FillMaterials(Transform transform)
	{
		var r = transform.GetComponent<Renderer> ();
		if (r)
		{
			foreach(var m in r.materials)
			{
				if(m.shader.name != "Custom/Character")
				{
					// Uncomment when adding a new character, useful for finding all materials
					//Debug.Log(transform.GetPath() + " does not use the custom Character shader.");
				}
				else
				{
					_materials.Add(m);
				}
			}
		}

		for (int i = 0; i < transform.childCount; ++i)
		{
			FillMaterials(transform.GetChild(i));
		}
	}

	private void OnAvatarDeath(AvatarDeathEvent evt)
	{
		if(evt.victim == _player)
		{
			_deathTimer.Start();
		}
	}
}
