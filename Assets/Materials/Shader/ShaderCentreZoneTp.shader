﻿Shader "Custom/ShaderCentreZoneTp" {
		Properties{
		_Color("Color", Color) = (1,1,1,1)
	}
	
	SubShader {
		Tags { "RenderType"="Opaque"  "Queue" = "Geometry+1" }
		LOD 200
			ColorMask RGB
			Cull Back
			ZWrite Off
			ZTest Less
		Blend SrcAlpha OneMinusSrcAlpha

			Stencil{
			Ref 1
			Comp equal
			Pass IncrSat
		}

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
			// Albedo comes from a texture tinted by color
			fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
			o.Albedo = c.rgb;
			// Metallic and smoothness come from slider variables
			o.Metallic = _Metallic;
			o.Smoothness = _Glossiness;
			o.Alpha = c.a;
		}
		ENDCG
	}
	FallBack "Diffuse"
}
