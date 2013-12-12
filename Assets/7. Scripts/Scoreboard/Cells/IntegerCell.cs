using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class IntegerCell : Cell 
{
    public int amount { get; set; }

    public override String GetContent()
    {
        return amount.ToString();
    }

}

