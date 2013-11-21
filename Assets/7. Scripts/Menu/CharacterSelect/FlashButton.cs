using UnityEngine;
using System.Collections;

/// <summary>
/// Class to make the A_Button flash with a 0.75 seconds interval.
/// </summary>
public class FlashButton : MonoBehaviour 
{
    private GameObject _gameObject;
    private float _timer = 1.5f;

    /// <summary>
    /// Find GameObject A_button.
    /// </summary>
    void Awake()
    {
        _gameObject = transform.FindChild("a_button").gameObject;
    }

    /// <summary>
    /// Flash A_Button timer
    /// </summary>
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
