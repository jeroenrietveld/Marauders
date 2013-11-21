using UnityEngine;
using System.Collections;

public class FlashButton : MonoBehaviour 
{
    private GameObject _gameObject;
    private float _timer = 1.5f;

    void Awake()
    {
        _gameObject = transform.FindChild("a_button").gameObject;
    }

	void Update () 
    {
        _timer -= Time.deltaTime;

        if (_timer <= 0f) 
        {
            _gameObject.SetActive(true);
            _timer = 1.5f;
        }
        else if (_timer <= 0.75f)
        {
            _gameObject.SetActive(false);
            
        }
	}
}
