using UnityEngine;
using System.Collections.Generic;

public class StaticMeshBatcher : MonoBehaviour {

	// Use this for initialization
	void Start () {
		var child = transform.GetChild (0).gameObject;
		var map = new Dictionary<Material, List<MeshFilter>> ();

		var renderers = GetComponentsInChildren<MeshRenderer> ();
		foreach(var r in renderers)
		{
			var material = r.sharedMaterial;

			List<MeshFilter> filters;

			if(map.ContainsKey(material))
			{
				filters = map[material];
			}
			else
			{
				filters = new List<MeshFilter>();
				map.Add(material, filters);
			}

			filters.Add(r.GetComponent<MeshFilter>());
		}

		foreach(var pair in map)
		{
			var combiner = new CombineInstance[pair.Value.Count];

			for(int i = 0; i < combiner.Length; ++i)
			{
				combiner[i].mesh = pair.Value[i].sharedMesh;
				combiner[i].transform = pair.Value[i].transform.localToWorldMatrix;
				pair.Value[i].gameObject.SetActive(false);
			}

			var go = new GameObject();
			go.transform.parent = transform;
			var filter = go.AddComponent<MeshFilter>();
			filter.mesh = new Mesh();
			filter.mesh.CombineMeshes(combiner);

			go.AddComponent<MeshRenderer>().material = pair.Key;
		}

		Destroy (child);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
