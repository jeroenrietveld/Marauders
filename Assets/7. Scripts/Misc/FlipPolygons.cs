using UnityEngine;
using System.Collections;
using System;

public class FlipPolygons : MonoBehaviour {

	// Use this for initialization
	void Awake()
	{
		Mesh mesh = GetComponent<MeshFilter>().mesh;

		int[] triangles = mesh.triangles;
		Array.Reverse(triangles);
		mesh.triangles = triangles;

	}
}
