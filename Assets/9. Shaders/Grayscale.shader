Shader "Custom/Grayscale" {
    Properties {
        _MainTex ("Base (RGB)", 2D) = "white" {}
    }
    SubShader {
		Tags { "Queue"="AlphaTest+2" "RenderType"="Opaque" }
		
		Blend SrcAlpha OneMinusSrcAlpha
 
        CGPROGRAM
        #pragma surface surf Lambert
 
        sampler2D _MainTex;
 
        struct Input {
            float2 uv_MainTex;
        };
 
        void surf (Input IN, inout SurfaceOutput o) {
            half4 c = tex2D (_MainTex, IN.uv_MainTex);
            o.Albedo = (c.r + c.g + c.b)/3;
            o.Alpha = c.a;
        }
        ENDCG
    } 
    FallBack "Diffuse"
}