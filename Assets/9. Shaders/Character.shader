Shader "Custom/Character" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_PlayerColor ("Player Color", Color) = (1, 1, 1, 1)
		
		_DeathPhase ("Death Phase", Float) = 0
		_Shear ("Death Shear factor", Float) = 50
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
	
		Tags { "Queue"="Transparent" "RenderType"="Opaque" }
		
		Blend SrcAlpha OneMinusSrcAlpha
		ZWrite Off
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float4 _Color;
		
		float _DeathPhase;
		float _Shear;
		
		struct Input
		{
			float2 uv_MainTex;
		};
		
		void vert(inout appdata_full input)
		{
			input.vertex.x += input.vertex.z * _DeathPhase * _Shear;
			input.vertex.z += input.vertex.z * _DeathPhase * _Shear;
		}
		
		void surf (Input input, inout SurfaceOutput output)
		{
			float3 c = tex2D(_MainTex, input.uv_MainTex).rgb * _Color.rgb;
			output.Albedo = pow(c, 1 - _DeathPhase);
			output.Alpha = saturate(1 - _DeathPhase);
		}
		
		ENDCG
	}
	
	Fallback "Diffuse"
}
