using System;
using UnityEngine;

public class Menu2DComponent
{
    public Texture texture { get; set; }
    public Rect position { get; set; }

    public Menu2DComponent(Texture texture, Rect position)
    {
        this.texture = texture;
        this.position = position;
    }
}
