using UnityEngine;
using System.Collections;

public class nl : Locale {
	
	public nl()
	{
		//CharacterSelect
        Translations["press_join"] = "Klik A om te spelen";
        Translations["connect_controller"] = "Sluit Controller aan";
        Translations["press_select"] = "Klik A om te selecteren";
        Translations["press_reselect"] = "Klik b om te herselecteren";
        Translations["text_defense"] = "Defensief";
        Translations["text_offense"] = "Offensief";
        Translations["text_utility"] = "Utility";
		
		Translations["speedboost_pickup"] = "Pak speedboost op";

        //Scoreboard titles
        //Players
        Translations["scoreboard_player_One"] = "Speler 1";
        Translations["scoreboard_player_Two"] = "Speler 2";
        Translations["scoreboard_player_Three"] = "Speler 3";
        Translations["scoreboard_player_Four"] = "Speler 4";

        //Other cells
        Translations["scoreboard_timesync"] = "Synchronisatie";
        Translations["scoreboard_titles"] = "Titels";
        Translations["scoreboard_eliminations"] = "Eliminaties";
        Translations["scoreboard_eliminated"] = "Geëlimineerd";
        Translations["scoreboard_suicides"] = "Zelfmoorden";
        Translations["scoreboard_ownedshrines"] = "Veroverde altaren";

	}
	
	public override string GetLanguageCode ()
	{
		return "nl";
	}
	
	
}
