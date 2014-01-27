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


	private static Dictionary<string, GameObject> _gameObjectCache = new Dictionary<string, GameObject>();

	public static GameObject GameObject(string name)
	{
		if(!_gameObjectCache.ContainsKey(name))
		{
			_gameObjectCache.Add(name, Resources.Load(name) as GameObject);
		}

		return UnityEngine.GameObject.Instantiate (_gameObjectCache [name]) as GameObject;
	}
}
