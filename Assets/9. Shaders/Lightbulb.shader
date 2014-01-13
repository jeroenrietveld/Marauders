Shader "Custom/Lightbulb" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 0.5)
	}
	SubShader {
		Pass
		{
			Tags { "Queue"="Transparent" "RenderType"="Transparent" }
			
			Blend SrcAlpha OneMinusSrcAlpha
			
			Lighting Off
			
			SetTexture [_MainTex] 
			{
				ConstantColor[_Color]
				combine constant * texture
			}
		}
	}
	FallBack "Diffuse"
}
