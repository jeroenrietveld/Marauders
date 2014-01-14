using UnityEngine;
using System.Collections;
using System;

public class FlipPolygons : MonoBehaviour {

	// Use this for initialization
	void Awake()
	{
		Mesh mesh = GetComponent<MeshCollider>().mesh;
		if(mesh != null)
		{
			int[] triangles = mesh.triangles;
			Array.Reverse(triangles);
			mesh.triangles = triangles;
		}
	}
}
