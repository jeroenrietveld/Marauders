using System;
using System.Collections.Generic;
using UnityEngine;

public class CharacterSelectGlobal : MonoBehaviour
{
    public List<Menu2DComponent> components;

    void Awake()
    {
        components = new List<Menu2DComponent>();
        Rect position = new Rect(0,0, 100, 233);
        Menu2DComponent player1_box = new Menu2DComponent(Resources.Load("Textures/select_14") as Texture, position);
        components.Add(player1_box);
    }

    void OnGUI()
    {
        GUI.depth = 0;

        foreach(Menu2DComponent component in components)
        {       
            GUI.DrawTexture(component.position, component.texture);
        }
    }
}
