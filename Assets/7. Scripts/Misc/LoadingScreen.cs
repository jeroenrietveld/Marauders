using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
        private bool _loading = true;
        public Texture controlsTexture;
        public GUISkin skin;
        public string tip;
        private bool _done = false;

        private string _loadingText;
        void Awake()
        {         
            DontDestroyOnLoad(gameObject);
        }

        void Start()
        {
            tip = Tip.GetRandom();
            _loadingText = Locale.Current["loading_text"];
        }

        void Update()
        {
            if (Application.isLoadingLevel && Application.loadedLevelName.Equals("Menu"))
            {
                _loading = true;
                _done = true;
            }
            else
            {
                _loading = false;
            }
        }

        void OnGUI()
        {
            
            if (_loading && _done)
            {
                float fontScale = Screen.width / 768f;               
                GUI.skin = skin;
                GUI.skin.label.fontSize = (int)(15 * fontScale);
                GUI.depth = -10;
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.depth = -20;
                GUI.skin.label.alignment = TextAnchor.UpperLeft;
                GUI.Label(new Rect(70, 70, Screen.width / 2, Screen.height - 70), _loadingText);
                GUI.DrawTexture(new Rect((Screen.width/2), 50, (Screen.width/2) - 10, Screen.height*0.75f), controlsTexture, ScaleMode.ScaleToFit);
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height - 60), "<color=cyan>Tip: " + tip + "</color>");
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height - 10), "Loading...");               
            }
            if(!_loading && _done)
            {
                GameManager.Instance.PauseGame();
                float fontScale = Screen.width / 768f;
                GUI.skin = skin;
                GUI.skin.label.fontSize = (int)(15 * fontScale);
                GUI.depth = -10;
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.depth = -20;
                GUI.skin.label.alignment = TextAnchor.UpperLeft;
                GUI.Label(new Rect(70, 70, Screen.width / 2, Screen.height - 70), _loadingText);
                GUI.DrawTexture(new Rect((Screen.width / 2), 50, (Screen.width / 2) - 10, Screen.height*0.75f), controlsTexture, ScaleMode.ScaleToFit);
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height - 60), "<color=cyan>Tip: " + tip + "</color>");
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height - 10), "Press <color=green>A</color> to start the game.");
                if( GameManager.Instance.playerRefs.OrderBy(x => x.indexInt).First().controller.JustPressed(XInputDotNetPure.Button.A))
                {
                    _done = false;
                    GameManager.Instance.ResumeGame();

                    //Already set the next random tip
                    tip = Tip.GetRandom();
                }
            }
        }
    }

public class Tip
{
    public static string GetRandom()
    {
        System.Random r = new System.Random();
        return Locale.Current["tip_" + r.Next()%11];
    }
}