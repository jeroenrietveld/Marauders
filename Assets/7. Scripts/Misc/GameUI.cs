using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class GameUI : MonoBehaviour {
	
	private Material _material;
	private Texture _texture;
	private List<List<Rect>> _cooldownUIPositions;
	private Color[] _skillColors = new Color[]{Color.red, Color.blue, Color.yellow};
	private Texture[] _skillIcons;

	// Use this for initialization
	void Start () {
		_material = Resources.Load("Materials/Cooldown", typeof(Material)) as Material;
		_texture = Resources.Load ("Textures/Cooldown", typeof(Texture)) as Texture;
		_skillIcons = new Texture[]
		{
			Resources.Load ("Textures/Offensive_icon", typeof(Texture)) as Texture,
			Resources.Load ("Textures/Defensive_icon", typeof(Texture)) as Texture,
			Resources.Load ("Textures/Utility_icon", typeof(Texture)) as Texture
		};

		_cooldownUIPositions = new List<List<Rect>> ();
		List<Rect> positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (80, 10, 40, 40));
		positions.Add (new Rect (60, 60, 40, 40));
		positions.Add (new Rect (10, 80, 40, 40));
		positions.Add (new Rect (10, 10, 50, 50));

		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 120, 10, 40, 40));
		positions.Add (new Rect (Screen.width - 100, 60, 40, 40));
		positions.Add (new Rect (Screen.width - 50, 80, 40, 40));
		positions.Add (new Rect (Screen.width - 60, 10, 50, 50));

		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (80, Screen.height - 50, 40, 40));
		positions.Add (new Rect (60, Screen.height - 100, 40, 40));
		positions.Add (new Rect (10, Screen.height - 120, 40, 40));
		positions.Add (new Rect (10, Screen.height - 60, 50, 50));

		positions = new List<Rect> ();
		_cooldownUIPositions.Add (positions);
		positions.Add (new Rect (Screen.width - 120, Screen.height - 50, 40, 40));
		positions.Add (new Rect (Screen.width - 100, Screen.height - 100, 40, 40));
		positions.Add (new Rect (Screen.width - 50, Screen.height - 120, 40, 40));
		positions.Add (new Rect (Screen.width - 60, Screen.height - 60, 50, 50));
	}

	void OnGUI ()
	{
		var playerRefs = GameManager.Instance.playerRefs;

		for(int playerIndex = 0; playerIndex < playerRefs.Count; playerIndex++)
		{
			PlayerRef player = playerRefs[playerIndex];

			_material.SetFloat("phase", 0.5f);
			_material.SetColor("playerColor", player.color);
			Graphics.DrawTexture(_cooldownUIPositions[playerIndex][3], _texture, _material);

			for(int i = 0; i < player.skills.Length; i++)
			{
				SkillBase skillBase = (SkillBase) player.avatar.GetComponent(player.skills[i]);
				_material.SetFloat("phase", skillBase.cooldown.Phase());
				_material.SetColor("playerColor", _skillColors[i]);
				Graphics.DrawTexture(_cooldownUIPositions[playerIndex][i], _texture, _material);
				Graphics.DrawTexture(_cooldownUIPositions[playerIndex][i], _skillIcons[i]);
			}
		}
	}
}
