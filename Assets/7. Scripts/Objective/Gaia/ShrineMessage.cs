using UnityEngine;
using XInputDotNetPure;
using System.Collections;

public class ShrineMessage : Interactable
{
	public override void OnInteract (GameObject obj)
	{
	}	

	private string message;
	private GUIStyle style;

	public override void ShowMessage ()
	{
		Vector3 screenPoint = Camera.main.WorldToScreenPoint(this.transform.position);

		Shrine shrine = GetComponent<Shrine>();

		if (shrine)
		{
			if (shrine.capturable)
			{

			
					message = Locale.Current["shrine_capturable"];
					GUI.DrawTexture(new Rect(screenPoint.x, Screen.height - screenPoint.y, 32, 32), Images.Get(Button.B) );

				//}
			} else
			{
				message = Locale.Current["shrine_locked"];
			}


			//Displaying message
			GUI.Label (new Rect (screenPoint.x + 32, (Screen.height - screenPoint.y) + 5 , 500, 50), "<color='#ffffffff'><size='" + (int)(20f * ( (float)Screen.height / 700f )) + "'>" + message +"</size></color>", this.style);


		}
	}



	public void Start()
	{
		this.style = new GUIStyle();
		this.style.alignment = TextAnchor.UpperLeft;
		this.style.font = (Font)Resources.Load ("Textures/WorldSelect/BankGothic/BankGothicCMdBT-Medium", typeof(Font)); 
		this.style.fontStyle = FontStyle.Bold;
		this.style.richText = true;
	}
}
