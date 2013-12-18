using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

public class TimeSyncBar : MonoBehaviour
{
    private float _percentage;

    void Start() 
    {
    }
    void Update() 
    {
        renderer.material.SetFloat("Percentage", _percentage);
    }

    /// <summary>
    /// Values should be withing the range of 0 - 1.
    /// </summary>
    /// <param name="addition"></param>
    public void Add(float addition)
    {
        _percentage = Mathf.Clamp(_percentage + addition, 0, 1);
    }

    public float GetPercentage()
    {
        return _percentage;
    }
}
