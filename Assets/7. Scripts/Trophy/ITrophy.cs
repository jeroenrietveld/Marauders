using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

public interface ITrophy
{
    /// <summary>
    /// The column name of the Trophy
    /// </summary>
    string Column { get; set; }

    /// <summary>
    /// Gets the Title that is associated with the Trophy.
    /// </summary>
    string Title { get; set; }
    
    /// <summary>
    /// The condition (less than or greater than)
    /// </summary>
    string Condition { get; set; }

    /// <summary>
    /// Calculate which player or players should get this trophy. If two players
    /// have the highest score (same amount of kills i.e.) they both get the Trophy + Title. 
    /// Returns a list with players that gets the Trophy and Title.
    /// </summary>
    /// <param name="players"></param>
    /// <returns></returns>
    List<PlayerTest> CalculateTrophy(ICollection<PlayerTest> players);
}

