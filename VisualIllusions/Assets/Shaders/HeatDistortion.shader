Shader "Hidden/Custom/HeatDistortion" {
    Properties {
        _Amplitude ("Amplitude", Float) = 0
        _Frequency ("Frequency", Float) = 0
        _Speed ("Speed", Float) = 0
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        Pass {
            ZTest Always Cull Off ZWrite Off
            HLSLPROGRAM
           
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"
            
            sampler2D _MainTex;
            float _Amplitude;
            float _Frequency;
            float _Speed;
            
            struct v2f {
                float4 pos : SV_POSITION;
                float2 uv  : TEXCOORD0;
            };
            
            v2f vert(uint vertexID : SV_VertexID) {
                v2f o;
                // Ein Fullscreen-Dreieck erzeugen
                float2 pos[3];
                pos[0] = float2(-1.0, -1.0);
                pos[1] = float2( 3.0, -1.0);
                pos[2] = float2(-1.0,  3.0);
                o.pos = float4(pos[vertexID], 0.0, 1.0);
                // UV-Koordinaten aus dem Clip-Space berechnen
                o.uv = (o.pos.xy + 1.0) * 0.5;
                return o;
            }
            
            float4 frag(v2f i) : SV_Target {
                // Offset rein mathematisch berechnen
                float2 offset;
                offset.x = sin(i.uv.y * _Frequency + _Time.y * _Speed) * _Amplitude;
                offset.y = cos(i.uv.x * _Frequency + _Time.y * _Speed) * _Amplitude;
                float2 uvDistorted = i.uv + offset;

                // Y-Koordinate bedingt invertieren:
                float2 uvFixed = uvDistorted;
                #if defined(UNITY_UV_STARTS_AT_TOP)
                    uvFixed.y = 1.0 - uvFixed.y;
                #endif

                return tex2D(_MainTex, uvFixed);
            }
            ENDHLSL
        }
    }
}
