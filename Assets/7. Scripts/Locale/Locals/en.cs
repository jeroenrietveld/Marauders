using UnityEngine;
using System.Collections;

public class en : Locale {

	public en()
	{

		//General
		Translations["weapon_pickup"] = "Pickup";

		//CTF gametype
		Translations["flag"] = "flag";

		//Boosts
		Translations["speedboost_pickup"] = "Pickup speedboost";

	}

	public override string GetLanguageCode ()
	{
		return "en";
	}


}
