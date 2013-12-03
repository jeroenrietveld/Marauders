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
		
		Translations["speedboost_pickup"] = "Pap speedboost op";
	}
	
	public override string GetLanguageCode ()
	{
		return "nl";
	}
	
	
}
