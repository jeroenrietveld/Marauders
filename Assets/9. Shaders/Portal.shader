Shader "Custom/Portal" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		
		Percentage ("Percentage", Float) = 0.0
		InverseScale ("Inverse scale", Vector) = (0, 0, 0, 0)
		FractalLayer0 ("Fractal layer 0", 2D) = "white" {}
		FractalOffset ("Fractal offset", Vector) = (0, 0, 0, 0)
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Opaque" }
		
		Cull Off
		Blend SrcAlpha OneMinusSrcAlpha
		
		Pass {
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			
			float Percentage;
			float4 InverseScale;
			sampler2D FractalLayer0;
			float4 FractalOffset;
			
			struct FragInput
			{
				float4 position : SV_POSITION;
				float2 texCoord : TEXCOORD0;
				float distanceToCenter : TEXCOORD1;
			};
			
			FragInput vert(appdata_base input)
			{
				FragInput output;
				
				output.position = mul(UNITY_MATRIX_MVP, input.vertex);
				output.texCoord = input.texcoord.xy;
				output.distanceToCenter = length(input.vertex * InverseScale);
				
				return output;
			}
			
			half4 frag (FragInput input) : COLOR
			{
				float fractSample = tex2D(FractalLayer0, input.texCoord + FractalOffset.xy);
			
				if(input.distanceToCenter + (fractSample) > Percentage) discard;
			
				float4 color = tex2D(_MainTex, input.texCoord);
				return color;
			}
			ENDCG
		}
	} 
}
