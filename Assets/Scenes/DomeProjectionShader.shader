Shader "Custom/DomeProjection"
{
    Properties
    {
        _MainTex ("Main Texture", 2D) = "white" {}
        _UVMap ("UV Distortion Map", 2D) = "white" {}
        _DistortionScale ("Distortion Scale", Float) = 1 // Add a property to control distortion
    }
    SubShader
    {
        Tags { "RenderType"="Opaque" }
        Pass
        {
            CGPROGRAM
            #pragma vertex vert
            #pragma fragment frag
            #include "UnityCG.cginc"

            struct appdata_t {
                float4 vertex : POSITION;
                float2 uv : TEXCOORD0;
            };

            struct v2f {
                float2 uv : TEXCOORD0;
                float4 vertex : SV_POSITION;
            };

            sampler2D _MainTex;
            sampler2D _UVMap;
            float _DistortionScale; // Distortion scale variable

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv; // Pass through original UV coordinates
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Sample the distortion map (_UVMap) to get the distortion for the UV coordinates
                float2 distortion = tex2D(_UVMap, i.uv).xy * _DistortionScale; // Scale the distortion

                // Apply the distortion to the original UV coordinates
                float2 distortedUV = i.uv + distortion;

                // Uncomment to debug UVs:
                // return fixed4(distortedUV.x, distortedUV.y, 0.0, 1.0); // Debugging UVs

                // Use the distorted UVs to sample the main texture (_MainTex)
                fixed4 col = tex2D(_MainTex, distortedUV);

                // Return the final color with the applied distortion
                return col;
            }
            ENDCG
        }
    }
}