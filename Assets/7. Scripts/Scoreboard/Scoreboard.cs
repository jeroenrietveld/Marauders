using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using XInputDotNetPure;
 
public class Scoreboard : MonoBehaviour
{
    private bool _visible;
	private List<List<Cell>> _cells;
    public GUISkin scoreboardskin;

    private Color _currentColor;

    public float horizontalOffset = 10f;
    public float verticalOffset = 5f;

    private Rect _scoreboardRect;

    private Material _material;
    private Texture _texture;

    //Trophytextures
    private Texture _redTrophyTexture;
    private Texture _blueTrophyTexture;
    private Texture _purpleTrophyTexture;
    private Texture _greenTrophyTexture;
    private Texture _currentTrophyTexture;

    //Maraudertextures
    private Texture _samuraiTexture;
    private Texture _thiefTexture;
    private Texture _juggernautTexture;
    private Texture _currentTexture;

    public List<Trophy> trophyList;

    public Scoreboard scoreboard;

    private void Initialize() 
    {      
        //Set the scoreboard
        if(scoreboard == null)
        {
            scoreboard = GameObject.Find("_GLOBAL").GetComponent<Scoreboard>();
        }
        else
        {
            scoreboard.Clear();
            Initialize();
        }

        //Set all the textures and materials
        _material = Resources.Load("Materials/Cooldown", typeof(Material)) as Material;
        _texture = Resources.Load("Textures/Cooldown", typeof(Texture)) as Texture;

        _redTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_red", typeof(Texture)) as Texture;
        _blueTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_blue", typeof(Texture)) as Texture;
        _greenTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_green", typeof(Texture)) as Texture;
        _purpleTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_purple", typeof(Texture)) as Texture;

        _samuraiTexture = Resources.Load("Textures/Scoreboard/samurai", typeof(Texture)) as Texture;
        _thiefTexture = Resources.Load("Textures/Scoreboard/thief", typeof(Texture)) as Texture;
        _juggernautTexture = Resources.Load("Textures/Scoreboard/juggernaut", typeof(Texture)) as Texture;

        //Add the main cells.
        foreach (var player in GameManager.Instance.playerRefs)
        {
            List<Cell> addition = new List<Cell>();
            AddCellList(addition);

            addition.Add(new PlayerCell(player));

            //Declare all cells
            TimeSyncCell timeSync = new TimeSyncCell(player);
            TitlesCell titles = new TitlesCell();
            CustomCell eliminations = new CustomCell("Eliminations", CellType.Integer, 0, true);
            CustomCell eliminated = new CustomCell("Eliminated", CellType.Integer, 0, true);
            CustomCell suicides = new CustomCell("Suicides", CellType.Integer, 0, true);

            //Add all cells to the list of cells
            addition.Add(timeSync);
            addition.Add(titles);
            addition.Add(eliminations);
            addition.Add(eliminated);
            addition.Add(suicides);

            //Add the game specific cells
            switch(GameManager.Instance.matchSettings.gameMode.id)
            {
                case GameModeID.Normal: AddGameSpecificCell(player.index, new CustomCell("Owned Shrines", CellType.Integer, 0, true)); break;
            }
        }
        

        //Set trophys
        var resources = Resources.LoadAll("JSON/Trophy");

        foreach (object resource in resources)
        {
            var node = SimpleJSON.JSON.Parse(((TextAsset)resource).text);
            string column = node["column"].Value;
            string title = node["title"].Value;
            string condition = node["condition"].Value;

            Trophy t = new Trophy();
            t.Column = column;
            t.Title = title;
            t.Condition = condition;
            trophyList.Add(t);
        }

        //Done
    }

    /// <summary>
    /// This method will add a cell list to the end of the list
    /// </summary>
    /// <param name="list"></param>
	public void AddCellList(List<Cell> list)
	{
		_cells.Add (list);
	}   

    /// <summary>
    /// This method will add a custom cell just after the static cells.
    /// </summary>
    /// <param name="gameSpecificCell"></param>
    public void AddGameSpecificCell(PlayerIndex playerIndex, CustomCell gameSpecificCell)
    {
        int index = 0;
        for (int i = 0; i < _cells.Count; i++)
        {
            if (((PlayerCell)_cells[i][0]).player.index == playerIndex)
            {
                index = i;
            }
        }
        for (int j = 0; j < _cells[index].Count; j++)
        {
            if (_cells[index][j].cellType != CellType.Static)
            {
                //This spot is the first spot after the static cells
                _cells[index].Insert(j, gameSpecificCell);
                break;
            }
        }
    }

