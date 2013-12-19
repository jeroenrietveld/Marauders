using UnityEngine;
using System.Collections;

public static class TransformHelper {

	/*public static Transform FindInChildren(Transform transform, string name)
	{

	}*/

	public static Transform FindInChildren(this Transform transform, string name)
	{
		if(transform.gameObject.name == name)
		{
			return transform;
		}
		
		for(int i = 0; i < transform.childCount; i++)
		{
			Transform result = FindInChildren(transform.GetChild(i), name);
			if(result)
			{
				return result;
			}
		}
		
		return null;
	}

	public static void DestroyChildren(this Transform transform)
	{
		if (transform != null)
		{
			//Removing all models from the hands
			foreach (Transform t in transform)
			{
				GameObject.Destroy(t.gameObject);
			}

			transform.DetachChildren();
		}
	}
	
}
