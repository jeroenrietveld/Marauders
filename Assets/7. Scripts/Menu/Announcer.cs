using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

public class Announcer : MonoBehaviour {
	/// <summary>
	/// A list of Announcements that should be made
	/// </summary>
	private List<Announcement> _announcements;

	/// <summary>
	/// The current Announcement
	/// </summary>
	private Announcement _currentAnnouncement;

	/// <summary>
	/// The current announcement's opacity.
	/// </summary>
	private int _currentAnnouncementOpacity ;

	/// <summary>
	/// The duration of the an Announcement.
	/// </summary>
	public float announcementDuration { get; set; }

	/// <summary>
	/// The duration of the fadeout
	/// </summary>
	public float announcementFadeout {get;set;}

	/// <summary>
	/// The style of the Announcement
	/// </summary>
	public GUIStyle announcementStyle { get; set; }

	/// <summary>
	/// Gets or sets the announcement opacity.
	/// </summary>
	public int announcementOpacity {get;set;}

	/// <summary>
	/// The timer that handles the fadeout
	/// </summary>
	/// <value>The _fadeout timer.</value>
	private Timer _fadeoutTimer { get; set; }

	/// <summary>
	/// The timer that handles the message display
	/// </summary>
	/// <value>The _message timer.</value>
	private Timer _messageTimer { get;set; }

	public void Announce(AnnouncementType type, string text)
	{
		this.Announce ( new Announcement(type, text, ""));
	}

	public void Announce(AnnouncementType type, string text, string voiceUrl)
	{
		this.Announce ( new Announcement(type, text, voiceUrl));
	}

	public void Announce(Announcement announcement)
	{
		this._announcements.Add (announcement);

		UpdateCurrentMessage();
	}

	private void UpdateCurrentMessage()
	{
		//Shoulc pick a next Announcement
		if (_announcements.Count > 0 && _currentAnnouncement == null)
		{
			//Taking the first Announcement, causing the other Announcement to shift down
			this._currentAnnouncement = _announcements[0];
			this._announcements.RemoveAt(0);
			this._currentAnnouncementOpacity = this.announcementOpacity;
			
			//Starting the timer
			_messageTimer.Start();
		}
	}

	// Use this for initialization
	void Start () {
		this._announcements = new List<Announcement> ();
		this.announcementDuration = 3000;
		this.announcementFadeout = 500;
		this.announcementOpacity = 99; // ranges from 0-100, 100 is full opacity 0 is not visible
		this.announcementStyle = new GUIStyle();
		this.announcementStyle.alignment = TextAnchor.MiddleCenter;
		this.announcementStyle.font = (Font)Resources.Load ("Textures/WorldSelect/BankGothic/BankGothicCMdBT-Medium", typeof(Font)); 
		this.announcementStyle.fontStyle = FontStyle.Bold;
		this.announcementStyle.richText = true;

		//Initialize timer
		_fadeoutTimer = new Timer(0.5f);
		_fadeoutTimer.AddTickCallback( 
			delegate() 
		    {
				this._currentAnnouncementOpacity = (int)((1f - _fadeoutTimer.Phase()) * (float)this.announcementOpacity);
			}
		);
		_fadeoutTimer.AddPhaseCallback(
			delegate() 
			{
				this._currentAnnouncement = null;
				this.UpdateCurrentMessage();

			}
		);
	
		//The messagetimer
		_messageTimer = new Timer(2.5f);
		_messageTimer.AddPhaseCallback( _fadeoutTimer.Start );
	}

	
	// Update is called once per frame
	void Update () 
	{
		//Updating the timers
		_messageTimer.Update ();
		_fadeoutTimer.Update ();
	}

	void OnGUI()
	{
		if (_currentAnnouncement != null)
		{
			GUI.Label (
				new Rect(0, 0, Screen.width, Screen.height),
				"<color='#FFFFFF" + this._currentAnnouncementOpacity.ToString("D2") + "'><size='" + (int)(40f * ( (float)Screen.height / 700f )) + "'>" + this._currentAnnouncement.text + "</size></color>",
				this.announcementStyle);
			Debug.Log ("<color='#FFFFFF" + this._currentAnnouncementOpacity.ToString("D2") + "'>Opacity: " + this._currentAnnouncementOpacity.ToString("D2") + "</color>");
		}
	}
}

public enum AnnouncementType
{
	ShrineCapture
}

public class Announcement
{
	public Announcement(AnnouncementType type, string text, string voiceUrl)
	{
		this.type = type;
		this.text = text;
		this.voiceUrl = voiceUrl;
	}

	public AnnouncementType type;
	public string text;
	public string voiceUrl;
}


