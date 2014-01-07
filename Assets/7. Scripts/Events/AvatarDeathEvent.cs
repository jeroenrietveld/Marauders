using UnityEngine;
using System.Collections;

public struct AvatarDeathEvent
{
	public Player victim;
	public Player offender;

	public AvatarDeathEvent(Player victim, Player offender)
	{
		this.victim = victim;
		this.offender = offender;

        // Offender gets 1 extra kill, victim 1 extra death
        this.offender.kills++;
        this.victim.deaths++;
	}
}