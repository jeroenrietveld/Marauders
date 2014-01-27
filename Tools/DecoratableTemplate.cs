
/// <summary>
/// Decoratable ##type## type. Setting / assigning works as usual, getting the value
/// passes the value through a Filter stack which can alter it before being returned.
/// This allows for temporary changes and effects to be applied to the value with low coupling.
/// </summary>
/// <example>
/// Temporary reduction in player damage resistance when poisoned:
/// <code>
/// public class Player : MonoBehaviour
/// {
/// 	Decoratable##Type## damageResistance = new Decoratable##Type##(1);
/// }
/// 
/// public class PoisonEffect : MonoBehaviour
/// {
/// 	public float damageResistanceFactor = 0.5f;
/// 
/// 	void OnEnable()
/// 	{
/// 		GetComponent<Player>().damageResistance.filters += ModulateDR;
/// 	}
/// 
/// 	void OnDisable()
/// 	{
/// 		GetComponent<Player>().damageResistance.filters -= ModulateDR;
/// 	}
/// 	
/// 	##type## ModulateDR(##type## dr)
/// 	{
/// 		return dr * damageResistanceFactor;
/// 	}
/// }
/// </code>
/// </example>
[System.Serializable]
public class Decoratable##Type##
{
	/// <summary>
	/// Signature of a Filter delegate. Accepts the value so far, returns the modified value.
	/// </summary>
	public delegate ##type## Filter(##type## value);

	/// <summary>
	/// Filters currently affecting the value. Add / remove your filters to this member.
	/// </summary>
	public List<FilterPair> filters = new List<FilterPair>();

	public struct FilterPair
	{
		public Filter filter;
		public bool once;

		public FilterPair(Filter filter, bool once)
		{
			this.filter = filter;
			this.once = once;
		}
	}

	/// <summary>
	/// Getter and Setter for the value this Decoratable wraps. Getting the value passes the value 
	/// through the Filter stack before returning. Since only value types are Decoratable, rawValue
	/// remains unaltered, i.e. if the filter stack delegates are pure functions, multiple calls to
	/// the getter will all return the same value.
	/// </summary>
	public ##type## value
	{
		set
		{
			rawValue = value;
		}

		get
		{
			var val = rawValue;

			for(int i = 0; i < filters.Count;)
			{
				var pair = filters[i];

				if(pair.once)
				{
					filters.RemoveAt(i);
				}
				else 
				{
					i++;
				}

				val = pair.filter(val);
			}

			return val;
		}
	}

	/// <summary>
	/// Gets or sets the raw value. Setting rawValue is the same as setting value, getting the raw value
	/// bypasses the Filter stack.
	/// </summary>
    public ##type## rawValue;

	public Decoratable##Type##(##type## v)
	{
		rawValue = v;
	}

	/// <summary>
	/// Allow implicit conversions from a Decoratable##Type## value to its wrapped type.
	/// </summary>
	public static implicit operator ##type##(Decoratable##Type## d)
	{
		return d.value;
	}

	public void AddFilter(Filter filter, bool once = false)
	{
		filters.Add(new FilterPair(filter, once));
	}

	public void RemoveFilter(Filter filter)
	{
		filters.RemoveAll(delegate(FilterPair pair){
			return pair.filter == filter;
		});
	}
}
