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
	private static Color
		INACTIVE_COLOR = new Color(.5f, .5f, .5f),
		CAPTURABLE_COLOR = new Color(1, 1, 1);


	private bool _capturable;
	public bool capturable
	{
		get
		{
			return _capturable;
		}
		private set
		{
			_capturable = value;
			SetColor(value ? CAPTURABLE_COLOR : INACTIVE_COLOR);
		}
	}

	private Player _owner;
	public Player owner
	{
		get
		{
			return _owner;
		}
		private set
		{
			_owner = value;
			if(value == null)
			{
				SetColor(capturable ? CAPTURABLE_COLOR : INACTIVE_COLOR);
			}
			else
			{
				SetColor(_owner.color);
			}
		}
	}
	
	public bool captured {  get { return _owner != null; } }

	private ShrineOrb[] _orbs;

	void Start()
	{
		_orbs = GetComponentsInChildren<ShrineOrb> ();

		Reset ();
	}

	public override void OnAttack(Attack attacker)
	{
		if (capturable && attacker.isCombo)
		{
			var player = attacker.GetComponent<Avatar>().player;

			if(player != _owner)
			{
				var oldOwner = owner;
				owner = player;

				Event.dispatch(new ShrineCapturedEvent(this, player, oldOwner));
			}
		}
	}
	
	public void Reset()
	{
		capturable = false;
		owner = null;
	}

	public void Activate()
	{
		capturable = true;
	}

	private void SetColor(Color color)
	{
		foreach(var orb in _orbs)
		{
			orb.SetColor(color);
		}
	}
}
