using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
        private bool _loading = true;
        public Texture loadingTexture;
        public GUISkin skin;

        private bool _done = false;

        void Awake()
        {         
            DontDestroyOnLoad(gameObject);
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
                GUI.skin.label.fontSize = (int)(20 * fontScale);
                GUI.depth = -10;
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.depth = -20;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loadingTexture, ScaleMode.ScaleToFit);
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Loading...");               
            }
            if(!_loading && _done)
            {
                GameManager.Instance.PauseGame();
                float fontScale = Screen.width / 768f;
                GUI.skin = skin;
                GUI.skin.label.fontSize = (int)(20 * fontScale);
                GUI.depth = -10;
                GUI.Box(new Rect(0, 0, Screen.width, Screen.height), "");
                GUI.depth = -20;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loadingTexture, ScaleMode.ScaleToFit);
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Press A to start the game.");
                if( GameManager.Instance.playerRefs.OrderBy(x => x.indexInt).First().controller.JustPressed(XInputDotNetPure.Button.A))
                {
                    _done = false;
                    GameManager.Instance.ResumeGame();
                }
            }
        }
    }
