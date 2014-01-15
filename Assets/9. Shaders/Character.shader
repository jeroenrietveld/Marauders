Shader "Custom/Character" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_PlayerColor ("Player Color", Color) = (1, 1, 1, 1)
		
		_DeathPhase ("Death Phase", Float) = 0
		_Shear ("Death Shear factor", Float) = 300
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
		
		Blend SrcAlpha OneMinusSrcAlpha
		
		CGPROGRAM
		#pragma surface surf Lambert vertex:vert

		sampler2D _MainTex;
		float4 _Color;
		
		float _DeathPhase;
		float _Shear;
		
		struct Input
		{
			float2 uv_MainTex;
			float3 pos;
		};
		
		void vert(inout appdata_full input, out Input output)
		{
			input.vertex.x += input.vertex.z * _DeathPhase * _Shear;
			input.vertex.z += input.vertex.z * _DeathPhase * _Shear;
			
			UNITY_INITIALIZE_OUTPUT(Input, output);
			output.pos = input.vertex;
		}
		
		void surf (Input input, inout SurfaceOutput output)
		{
			float3 c = tex2D(_MainTex, input.uv_MainTex).rgb * _Color.rgb;
			output.Albedo = c;
			output.Alpha = saturate(1 - _DeathPhase) * smoothstep(_Shear * 0.01 * _DeathPhase, _Shear * 0.02 * _DeathPhase, length(input.pos));
		}
		
		ENDCG
	}
	
	Fallback "Diffuse"
}
