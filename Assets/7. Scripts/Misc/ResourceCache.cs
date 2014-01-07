using UnityEngine;
using System.Collections.Generic;

using SimpleJSON;

public static class ResourceCache {

	private static Dictionary<string, JSONNode> _jsonCache = new Dictionary<string, JSONNode>();

	public static JSONNode json(string documentName)
	{
		if (_jsonCache.ContainsKey (documentName))
		{
			return _jsonCache[documentName];
		}
		else
		{
			var jsonText = Resources.Load(documentName) as TextAsset;
			if(!jsonText) throw new System.ArgumentException("Unable to load file " + documentName);

			var node = JSON.Parse(jsonText.text);
			_jsonCache[documentName] = node;
			return node;
		}
	}
}
