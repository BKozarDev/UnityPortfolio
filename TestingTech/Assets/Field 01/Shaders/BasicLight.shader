Shader "Field01/BasicLight"
{
	Properties
	{
		_EmissiveColor("EmissiveColor", Color) = (1,1,1,1)
		_AmbientColor("AmbientColor", Color) = (1,1,1,1)
		_MySliderValue("Slider", Range(0,10)) = 2.5
		//_RampTex("Ramp Texture", 2D) = "gray"
	}
		SubShader
	{
		Tags { "RenderType" = "Opaque" }
		LOD 200

		CGPROGRAM
		// Physically based Standard lighting model, and enable shadows on all light types
		#pragma surface surf BasicDiffuse

		inline float4 LightingBasicDiffuse(SurfaceOutput s, fixed3 lightDir, fixed atten)
		{
			float diflight = max(0, dot(s.Normal, lightDir));
			float4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * (diflight * atten * 2);
			col.a = s.Alpha;
			return col;

			/*float difLight = dot(s.Normal, lightDir);
			float rimLight = dot(s.Normal, viewDir);
			float hLambert = difLight * 0.5 + 0.5;
			float3 ramp = tex2D(_RampTex, float2(hLambert, rimLight)).rgb;

			float4 col;
			col.rgb = s.Albedo * _LightColor0.rgb * (ramp);
			col.a = s.Alpha;
			return col;*/
		}

		float4 _EmissiveColor;
		float4 _AmbientColor;
		float _MySliderValue;
		//sampler2D _RampTex;

		struct Input
		{
			float2 uv_MainTex;
		};

		void surf(Input IN, inout SurfaceOutput o)
		{
			float4 c = pow((_EmissiveColor + _AmbientColor), _MySliderValue);
			o.Albedo = c.rgb;
			o.Alpha = c.a;
		}
		ENDCG
	}
		FallBack "Diffuse"
}
