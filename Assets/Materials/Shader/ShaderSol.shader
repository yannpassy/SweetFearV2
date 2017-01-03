Shader "Custom/ShaderSol" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
		_MainTex ("Albedo (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Detail("Detail", 2D) = "gray" {}
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Geometry"}
		LOD 200
		
		Stencil{
			Ref 1
			Comp always
			Pass replace
		}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;
		sampler2D _Detail;

		struct Input {
			float2 uv_MainTex;
			float4 screenPos;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			//o.Albedo = c.rgb;

			o.Albedo = tex2D(_MainTex, IN.uv_MainTex).rgb;
			float2 screenUV = IN.screenPos.xy / IN.screenPos.w;
			screenUV *= float2(8, 6);
			o.Albedo *= tex2D(_Detail, screenUV).rgb * 2;

			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
