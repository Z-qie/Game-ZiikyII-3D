Shader "Unlit/drawShader"
{
    Properties
    {
       /* _MainTex ("Texture", 2D) = "white" {}*/
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
            //#pragma multi_compile_fog

            #include "UnityCG.cginc"

            struct appdata
            {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            
            sampler2D _SourceTex;
            float4  _Pos;

            struct v2f
            {
                float2 uv : TEXCOORD0;
                UNITY_FOG_COORDS(1)
                float4 vertex : SV_POSITION;
            };
 
            v2f vert (appdata v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                //UNITY_TRANSFER_FOG(o,o.vertex);
                return o;
            }

            float4 frag(v2f i) : SV_Target
            {
                float2 uv = i.uv;
                //col = length(uv - _Pos.xy);
                // apply fog
                //UNITY_APPLY_FOG(i.fogCoord, col);
                //return length(uv - _Pos.xy);
                float v = max(_Pos.z - length(uv - _Pos.xy) / _Pos.z, 0) + tex2D(_SourceTex, uv).x;
                return v;
            }
            ENDCG
        }
    }
}
