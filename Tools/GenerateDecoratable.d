import std.stdio;
import std.file;
import std.array;
import std.string;

void main()
{
	auto classTemplate = readText("DecoratableTemplate.cs");
	auto outputFile = File("../Assets/7. Scripts/Misc/Decoratable.cs", "w");
	
	outputFile.writeln("// GENERATED CODE, DO NOT MODIFY. MODIFY TOOLS/DECORATABLETEMPLATE.CS INSTEAD");
	outputFile.writeln("using UnityEngine;");
	
	foreach(t; ["float", "Vector3"])
	{
		auto generatedCode = classTemplate.
			replace("##type##", t).
			replace("##Type##", t.capitalize);
			
		outputFile.write(generatedCode);
	}
}