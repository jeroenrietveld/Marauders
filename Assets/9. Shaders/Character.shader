Shader "Custom/Character" {
	Properties {
		_MainTex ("Base (RGB)", 2D) = "white" {}
		_Color ("Color", Color) = (1, 1, 1, 1)
		_PlayerColor ("Player Color", Color) = (1, 1, 1, 1)
		
		_DeathPhase ("Death Phase", Float) = 0
		_Shear ("Death Shear factor", Float) = 20
		_ShearDirection ("Death Shear Direction", Vector) = (0, 0, 1, 0)
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
		float3 _ShearDirection;
		
		struct Input
		{
			float2 uv_MainTex;
			float3 pos;
		};
		
		void vert(inout appdata_full input, out Input output)
		{
			input.vertex.xyz *= 1 + _DeathPhase * 4;
			input.vertex.xyz += _ShearDirection * _DeathPhase * _Shear;
			
			UNITY_INITIALIZE_OUTPUT(Input, output);
			output.pos = input.vertex;
		}
		
		void surf (Input input, inout SurfaceOutput output)
		{
			float3 c = tex2D(_MainTex, input.uv_MainTex).rgb * _Color.rgb;
			output.Albedo = c;
			//output.Alpha = saturate(1 - _DeathPhase) * smoothstep(_Shear * 0.01 * _DeathPhase, _Shear * 0.02 * _DeathPhase, length(input.pos));
			
			float phase = smoothstep(0, 1, _DeathPhase);
			output.Alpha = 1 - phase;
			output.Emission = c * phase;
		}
		
		ENDCG
	}
	
	Fallback "Diffuse"
}
