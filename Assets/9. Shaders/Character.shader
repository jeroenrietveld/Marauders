Shader "Custom/Character" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_PlayerColor ("Player Color", Color) = (1, 1, 1, 1)
	}
	SubShader {
		Pass
		{
			Tags { "Queue"="AlphaTest+1" "RenderType"="Opaque" }
			
			ZWrite Off
			ZTest Greater
			
			Lighting Off
			Color [_PlayerColor]
			
		}
	
		Tags { "Queue"="AlphaTest+2" "RenderType"="Opaque" }
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float4 _Color;
		
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void vert(inout appdata_full input, out Input output)
		{
			UNITY_INITIALIZE_OUTPUT(Input, output);
		}
		
		void surf (Input input, inout SurfaceOutput output)
		{
			output.Albedo = tex2D(_MainTex, input.uv_MainTex).rgb * _Color.rgb;
		}
		
		ENDCG
	}
	
	Fallback "Diffuse"
}
