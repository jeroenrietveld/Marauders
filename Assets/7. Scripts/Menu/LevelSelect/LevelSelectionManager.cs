using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

public class LevelSelectionManager : MonoBehaviour {

	public static List<Level> levels;
	public static Dictionary<LevelSelectionState, LevelSelectionBlockBase> selectionBlocks;
	public static LevelSelectionBlockBase currentState;

    private static string _resourcePath = "Materials/Levels";

	// Use this for initialization
	void Start() {
		levels = new List<Level>();
		selectionBlocks = new Dictionary<LevelSelectionState, LevelSelectionBlockBase> ();

		//Get all the files from the Levels map.
        var resources = Resources.LoadAll(_resourcePath);

		//Read the JSON files and create the levels
        foreach(object resource in resources)
        {
			var node = SimpleJSON.JSON.Parse(((TextAsset)resource).text);
			var levelName = node["levelname"].Value;
			var gameModes = node["gamemodes"].AsArray;
			var previewImagePath = node["previewImagePath"].Value;
			var levelInfo = node["levelinfo"].Value;
            var groundtype = node["groundType"].Value;
			//the gameModes array is still a JSONArray. SimpleJSON does not support easy 
			//conversion to normal string arrays, therefore we need to do it the ugly way.
			string[] actualGameModes = new string[gameModes.Count];
			for(int i = 0; i < gameModes.Count; i++)
			{
				actualGameModes[i] = node["gamemodes"][i];
			}
			Texture2D previewImage = (Texture2D) Resources.Load(previewImagePath);
				
			//Finally, add the level with its info to the level list.
			levels.Add(new Level(levelName, actualGameModes, previewImage , levelInfo, groundtype));
		}
		
		selectionBlocks.Add (LevelSelectionState.LevelSelection, new LevelSelectionBlock ());
		selectionBlocks.Add (LevelSelectionState.SettingSelection, new SettingsBlock ());
        selectionBlocks.Add (LevelSelectionState.NotSelecting, null);
        currentState = selectionBlocks[LevelSelectionState.LevelSelection];
	}
	
	public static void ChangeState(LevelSelectionState state)
	{
		currentState = selectionBlocks [state];
	}
}
