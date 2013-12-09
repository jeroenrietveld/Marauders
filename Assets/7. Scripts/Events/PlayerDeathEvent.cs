using UnityEngine;
using System.Collections;

public struct PlayerDeathEvent
{
	public Player victim;
	public Player offender;

	public PlayerDeathEvent(Player victim, Player offender)
	{
		this.victim = victim;
		this.offender = offender;
	}
}