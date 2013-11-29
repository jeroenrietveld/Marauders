﻿using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class LevelSelectionManager : MonoBehaviour {

	public static List<Level> levels;
	public static Dictionary<LevelSelectionState, LevelSelectionBlockBase> selectionBlocks;
	public static LevelSelectionBlockBase currentState;

	// Use this for initialization
	void Start () {
		levels = new List<Level>();
		selectionBlocks = new Dictionary<LevelSelectionState, LevelSelectionBlockBase> ();

		//Check if the directory exists
		if(Directory.Exists("./Assets/7. Scripts/Levels/"))
		{
			//Get all the files with an extension .json from the Levels map.
			string[] files = Directory.GetFiles("./Assets/7. Scripts/Levels/", "*.json");		
			
			//Read the JSON files and create the levels
			foreach(string file in files)
			{
				string jsonInfo = File.ReadAllText(file);
				var node = SimpleJSON.JSON.Parse(jsonInfo);
				var levelName = node["levelname"].Value;
				var gameModes = node["gamemodes"].AsArray;
				var previewImagePath = node["previewImagePath"].Value;
				var levelInfo = node["levelinfo"].Value;
				
				//the gameModes array is still a JSONArray. SimpleJSON does not support easy 
				//conversion to normal string arrays, therefore we need to do it the ugly way.
				string[] actualGameModes = new string[gameModes.Count];
				for(int i = 0; i < gameModes.Count; i++)
				{
					actualGameModes[i] = node["gamemodes"][i];
				}
				Texture2D previewImage = (Texture2D) Resources.Load(previewImagePath);
				
				//Finally, add the level with its info to the level list.
				levels.Add(new Level(levelName, actualGameModes, previewImage , levelInfo));
			}
		} else 
		{
			throw new DirectoryNotFoundException();
		}
		
		selectionBlocks.Add (LevelSelectionState.LevelSelection, new LevelSelectionBlock ());
		selectionBlocks.Add (LevelSelectionState.SettingSelection, new SettingsBlock ());
		currentState = selectionBlocks [LevelSelectionState.LevelSelection];
	}
	
	public static void ChangeState(LevelSelectionState state)
	{
		currentState = selectionBlocks [state];
	}
}
