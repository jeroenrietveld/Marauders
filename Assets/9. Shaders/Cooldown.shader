Shader "Custom/Cooldown" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		playerColor ("Player Color", Color) = (1, 1, 1, 1)
		phase ("Phase", range(0, 1)) = 0.5
	}
	SubShader {
		
		Pass {
			Tags { "Queue"="Transparent" "RenderType"="Opaque" }
			LOD 200
			Blend SrcAlpha OneMinusSrcAlpha
			
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
				
			#include "UnityCG.cginc"

			sampler2D _MainTex;
			half3 playerColor;
			float phase;

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
				float4 color = tex2D(_MainTex, input.texCoord);
				
				half damageAlpha = 0.25;
				
				return half4(playerColor, (damageAlpha + (color.a < phase) * (1 - damageAlpha)) * color.r);
			}
			
			ENDCG
		}
	} 
	FallBack "Diffuse"
}
