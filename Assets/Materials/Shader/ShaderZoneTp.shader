Shader "Custom/ShaderZoneTp" {
	Properties {
		_Color ("Color", Color) = (1,1,1,1)
	}
	SubShader {
		Tags { "RenderType"="Opaque" "Queue" = "Geometry+1" }

		LOD 200
		ColorMask RGB
		Cull Front 
		ZWrite Off
		ZTest Greater  
		Stencil{
			Ref 1
			Comp equal
		} 

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		sampler2D _MainTex;

		struct Input {
			float2 uv_MainTex;
			float4 color : COLOR;
			float4 screenPos;
			float eyeDepth;
		};

		half _Glossiness;
		half _Metallic;
		fixed4 _Color;

		void surf (Input IN, inout SurfaceOutputStandard o) {
		// il faut récupéré les depth 	
		o.Albedo = _Color.rgb;
		o.Normal = half3(0, 0, -1);
		o.Alpha = 1;
				
		}
		ENDCG
	}
	FallBack "Diffuse"
}
