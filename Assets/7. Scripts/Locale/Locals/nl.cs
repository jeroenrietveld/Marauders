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

        //Tips - TODO: translate this.
        Translations["tip_0"] = "With Bulwark activated, you cannot be stunned.";
        Translations["tip_1"] = "Every attack you land will stun the opponent for a brief time.";
        Translations["tip_2"] = "Windsweep will deal damage and knock enemies back!";
        Translations["tip_3"] = "Destabilize will remove all forces, so make good use of it!";
        Translations["tip_4"] = "Obliterate can hit every opponent up to two times.";
        Translations["tip_5"] = "Xero, the thief, has the highest attack speed.";
        Translations["tip_6"] = "You will deal more damage if you hit an opponent on his remaining heartbeat.";
        Translations["tip_7"] = "A suicide will add an extra penalty to your Time Sync.";
        Translations["tip_8"] = "Knowing your opponents skills will give you an advantage in battle.";
        Translations["tip_9"] = "Who said killstealing is not allowed?";
        Translations["tip_10"] = "Dashing will also apply force to an opponent if you collide.";
	}
	
	public override string GetLanguageCode ()
	{
		return "nl";
	}
	
	
}
