Shader "Custom/Character" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
		CutoffHeight ("Height to cutoff", Float) = -1000
		CutoffSize ("Size to cutoff", Float) = 0.05
		
		LookupTable ("Lookup Table (RGB)", 2D) = "white" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" }
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;

			float CutoffHeight;
			float CutoffSize;
			
			sampler2D LookupTable;
			
			struct FragInput
			{
				float4 position : SV_POSITION;
				float2 texCoord : TEXCOORD0;
				float difference : TEXCOORD1;
			};
			
			FragInput vert(appdata_base input)
			{
				FragInput output;
				
				float height = mul(_Object2World, input.vertex).z;
				
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texCoord = input.texcoord.xy;	
				output.difference = height - CutoffHeight;
				
				return output;
			}
			
			half4 frag (FragInput input) : COLOR
			{
				float4 color = tex2D(_MainTex, input.texCoord);
			
				if(input.difference < 0)
				{
					discard;
				}
			
				if(input.difference < CutoffSize)
				{
					color = float4(tex2D(LookupTable, float2(-input.difference / CutoffSize, 0.5)));
				}
				
				return color;
			}
			
			ENDCG
		}
	}
}
