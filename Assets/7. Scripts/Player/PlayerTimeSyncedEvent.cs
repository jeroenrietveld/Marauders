using UnityEngine;
using System.Collections;

// When receiving TimeSync
public struct PlayerTimeSyncEvent
{
	public Player player;
	public int amount;
	public Vector3 worldSpacePosition;

	public bool hasPosition { get { return !float.IsNaN(worldSpacePosition.sqrMagnitude); } }

	public PlayerTimeSyncEvent(Player player, int amount, Vector3 worldSpacePosition)
	{
		this.player = player;
		this.amount = amount;
		this.worldSpacePosition = worldSpacePosition;
	}

	public int newTimeSync { get { return player.timeSync; } }
	public int oldTimeSync { get { return Mathf.Clamp(player.timeSync - amount, 0, GameManager.Instance.matchSettings.timeSync); } }
}

// When fully synced
public struct PlayerTimeSyncedEvent
{
	public Player Player;

	public PlayerTimeSyncedEvent(Player player)
	{
		this.Player = player;
	}
}