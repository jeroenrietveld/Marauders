using UnityEngine;
using System.Collections.Generic;

/// <summary>
/// Central event dispatching mechanism. Listeners for a particular Event type are registered with this class, 
/// Events are dipatched through this class.
/// </summary>
/// <example>
/// Registering an Event listener:
/// <code>
/// public class SomeComponent : MonoBehaviour
/// {
/// 	void Start()
/// 	{
/// 		Event.register<SomeEvent>(HandleSomeEvent);
/// 	}
/// 	
/// 	void OnDisable()
/// 	{
/// 		// Always unregister your listeners to avoid dangling references when the scene is destroyed.
/// 		Event.unregister<SomeEvent>(HandleSomeEvent);
/// 	}
/// 
/// 	void HandleSomeEvent(SomeEvent evt)
/// 	{
/// 		// TODO Do something useful...
/// 	}
/// }
/// </code>
/// </example>
/// <example>
/// Dispatching an Event:
/// <code>
/// SomeEvent event = new SomeEvent(eventValue1, eventValue2);
/// Event.dispatch(event);
/// </code>
/// </example>
public static class Event {

	///<summary>
	/// EventListener signature. Has a single parameter, which is of the type for which Event it will be registered.
	///</summary>
	public delegate void EventListener<T>(T evt);

	/// <summary>
	/// Contains Event listeners indexed by Event type
	/// </summary>
	private static Dictionary<System.Type, System.Delegate> _listeners = new Dictionary<System.Type, System.Delegate>();

	/// <summary>
	/// Register the specified listener. The listener will be called on all events of type T.
	/// </summary>
	/// <param name="listener">Event Listener.</param>
	/// <typeparam name="T">The type of Event to listen for. Only value types are accepted.</typeparam>
	public static void register<T>(EventListener<T> listener) where T : struct
	{
		var type = typeof(T);

		if(_listeners.ContainsKey(type))
		{
			_listeners[type] = System.Delegate.Combine(_listeners[type], listener);
		}
		else
		{
			_listeners.Add(type, listener);
		}
	}

	/// <summary>
	/// Unregister the specified listener. This must always be done, for example in OnDisable, in order to avoid
	/// calling destroyed delegates.
	/// </summary>
	public static void unregister<T>(EventListener<T> listener) where T : struct
	{
		var type = typeof(T);

		if(_listeners.ContainsKey(type))
		{
			var newListeners = System.Delegate.Remove(_listeners[type], listener);

			if(newListeners == null)
			{
				_listeners.Remove(type);
			}
			else
			{
				_listeners[type] = newListeners;
			}
		}
	}

	/// <summary>
	/// Calls all listeners registered to receive this Event type.
	/// </summary>
	/// <typeparam name="T">The Event type. Only value types are accepted</typeparam>
	public static void dispatch<T>(T evt) where T : struct
	{
		var type = typeof(T);

		if(_listeners.ContainsKey(type))
		{
			_listeners[type].DynamicInvoke(evt);
		}
	}
}
