// GENERATED CODE, DO NOT MODIFY. MODIFY TOOLS/DECORATABLETEMPLATE.CS INSTEAD
using UnityEngine;

/// <summary>
/// Decoratable float type. Setting / assigning works as usual, getting the value
/// passes the value through a Filter stack which can alter it before being returned.
/// This allows for temporary changes and effects to be applied to the value with low coupling.
/// </summary>
/// <example>
/// Temporary reduction in player damage resistance when poisoned:
/// <code>
/// public class Player : MonoBehaviour
/// {
/// 	DecoratableFloat damageResistance = new DecoratableFloat(1);
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
/// 	float ModulateDR(float dr)
/// 	{
/// 		return dr * damageResistanceFactor;
/// 	}
/// }
/// </code>
/// </example>
[System.Serializable]
public class DecoratableFloat
{
	/// <summary>
	/// Signature of a Filter delegate. Accepts the value so far, returns the modified value.
	/// </summary>
	public delegate float Filter(float value);

	/// <summary>
	/// Filters currently affecting the value. Add / remove your filters to this member.
	/// </summary>
	public Filter filters;

	/// <summary>
	/// Getter and Setter for the value this Decoratable wraps. Getting the value passes the value 
	/// through the Filter stack before returning. Since only value types are Decoratable, rawValue
	/// remains unaltered, i.e. if the filter stack delegates are pure functions, multiple calls to
	/// the getter will all return the same value.
	/// </summary>
	public float value
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

	/// <summary>
	/// Gets or sets the raw value. Setting rawValue is the same as setting value, getting the raw value
	/// bypasses the Filter stack.
	/// </summary>
    public float rawValue;


	public DecoratableFloat(float v)
	{
        filters = null;
		rawValue = v;
	}

	/// <summary>
	/// Allow implicit conversions from a DecoratableFloat value to its wrapped type.
	/// </summary>
	public static implicit operator float(DecoratableFloat d)
	{
		return d.value;
	}
}

/// <summary>
/// Decoratable Vector3 type. Setting / assigning works as usual, getting the value
/// passes the value through a Filter stack which can alter it before being returned.
/// This allows for temporary changes and effects to be applied to the value with low coupling.
/// </summary>
/// <example>
/// Temporary reduction in player damage resistance when poisoned:
/// <code>
/// public class Player : MonoBehaviour
/// {
/// 	DecoratableVector3 damageResistance = new DecoratableVector3(1);
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
/// 	Vector3 ModulateDR(Vector3 dr)
/// 	{
/// 		return dr * damageResistanceFactor;
/// 	}
/// }
/// </code>
/// </example>
[System.Serializable]
public class DecoratableVector3
{
	/// <summary>
	/// Signature of a Filter delegate. Accepts the value so far, returns the modified value.
	/// </summary>
	public delegate Vector3 Filter(Vector3 value);

	/// <summary>
	/// Filters currently affecting the value. Add / remove your filters to this member.
	/// </summary>
	public Filter filters;

	/// <summary>
	/// Getter and Setter for the value this Decoratable wraps. Getting the value passes the value 
	/// through the Filter stack before returning. Since only value types are Decoratable, rawValue
	/// remains unaltered, i.e. if the filter stack delegates are pure functions, multiple calls to
	/// the getter will all return the same value.
	/// </summary>
	public Vector3 value
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

	/// <summary>
	/// Gets or sets the raw value. Setting rawValue is the same as setting value, getting the raw value
	/// bypasses the Filter stack.
	/// </summary>
    public Vector3 rawValue;


	public DecoratableVector3(Vector3 v)
	{
        filters = null;
		rawValue = v;
	}

	/// <summary>
	/// Allow implicit conversions from a DecoratableVector3 value to its wrapped type.
	/// </summary>
	public static implicit operator Vector3(DecoratableVector3 d)
	{
		return d.value;
	}
}
