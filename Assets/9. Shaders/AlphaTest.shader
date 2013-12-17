Shader "Custom/Alpha" {
	Properties {
		_MainTex ("Base (RGB) Alpha (A)", 2D) = "white" {}
		playerColor ("Player Color", Color) = (1, 1, 1, 1)
		phase ("Phase", range(0, 1)) = 0.5
	}
	SubShader {
		Tags { "Queue"="Transparent" "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		half3 playerColor;
		float phase;

		struct Input {
			float2 uv_MainTex;
		};

		void surf (Input IN, inout SurfaceOutput o) {
			half4 c = tex2D (_MainTex, IN.uv_MainTex);
			o.Emission = c.rgb * playerColor;
			if(step(max(phase, 0.0001), c.a) == 0) discard;
		}
		ENDCG
	} 
	FallBack "Diffuse"
}
