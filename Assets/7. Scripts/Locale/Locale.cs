using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Reflection;

public abstract class Locale {
	public static void Initialize()
	{
		//Initialized the available locals
		_AvailableLocals = new Dictionary<string, Locale>();

		//Getting all the types
		Type[] allTypes = Assembly.GetExecutingAssembly().GetTypes();
		List<Type> localeTypes = new List<Type>();
		
		//Looping each type we can find
		foreach (Type type in allTypes)
		{
			//Is it a subclass?
			if (type.IsSubclassOf(typeof(Locale)))
			{
				//Yes; adding it to the list
				Locale locale = (Locale)type.GetConstructor(Type.EmptyTypes).Invoke(null);
				_AvailableLocals.Add (locale.GetLanguageCode(), locale);

			}
		}

		//Nasty way of loading default languages, but w/e
		_Current = AvailableLocals["en"];
		_Default = AvailableLocals["en"];

	}

	/// <summary>
	/// Gets the current locale
	/// </summary>
	/// <value>The current.</value>
	public static Locale Current
	{
		get { 
			if (_AvailableLocals == null)
			{
				Locale.Initialize();
			}

			return _Current; 
		}
	}
	private static Locale _Current;

	/// <summary>
	/// Gets the default locale
	/// </summary>
	/// <value>The default.</value>
	public static Locale Default
	{
		get { return _Default; }
	}
	private static Locale _Default;
	
	/// <summary>
	/// Available locals
	/// </summary>
	/// <returns>The locals.</returns>
	public static Dictionary<string,Locale> AvailableLocals
	{
		get { return _AvailableLocals; }
	}
	private static Dictionary<string,Locale> _AvailableLocals;

	/// <summary>
	/// The language code.
	/// </summary>
	public abstract string GetLanguageCode();

	public static void SwitchLocale(string Language)
	{
		_Current = _AvailableLocals[Language];
	}
	
	/// <summary>
	/// Gets the <see cref="Locale"/> with the specified key.
	/// </summary>
	/// <param name="key">Key.</param>
	public string this[string key]
	{
		get
		{
			//Checking if we have the translations
			if (Translations.ContainsKey(key))
			{
				return Translations[key];
			}

			return "Locale '" + this.GetLanguageCode() + "' does not contain key '" + key + "'";
		}
	}

	/// <summary>
	/// The translations.
	/// </summary>
	protected Dictionary<string, string>Translations = new Dictionary<string, string>();
	
}


