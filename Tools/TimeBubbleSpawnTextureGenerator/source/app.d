import std.file;
import std.exception;
import std.range;
import std.algorithm;
import std.array;
import std.conv;
import std.math;

import derelict.devil.il;

import std.stdio : writeln;

enum templateName = "TimeBubbleSpawnTextureTemplate.png";
enum outputName = "../../Assets/2. Textures/TimeBubbleSpawnTexture.png";

class Image
{
	this(Pixel[] pixels, size_t width, size_t height)
	{
		this.pixels = pixels.zip(size_t.max.iota).map!(a => PixelMeta.fromPixel(a[0], a[1], width)).array;
		
		this.width = width;
		this.height = height;
	}
	
	ref PixelMeta opIndex(size_t x, size_t y)
	{
		return pixels[x + y * height];
	}

	PixelMeta[] pixels;
	const size_t width, height;
}

struct Pixel
{
	ubyte r, g, b, a;
}

struct PixelMeta
{
	static PixelMeta fromPixel(Pixel p, size_t idx, size_t width)
	{
		return PixelMeta(p.r / 255.0L, p.g / 255.0L, p.b / 255.0L, 0, idx % width, idx / width);
	}

	real r, g, b, a;
	size_t x;
	size_t y;

	enum NoPath = 0;
	
	ubyte pathID = NoPath;
	real pathLength = real.infinity;
	
	bool isAnchor()
	{
		return r > 0 && g > 0;
	}
}

struct FloodFillTask
{
	size_t x;
	size_t y;
	ubyte pathID;
	real length;
}

struct BilerpTask
{
	size_t x;
	size_t y;
	real length;
	uint generation;
	real r;
	real g;
	ubyte pathID;
}

void main(string[] args)
{
	DerelictIL.load();
	ilInit();
	
	ilEnable(IL_FILE_OVERWRITE);

	ILuint handle;
	ilGenImages(1, &handle);
	ilBindImage(handle);
	
	writeln("Loading image...");
	ilLoadImage(templateName);
	
	const
		width = ilGetInteger(IL_IMAGE_WIDTH),
		height = ilGetInteger(IL_IMAGE_HEIGHT);
	
	auto rawPixels = (cast(Pixel*)ilGetData())[0 .. width * height];
	auto image = new Image(
		rawPixels,
		width,
		height
	);
	
	writeln("Assign particle path ids to anchor points...");
	foreach(i, id; size_t.max.iota.zip(image.pixels).filter!(a => a[1].isAnchor).map!(a => a[0]).zip(iota(cast(ubyte)1, ubyte.max)))
	{
		image.pixels[i].pathID = id;
	}
	
	writeln("Flood filling particle paths...");
	floodFill!
	(
		(task, pixel) => pixel.g > 0 && task.length < pixel.pathLength,
		(task, ref pixel) { pixel.pathLength = task.length; pixel.pathID = task.pathID; pixel.a = 1; },
		(x, y, parent) => FloodFillTask(x, y, parent.pathID, parent.length+1)
	)
	(
		image,
		image.pixels.filter!(a => a.isAnchor).map!(p => FloodFillTask(p.x, p.y, p.pathID, 1)).array
	);
	
	const numParticlePaths = image.pixels.map!(a => a.pathID).reduce!max;
	
	writeln("Flood filling main path...");
	floodFill!
	(
		(task, pixel) => pixel.r > 0 && task.length < pixel.pathLength,
		(task, ref pixel) { pixel.pathLength = task.length; pixel.pathID = task.pathID; pixel.a = 1; },
		(x, y, parent) => FloodFillTask(x, y, parent.pathID, parent.length+1)
	)
	(
		image,
		image.width.iota.map!(a => FloodFillTask(a, 0, (numParticlePaths + 1) & 0xFF, 1)).array
	);
	
	writeln("Normalizing path lengths...");
	const numPaths = image.pixels.map!(a => a.pathID).reduce!max;
	foreach(id; iota(1, numPaths+1))
	{
		const length = image.pixels.filter!(a => a.pathID == id).map!(a => a.pathLength).reduce!max;
		
		foreach(ref p; image.pixels)
		{
			if(p.pathID == id) p.pathLength /= length;
		}
	}
	
	writeln("Apply bilinear interpolation error correction...");
	floodFill!
	(
		(task, pixel) => task.generation < 2,
		(task, ref pixel) { if(pixel.pathID == 0 && task.length < pixel.pathLength) { pixel.pathLength = task.length; pixel.r = task.r; pixel.g = task.g; pixel.pathID = task.pathID; } },
		(x, y, parent) => BilerpTask(x, y, parent.length, parent.generation+1, parent.r, parent.g, parent.pathID)
	)
	(
		image,
		image.pixels.filter!(a => a.pathID > 0).map!(p => BilerpTask(p.x, p.y, p.pathLength, 0, p.r, p.g, (p.pathID+1)&0xFF)).array
	);
	
	writeln("Inverting path lengths");
	foreach(ref p; image.pixels)
	{
		if(p.pathLength.isFinite) p.pathLength = 1 - p.pathLength;
	}
	
	writeln("Encoding results...");
	foreach(i, ref p; rawPixels)
	{
		auto input = image.pixels[i];
		if(input.pathLength.isFinite)
		{
			p.r = (cast(uint)(input.r * 0xFF * input.pathLength)) & 0xFF;
			p.g = (cast(uint)(input.g * 0xFF * input.pathLength)) & 0xFF;
		}
		
		p.b = input.pathID;
		p.a = (cast(uint)(input.a * 0xFF)) & 0xFF;
	}
	
	writeln("Saving result...");
	ilSaveImage(outputName);
	writeln("Done.");
}

void floodFill(alias cmp, alias apply, alias create, T)(Image image, T[] stack)
{
	T[] buffer;
	
	while(!stack.empty)
	{
		auto task = stack.back;
		stack.popBack;
		
		if(cmp(task, image[task.x, task.y]))
		{
			apply(task, image[task.x, task.y]);
			
			if(task.x > 0)
			{
											addIf!(buffer, image, cmp)(create(task.x-1, task.y  , task));
				if(task.y > 0)				addIf!(buffer, image, cmp)(create(task.x-1, task.y-1, task));
				if(task.y < image.height-1)	addIf!(buffer, image, cmp)(create(task.x-1, task.y+1, task));
			}
			if(task.x < image.width-1)
			{
											addIf!(buffer, image, cmp)(create(task.x+1, task.y  , task));
				if(task.y > 0)				addIf!(buffer, image, cmp)(create(task.x+1, task.y-1, task));
				if(task.y < image.height-1)	addIf!(buffer, image, cmp)(create(task.x+1, task.y+1, task));
			}
			
			if(task.y > 0)				addIf!(buffer, image, cmp)(create(task.x, task.y-1, task));
			if(task.y < image.height-1)	addIf!(buffer, image, cmp)(create(task.x, task.y+1, task));
		}
		
		if(stack.empty)
		{
			writeln("|- Swapping task buffers <", buffer.length, ">...");
			auto tmp = stack;
			stack = buffer;
			buffer = tmp[];
			buffer.assumeSafeAppend;
			
			stack.sort!((a, b) => a.length > b.length);
		}
	}
}

void addIf(alias buffer, alias image, alias cmp, T)(T task)
{
	if(cmp(task, image[task.x, task.y])) buffer ~= task;
}
