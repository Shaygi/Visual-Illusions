Shader "Unlit/PortalShader"
{
    Properties
    {
        // Definiert die Haupttextur (_MainTex) mit dem Standardwert "weiß"
        _MainTex ("Texture", 2D) = "white" {}
    }
    SubShader
    {
        // Setzt Render-Tags: Der Shader wird als transparent behandelt und Projektoren ignoriert
        Tags{ "Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent" }
        Lighting Off      // Beleuchtung wird ausgeschaltet, da es sich um einen Unlit-Shader handelt
        Cull Back         // Backface Culling: Rückseiten werden nicht gerendert
        ZWrite On         // Aktiviert das Schreiben in den Tiefenpuffer (Z-Buffer)
        ZTest Less        // Z-Test: Pixel werden nur gezeichnet, wenn sie näher an der Kamera sind

        Fog{ Mode Off }   // Deaktiviert den Nebel

        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            
            #include "UnityCG.cginc"

            // Struktur für die Vertex-Daten, die vom Mesh geliefert werden
            struct appdata
            {
                float4 vertex : POSITION;  // Position des Scheitelpunkts
                float2 uv : TEXCOORD0;     // Texturkoordinaten
            };

            // Struktur für die Daten, die vom Vertex-Shader an den Fragment-Shader übergeben werden
            struct v2f
            {
                float4 vertex : SV_POSITION;  // Position im Clip-Raum
                float4 screenPos : TEXCOORD1; // Position im Bildschirmraum
            };

            // Vertex-Shader: Transformiert die Scheitelpunktdaten in Clip- und Bildschirmkoordinaten
            v2f vert (appdata v)
            {
                v2f o;
                // Berechne die Position im Clip-Raum
                o.vertex = UnityObjectToClipPos(v.vertex);
                // Berechne die Bildschirmposition des Scheitelpunkts
                o.screenPos = ComputeScreenPos(o.vertex);
                return o;
            }
            
            // Sampler für die Haupttextur
            sampler2D _MainTex;

            // Fragment-Shader: Bestimmt die endgültige Farbe jedes Pixels
            fixed4 frag (v2f i) : SV_Target
            {
                // Führe die perspektivische Division durch, um die echten Bildschirmkoordinaten zu erhalten
                i.screenPos /= i.screenPos.w;
                // Lese die Farbe der Textur anhand der berechneten Bildschirmkoordinaten (x und y)
                fixed4 col = tex2D(_MainTex, float2(i.screenPos.x, i.screenPos.y));
                
                return col;
            }
            ENDCG
        }
    }
}
