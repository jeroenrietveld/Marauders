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
	public string animationName { get; protected set; }

    public abstract void performAction();
}

