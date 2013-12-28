using UnityEngine;
using System.Collections;

public struct PlayerTimeSyncedEvent
{
	public PlayerRef Player;

	public PlayerTimeSyncedEvent(PlayerRef player)
	{
		this.Player = player;
	}
}