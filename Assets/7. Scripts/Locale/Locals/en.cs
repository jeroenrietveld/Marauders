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
		
		//CTF gametype
		Translations["flag"] = "flag";

		//Boosts
		Translations["speedboost_pickup"] = "Pickup speedboost";

		Translations["shrine_capture"] = "Combo to capture";

	}

	public override string GetLanguageCode ()
	{
		return "en";
	}


}
