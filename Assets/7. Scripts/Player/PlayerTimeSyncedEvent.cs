using UnityEngine;
using System.Collections;

public struct PlayerTimeSyncedEvent
{
	public Player Player;

	public PlayerTimeSyncedEvent(Player player)
	{
		this.Player = player;
	}
}