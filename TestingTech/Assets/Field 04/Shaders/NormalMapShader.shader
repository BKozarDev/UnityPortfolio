﻿Shader "Field04/NormalMapShader"
{
    Properties
    {
        _Color ("Color", Color) = (1,1,1,1)
		_NormalTex("Normal Map", 2D) = "bump" {}
		_NormalIntensity ("Normal Map Intemsity", Range(0,2)) = 1
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf Standard fullforwardshadows

		// Use shader model 3.0 target, to get nicer looking lighting
		#pragma target 3.0

		float4 _Color;
		sampler2D _NormalTex;
		float _NormalIntensity;

        struct Input
        {
			float2 uv_NormalTex;
        };


        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {

			o.Albedo = _Color.rgb;
			o.Alpha = 1.0;

			float3 normalMap = UnpackNormal(tex2D(_NormalTex, IN.uv_NormalTex));
			normalMap = float3(normalMap.x * _NormalIntensity, normalMap.y * _NormalIntensity, normalMap.z);
			o.Normal = normalMap.rgb;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
