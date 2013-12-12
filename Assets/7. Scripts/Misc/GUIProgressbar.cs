using UnityEngine;
using System.Collections;

public class GUIProgressbar : MonoBehaviour{

    private float _progress;

    public float max { get; set; }
    public float effective { get; set; }

    private Texture2D _progbarEmpty;
    private Texture2D _progbarFull;

    public Vector2 pos { get; set; }
    public Vector2 size { get; set; }

    private Vector2 padding = new Vector2(2, 2);

    void OnGUI()
    {        
        GUI.depth = -1;
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), _progbarEmpty);
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(_progress), size.y), _progbarFull);
    }

    void OnEnable()
    {
        size -= padding;
        pos += 0.5f * padding;
        Debug.Log(size);
    }

    void Start()
    {      
        _progbarEmpty = Resources.Load("Textures/progbar_empty", typeof(Texture2D)) as Texture2D;
        _progbarFull = Resources.Load("Textures/progbar_full", typeof(Texture2D)) as Texture2D;         
    }
	
	// Update is called once per frame
	void Update () {
        _progress = (effective / max);
	}   
}
