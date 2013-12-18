using UnityEngine;
using System.Collections;

public class GUITimeSyncBar : MonoBehaviour{

    private float _percentage;

    private Texture2D _progbarEmpty;
    private Texture2D _progbarFull;

    public Vector2 pos { get; set; }
    public Vector2 size { get; set; }

    void OnGUI()
    {
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x, size.y), _progbarEmpty);
        GUI.DrawTexture(new Rect(pos.x, pos.y, size.x * Mathf.Clamp01(_percentage), size.y), _progbarFull);
    }


    void Start()
    {      
        _progbarEmpty = Resources.Load("Textures/progbar_empty", typeof(Texture2D)) as Texture2D;
        _progbarFull = Resources.Load("Textures/progbar_full", typeof(Texture2D)) as Texture2D;         
    }
	
	// Update is called once per frame
	void Update () {
        //We use OnGUI to update the progress bar.
	}   

    public void Add(float addition)
    {
        _percentage = Mathf.Clamp(_percentage + addition, 0, 1);
    }

    public float GetPercentage()
    {
        return _percentage;
    }
}
