using UnityEngine;
using System.Collections;

public class FlashButton : MonoBehaviour 
{
    private GameObject gameObject;
    private float timer = 1.5f;

    void Awake()
    {
        gameObject = transform.FindChild("a_button").gameObject;
    }

	void Update () 
    {
        timer -= Time.deltaTime;

        if (timer <= 0f) 
        {
            gameObject.SetActive(true);
            timer = 1.5f;
        }
        else if (timer <= 0.75f)
        {
            gameObject.SetActive(false);
            
        }
	}
}
