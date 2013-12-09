using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

/// <summary>
/// This class holds an instance of the ScoreboardManager and is used to hold a list containing all scoreboards.
/// </summary>
public class ScoreboardManager
{
    public List<Scoreboard> scoreboards;
    private static ScoreboardManager _instance;

    public static ScoreboardManager Instance
    {
        get
        {
            if (_instance == null)
            {
                _instance = new ScoreboardManager();
            }
            return _instance;
        }
    }

    private ScoreboardManager()
    {
        this.scoreboards = new List<Scoreboard>();
    }
}
