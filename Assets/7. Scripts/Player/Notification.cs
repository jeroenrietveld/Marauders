using UnityEngine;
using System.Collections;

public class Notification : MonoBehaviour {

	private string _notification;
	private Timer _notificationTimer;
	private bool _active = false;
	private Rect _position;
	private Material _material;
	private Texture _texture;
	private Color _color;

	// Use this for initialization
	void Start () {
		_notificationTimer = new Timer (1.5f);

		_notificationTimer.AddCallback (_notificationTimer.startTime, delegate() {
			_active = true;
		});

		_notificationTimer.AddCallback (_notificationTimer.endTime, delegate() {
			_active = false;
		});

		_color = GetComponent<Avatar> ().player.color;
	}

	void OnGUI()
	{
		if(_active)
		{
			Graphics.DrawTexture(_position, _texture, _material);
			_position.y -= _notificationTimer.Phase() * 2;
		}
	}

	// Update is called once per frame
	void Update () {
		_notificationTimer.Update ();
	}

	public void Notify(Material mat, Texture tex, Vector3 pos)
	{
		if(_notificationTimer.running)
		{
			_notificationTimer.Stop();
			_active = false;
		}

		_material = mat;
		_texture = tex;
		_position = new Rect(pos.x - 32, pos.y - 32, 64, 64);

		_notificationTimer.Start ();
	}
}
