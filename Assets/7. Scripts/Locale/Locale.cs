using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;
using SimpleJSON;

public class Locale {
	
	public static Locale Current
	{
		get;
		set;
	}

	public static List<Locale> AvailableLocales
	{
		get;
		private set;
	}

	private static string localePath = "Locale/Locales";
	static Locale()
	{
		var jsonTexts = Resources.LoadAll<TextAsset>(localePath);

		var locales = new List<Locale>();
		foreach(var text in jsonTexts)
		{
			locales.Add(new Locale(JSON.Parse(text.text)));
		}

		AvailableLocales = locales;

		foreach(var locale in locales)
		{
			if(locale["locale_code"].Equals("en"))
			{
				Current = locale;
			}
		}

		if(Current == null)
		{
			Current = locales[0];
		}
	}

	private Locale(JSONNode json)
	{
		foreach(KeyValuePair<string, JSONNode> kv in json.AsObject)
		{
			Translations[kv.Key] = kv.Value.Value;
		}
	}

	public string this[string key]
	{
		get
		{
			if (Translations.ContainsKey(key))
			{
				return Translations[key];
			}

			return "Missing String: '" + key + "'";
		}
	}

	private Dictionary<string, string> Translations = new Dictionary<string, string>();
	
}

