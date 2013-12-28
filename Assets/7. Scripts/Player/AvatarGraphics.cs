using UnityEngine;
using System.Collections.Generic;

public class AvatarGraphics : MonoBehaviour {

	private List<Material> _materials = new List<Material>();

	// Use this for initialization
	void Start () {
		FillMaterials(transform);

		var playerColor = GetComponent<Avatar>().player.color;

		foreach (var m in _materials)
		{
			m.SetColor("_PlayerColor", playerColor);
		}
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
}