    void Start()
    {
        _cells = new List<List<Cell>>();
        trophyList = new List<Trophy>();            

        _visible = false;

         Initialize();             
    }

   void OnGUI()
    {
        if (_visible)
        {
            if (_cells.Count > 0)
            {

                if (UnityEngine.Event.current.type != EventType.Repaint)
                {
                    //This prevents the drawing in the 3D scape.
                    //Graphics.DrawTexture does this.
                    return;
                }

                //Set the scoreboard rectangle
                _scoreboardRect = new Rect(20, 20, Screen.width - 40, Screen.height - 40);

                //Set the skin
                GUI.skin = scoreboardskin;

                //Actually paint the scoreboard
                _scoreboardRect = GUI.Window(0, _scoreboardRect, DrawScoreboard, "");
            }
        }
    }

   void DrawScoreboard(int windowID)
   {
       #region Scaling variables
       float boxWidth = _scoreboardRect.width - horizontalOffset*2;
       float boxHeight = _scoreboardRect.height / 4f - verticalOffset*2;

       int rows = _cells.Count;
       int columns = _cells[0].Count;

       float cellWidth = boxWidth/columns;
       float cellTop = boxHeight*0.1f;
       float cellHeight = boxHeight * 0.8f;

       float titleOffset = cellHeight * 0.25f;

       float fontScale = Screen.width / 768f;
       #endregion

       GUI.contentColor = Color.black;

       GUI.skin.label.fontSize = (int) (10 * fontScale);

       List<Player> playersByTimeSync = GameManager.Instance.playersByTimeSync();

       for (int i = 0; i < rows; i++)
       {
           //Draw a transparent background
           GUI.Box(new Rect(horizontalOffset, verticalOffset + i * (boxHeight + verticalOffset * 2), boxWidth, boxHeight), "");
           
           //Loop through each cell
           for (int j = 0; j < columns; j++)
           {            
               //Draw the title
               GUI.Label(new Rect(j * cellWidth + horizontalOffset, i * (boxHeight + verticalOffset*2) + cellTop, cellWidth, cellHeight), _cells[i][j].title);
               
               //Draw the content
               if (_cells[i][j] is TitlesCell)
               {
                   scoreboardskin.label.alignment = TextAnchor.UpperLeft;
                   GUI.contentColor = _currentColor;
                   GUI.Label(new Rect(j * cellWidth + horizontalOffset, i * (boxHeight + verticalOffset * 2) + cellTop + titleOffset, cellWidth, cellHeight), _cells[i][j].GetContent());
                   GUI.contentColor = Color.black;
                   scoreboardskin.label.alignment = TextAnchor.UpperCenter;
               } 
               else if(_cells[i][j] is PlayerCell)
               {
                   float textureSize = Math.Min(cellWidth, cellHeight) * 0.8f;
                   float horizontalTextureOffset = (cellWidth - textureSize) / 2;
                   float verticalTextureOffset = cellHeight * 0.15f;
                   switch((string)((PlayerCell)_cells[i][j]).content)
                   {
                       case "Samurai": _currentTexture = _samuraiTexture; break;
                       case "Thief": _currentTexture = _thiefTexture; break;
                       case "Juggernaut": _currentTexture = _juggernautTexture; break;
                   }
                   Graphics.DrawTexture(new Rect(j * cellWidth + horizontalOffset + horizontalTextureOffset, i * (boxHeight + verticalOffset * 2) + titleOffset + verticalTextureOffset, textureSize, textureSize), _currentTexture);
                   _currentColor = ((PlayerCell)_cells[i][j]).player.color;
               } 
               else if(_cells[i][j] is TimeSyncCell)
               {
                   //TimeSync texture here
                   float percentage = (int)((TimeSyncCell)_cells[i][j]).player.timeSync / (float)GameManager.Instance.matchSettings.timeSync;
                   _material.SetFloat("phase", percentage);
                   _material.SetColor("playerColor", _currentColor);
                   float matSize = Math.Min(cellWidth, cellHeight) * 0.75f;
                   float horizontalMatOffset = (cellWidth - matSize) / 2f;
                   float verticalMatOffset = cellHeight * 0.15f;
                   Graphics.DrawTexture(new Rect(j * cellWidth + horizontalOffset + horizontalMatOffset, i * (boxHeight + verticalOffset * 2) + titleOffset + verticalMatOffset, matSize, matSize), _texture, _material);
                   
                   scoreboardskin.label.alignment = TextAnchor.MiddleCenter;
                   GUI.Label(new Rect(j * cellWidth + horizontalOffset, i * (boxHeight + verticalOffset * 2) + 0.20f*(cellTop) + titleOffset, cellWidth, cellHeight), (int)(percentage*100f) + "%");
                   scoreboardskin.label.alignment = TextAnchor.UpperCenter;
               }
               else 
               {
                   GUI.Label(new Rect(j * cellWidth + horizontalOffset, i * (boxHeight + verticalOffset * 2) + cellTop + titleOffset, cellWidth, cellHeight), _cells[i][j].GetContent());
                   if(_cells[i][j].HasTrophy())
                   {
                       switch(((PlayerCell)_cells[i][0]).player.index)
                       {
                           case PlayerIndex.One: _currentTrophyTexture = _redTrophyTexture; break;
                           case PlayerIndex.Two: _currentTrophyTexture = _blueTrophyTexture; break;
                           case PlayerIndex.Three: _currentTrophyTexture = _greenTrophyTexture; break;
                           case PlayerIndex.Four: _currentTrophyTexture = _purpleTrophyTexture; break;
                       }
                       float trophySize = Math.Min(cellWidth * 0.4f, cellHeight * 0.4f);
                       float horizontalTrophyOffset = (cellWidth - trophySize) / 2f;
                       float verticalTrophyOffset = cellHeight * 0.70f;
                       Graphics.DrawTexture(new Rect(j * cellWidth + horizontalOffset + horizontalTrophyOffset, i * (boxHeight + verticalOffset * 2) + verticalTrophyOffset, trophySize, trophySize), _currentTrophyTexture);
                   }
               }             
           }
       }  
    }

