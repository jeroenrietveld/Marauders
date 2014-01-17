using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;


public enum CellType
{
    Integer,
    String,
    Percentage,
    Static
}

public abstract class Cell
{
    public String title;

    public object content;
            
    public bool trophysEnabled;

    public CellType cellType;

    public abstract string GetContent();

    public virtual bool HasTrophy()
    {
        return trophysEnabled;
    }
}
