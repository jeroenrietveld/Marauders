using UnityEngine;
using System.Collections;

public class GUIProgressbar : MonoBehaviour{

    private float _progress;

    public float max { get; set; }
    public float effective { get; set; }

    private Texture2D _progbarEmpty;
    private Texture2D _progbarFull;

    public Vector3 pos { get; set; }
    public Vector3 size { get; set; }

    void OnGUI()
    {
        pos = this.transform.position;
        var newPos = Camera.main.WorldToScreenPoint(pos);
        GUI.DrawTexture(new Rect(newPos.x, newPos.y, size.x, size.z), _progbarEmpty);
        GUI.DrawTexture(new Rect(newPos.x, newPos.y, size.x * Mathf.Clamp01(_progress), size.z), _progbarFull);
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

    public void Add(float addition)
    {
        effective = Mathf.Clamp(effective + addition, 0, max);
    }
}
