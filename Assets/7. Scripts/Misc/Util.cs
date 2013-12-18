using UnityEngine;
using System.Collections;

public static class Util {

	public static Transform FindInChildren(Transform transform, string name)
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

}
