using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using System.Collections;

public class LoadingScreen : MonoBehaviour
{
        private bool loading = true;
        public Texture loadingTexture;
        public GUISkin skin;

        void Awake()
        {         
            DontDestroyOnLoad(gameObject);
        }

        void Update()
        {
            if (Application.isLoadingLevel)
            {
                loading = true;
            }
            else
            {
                loading = false;
            }
        }

        void OnGUI()
        {
            if (loading)
            {
                GUI.skin = skin;
                GUI.depth = -20;
                GUI.DrawTexture(new Rect(0, 0, Screen.width, Screen.height), loadingTexture, ScaleMode.ScaleToFit);
                GUI.skin.label.alignment = TextAnchor.LowerCenter;
                GUI.Label(new Rect(0, 0, Screen.width, Screen.height), "Loading...");
            }
        }
    }
