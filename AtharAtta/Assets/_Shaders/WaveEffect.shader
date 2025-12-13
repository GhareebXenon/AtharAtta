Shader "UI/WavyBottomUnlit"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color ("Tint", Color) = (1,1,1,1)

        _Amplitude ("Wave Amplitude", Range(0, 0.1)) = 0.03
        _Frequency ("Wave Frequency", Range(1, 20)) = 10
        _Speed ("Wave Speed", Range(0, 5)) = 1
        _MaskHeight ("Flat Area Height", Range(0, 1)) = 0.25
    }

    SubShader
    {
        Tags 
        { 
            "Queue"="Transparent"
            "IgnoreProjector"="True"
            "RenderType"="Transparent"
            "PreviewType"="Plane"
            "CanUseSpriteAtlas"="True"
        }

        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        Lighting Off
        ZWrite Off

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _MainTex;
            float4 _MainTex_ST;
            float4 _Color;

            float _Amplitude;
            float _Frequency;
            float _Speed;
            float _MaskHeight;

            struct appdata_t
            {
                float4 vertex : POSITION;
                float2 texcoord : TEXCOORD0;
                float4 color : COLOR;
            };

            struct v2f
            {
                float4 vertex : SV_POSITION;
                float2 uv : TEXCOORD0;
                float4 color : COLOR;
            };

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.texcoord, _MainTex);
                o.color = v.color * _Color;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Time-based horizontal movement (Right -> Left)
                float waveX = (i.uv.x - _Time.y * _Speed) * _Frequency;

                // Sine wave
                float wave = sin(waveX) * _Amplitude;

                // Mask so wave affects bottom only
                float mask = smoothstep(0.0, _MaskHeight, i.uv.y);

                // Apply wave to UV.y
                float2 uv = i.uv;
                uv.y += wave * (1.0 - mask);

                fixed4 col = tex2D(_MainTex, uv) * i.color;
                return col;
            }
            ENDCG
        }
    }
}
