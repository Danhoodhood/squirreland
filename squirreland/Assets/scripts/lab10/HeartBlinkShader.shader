Shader "Custom/HeartBlinkShader"
{
    Properties
    {
        _MainTex ("Sprite Texture", 2D) = "white" {}
        _StartColor ("Start Color", Color) = (1,0,0,1) // синий
        _EndColor ("End Color", Color) = (1,1,0,1)     // белый
        _TimeValue ("Time", Float) = 0
        _BlinkSpeed ("Blink Speed", Float) = 3.0
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _StartColor;
            float4 _EndColor;
            float _TimeValue;
            float _BlinkSpeed;

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Берём цвет из текстуры
                fixed4 texColor = tex2D(_MainTex, i.uv);

                // Вычисляем фактор мигания от времени
                float t = (sin(_TimeValue * _BlinkSpeed) + 1.0) / 2.0;

                // Линейная интерполяция между StartColor и EndColor
                fixed4 blinkColor = lerp(_StartColor, _EndColor, t);

                // Умножаем цвет мигания на текстуру, сохраняем альфу
                fixed4 finalColor = blinkColor * texColor;

                return finalColor;
            }
            ENDCG
        }
    }
}
