using UnityEngine;
using System.Collections.Generic;

public class AvatarGraphics : MonoBehaviour {

	private Dictionary<GameObject, Material> _materials = new Dictionary<GameObject, Material>();
	private List<Renderer> _renderers = new List<Renderer>();

	private Timer _deathTimer;

	private Player _player;

	public int materialStack = 0;

	// Use this for initialization
	void Start () {
		FillMaterials(transform);

		var avatar = GetComponent<Avatar> ();
		var playerColor = avatar.player.color;
		_player = avatar.player;

		foreach (var m in _materials.Values)
		{
			m.SetColor("_PlayerColor", playerColor);
		}

		_deathTimer = new Timer (.6f);
		_deathTimer.AddPhaseCallback (_player.StartSpawnProcedure);

		_deathTimer.AddTickCallback(delegate
		{
			foreach(var m in _materials.Values)
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
					if(!_materials.ContainsKey(transform.gameObject))
					{
						_materials.Add(transform.gameObject, m);
						any = true;
					}
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
			foreach(var m in _materials.Values)
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

	public void AddMaterial(Material mat)
	{
		foreach(var r in _renderers)
		{
			if(materialStack < 1)
			{
				r.material = mat; 
			} 
			else 
			{
				List<Material> list = new List<Material>(r.materials);
				list.Add(mat);

				r.materials = list.ToArray();
			}
		}

		materialStack++;
	}

	public void RemoveMaterial(Material mat)
	{
		foreach(var r in _renderers)
		{
			List<Material> list = new List<Material>();

			foreach(Material m in r.materials)
			{
				if(m.shader != mat.shader)
				{
					list.Add(m);
				}
			}

			r.materials = list.ToArray();
		}

		materialStack--;

		if(materialStack == 0)
		{
			foreach(var pair in _materials)
			{
				pair.Key.renderer.material = pair.Value;
			}
		}
	}
}
