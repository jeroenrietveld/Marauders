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
			_shrineMovement.SetShrineActive(value);
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

	private Timer _underAttackTimer = new Timer (1.5f);
	public bool underAttack { get { return _underAttackTimer.running; } }

	public int shrineGUIIndex;
	private Vector3 _shrineGUIPosition = new Vector3(0, 0.9f);
	private Vector3 _shrineGUIPositionOffset = new Vector3 (0.2f, 0);
	private GameObject _shrineGUI;	

	private ShrineMovement _shrineMovement;

	void Start()
	{
        shrineActivatedConquered = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
        shrineSourceHit = GameManager.Instance.soundInGame.AddAndSetupAudioSource(gameObject, SoundSettingTypes.volume);
		_orbs = GetComponentsInChildren<ShrineOrb> ();
		_light = GetComponentInChildren<Light> ();

		_shrineGUI = ResourceCache.GameObject ("Prefabs/GUI/ShrineGUI");
		//_shrineGUIPosition = CameraSettings.cameraSettings.PointToWorldPoint (_shrineGUIPosition);

		if(shrineGUIIndex != 1)
		{
			_shrineGUIPosition += (shrineGUIIndex == 0) ? - _shrineGUIPositionOffset : _shrineGUIPositionOffset;
		}

		_shrineGUI.transform.position = _shrineGUIPosition;

		_lightTimer = new Timer (1);
		_lightTimer.AddTickCallback(delegate
		{
			var color = Color.Lerp(_previousLightColor, _targetLightColor, _lightTimer.Phase());

			_light.color = color;

			foreach(var orb in _orbs)
			{
				orb.SetColor(color);
			}
			
			_shrineGUI.renderer.material.color = color;
		});

		_shrineMovement = GetComponentInChildren<ShrineMovement> ();

		Reset ();
	}

	void Update()
	{
		_lightTimer.Update ();
		_underAttackTimer.Update ();
	}

	protected override void ApplyAttack(DamageSource source)
	{
        if(capturable)
        {
			GameManager.Instance.soundInGame.PlaySoundIndex(shrineSourceHit, "ShrineHit", source.comboCount, false);
            if (source.isCombo)
            {
                var player = source.inflicter;

                if (player != _owner)
                {
                    var oldOwner = owner;
                    owner = player;

                    Event.dispatch(new ShrineCapturedEvent(this, player, oldOwner));
                    GameManager.Instance.soundInGame.PlaySound(shrineActivatedConquered, "Shrine-conquered", true);
                }
            }

			_underAttackTimer.Start();
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
