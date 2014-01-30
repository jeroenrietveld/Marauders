using UnityEngine;
using System.Collections;

public class en : Locale {

	public en()
	{
		//General
		Translations["weapon_pickup"] = "Pickup";

		//CharacterSelect
        Translations["press_join"] = "Press A to join";
        Translations["connect_controller"] = "Connect Controller";
        Translations["press_select"] = "Press A to select";
        Translations["press_reselect"] = "Press B to reselect";
        Translations["text_defense"] = "Defense";
        Translations["text_offense"] = "Offense";
        Translations["text_utility"] = "Utility";
        Translations["text_select"] = "select";
        Translations["text_back"] = "back";
        Translations["select_your_marauder"] = "SELECT YOUR MARAUDER";
		
		//CTF gametype
		Translations["flag"] = "flag";

		//Boosts
		Translations["speedboost_pickup"] = "Pickup speedboost";

		//Shrines
		Translations["shrine_capturable"] = "Attack to capture this Shrine";
		Translations["shrine_captured"] = "You already captured this Shrine";
		Translations["shrine_locked"] = "Shrine can't be captured at this time";
		Translations["shrine_pointsgiven"] = "Last shrine was capturedtimesync reward will be given";

		//Shrine announcements 
		Translations["shrine_announcement_capturable"] = "Shrine can now be captured";
		Translations["shrine_subannouncement_capturable"] = "Attack to capture shrine";
		Translations["shrine_announcement_captured"] = "Player {0} has captured a shrine"; //Disabled
		Translations["shrine_subannouncement_captured"] = "";
		Translations["shrine_announcement_capturable_last"] = "Last shrine can now be captured";
		Translations["shrine_subannouncement_capturable_last"] = "Timesync will be rewarded when captured";
		Translations["shrine_announcement_rewards"] = "Last shrine has been captured";
		Translations["shrine_subannouncement_rewards"] = "Timesync has been rewarded";

		//Ability descriptions - offensive
		Translations["ability_windsweep"] = "Strike with wind, causing \ndamage and knockback";
        Translations["ability_obliterate"] = "Spin around fiercely, \ndamaging nearby enemies";
        Translations["ability_sunderstrike"] = "Strike the enemy, piercing \nthrough all defenses";

		//Ability descriptions - defensive
        Translations["ability_destabilize"] = "Fade out to avoid \nall incoming damage";
        Translations["ability_riposte"] = "Counter the next \nincoming attack";
        Translations["ability_bulwark"] = "Take reduced damage \nfor a short time";

		//Ability descriptions - Utility
        Translations["ability_dash"] = "Quickly dash several \nfeet forward";

		//World descriptions
		Translations["world_gaia"] = "Far from the lands of war, death and suffering an old forest stands, energetic and wise. Spreading vastly over miles of land, It is no ordinary place. Blessed by the god of nature, trees tower over a 100 meters above the rich and lush forest including a variety of wild and plantlife.";
		Translations["world_synchronization"] = "The amount of synchronization a player needs to acquire before claiming the world. Increasing or decreasing the amount of synchronization required influences the length of the game.";

		//Character descriptions
		Translations["character_samurai"] = "Yuro - A young and solitary samurai, determined to serve his deceased landlord and reclaiming his family’s honour. Gracefully wielding his katana with deadly intent.";
		Translations["character_thief"] = "Xero - A mysterious and acrobatic thief, seeking to steal all he lays his eyes on. Wielding dual daggers, his attacks are swift and efficient.";
		Translations["character_juggernaut"] = "Juggernaut - A fierce and crude enforcer of the law, on a neverending quest to reunite himself with his long lost wife. Wielding a massive hammer and shield, obliterating all in his way.";
		Translations["character_shaman"] = "Shaman - A strong and independant shamaness, with an unquenchable thirst for proving herself in battle. Furiously wielding an Daibo to her hearts content.";

        //Scoreboard titles
        //Players
        Translations["scoreboard_player_One"] = "Player 1";
        Translations["scoreboard_player_Two"] = "Player 2";
        Translations["scoreboard_player_Three"] = "Player 3";
        Translations["scoreboard_player_Four"] = "Player 4";

        //Other cells
        Translations["scoreboard_timesync"] = "Synchronisation";
        Translations["scoreboard_titles"] = "Titles";
        Translations["scoreboard_eliminations"] = "Eliminations";
        Translations["scoreboard_eliminated"] = "Eliminated";
        Translations["scoreboard_suicides"] = "Suicides";
        Translations["scoreboard_ownedshrines"] = "Owned Shrines";

	}

	public override string GetLanguageCode ()
	{
		return "en";
	}


}
