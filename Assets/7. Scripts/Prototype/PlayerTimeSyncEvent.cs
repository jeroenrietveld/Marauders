using UnityEngine;
using System.Collections;

public struct PlayerTimeSyncEvent
{
	public PlayerRef Player;

	public PlayerTimeSyncEvent(PlayerRef player)
	{
		this.Player = player;
	}
}