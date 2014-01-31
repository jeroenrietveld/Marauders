using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {
	
	public List<Color> lightBulbColors = new List<Color> ();
	public List<Vector2> timeSyncSliceOffsets = new List<Vector2>();
	public List<Vector2> timeSyncSliceBounds = new List<Vector2>();
	public List<float> timeSyncSliceAlphas = new List<float> ();
	public List<int> shownTimeSync = new List<int> ();

	public Material timeSyncMaterial;
	public Material timeSyncSliceMaterial;
	public Texture timeSyncTexture;
	public Material lightbulbMat;
	public Texture lightbulbTex;
	
	private GUIStyle _style;
	private List<List<Rect>> _cooldownUIPositions;

	private List<PlayerUI> _playerUIs = new List<PlayerUI> ();

	// Use this for initialization
	void Start () {
		foreach(var player in GameManager.Instance.playerRefs)
		{
			var ui = new PlayerUI(player);
			_playerUIs.Add(ui);
		}

		/*
		foreach (Player player in GameManager.Instance.playerRefs) 
		{
			lightBulbColors.Add(new Color(0.5f, 0.5f, 0.5f, 0.5f));
			timeSyncSliceOffsets.Add(new Vector2(0, 0));
			timeSyncSliceBounds.Add(new Vector2(0, 0));
			timeSyncSliceAlphas.Add(0);
			shownTimeSync.Add(0);
		}

		_style = new GUIStyle();
		_style.alignment = TextAnchor.MiddleCenter;
		_style.font = (Font)Resources.Load ("Textures/WorldSelect/BankGothic/BankGothicCMdBT-Medium", typeof(Font)); 
		_style.fontStyle = FontStyle.Bold;
		_style.fontSize = 35;
		_style.richText = true;

		FillPositions ();
		*/
	}

	void OnGUI ()
	{
		/*
		var playerRefs = GameManager.Instance.playerRefs;

		for(int playerIndex = 0; playerIndex < playerRefs.Count; playerIndex++)
		{
			Player player = playerRefs[playerIndex];
			lightbulbMat.SetColor("_Color", lightBulbColors[playerIndex]);
			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][1], lightbulbTex, lightbulbMat);

			timeSyncMaterial.SetFloat("phase", shownTimeSync[player.indexInt] / (float)GameManager.Instance.matchSettings.timeSync);
			timeSyncMaterial.SetColor("playerColor", player.color);
			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][0], timeSyncTexture, timeSyncMaterial);

			int timeSync = GameManager.Instance.matchSettings.timeSync;
			GUI.Label(_cooldownUIPositions[playerIndex][0], ((int)(((float)player.timeSync/(float)timeSync) * 100)).ToString()+"%", _style);

			if(timeSyncSliceAlphas[playerIndex] > 0)
			{
				timeSyncSliceMaterial.SetFloat("upperBound", timeSyncSliceBounds[playerIndex].y);
				timeSyncSliceMaterial.SetFloat("lowerBound", timeSyncSliceBounds[playerIndex].x);
				timeSyncSliceMaterial.SetFloat("alpha", timeSyncSliceAlphas[playerIndex]);
				timeSyncSliceMaterial.SetColor("playerColor", player.color);

				var rect = _cooldownUIPositions[playerIndex][0];
				var center = rect.center += timeSyncSliceOffsets[playerIndex];

				var scale = 1 + timeSyncSliceOffsets[playerIndex].magnitude / 100;
				rect.width *= scale;
				rect.height *= scale;
				rect.center = center;

				Graphics.DrawTexture(rect, timeSyncTexture, timeSyncSliceMaterial);
			}

			GUI.Label (_cooldownUIPositions[playerIndex][0], (GameManager.Instance.playersByTimeSync().IndexOf(player) + 1).ToString());
		}
		*/
	}

	void Update()
	{
		foreach(var ui in _playerUIs)
		{
			ui.Update();
		}
	}

	private void FillPositions()
	{
		_cooldownUIPositions = new List<List<Rect>> ();
		List<Rect> positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (25, 25, 128, 128));
		positions.Add (new Rect (0 - 225, 0 - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 153, 25, 128, 128));
		positions.Add (new Rect (Screen.width - 225, 0 - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (25, Screen.height - 153, 128, 128));
		positions.Add (new Rect (0 - 225, Screen.height - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 153, Screen.height - 153, 128, 128));
		positions.Add (new Rect (Screen.width - 225, Screen.height - 225, 450, 450));
	}

	public Vector2 ToPlayerLocalScreenSpace(Vector2 v, Player p)
	{
		return v - _cooldownUIPositions[p.indexInt][0].center;
	}
}

public class PlayerUI
{
	private static Vector3[] GUI_POSITIONS = new []{ new Vector3(-1.3f, .75f), new Vector3(1.3f, .75f), new Vector3(-1.3f, -.75f), new Vector3(1.3f, -.75f) };

	public Vector3 position { get { return GUI_POSITIONS[_player.indexInt]; } }

	private Player _player;
	private GameObject _uiRoot;

	private GameObject _timeSyncIndicator;
	private Timer _timeSyncIndicatorUpdater = new Timer(1);
	private Timer _respawnTimer = new Timer ();
	private TextMesh _timeSyncPercentage;

