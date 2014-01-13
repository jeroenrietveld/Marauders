using UnityEngine;
using System.Collections;

public struct ShrineCapturedEvent
{
	public Shrine shrine;
	public Player newOwner;
	public Player oldOwner;

	public ShrineCapturedEvent(Shrine shrine, Player newOwner, Player oldOwner)
	{
		this.shrine = shrine;
		this.newOwner = newOwner;
		this.oldOwner = oldOwner;
	}
}

public class Shrine : Attackable {

	private bool _canBeCaptured = true;
	public bool canBeCaptured { get { return _canBeCaptured; } }

	private Player _owner;
	public Player owner { get { return _owner; } }


	public bool captured {  get { return _owner != null; } }

	public override void OnAttack(Attack attacker)
	{
		if (_canBeCaptured && attacker.isCombo)
		{
			var player = attacker.GetComponent<Avatar>().player;

			if(player != _owner)
			{
				var oldOwner = _owner;
				_owner = player;

				Event.dispatch(new ShrineCapturedEvent(this, player, oldOwner));
			}
		}
	}
	
	public void Reset()
	{
		_canBeCaptured = true;
		_owner = null;
	}

	public void Activate()
	{
		_canBeCaptured = true;
	}
}
