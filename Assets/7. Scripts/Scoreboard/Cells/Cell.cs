using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public abstract class Cell 
{
    public Vector2 pos;
    public Vector2 size;
    public abstract String GetContent();
}