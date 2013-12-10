using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

/// <summary>
/// Made this class only for not having compile errors from the PlayerModel class.
/// </summary>
public abstract class SkillBase : MonoBehaviour
{
	public abstract string animationName { get; set; }

    public abstract void performAction(Player player);
}

