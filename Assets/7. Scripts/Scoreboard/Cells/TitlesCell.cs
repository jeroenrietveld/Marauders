using System;


public class TitlesCell : Cell
{
    public TitlesCell()
    {
        this.title = "Titles";
        this.content = "TITLE \n";
        this.trophysEnabled = false;
        this.cellType = CellType.Static;
    }

    public void AddTitle(string titleName)
    {
        this.content += titleName + " \n";
    }

    public override string GetContent()
    {
        return content.ToString();
    } 
}

