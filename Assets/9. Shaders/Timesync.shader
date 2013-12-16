Shader "Custom/Timesync" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
		Percentage ("Timesync percentage", Range(0.0, 1.0)) = 0.5
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			fixed Percentage;
			
			struct FragInput
			{
				float4 position : SV_POSITION;
				float2 texCoord : TEXCOORD0;
			};
			
			FragInput vert(appdata_base input)
			{
				FragInput output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texCoord = input.texcoord.xy;	
				
				return output;
			}
			
			half4 frag (FragInput input) : COLOR
			{
				float4 color;
			
				if(input.texCoord.x < 1 - Percentage) {
					color = float4(0.1, 0.1, 0.1, 1);
				} else {
					color = tex2D(_MainTex, input.texCoord);
				}
				
				return color;
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
