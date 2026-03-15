Shader "Custom/ExactColorSwap"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _OriginalColor ("Original Color", Color) = (1,1,1,1)
        _TargetColor ("Target Color", Color) = (1,1,1,1)
        _Tolerance ("Tolerance", Range(0,0.1)) = 0.02
    }

    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent" }
        Blend SrcAlpha OneMinusSrcAlpha
        ZWrite Off
        Cull Off

        Pass
        {
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
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            float4 _MainTex_ST;

            float4 _OriginalColor;
            float4 _TargetColor;
            float _Tolerance;

            v2f vert(appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                return o;
            }

            half4 frag(v2f i) : SV_Target
            {
                half4 col = tex2D(_MainTex, i.uv);

                // keep transparency
                if (col.a <= 0)
                    return col;

                // compare RGB only with tolerance
                if (
                    abs(col.r - _OriginalColor.r) < _Tolerance &&
                    abs(col.g - _OriginalColor.g) < _Tolerance &&
                    abs(col.b - _OriginalColor.b) < _Tolerance
                )
                {
                    return half4(_TargetColor.rgb, col.a);
                }

                return col;
            }

            ENDCG
        }
    }
}