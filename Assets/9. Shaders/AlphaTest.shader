Shader "Custom/AlphaTest" { 
	Properties { 
	   _MainTex ("Font Texture", 2D) = "white" {} 
	   _AlphaPercentage ("Alpha Percentage", range(0.0, 1.0)) = 0.0
	} 

	SubShader { 
		Tags { "Queue"="AlphaTest" } 
		AlphaTest Greater [_AlphaPercentage]

		Pass {
			SetTexture[_MainTex] {
				Combine texture, texture * primary
			}
		}
	}
}