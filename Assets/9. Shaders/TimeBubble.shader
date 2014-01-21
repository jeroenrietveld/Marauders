Shader "Custom/TimeBubble" {
	Properties {
		FractalLayer1 ("Fractal Layer 1 (Grayscale)", 2D) = "white" {}
		FractalLayer2 ("Fractal Layer 2 (Grayscale)", 2D) = "white" {}
		
		FractalOffsets ("Fractal Sampling Offsets (x1, y1, x2, y2)", Vector) = (0, 0, 0, 0)
		
		DistortionCenter1 ("Distortion ripple center value 1", Float) = 0.5
		DistortionCenter2 ("Distortion ripple center value 2", Float) = 0.75
		DistortionRange1 ("Distortion ripple value range 1", Float) = 0.0125
		DistortionRange2 ("Distortion ripple value range 2", Float) = 0.0125
		DistortionColor1 ("Distortion Color 1", Color) = (1, 1, 1, 1)
		DistortionColor2 ("Distortion Color 2", Color) = (1, 1, 1, 1)
		
		FillAlpha ("Fill Alpha", Float) = 0.125
	}
	SubShader {
		Tags { "RenderType"="Transparent" "Queue"="Transparent" }
		
		Pass {
			Blend One OneMinusSrcColor
			Cull Off
			ZWrite Off
		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D FractalLayer1;
			sampler2D FractalLayer2;
			
			float4 FractalOffsets;
			
			float DistortionCenter1;
			float DistortionCenter2;
			float DistortionRange1;
			float DistortionRange2;
			
			float4 DistortionColor1;
			float4 DistortionColor2;
			
			float FillAlpha;

			struct FragInput
			{
				float4 position : SV_POSITION;
				float2 texCoord0 : TEXCOORD0;
				float2 texCoord1 : TEXCOORD1;
			};
			
			FragInput vert(appdata_base input)
			{
				FragInput output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texCoord0 = input.texcoord.xy + FractalOffsets.xy;
				output.texCoord1 = input.texcoord.xy + FractalOffsets.zw;
				
				return output;
			}

			half4 frag (FragInput input) : COLOR
			{
				float fl1 = tex2D(FractalLayer1, input.texCoord0).r;
				float fl2 = tex2D(FractalLayer2, input.texCoord1).r;
				
				float distortion1 = step(abs(fl1 + fl2 - DistortionCenter1), DistortionRange1);
				float distortion2 = step(abs(fl1 + fl2 - DistortionCenter2), DistortionRange2);
				
				
				
				return 
					float4(DistortionColor1.rgb * (distortion1 * DistortionColor1.a), 1.0) + 
					float4(DistortionColor2.rgb * (distortion2 * DistortionColor2.a), 1.0) + 
					float4(DistortionColor1.rgb * FillAlpha * (1 - step(DistortionRange1, fl1 + fl2 - DistortionCenter1)), 1.0) +
					float4(DistortionColor2.rgb * FillAlpha * step(DistortionRange1, fl1 + fl2 - DistortionCenter1), 1.0);
			}
			
			ENDCG
		}
	}
}
