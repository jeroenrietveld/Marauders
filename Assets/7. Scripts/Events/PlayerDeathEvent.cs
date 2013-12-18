using UnityEngine;
using System.Collections;

public struct AvatarDeathEvent
{
	public PlayerRef victim;
	public PlayerRef offender;

	public AvatarDeathEvent(PlayerRef victim, PlayerRef offender)
	{
		this.victim = victim;
		this.offender = offender;
	}
}