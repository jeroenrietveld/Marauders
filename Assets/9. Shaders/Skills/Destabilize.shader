Shader "Custom/Destabilize" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Transparent" }
		
		Pass {
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"
			
			sampler2D _MainTex;

			struct FragInput {
				float4 position : SV_POSITION;
				float3 cameraSpaceNormal : TEXCOORD0;
			};
			
			FragInput vert(appdata_base input)
			{
				FragInput output;
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.cameraSpaceNormal = mul(UNITY_MATRIX_MV, float4(input.normal, 0)).xyz;
				return output;
			}

			half4 frag (FragInput input) : COLOR
			{
				float3 cameraSpaceNormal = normalize(input.cameraSpaceNormal);
				
				return half4(0.9, 0.9, 0.9, 1 - dot(cameraSpaceNormal, float3(0, 0, 1)));
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
