Shader "Custom/TimeBubbleRipple" {
	Properties {
		FractalLayer ("Fractal (RGB, Phase)", 2D) = "white" {}
		LookupTable ("Lookup Table (RGB)", 2D) = "white" {}
		RipplePhase ("Ripple Phase", Float) = 0
		RippleCompression ("Ripple Compression", Float) = 5
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull Off
		Blend One One
		ZWrite Off
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D FractalLayer;
			sampler2D LookupTable;
			
			float RipplePhase;
			float RippleCompression;

			struct FragmentInput {
				float4 position : SV_POSITION;
				float2 texCoord : TEXCOORD0;
			};
			
			FragmentInput vert(appdata_base input)
			{
				FragmentInput output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texCoord = input.texcoord;
				
				return output;
			}

			half4 frag (FragmentInput input) : COLOR
			{
				float4 fractal = tex2D(FractalLayer, input.texCoord);
				
				float localPhase = (fractal.a - RipplePhase) * fractal.g * RippleCompression;
				if(localPhase <= 0 || localPhase > 1) discard;
				
				//return tex2D(LookupTable, float2(1 - fractal.a, 0.5)) * fractal.g;
				return float4(tex2D(LookupTable, float2(localPhase, 0.5)).rgb, 1.0);
			}
			ENDCG
		}
	}
}
