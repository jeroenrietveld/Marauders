using UnityEngine;
using System.Collections;

/// <summary>
/// After the player has gone through the menus and press play, this
/// class can be created with an unityScene ID.
/// </summary>
public class StartGame 
{
	public StartGame(int unitySceneId)
	{
		Application.LoadLevel(unitySceneId);
	}
}
