﻿using UnityEngine;
using System.Collections.Generic;

public class AvatarGraphics : MonoBehaviour {

	private List<Material> _materials = new List<Material>();
	private List<Renderer> _renderers = new List<Renderer>();

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

		_deathTimer = new Timer (.6f);
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
			bool any = false;

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
					any = true;
				}
			}

			if(any) _renderers.Add(r);
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
			foreach(var m in _materials)
			{
				var q = Quaternion.AngleAxis(Random.value * 360, Vector3.up);
				m.SetVector("_ShearDirection", q * new Vector3(0, 0, 1));
			}
			foreach(var r in _renderers)
			{
				r.castShadows = false;
			}

			_deathTimer.Start();
		}
	}

	public void ApplySkillMaterial(Transform transform, Material mat)
	{
		var r = transform.GetComponent<Renderer> ();

		if (r)
		{
			if(r.material.shader.name == "Custom/Character")
			{
				r.material = mat;
			}
		}
		
		for (int i = 0; i < transform.childCount; ++i)
		{
			ApplySkillMaterial(transform.GetChild(i), mat);
		}
	}

	public void ApplyDefaultMaterial(Transform transform)
	{
		var r = transform.GetComponent<Renderer> ();

		/*if(r)
		{
			foreach(var mat in _materials)
			{
				if(mat.name == r.material.name)
				{
					r.material = mat;
				}
			}
		}*/

		for (int i = 0; i < transform.childCount; ++i)
		{
			ApplyDefaultMaterial(transform.GetChild(i));
		}
	}
}
