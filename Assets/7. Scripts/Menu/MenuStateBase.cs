using UnityEngine;
using System.Collections;

public abstract class MenuStateBase
{
	public Vector3 center { protected set; get; }

	public abstract void Update(MenuManager manager);

	public virtual void OnActive()
	{
	}

	public virtual void OnInactive()
	{
	}
}
