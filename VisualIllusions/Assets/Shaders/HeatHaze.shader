Shader "Custom/HeatHaze" {
    Properties {
        _NoiseTex ("Noise Texture", 2D) = "white" {} // Rauschtextur, z.B. Perlin Noise
        _Distortion ("Distortion", Range(0,0.1)) = 0.05 // Verzerrungsintensität
        _Speed ("Speed", Range(0,10)) = 1 // Animationsgeschwindigkeit
    }
    SubShader {
        Tags { "RenderType"="Opaque" }
        GrabPass { } // Holt den aktuellen Frame als Textur
        
        Pass {
            CGPROGRAM
            #pragma vertex vert_img
            #pragma fragment frag
            #include "UnityCG.cginc"

            sampler2D _GrabTexture; // Enthält den Bildschirm-Content
            sampler2D _NoiseTex;
            float _Distortion;
            float _Speed;
            
            // Der Fragment-Shader: Hier wird die Verzerrung berechnet
            fixed4 frag(v2f_img i) : SV_Target {
                // Erzeuge eine Rauschkoordinate, skaliert für mehr Detail und animiert mit der Zeit
                float2 noiseUV = i.uv * 10.0 + float2(_Time.y * _Speed, _Time.y * _Speed);
                // Sample die Rauschtextur. Der Wert wird von [0,1] in [-1,1] umgerechnet.
                float2 noise = tex2D(_NoiseTex, noiseUV).rg * 2.0 - 1.0;
                // Verschiebe die UV-Koordinaten basierend auf der Verzerrung und dem Noise
                float2 distortedUV = i.uv + noise * _Distortion;
                // Sample den GrabPass mit den verzerrten UV-Koordinaten
                fixed4 col = tex2D(_GrabTexture, distortedUV);
                return col;
            }
            ENDCG
        }
    }
    FallBack "Diffuse"
}
