﻿using UnityEngine;
using System.Collections;

public struct AvatarDeathEvent
{
	public Player victim;
	public Player offender;

	public AvatarDeathEvent(Player victim, Player offender)
	{
		this.victim = victim;
		this.offender = offender;

        if(this.offender != null)
		{
			this.offender.kills++;
		}
		this.victim.deaths++;
	}
}