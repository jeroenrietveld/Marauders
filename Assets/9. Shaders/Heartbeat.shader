Shader "Custom/Heartbeat" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		_color ("Color", Color) = (1, 1, 1, 1)
		_phase ("phase", Float) = 1
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
			half3 _color;
			half _phase;

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
				
				half damageAlpha = .25;
				
				return half4(_color, (damageAlpha + (c.a < _phase) * (1 - damageAlpha)) * c.r);
			}
			ENDCG
		}
	}
}
