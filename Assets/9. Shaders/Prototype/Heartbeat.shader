Shader "Custom/Heartbeat" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert 

		sampler2D _MainTex;
		float health;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Albedo = float3(1,0,0);
			o.Alpha = step(max(health, 0.001), c.a);
			if (o.Alpha == 0) { discard; }
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
