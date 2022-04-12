Shader "Unlit/HLSL"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Color1 ("Color 1", Color) = (1,1,1,1)
        _Thickness ("Thickness", Range(0.0,0.25)) = 0.1
        _Start ("Start", Range(0.0,1.0)) = 0.0
        _End ("End", Range(0.0,1.0)) = 1.0
        _Rotate ("Rotate", Float) = 0
    }
    SubShader
    {
        Tags { "RenderType"="Transparent" "Queue"="Transparent"}
        Blend SrcAlpha OneMinusSrcAlpha
        Cull Off
        LOD 100

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
            float4 _MainTex_ST, _Color1;
            float _Thickness, _Start, _End, _Rotate;

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = TRANSFORM_TEX(v.uv, _MainTex);
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float invlerp(float a, float b, float t)
            {
                return (t-a)/(b-a);
            }
            
            fixed4 frag (v2f i) : SV_Target
            {
                fixed4 col = 1;
                float2 nuv = i.uv - .5;

                //Fill
                float fillgradient = atan2(nuv.x, nuv.y);
                fillgradient = invlerp(-3.1415, 3.1415, fillgradient);
                float stepStart = step(_Start, fillgradient);
                float stepEnd = step(1.-_End, 1.-fillgradient);

                //Circle
                float circle = smoothstep(_Thickness+0.01, _Thickness, abs(length(nuv) - 0.425+_Thickness));

                //Result
                float4 fill1 = fixed4(_Color1.rgb, stepStart*stepEnd*circle * _Color1.a);
                
                col = fill1;
                
                return col;
            }
            ENDCG
        }
    }
}
