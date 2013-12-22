Shader "Custom/Heartbeat" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		playerColor ("Player Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Opaque" }
		
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			AlphaTest Greater 0
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half3 playerColor;
			float health;

			struct FragInput {
				float4 position : SV_POSITION;
				float2 uv_MainTex : TEXCOORD0;
			};
			
			FragInput vert(appdata_base input)
			{
				FragInput output;
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.uv_MainTex = input.texcoord.xy;
				return output;
			}

			half4 frag (FragInput input) : COLOR
			{
				half4 c = tex2D (_MainTex, input.uv_MainTex);
				
				return half4(playerColor, (0.5 + step(health, c.a) * 0.5) * c.r);
			}
			ENDCG
		}
	}
}
