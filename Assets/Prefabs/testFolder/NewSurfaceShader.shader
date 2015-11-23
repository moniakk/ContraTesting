﻿Shader "Custom/NewSurfaceShader" {
	Properties{
		_Color("Color", Color) = (1,1,1,1)
		_MainTex("Albedo (RGB)", 2D) = "white" {}
	_MainTex1("Albedo1 (RGB)", 2D) = "white" {}
		_Glossiness("Smoothness", Range(0,1)) = 0.5
		_Metallic("Metallic", Range(0,1)) = 0.0
		_Speed("Speed", Range(0,10)) = 0.0
	}
		SubShader{
			Tags { "RenderType" = "Opaque" }
			LOD 200

			CGPROGRAM
			// Physically based Standard lighting model, and enable shadows on all light types
			#pragma surface surf Standard fullforwardshadows

			// Use shader model 3.0 target, to get nicer looking lighting
			#pragma target 3.0

			sampler2D _MainTex;
		sampler2D _MainTex1;

			struct Input {
				float2 uv_MainTex;
				float2 uv_MainTex1;
			};

			half _Glossiness;
			half _Metallic;
			fixed4 _Color;
			half _Speed;

			void surf(Input IN, inout SurfaceOutputStandard o) {
				// Albedo comes from a texture tinted by color
				float2 cords = IN.uv_MainTex + IN.uv_MainTex1;
				cords.x += _Time.x * _Speed;
				fixed4 c = tex2D(_MainTex , cords) * _Color;
				fixed4 c1 = tex2D(_MainTex1, cords) * _Color;
				o.Albedo = c.rgb + c1.rgb;
				// Metallic and smoothness come from slider variables
				o.Metallic = _Metallic;
				o.Smoothness = _Glossiness;
				//o.Alpha = c.a;
			}
			ENDCG
		}
			FallBack "Diffuse"
}
