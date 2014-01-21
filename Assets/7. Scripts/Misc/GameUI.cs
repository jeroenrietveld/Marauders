using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {
	
	public List<Color> lightBulbColors = new List<Color> ();

	private Material _material;
	private Texture _texture;
	private Material _lightbulbMat;
	private Texture _lightbulbTex;

	private List<List<Rect>> _cooldownUIPositions;
	private Color[] _skillColors = new Color[]{Color.red, Color.blue, Color.yellow};
	private Texture[] _skillIcons;

	// Use this for initialization
	void Start () {
		_material = Resources.Load("Materials/Cooldown", typeof(Material)) as Material;
		_texture = Resources.Load ("Textures/Cooldown", typeof(Texture)) as Texture;

		_lightbulbMat = Resources.Load ("Materials/Lightbulb", typeof(Material)) as Material;
		_lightbulbTex = Resources.Load ("Textures/GUI_lightbulb", typeof(Texture)) as Texture;

		_skillIcons = new Texture[]
		{
			Resources.Load ("Textures/Offensive_icon", typeof(Texture)) as Texture,
			Resources.Load ("Textures/Defensive_icon", typeof(Texture)) as Texture,
			Resources.Load ("Textures/Utility_icon", typeof(Texture)) as Texture
		};

		foreach (Player player in GameManager.Instance.playerRefs) 
		{
			lightBulbColors.Add(new Color(0.5f, 0.5f, 0.5f, 0.5f));
		}

		FillPositions ();
	}

	void OnGUI ()
	{
		var playerRefs = GameManager.Instance.playerRefs;

		for(int playerIndex = 0; playerIndex < playerRefs.Count; playerIndex++)
		{
			Player player = playerRefs[playerIndex];
			_lightbulbMat.SetColor("_Color", lightBulbColors[playerIndex]);

			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][1], _lightbulbTex, _lightbulbMat);

			_material.SetFloat("phase", player.timeSync / (float)GameManager.Instance.matchSettings.timeSync);
			_material.SetColor("playerColor", player.color);

			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][0], _texture, _material);

			GUI.Label (_cooldownUIPositions[playerIndex][0], (GameManager.Instance.playersByTimeSync().IndexOf(player) + 1).ToString());
		}
	}

	private void FillPositions()
	{
		_cooldownUIPositions = new List<List<Rect>> ();
		List<Rect> positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (10, 10, 100, 100));
		positions.Add (new Rect (0 - 225, 0 - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 110, 10, 100, 100));
		positions.Add (new Rect (Screen.width - 225, 0 - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (10, Screen.height - 110, 100, 100));
		positions.Add (new Rect (0 - 225, Screen.height - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 110, Screen.height - 60, 100, 100));
		positions.Add (new Rect (Screen.width - 225, Screen.height - 225, 450, 450));
	}
}