    public void CalculateTrophys()
    {
       foreach(Trophy trophy in trophyList)
       {
           int index = _cells[0].IndexOf(FindCell(GameManager.Instance.playerRefs[0].index, trophy.Column));

           //Make a list to hold multiple winners
           List<int> winnerIndexes = new List<int>();

           if (index != -1)
           {
               for (int i = 0; i < _cells.Count; i++)
               {
                   if (_cells[i][index].cellType == CellType.Integer)
                   {
                       switch (trophy.Condition)
                       {
                           case "<":
                               if(winnerIndexes.Count == 0)
                               {
                                   winnerIndexes.Add(i);
                               }
                               else if ((int)_cells[i][index].content == (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Add(i);
                               }
                               else if ((int)_cells[i][index].content < (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Clear();
                                   winnerIndexes.Add(i);
                               }
                               break;
                           case ">":
                               if (winnerIndexes.Count == 0)
                               {
                                   winnerIndexes.Add(i);
                               }
                               else if ((int)_cells[i][index].content == (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Add(i);
                               }
                               else if ((int)_cells[i][index].content > (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Clear();
                                   winnerIndexes.Add(i);
                               }
                               break;
                       }
                   }
               }
               foreach (int winner in winnerIndexes)
               {
                   ((CustomCell)_cells[winner][index]).hasTrophy = true;
                   ((TitlesCell)_cells[winner][2]).AddTitle(trophy.Title);                 
               }

               //Clear the list for the next trophy calculation
               winnerIndexes.Clear();
           }
       }
    }

    public void AddContent(PlayerIndex index, string cellname, object content)
    {
        Cell cell = FindCell(index, cellname);
        switch(cell.cellType)
        {           
            case CellType.String:
                string tempString = (string)cell.content;
                tempString += (string)content;
                cell.content = tempString;
                break;
            default:
                int tempInt = (int)cell.content;
                tempInt += (int)content;
                cell.content = tempInt;
                break;
        }
    }

    public Cell FindCell(PlayerIndex index, string cellname)
    {
        //Find the cell to set the trophy
        for (int i = 0; i < _cells.Count; i++)
        {
            //First, find the player
            if (((PlayerCell)_cells[i][0]).player.index == index)
            {
                //We found the player, now find the cellname
                for (int j = 0; j < _cells[i].Count; j++)
                {
                    if (_cells[i][j].title.Equals(cellname))
                    {
                        return _cells[i][j];
                    }
                }
            }
        }
        return null;
    }

    public void Show()
    {
        _visible = true;
    }

    public void Hide()
    {
        _visible = false;
    }

    public void Clear()
    {
        //If there might be any leftovers of the scoreboard, destroy them
        scoreboard = null;
        Destroy(this);      
    }
}

