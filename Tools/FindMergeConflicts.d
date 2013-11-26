import std.stdio;
import std.file;
import std.algorithm;
import std.process;

void main()
{
	foreach(file; dirEntries("../Assets", SpanMode.depth))
	{
		try if(file.readText().canFind("<<<<<<< HEAD")) writeln(file.name);
		catch(Exception e){}
	}
	
	writeln("Press Enter to exit..");
	readln();
}