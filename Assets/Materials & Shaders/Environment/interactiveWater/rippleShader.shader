Shader "Unlit/rippleShader"
{
    Properties
    {
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        LOD 100

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            // make fog work
            #pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };

            sampler2D _PrevRT;
            sampler2D _CurrentRT;
            float4 _CurrentRT_TexelSize; 

            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float3 e = float3(_CurrentRT_TexelSize.xy, 0);
                float2 uv = i.uv;

                float p10 = tex2D(_CurrentRT, uv - e.zy).x; // down
                float p01 = tex2D(_CurrentRT, uv + e.zy).x; // up
                float p21 = tex2D(_CurrentRT, uv + e.xz).x; // right
                float p12 = tex2D(_CurrentRT, uv - e.xz).x; // left

                float p11 = tex2D(_PrevRT, uv).x;
                float d = (p10 + p01 + p12 + p21) / 2 - p11;
                d *= 0.99f;
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                return d;
            }
            ENDCG
        }
    }
}
