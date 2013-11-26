﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SimpleJSON;
using System.IO;
using UnityEngine;

/// <summary>
/// This class holds information about levels.
/// </summary>
class Level
 {
     public string levelName;
     public string[] gameModes;
     public Texture2D previewImage;
     public string levelInfo;

    /// <summary>
    /// A constructor holding all data.
    /// </summary>
    /// <param name="levelName">The name of the level.</param>
    /// <param name="gameModes">All the possible game modes.</param>
    /// <param name="previewImagePath">The path to the previewImage.</param>
    /// <param name="levelInfo">The info of the level.</param>
     public Level(string levelName, string[] gameModes, Texture2D previewImage, string levelInfo)
     {
         this.levelName = levelName;
         this.gameModes = gameModes;
         this.previewImage = previewImage;
         this.levelInfo = levelInfo;
     }

 }
