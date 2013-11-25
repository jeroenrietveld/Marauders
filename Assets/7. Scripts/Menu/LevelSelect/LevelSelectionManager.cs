using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

/// <summary>
/// A static class being made when the game starts. This class will load all levels, including their info and pictures.
/// </summary>
static class LevelSelectionManager
{
    public static List<Level> levels;
    public static LevelSelectionState currentState;

    /// <summary>
    /// The constructor.
    /// </summary>
    public static void Initialize()
    {
        levels = new List<Level>();
        currentState = LevelSelectionState.LevelSelection;
        //Get all the files with an extension .json from the Levels map.
        string[] files = Directory.GetFiles("../Levels/", "*.json");

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

            //Finally, add the level with its info to the level list.
            levels.Add(new Level(levelName, actualGameModes, previewImagePath, levelInfo));
        }
    }
}
