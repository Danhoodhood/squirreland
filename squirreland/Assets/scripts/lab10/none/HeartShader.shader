Shader "Custom/HeartShader"
{
    Properties
    {
        _StartColor ("Start Color", Color) = (1,0,0,1)
        _EndColor ("End Color", Color) = (1,1,0,1)
        _TimeValue ("Time", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        LOD 100

        Pass
        {
            Blend SrcAlpha OneMinusSrcAlpha
            Cull Off
            ZWrite Off

            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
            };

            float4 _StartColor;
            float4 _EndColor;
            float _TimeValue;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Плавная смена цвета через синус
                float t = (sin(_TimeValue * 3.0) + 1) / 2.0;
                return lerp(_StartColor, _EndColor, t);
            }
            ENDCG
        }
    }
}
