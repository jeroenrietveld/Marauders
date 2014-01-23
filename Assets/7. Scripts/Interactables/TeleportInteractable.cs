using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class TeleportInteractable : MonoBehaviour {

	/// <summary>
	/// A list with other teleports, the key is their channel
	/// </summary>
	public static Dictionary<int, List<TeleportInteractable>> teleportChannels ;

	public static List<Avatar> excludedAvatars;

	/// <summary>
	/// The teleport channel
	/// </summary>
	public int channel
	{
		get
		{
			return this._channel;
		} 
		set
		{
			UnregisterChannel();

			this._channel = value;

			RegisterChannel();
		}
	}
	[SerializeField]
	private int _channel;

	private void UnregisterChannel()
	{
		Debug.Log ("Teleport unregistering to channel: " + this.channel);

		//Do we need to initialize the channels?
		if (teleportChannels == null) { teleportChannels = new Dictionary<int, List<TeleportInteractable>>(); }

		//Removing ourself from our previous channel
		teleportChannels[this.channel].Remove (this);
		
		//Possibly removing the channel
		if (teleportChannels[this.channel].Count == 0) { teleportChannels.Remove(this._channel); }
	}

	private void RegisterChannel()
	{
		Debug.Log ("Teleport registering to channel: " + this.channel.ToString());

		//Do we need to initialize the channels?
		if (teleportChannels == null) { teleportChannels = new Dictionary<int, List<TeleportInteractable>>(); }

		//Do we need to create a new channel?
		if (!teleportChannels.ContainsKey(this.channel)) { teleportChannels.Add (this.channel, new List<TeleportInteractable>()); }
		
		//Adding us to the channel
		teleportChannels[this.channel].Add(this);
	}

	/// <summary>
	/// Can we exit using this teleport
	/// </summary>
	public bool canExit = true;

	/// <summary>
	/// Can we enter using this teleport
	/// </summary>
	public bool canEnter = true;

	// Use this for initialization
	void Start () {
		Debug.Log ("Channel " + this.channel);

		RegisterChannel();
	}

	/// <summary>
	/// Raises the trigger enter event; Teleporting the player
	/// </summary>
	void OnTriggerEnter(Collider other) 
	{
		//Getting the avatar
		Avatar avatar = other.gameObject.GetComponent<Avatar>();

		//Checking if we should continue
		if (avatar == null) { return; }

		//Are we already teleporting?
		var isTeleporting = avatar.GetComponent<TeleportingFlag>();

		if (isTeleporting != null) { return; }

		//Checking if we should continue
		if (!canEnter) { return; }

		Debug.Log ("Avatar has entered teleport");

		//Teleporting
		Teleport (avatar);
	}

	public void Teleport(Avatar avatar)
	{
		//creating a list of options
		List<TeleportInteractable> options = new List<TeleportInteractable>();

		//filling it and removing ourself as an option
		options.AddRange(teleportChannels[this.channel]);
		options.Remove (this);
		options.RemoveAll(t => t.canExit == false);

		//Checking if we have other teleports to teleport to
		if (options.Count == 0) { return; }
	
		//Creating the teleportingFlag
		avatar.gameObject.AddComponent<TeleportingFlag>();

		//Grabbing the teleport to teleport to
		TeleportInteractable destination = options[Random.Range(0, options.Count - 1)];

		//Moving the avatar's location
		ObjectSpawner.Create(avatar.gameObject, destination.transform.position, 0.5f, Vector3.zero);
	}

	/// <summary>
	/// Raises the trigger exit event; 
	/// </summary>
	void OnTriggerExit(Collider other)
	{
		///Getting the avatar
		Avatar avatar = other.gameObject.GetComponent<Avatar>();
		
		//Checking if we should continue
		if (avatar == null) { return; }

		//Destroying the flag, we can now teleport once more
		Destroy(other.gameObject.GetComponent<TeleportingFlag>());
	}
}
