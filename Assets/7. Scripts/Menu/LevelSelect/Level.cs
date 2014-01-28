using System;
using UnityEngine;

public struct Level
{
	public string levelName;
	public string[] gameModes;
	public Texture2D previewImage;
	public string levelInfo;
    public string groundType;
    public string objective;

	public Level(string levelName, string[] gameModes, Texture2D previewImage, string levelInfo, string groundType, string objective)
	{
		this.levelName = levelName;
		this.gameModes = gameModes;
		this.previewImage = previewImage;
		this.levelInfo = levelInfo;
        this.groundType = groundType;
        this.objective = objective;
	}
}