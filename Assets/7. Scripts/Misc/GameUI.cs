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
			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][4], _lightbulbTex, _lightbulbMat); 

			_material.SetFloat("phase", player.timeSync / (float)GameManager.Instance.timeSyncLimit);
			_material.SetColor("playerColor", player.color);
			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][3], _texture, _material);
			GUI.Label (_cooldownUIPositions[playerIndex][3], (GameManager.Instance.playersByTimeSync().IndexOf(player) + 1).ToString());

			for(int i = 0; i < player.skills.Length; i++)
			{
				SkillBase skillBase = (SkillBase) player.avatar.GetComponent(player.skills[i]);
				_material.SetFloat("phase", (skillBase == null) ? 1 : skillBase.cooldown.Phase());
				_material.SetColor("playerColor", _skillColors[i]);
				Graphics.DrawTexture(_cooldownUIPositions[playerIndex][i], _texture, _material);
				Graphics.DrawTexture(_cooldownUIPositions[playerIndex][i], _skillIcons[i]);
			}
		}
	}

	private void FillPositions()
	{
		_cooldownUIPositions = new List<List<Rect>> ();
		List<Rect> positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (80, 10, 40, 40));
		positions.Add (new Rect (60, 60, 40, 40));
		positions.Add (new Rect (10, 80, 40, 40));
		positions.Add (new Rect (10, 10, 50, 50));
		positions.Add (new Rect (0 - 225, 0 - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 120, 10, 40, 40));
		positions.Add (new Rect (Screen.width - 100, 60, 40, 40));
		positions.Add (new Rect (Screen.width - 50, 80, 40, 40));
		positions.Add (new Rect (Screen.width - 60, 10, 50, 50));
		positions.Add (new Rect (Screen.width - 225, 0 - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (80, Screen.height - 50, 40, 40));
		positions.Add (new Rect (60, Screen.height - 100, 40, 40));
		positions.Add (new Rect (10, Screen.height - 120, 40, 40));
		positions.Add (new Rect (10, Screen.height - 60, 50, 50));
		positions.Add (new Rect (0 - 225, Screen.height - 225, 450, 450));
		
		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 120, Screen.height - 50, 40, 40));
		positions.Add (new Rect (Screen.width - 100, Screen.height - 100, 40, 40));
		positions.Add (new Rect (Screen.width - 50, Screen.height - 120, 40, 40));
		positions.Add (new Rect (Screen.width - 60, Screen.height - 60, 50, 50));
		positions.Add (new Rect (Screen.width - 225, Screen.height - 225, 450, 450));
	}
}
