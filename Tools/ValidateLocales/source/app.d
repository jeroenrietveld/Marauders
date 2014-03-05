import std.stdio;
import std.file;
import std.array;
import std.algorithm;
import std.typecons;
import std.range;

import vibe.data.json;

immutable localeResourcePath = "../../Assets/Resources/Locale/";
immutable scriptsPath = "../../Assets/7. Scripts/";
immutable localeKeyPath = localeResourcePath ~ "LocaleKeys.json";
immutable localesPath = localeResourcePath ~ "Locales/";

T readJsonFile(T)(string fileName)
{
	return readText(fileName).parseJsonString().deserializeJson!T;
}

void main()
{
	writeln("Reading master key file...");
	const keys = readJsonFile!(string[])(localeKeyPath);
	const keySet = keys.map!(a => tuple(a, 0)).assocArray;
	writefln("Read %s keys", keys.length);

	{
		writeln("Checking for key usage in codebase...");

		keyLoop:
		foreach(key; keys)
		{
			foreach(string scriptPath; dirEntries(scriptsPath, "*.cs", SpanMode.breadth))
			{
				if(scriptPath.readText().canFind(chain('"'.only, key, '"'.only))) continue keyLoop;
			}
			writeln("Warning: Key literal not found in codebase: ", key);
		}
	}

	writeln("Validating locales...");
	foreach(string localePath; dirEntries(localesPath, "*.json", SpanMode.breadth))
	{
		writefln("Validating Locale %s...", localePath);
		const locale = readJsonFile!(string[string])(localePath);

		writeln("Checking for missing keys...");
		foreach(key; keys)
		{
			if(!(key in locale))
			{
				stderr.writeln("Missing Key: ", key);
			}
		}

		writeln("Checking for superfluous keys...");
		foreach(key; locale.byKey)
		{
			if(!(key in keySet))
			{
				stderr.writeln("Superfluous Key: ", key);
			}
		}
	}
}
