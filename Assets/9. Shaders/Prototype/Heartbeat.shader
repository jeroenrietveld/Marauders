Shader "Custom/Heartbeat" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="AlphaTest" "RenderType"="Transparent" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert alpha

		sampler2D _MainTex;
		float health;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = float3(1,0,0);
			o.Alpha = step(max(health, 0.001), c.a);
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
