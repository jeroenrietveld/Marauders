using UnityEngine;
using System.Collections;

public class GUIProgressbar : MonoBehaviour {

    private float _progress;
    private float _maximum;
    private float _effective;

    public float max { get; set; }
    public float effective { get; set; }

    public Texture2D progbarEmpty;
    public Texture2D progbarFull;

    public Vector2 pos { get; set; }
    public Vector2 size { get; set; }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), progbarEmpty);
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(_progress), size.y), progbarFull);
    }

    void Start()
    {      
        progbarEmpty = Resources.Load("Textures/progbar_empty", typeof(Texture2D)) as Texture2D;
        progbarFull = Resources.Load("Textures/progbar_full", typeof(Texture2D)) as Texture2D;
        _progress = 1;
        max = 50;
        effective = 0;
    }
	
	// Update is called once per frame
	void Update () {
        effective += Time.deltaTime*2f;
        _progress = (effective / max);
	}
}
