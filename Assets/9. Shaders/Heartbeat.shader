Shader "Custom/Heartbeat" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		playerColor ("Player Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "RenderType"="Opaque" }
		
		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha
			AlphaTest Greater 0
			ZTest Always
			ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half3 playerColor;
			half health;
			half previousHealth;

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
				
				half justDamaged = (1 - step(health, c.a)) * step(previousHealth, c.a);
				half damageAlpha = .25;
				
				return half4(lerp(playerColor, half3(1, 1, 1), justDamaged), (damageAlpha + step(previousHealth, c.a) * (1 - damageAlpha)) * c.r);
			}
			ENDCG
		}
	}
}
