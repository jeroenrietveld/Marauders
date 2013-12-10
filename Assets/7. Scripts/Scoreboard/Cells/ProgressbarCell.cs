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
        GameObject prefab = GameObject.Instantiate(Resources.Load("Prefabs/progbar")) as GameObject;
        progbar = prefab.GetComponent<GUIProgressbar>();
        progbar.effective = 0f;
        progbar.max = max;
        progbar.pos = new Vector2(prefab.transform.position.x, prefab.transform.position.y);
        progbar.size = new Vector2(20, 10);
    }

    public void Add(float addition)
    {
        progbar.effective += addition;
    }
}