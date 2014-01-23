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

    public float fontScale = (float)Screen.height / 768f;

    private Rect _scoreboardRect;

    private Material _material;
    private Texture _texture;

    //Trophytextures
    private Texture _redTrophyTexture;
    private Texture _blueTrophyTexture;
    private Texture _purpleTrophyTexture;
    private Texture _greenTrophyTexture;
    private Texture _currentTrophyTexture;

    //Placetextures
    private Texture _firstPlaceTexture;
    private Texture _secondPlaceTexture;
    private Texture _thirdPlaceTexture;
    private Texture _fourthPlaceTexture;
    private Texture _currentPlaceTexture;

    public List<Trophy> trophyList;

    public Scoreboard()
	{
        trophyList = new List<Trophy>();
		_cells = new List<List<Cell>> ();
	}

    void Awake()
    {
        DontDestroyOnLoad(this);
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
    public void AddGameSpecificCell(int index, CustomCell gameSpecificCell)
    {                
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
        _material = Resources.Load("Materials/Cooldown", typeof(Material)) as Material;
        _texture = Resources.Load("Textures/Cooldown", typeof(Texture)) as Texture;

        _redTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_red", typeof(Texture)) as Texture;
        _blueTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_blue", typeof(Texture)) as Texture;
        _greenTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_green", typeof(Texture)) as Texture;
        _purpleTrophyTexture = Resources.Load("Textures/Scoreboard/trophy_purple", typeof(Texture)) as Texture;

        _firstPlaceTexture = Resources.Load("Textures/Scoreboard/place_1st", typeof(Texture)) as Texture;
        _secondPlaceTexture = Resources.Load("Textures/Scoreboard/place_2nd", typeof(Texture)) as Texture;
        _thirdPlaceTexture = Resources.Load("Textures/Scoreboard/place_3rd", typeof(Texture)) as Texture;
        _fourthPlaceTexture = Resources.Load("Textures/Scoreboard/place_4th", typeof(Texture)) as Texture;

        trophyList = new List<Trophy>();
        _visible = false;
        
        #region Test - all comments atm
        /*
        GameManager.Instance.matchSettings.timeSync = 100;
        
        Scoreboard scoreboard = GameObject.Find("Scoreboard").GetComponent<Scoreboard>();

        #region Player 1
        var player = new Player(PlayerIndex.One);
        List<Cell> addition = new List<Cell>();
        player.timeSync = 23;
        scoreboard.AddCellList(addition);

        addition.Add(new PlayerCell(player));

        //Declare all cells
        TimeSyncCell timeSync = new TimeSyncCell(player);
        TitlesCell titleCell = new TitlesCell();
        CustomCell eliminations = new CustomCell("Eliminations", CellType.Integer, 10, true);
        CustomCell eliminated = new CustomCell("Eliminated", CellType.Integer, 9, true);
        CustomCell suicides = new CustomCell("Suicides", CellType.Integer, 1, true);

        //Testing timeSync
        timeSync.content = 0;

        //Add all cells to the list of cells
        addition.Add(timeSync);
        addition.Add(titleCell);
        addition.Add(eliminations);
        addition.Add(eliminated);
        addition.Add(suicides);

        #endregion
    
        #region Player 2
        var player2 = new Player(PlayerIndex.Two);
        player2.timeSync = 26;
        List<Cell> addition2 = new List<Cell>();
        scoreboard.AddCellList(addition2);

        addition2.Add(new PlayerCell(player2));

        //Declare all cells
        TimeSyncCell timeSync2 = new TimeSyncCell(player2);
        TitlesCell titleCell2 = new TitlesCell();
        CustomCell eliminations2 = new CustomCell("Eliminations", CellType.Integer, 7, true);
        CustomCell eliminated2 = new CustomCell("Eliminated", CellType.Integer, 5, true);
        CustomCell suicides2 = new CustomCell("Suicides", CellType.Integer, 7, true);

        //Add all cells to the list of cells
        addition2.Add(timeSync2);
        addition2.Add(titleCell2);
        addition2.Add(eliminations2);
        addition2.Add(eliminated2);
        addition2.Add(suicides2);

        #endregion

        #region Player 3
        var player3 = new Player(PlayerIndex.Three);
        player3.timeSync = 20;
        List<Cell> addition3 = new List<Cell>();
        scoreboard.AddCellList(addition3);

        addition3.Add(new PlayerCell(player3));

        //Declare all cells
        TimeSyncCell timeSync3 = new TimeSyncCell(player3);
        TitlesCell titleCell3 = new TitlesCell();
        CustomCell eliminations3 = new CustomCell("Eliminations", CellType.Integer, 7, true);
        CustomCell eliminated3 = new CustomCell("Eliminated", CellType.Integer, 5, true);
        CustomCell suicides3 = new CustomCell("Suicides", CellType.Integer, 7, true);

        //Add all cells to the list of cells
        addition3.Add(timeSync3);
        addition3.Add(titleCell3);
        addition3.Add(eliminations3);
        addition3.Add(eliminated3);
        addition3.Add(suicides3);

        #endregion

        for (int i = 0; i < 3; i++)
        {
            scoreboard.AddGameSpecificCell(i, new CustomCell("Owned Shrines", CellType.Integer, 0, true));
        }

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
        GameManager.Instance.AddPlayerRef(player);
        GameManager.Instance.AddPlayerRef(player2);
        GameManager.Instance.AddPlayerRef(player3);

        scoreboard.AddContent(PlayerIndex.Three, Locale.Current["scoreboard_ownedshrines"], 3);
        scoreboard.AddContent(PlayerIndex.One, Locale.Current["scoreboard_timesync"], player.timeSync);
        scoreboard.AddContent(PlayerIndex.Two, Locale.Current["scoreboard_timesync"], player2.timeSync);
        scoreboard.AddContent(PlayerIndex.Three, Locale.Current["scoreboard_timesync"], player3.timeSync);

        scoreboard.Show();
        scoreboard.CalculateTrophys();
         */
        #endregion
       
    }

   void OnGUI()
    {
        if (_cells.Count > 0)
        {
            if (_visible)
            {
                if (UnityEngine.Event.current.type != EventType.Repaint)
                {
                    //This prevents the drawing in the 3D scape.
                    //Graphics.DrawTexture does this.
                    return;
                }
                _scoreboardRect = new Rect(20, 20, Screen.width - 40, Screen.height - 40);
                GUI.skin = scoreboardskin;
                _scoreboardRect = GUI.Window(0, _scoreboardRect, DrawScoreboard, "");
            }
        }
    }

   void DrawScoreboard(int windowID)
   {
       float boxWidth = _scoreboardRect.width - horizontalOffset*2;
       float boxHeight = _scoreboardRect.height / 4f - verticalOffset*2;

       int rows = _cells.Count;
       int columns = _cells[0].Count;

       float cellWidth = boxWidth/columns;
       float cellTop = boxHeight*0.1f;
       float cellHeight = boxHeight * 0.8f;

       float titleOffset = cellHeight * 0.25f;

       float fontScale = Screen.width / 768f;

       GUI.contentColor = Color.black;

       GUI.skin.label.fontSize = (int) (10 * fontScale);

       List<Player> playersByTimeSync = GameManager.Instance.playersByTimeSync();
       for (int i = 0; i < rows; i++)
       {
           GUI.Box(new Rect(horizontalOffset, verticalOffset + i * (boxHeight + verticalOffset * 2), boxWidth, boxHeight), "");
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
                   int index = playersByTimeSync.IndexOf(((PlayerCell)_cells[i][j]).player);
                   switch(index)
                   {
                       case 0: _currentPlaceTexture = _firstPlaceTexture; break;
                       case 1: _currentPlaceTexture = _secondPlaceTexture; break;
                       case 2: _currentPlaceTexture = _thirdPlaceTexture; break;
                       case 3: _currentPlaceTexture = _fourthPlaceTexture; break;
                       default: _currentPlaceTexture = _firstPlaceTexture; break;
                   }
                   Graphics.DrawTexture(new Rect(j * cellWidth + horizontalOffset + horizontalTextureOffset, i * (boxHeight + verticalOffset * 2) + titleOffset + verticalTextureOffset, textureSize, textureSize), _currentPlaceTexture);
                   _currentColor = ((PlayerCell)_cells[i][j]).player.color;
               } 
               else if(_cells[i][j] is TimeSyncCell)
               {
                   //TimeSync texture here
                   float percentage = (int)_cells[i][j].content / (float)GameManager.Instance.matchSettings.timeSync;
                   _material.SetFloat("phase", percentage);
                   _material.SetColor("playerColor", _currentColor);
                   float matSize = Math.Min(cellWidth, cellHeight) * 0.75f;
                   float horizontalMatOffset = (cellWidth - matSize) / 2f;
                   float verticalMatOffset = cellHeight * 0.2f;
                   Graphics.DrawTexture(new Rect(j * cellWidth + horizontalOffset + horizontalMatOffset, i * (boxHeight + verticalOffset * 2) + titleOffset + verticalMatOffset, matSize, matSize), _texture, _material);
                   
                   scoreboardskin.label.alignment = TextAnchor.MiddleCenter;
                   GUI.Label(new Rect(j * cellWidth + horizontalOffset, i * (boxHeight + verticalOffset * 2) + 0.5f*(cellTop) + titleOffset, cellWidth, cellHeight), _cells[i][j].GetContent() + "%");
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
                       float verticalTrophyOffset = cellHeight * 0.8f;
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
           int index = _cells[0].IndexOf(FindCell(PlayerIndex.One, trophy.Column));

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
                                   break;
                               }
                               if ((int)_cells[i][index].content == (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Add(i);
                               }
                               if ((int)_cells[i][index].content < (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Clear();
                                   winnerIndexes.Add(i);
                               }
                               break;
                           case ">":
                               if (winnerIndexes.Count == 0)
                               {
                                   winnerIndexes.Add(i);
                                   break;
                               }
                               if ((int)_cells[i][index].content == (int)_cells[winnerIndexes[0]][index].content)
                               {
                                   winnerIndexes.Add(i);
                               }
                               if ((int)_cells[i][index].content > (int)_cells[winnerIndexes[0]][index].content)
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

    public void SetTrophy(PlayerIndex playerIndex, string cellName, string title)
    {
        //Find the cell to set the trophy
        for(int i = 0; i < _cells.Count; i++)
        {
            //First, find the player
            if(((PlayerCell)_cells[i][0]).player.index == playerIndex)
            {
                //We found the player, now find the cellname
                for(int j = 0; j < _cells[i].Count; j++)
                {
                    if(_cells[i][j].title.Equals(cellName))
                    {
                        //This is the cell we want to add the trophy to
                        ((CustomCell)_cells[i][j]).hasTrophy = true;
                        _cells[i][2].content += title;
                        break;
                    }
                }
            }
        }
   }

    public void SetTrophy(PlayerIndex index, Trophy trophy)
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
                    if (_cells[i][j].title.Equals(trophy.Column))
                    {
                        //This is the cell we want to add the trophy to
                        ((CustomCell)_cells[i][j]).hasTrophy = true;
                        _cells[i][2].content += trophy.Title;
                        break;
                    }
                }
            }
        }
    }

    public void SetContent(PlayerIndex index, string cellname, object content)
    {
        FindCell(index, cellname).content = content;
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
        _cells.Clear();
    }

    public bool IsVisible()
    {
        return _visible;
    }

    void Update()
    {
        if(GameManager.Instance.gameEnded)
        {
            if (ControllerInput.GetController(PlayerIndex.One).JustPressed(Button.A))
            {
                this.Hide();
                this.Clear();
                Application.LoadLevel("Menu");
            }
        }
    }
}

