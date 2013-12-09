using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class PercentageCell : Cell 
{
    public readonly int percentage;
    public float total { get; set; }
    public float effective { get; set; }
    
    public PercentageCell()
    {
        percentage = (int) (effective / total) * 100;
    }
}