	public PlayerUI(Player player)
	{
		_player = player;
		_uiRoot = new GameObject ();
		_uiRoot.transform.position = position;

		Init ();

		_timeSyncIndicatorUpdater.AddPhaseCallback(delegate {
			float timeSync = GameManager.Instance.matchSettings.timeSync;
			_timeSyncIndicator.renderer.material.SetFloat("phase", player.timeSync / timeSync);
			_timeSyncPercentage.text = ((int)((player.timeSync/timeSync) * 100)).ToString()+"%";
		});

		Event.register<PlayerTimeSyncEvent> (CreateTimeSyncSlice);
		Event.register<TimeBubbleAvatarExitEvent> (OnAvatarExit);
		Event.register<AvatarSpawnEvent> (OnAvatarSpawn);
	}

	~PlayerUI()
	{
		Event.unregister<PlayerTimeSyncEvent> (CreateTimeSyncSlice);
		Event.unregister<TimeBubbleAvatarExitEvent> (OnAvatarExit);
		Event.unregister<AvatarSpawnEvent> (OnAvatarSpawn);
	}

	private void Init()
	{
		var color = _player.color;

		_timeSyncIndicator = ResourceCache.GameObject ("Prefabs/GUI/TimeSyncIndicator");
		_timeSyncIndicator.renderer.material.SetColor ("playerColor", color);
		_timeSyncIndicator.transform.SetParentKeepLocal (_uiRoot.transform);
		
		var timeSyncIndicatorEdge = ResourceCache.GameObject ("Prefabs/GUI/TimeSyncIndicatorEdge");
		timeSyncIndicatorEdge.renderer.material.color = color;
		timeSyncIndicatorEdge.transform.SetParentKeepLocal (_uiRoot.transform);

		var respawnIndicator = ResourceCache.GameObject ("Prefabs/GUI/TimeSyncIndicatorEdge");
		respawnIndicator.renderer.material.color = color;
		respawnIndicator.transform.SetParentKeepLocal (_uiRoot.transform);

		_respawnTimer.AddTickCallback (delegate
		{
			respawnIndicator.transform.localScale = Vector3.Lerp (Vector3.one, timeSyncIndicatorEdge.transform.localScale, _respawnTimer.Phase ());
		});

		_timeSyncPercentage = new GameObject ().AddComponent<TextMesh> ();
		_timeSyncPercentage.gameObject.layer = LayerMask.NameToLayer ("GUI");
		_timeSyncPercentage.transform.SetParentKeepLocal (_uiRoot.transform);
		_timeSyncPercentage.alignment = TextAlignment.Center;
		_timeSyncPercentage.font = (Font)Resources.Load ("Textures/WorldSelect/BankGothic/BankGothicCMdBT-Medium", typeof(Font));
		_timeSyncPercentage.renderer.material = _timeSyncPercentage.font.material;
		_timeSyncPercentage.anchor = TextAnchor.MiddleCenter;
		_timeSyncPercentage.characterSize = .025f;

		_timeSyncPercentage.text = "0%";
	}

	public void Update()
	{
		_timeSyncIndicatorUpdater.Update ();
		_respawnTimer.Update ();
	}

	private void CreateTimeSyncSlice(PlayerTimeSyncEvent evt)
	{
		if(evt.player != _player) return;

		var slice = ResourceCache.GameObject ("Prefabs/GUI/TimeSyncSlice");
		slice.renderer.material.SetColor ("playerColor", _player.color);

		float 
			curr = (_player.timeSync) / (float)GameManager.Instance.matchSettings.timeSync,
			prev = (_player.timeSync + evt.amount) / (float)GameManager.Instance.matchSettings.timeSync;

		slice.renderer.material.SetFloat("upperBound", Mathf.Max(curr, prev));
		slice.renderer.material.SetFloat("lowerBound", Mathf.Min(curr, prev));
		slice.renderer.material.SetFloat("alpha", 1);
		
		if(evt.amount < 0)
		{
			slice.transform.position = _uiRoot.transform.position;
			ObjectMovement.Create(slice, Vector3.Lerp(slice.transform.position, Vector3.zero, .5f), Vector3.one, _timeSyncIndicatorUpdater.endTime);
			_timeSyncIndicator.renderer.material.SetFloat("phase", _player.timeSync / (float)GameManager.Instance.matchSettings.timeSync);
		}
		else
		{
			if(evt.hasPosition)
			{
				slice.transform.position = CameraSettings.cameraSettings.PointToWorldPoint(evt.worldSpacePosition);
				ObjectMovement.Create(slice, _uiRoot.transform.position, slice.transform.localScale, _timeSyncIndicatorUpdater.endTime);
				slice.transform.localScale = Vector3.one;
			}
			else
			{
				slice.transform.position = Vector3.Lerp(_uiRoot.transform.position, Vector3.zero, .5f);
				ObjectMovement.Create(slice, _uiRoot.transform.position, slice.transform.localScale, _timeSyncIndicatorUpdater.endTime);
				slice.transform.localScale = Vector3.one;
			}
		}

		_timeSyncIndicatorUpdater.Start ();
	}

	private void OnAvatarSpawn(AvatarSpawnEvent evt)
	{
		if(evt.player != _player) return;
		
		_respawnTimer.endTime = evt.spawnDelay;
		_respawnTimer.Start ();
	}

	private void OnAvatarExit(TimeBubbleAvatarExitEvent evt)
	{
		if(evt.avatar.player != _player) return;

		_respawnTimer.endTime = evt.respawnDelay;
		_respawnTimer.Start ();
	}
}
