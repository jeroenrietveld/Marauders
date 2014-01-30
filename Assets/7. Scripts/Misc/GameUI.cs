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

	// Use this for initialization
	void Start () {
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
	}

	void OnGUI ()
	{
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

			GUI.Label (_cooldownUIPositions[playerIndex][0], (int)(GameManager.Instance.playersByTimeSync().IndexOf(player) + 1).ToString());
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
