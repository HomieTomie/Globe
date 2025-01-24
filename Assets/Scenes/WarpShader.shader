Shader "Custom/WarpShader"
{
    Properties
    {
        _MainTex ("Texture", 2D) = "white" {}
        _Distortion ("Distortion Amount", Float) = 0.2
        _ImageScale ("Image Scale", Float) = 1.0
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
            
            struct appdata_t
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
            float _Distortion;
            float _ImageScale;

            v2f vert (appdata_t v)
            {
                v2f o;
                o.vertex = UnityObjectToClipPos(v.vertex);
                o.uv = v.uv;
                return o;
            }

            fixed4 frag (v2f i) : SV_Target
            {
                // Flip the X coordinate to correct mirroring issues
                i.uv.x = 1.0 - i.uv.x;

                // Scale the UVs to allow expansion of the warping effect
                float2 scaledUV = (i.uv - 0.5) * _ImageScale + 0.5;

                // Convert UV to normalized screen space (-1 to 1)
                float2 centeredUV = (scaledUV - 0.5) * 2.0;

                // Apply a barrel distortion effect
                float r2 = dot(centeredUV, centeredUV);
                float factor = 1.0 + _Distortion * r2;
                float2 warpedUV = 0.5 + centeredUV * factor * 0.5;

                // Clamp UVs to prevent artifacts
                warpedUV = clamp(warpedUV, 0.0, 1.0);

                // Sample the texture using warped UVs
                fixed4 col = tex2D(_MainTex, warpedUV);
                return col;
            }
            ENDCG
        }
    }
}
