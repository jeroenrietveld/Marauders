using UnityEngine;
using System.Collections;

public class en : Locale {

	public en()
	{
		Translations["speedboost_pickup"] = "Pickup speedboost";

	}

	public override string GetLanguageCode ()
	{
		return "en";
	}


}
