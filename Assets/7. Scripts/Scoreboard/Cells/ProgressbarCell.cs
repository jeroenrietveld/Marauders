using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ProgressbarCell : Cell
{
    GUITimeSyncBar progbar;
    public ProgressbarCell(float max) 
    { 
        GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/GUITimeSyncBar")) as GameObject;
        progbar = prefab.GetComponent<GUITimeSyncBar>();
    }

    public void Add(float addition)
    {              
        progbar.Add(addition);
    }

    public override String GetContent()
    {
        progbar.pos = this.pos;
        progbar.size = this.size;
        return null;
    }
}