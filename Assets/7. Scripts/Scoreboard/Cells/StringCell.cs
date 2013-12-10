using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


public class StringCell : Cell 
{
    public String content { get; set; }
    public StringCell(String content)
    {
        this.content = content;
    }
}

