using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class ProgressbarCell : Cell
{
    GUIProgressbar progbar;
    public ProgressbarCell(float max) 
    { 
        GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/GUIProgressbar")) as GameObject;
        progbar = prefab.GetComponent<GUIProgressbar>();
        progbar.effective = 0f;
        progbar.max = max;       
    }

    public void Add(float addition)
    {              
        progbar.effective = Mathf.Clamp(progbar.effective + addition, 0, progbar.max);
    }

    public override String GetContent()
    {
        progbar.pos = this.pos;
        progbar.size = this.size;
        return null;
    }
}