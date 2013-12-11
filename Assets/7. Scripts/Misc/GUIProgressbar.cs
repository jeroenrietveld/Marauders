using UnityEngine;
using System.Collections;

public class GUIProgressbar : GUIElement{

    private float _progress;

    public float max { get; set; }
    public float effective { get; set; }

    private Texture2D _progbarEmpty;
    private Texture2D _progbarFull;

    public Vector2 pos { get; set; }
    public Vector2 size { get; set; }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(pos.x-4, pos.y-4, size.x+8, size.y+8), _progbarEmpty);
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(_progress), size.y), _progbarFull);
    }

    void Start()
    {      
        _progbarEmpty = Resources.Load("Textures/progbar_empty", typeof(Texture2D)) as Texture2D;
        _progbarFull = Resources.Load("Textures/progbar_full", typeof(Texture2D)) as Texture2D;
        _progress = 1;
        max = 0;
        effective = 0;              
    }
	
	// Update is called once per frame
	void Update () {
        _progress = (effective / max);
	}

    public GUIContent GetGUIContent()
    {
        GUIContent content = new GUIContent();
        Texture2D texture = new Texture2D((int)size.x + 8, (int)size.y + 8);

        int i = 0;
        int j = 0;
        while (i < size.y)
        {
            while (j < size.x)
            {
                Color c = j < size.x * Mathf.Clamp01(_progress) ? _progbarFull.GetPixel(j, i) : _progbarEmpty.GetPixel(j, i);
                texture.SetPixel(j, i, c);
                j++;
            }
            i++;
        }

        content.image = texture;
        return content;
    }
}
