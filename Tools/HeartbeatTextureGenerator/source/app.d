import std.stdio;
import std.range;

import derelict.devil.il;
import gl3n.linalg;

enum templateName = "HeartbeatTemplate.png";
enum outputName = "Heartbeat.png";

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
		
	auto imgData = ilGetData();
	
	vec2 center = vec2(width / 2f, height / 2f);
	
	float[4] multiplier = [1, 1, 1, 1];
	
	foreach(y; 0..height)
		foreach(x; 0..width)
		{
			vec2 pos = vec2(x, y);
			vec2 dir = pos - center;
			
			if(dir.length > 0) dir.normalize();
			
			multiplier[3] = dir.dot(vec2(0, -1)) * .5f + .5f;
			
			auto idx = (y * width + x) * 4;
			foreach(i; 0..4)
				imgData[idx + i] = cast(ubyte)(imgData[idx + i] * multiplier[i]);
		}
	
	ilSaveImage(outputName);
}
