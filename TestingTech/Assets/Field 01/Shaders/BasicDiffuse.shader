Shader "Field01/BasicDiffuse"
{
    Properties
    {
		_EmissiveColor ("EmissiveColor", Color) = (1,1,1,1)
		_AmbientColor ("AmbientColor", Color) = (1,1,1,1)
		_MySliderValue ("Slider", Range(0,10)) = 2.5
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 200

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Lambert

		float4 _EmissiveColor;
		float4 _AmbientColor;
		float _MySliderValue;

        struct Input
        {
            float2 uv_MainTex;
        };

        void surf (Input IN, inout SurfaceOutput o)
        {
			float4 c = pow((_EmissiveColor + _AmbientColor), _MySliderValue);
            o.Albedo = c.rgb;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
