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
		INACTIVE_COLOR = Color.black,
		CAPTURABLE_COLOR = Color.white;


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
			UpdateColor();
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
			UpdateColor();
		}
	}
	
	public bool captured {  get { return _owner != null; } }

	private ShrineOrb[] _orbs;

	private Color _previousLightColor;
	private Color _targetLightColor;
	private Timer _lightTimer;
	private Light _light;

	void Start()
	{
		_orbs = GetComponentsInChildren<ShrineOrb> ();
		_light = GetComponentInChildren<Light> ();

		_lightTimer = new Timer (1);
		_lightTimer.AddTickCallback(delegate
		{
			var color = Color.Lerp(_previousLightColor, _targetLightColor, _lightTimer.Phase());

			_light.color = color;

			foreach(var orb in _orbs)
			{
				orb.SetColor(color);
			}
		});

		Reset ();
	}

	void Update()
	{
		_lightTimer.Update ();
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
		UpdateColor ();
	}

	public void Activate()
	{
		capturable = true;
	}

	private void UpdateColor()
	{
		Color orbColor = INACTIVE_COLOR;

		if(_capturable)
		{
			orbColor = _owner != null ? _owner.color : CAPTURABLE_COLOR;
		}

		_previousLightColor = _light.color;
		_targetLightColor = orbColor;
		_lightTimer.Start ();
	}
}
