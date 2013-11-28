using System;
using UnityEngine;

public struct Level
{
	public string levelName;
	public string[] gameModes;
	public Texture2D previewImage;
	public string levelInfo;

	public Level(string levelName, string[] gameModes, Texture2D previewImage, string levelInfo)
	{
		this.levelName = levelName;
		this.gameModes = gameModes;
		this.previewImage = previewImage;
		this.levelInfo = levelInfo;
	}
}