public struct Decoratable<T> where T : struct
{
	public delegate T Filter(T value);
	
	public Filter filters;


	public T value
	{
		set
		{
			rawValue = value;
		}

		get
		{
			var val = rawValue;

			if(filters != null)
			{
				foreach(var filter in filters.GetInvocationList())
				{
					val = ((Filter)filter)(val);
				}
			}

			return val;
		}
	}
	
	public T rawValue { get; set; }


	public Decoratable(T v)
	{
		value = v;
	}


	public static implicit operator T(Decoratable<T> d)
	{
		return d.value;
	}

	public static implicit operator Decoratable<T>(T v)
	{
		return new Decoratable<T>(v);
	}
}