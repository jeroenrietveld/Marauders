using UnityEngine;
using System.Collections;

/// <summary>
/// This class is a test to show how the game can be started.
/// In File -> Build Settings the scene ids are shown. If a certain
/// map is selected in the menu that id will be given into the 
/// constructor. So there must be a List or something where
/// the different scenes are stored.
/// </summary>
public class TestStartGame : MonoBehaviour 
{
	private StartGame startGame;

	void Awake()
	{
		//startGame = new StartGame(1);
		Application.LoadLevel("Bridge.unity");
	}
}
