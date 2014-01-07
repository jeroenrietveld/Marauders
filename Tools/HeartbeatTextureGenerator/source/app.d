import std.stdio;
import std.range;
import std.math;

import derelict.devil.il;
import gl3n.linalg;

enum templateName = "HeartbeatTemplate.png";
enum outputName = "Heartbeat.png";

struct Pixel
{
	ubyte r, g, b, a;
}

void main()
{
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
		
	auto imgData = (cast(Pixel*)ilGetData())[0..width*height];
	
	vec3 center = vec3(width / 2f, height / 2f, 0);
	
	float[4] multiplier = [1, 1, 1, 1];
	
	foreach(y; 0..height)
		foreach(x; 0..width)
		{
			auto idx = (y * width + x);
			auto p = &(imgData[idx]);
			
			vec3 pos = vec3(x, y, 0);
			vec3 dir = pos - center;
			
			if(dir.length > 0) dir.normalize();
			
			//multiplier[0] = multiplier[1] = multiplier[2] = p.r != 0;
			multiplier[3] = acos(dir.dot(vec3(0, 1, 0))) / PI;// * 0.5f;
			
			//if(dir.cross(vec3(0, -1, 0)).z > 0) multiplier[3] = 1 - multiplier[3];
			
			p.r = cast(ubyte)(p.r * multiplier[0]);
			p.g = cast(ubyte)(p.g * multiplier[1]);
			p.b = cast(ubyte)(p.b * multiplier[2]);
			p.a = cast(ubyte)(p.a * multiplier[3]);
		}
	
	ilSaveImage(outputName);
}
