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
			int numVerts = 0, numIndices = 0;

			foreach(var meshFilter in pair.Value)
			{
				numVerts += meshFilter.sharedMesh.vertexCount;
				numIndices += meshFilter.sharedMesh.GetIndices(0).Length;
			}

			Vector3[] verts = new Vector3[numVerts];
			Vector2[] uvs = new Vector2[numVerts];
			int[] indices = new int[numIndices];

			int srcIndexOffset = 0, tgtIndexOffset = 0, tgtVertexOffset = 0;

			foreach(var meshFilter in pair.Value)
			{
				var mesh = meshFilter.sharedMesh;
				var xform = meshFilter.transform.localToWorldMatrix;
				
				foreach(var index in mesh.GetIndices(0))
				{
					indices[tgtIndexOffset++] = index + srcIndexOffset;
				}
				srcIndexOffset += mesh.vertexCount;

				for(int i = 0; i < mesh.vertexCount; ++i)
				{
					verts[tgtVertexOffset] = xform.MultiplyPoint(mesh.vertices[i]);
					uvs[tgtVertexOffset] = mesh.uv[i];

					++tgtVertexOffset;
				}
			}


			//var combiner = new CombineInstance[pair.Value.Count];

			//for(int i = 0; i < combiner.Length; ++i)
			//{
			//	combiner[i].mesh = pair.Value[i].sharedMesh;
			//	combiner[i].transform = pair.Value[i].transform.localToWorldMatrix;
			//	pair.Value[i].gameObject.SetActive(false);
			//}

			var go = new GameObject();
			go.transform.parent = transform;
			var filter = go.AddComponent<MeshFilter>();
			filter.mesh = new Mesh();
			//filter.mesh.CombineMeshes(combiner);
			filter.mesh.vertices = verts;
			filter.mesh.uv = uvs;
			filter.mesh.SetIndices(indices, MeshTopology.Triangles, 0);
			filter.mesh.RecalculateNormals();

			go.AddComponent<MeshRenderer>().material = pair.Key;
		}

		Destroy (child);
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
