Shader "Custom/TrophyColor" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Pass {
			Tags { "RenderType"="Opaque" }
		
			CGPROGRAM
			#pragma vertex vert 
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half4 _Color;

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

				if(c.a == 0) {
					discard;
				}

				return _Color;
			}
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
