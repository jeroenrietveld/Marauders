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

    private AudioSource shrineSourceHit;
    private AudioSource shrineActivatedConquered;

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
        shrineActivatedConquered = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
        shrineSourceHit = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
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

	protected override void ApplyAttack(Attack attacker)
	{
        if(capturable)
        {
            GameManager.Instance.soundInGame.PlaySoundIndex(shrineSourceHit, "ShrineHit", attacker.comboCount, false);
            if (attacker.isCombo)
            {
                var player = attacker.GetComponent<Avatar>().player;

                if (player != _owner)
                {
                    var oldOwner = owner;
                    owner = player;

					GameObject Global = GameObject.Find("_GLOBAL");
					Announcer announcer = Global.GetComponent<Announcer>();
					announcer.Announce(AnnouncementType.ShrineCapture, "Shrine has been captured");

                    Event.dispatch(new ShrineCapturedEvent(this, player, oldOwner));
                    GameManager.Instance.soundInGame.PlaySound(shrineActivatedConquered, "Shrine-conquered", true);
                }
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
		GameObject Global = GameObject.Find("_GLOBAL");
		Announcer announcer = Global.GetComponent<Announcer>();
		announcer.Announce(AnnouncementType.ShrineCapture, "Shrine can be captured");

		capturable = true;
        GameManager.Instance.soundInGame.PlaySound(shrineActivatedConquered, "Shrine-activated", true);
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
