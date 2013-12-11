import std.file;
import std.exception;
import std.range;
import std.algorithm;
import std.array;
import std.conv;

import derelict.devil.il;

import std.stdio : writeln;

enum templateName = "TimeBubbleRippleTemplate.png";
enum outputName = "../../Assets/2. Textures/TimeBubbleRipple.png";

struct Point
{
	int x;
	int y;
	int pathLength;
}

struct Pixel
{
	ubyte r, g, b, a;
}

void main(string[] args)
{
	//args = args[1..$];
	//args = ["512", "0", "169", "0", "857", "0"];
	//enforce(args.length % 2 == 0);
	//enforce(args.length >= 2);
	
	DerelictIL.load();
	ilInit();
	
	ilEnable(IL_FILE_OVERWRITE);

	ILuint handle;
	ilGenImages(1, &handle);
	ilBindImage(handle);
	
	ilLoadImage(templateName);
	
	auto
		width = ilGetInteger(IL_IMAGE_WIDTH),
		height = ilGetInteger(IL_IMAGE_HEIGHT);
		
	Pixel[] imgData = (cast(Pixel*)ilGetData())[0 .. width * height];
	
	foreach(ref p; imgData)
	{
		if(p.r < 20) p.r = 0;
		p.g = 0;
	}
	
	int[] steps = new int[imgData.length];
	steps[] = int.max;
	
	Point[] floodFillBuffer = 
		//args.chunks(2).map!(a => Point(a[0].to!int, a[1].to!int)).array();
		iota(width).map!(a => Point(a, 0)).array;
	Point[] floodFillStack;
	
	size_t modifications = 0;
	
	while(floodFillBuffer.length)
	{
		auto point = floodFillBuffer[$-1];
		floodFillBuffer.length--;
		
		if(floodFillBuffer.length == 0)
		{
			floodFillStack.sort!((a, b) => a.pathLength > b.pathLength);
			auto temp = floodFillBuffer;
			floodFillBuffer = floodFillStack;
			floodFillStack = temp;
			floodFillStack.assumeSafeAppend();
		}
		
		size_t idx = point.x + point.y * width;
		
		point.pathLength += (0xFF - imgData[idx].r) * (imgData[idx].r < 200) * 32;
		
		if(imgData[idx].r == 0 || steps[idx] <= point.pathLength)
		{
			continue;
		}
		
		steps[idx] = point.pathLength;
		
		if(point.x > 0)
		{
									floodFillStack ~= Point(point.x-1, point.y  , point.pathLength+255);
			if(point.y > 0)			floodFillStack ~= Point(point.x-1, point.y-1, point.pathLength+255);
			if(point.y < height-1)	floodFillStack ~= Point(point.x-1, point.y+1, point.pathLength+255);
		}
		if(point.x < width-1)
		{
									floodFillStack ~= Point(point.x+1, point.y  , point.pathLength+255);
			if(point.y > 0)			floodFillStack ~= Point(point.x+1, point.y-1, point.pathLength+255);
			if(point.y < height-1)	floodFillStack ~= Point(point.x+1, point.y+1, point.pathLength+255);
		}
		
		if(point.y > 0)			floodFillStack ~= Point(point.x, point.y-1, point.pathLength+255);
		if(point.y < height-1)	floodFillStack ~= Point(point.x, point.y+1, point.pathLength+255);
		
		//writeln(point.x, " ", point.y, " ", point.pathLength);
		
		modifications++;
		if((modifications & 0xFFF) == 0)
		{
			writeln("Mods: ", modifications, ", Stack: ", floodFillStack.length);
		}
	}
	
	auto maxPathLength = steps.filter!(a => a != int.max).reduce!max;
	
	foreach(idx, ref pixel; imgData)
	{
		if(steps[idx] == int.max) continue;
		
		pixel.a = ((steps[idx] * 0xFF) / maxPathLength) & 0xFF;
		//pixel.r = pixel.g = pixel.b = 0xFF;
		pixel.g = 0xFF;
	}
	
	ilSaveImage(outputName);
}
