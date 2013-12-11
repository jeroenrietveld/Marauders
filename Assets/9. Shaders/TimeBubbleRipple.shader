Shader "Custom/TimeBubbleRipple" {
	Properties {
		FractalLayer ("Fractal (RGB, Phase)", 2D) = "white" {}
		LookupTable ("Lookup Table (RGB)", 2D) = "white" {}
		RipplePhase ("Ripple Phase", Float) = 0
		
		RippleColor ("Ripple Color", Color) = (1, 1, 1, 1)
		SrcColorWeight ("Source Color weight", Float) = 0.25
		RippleSize ("Ripple Size", Float) = 0.1
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Cull Off
		Blend SrcAlpha One
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D FractalLayer;
			sampler2D LookupTable;
			
			float RipplePhase;
			float4 RippleColor;
			
			float SrcColorWeight;
			float RippleSize;

			struct FragmentInput {
				float4 position : SV_POSITION;
				float2 texCoord0 : TEXCOORD0;
			};
			
			FragmentInput vert(appdata_base input)
			{
				FragmentInput output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texCoord0 = input.texcoord;
				
				return output;
			}

			half4 frag (FragmentInput input) : COLOR
			{
				float4 fractal = tex2D(FractalLayer, input.texCoord0);
				
				float localPhase = (fractal.a - RipplePhase) * RippleSize;
				if(localPhase <= 0 || localPhase > 1) discard;
				
				//return RippleColor * tex2D(LookupTable, float2(1 - fractal.a, 0.5)) * fractal.g;
				return RippleColor * tex2D(LookupTable, float2(/*fractal.r * */saturate(localPhase), 0.5)) * fractal.g;
			}
			ENDCG
		}
	}
}
