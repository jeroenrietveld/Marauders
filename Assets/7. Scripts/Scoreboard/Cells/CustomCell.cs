using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public class CustomCell : Cell 
{
    public bool hasTrophy;
    public CustomCell(string title, CellType cellType, object content, bool trophysEnabled)
    {
        this.title = title;
        this.trophysEnabled = trophysEnabled;
        this.content = content;
        this.cellType = cellType;       
    }

    public override string GetContent()
    {
        string result = content.ToString();
        switch (cellType)
        {
            case CellType.Percentage:
                result += "%";
                break;
        }
        return result;
    }



    public override bool HasTrophy()
    {
        return hasTrophy && trophysEnabled;
    }
}

