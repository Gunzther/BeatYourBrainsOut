Shader "Unlit/UIOutline"
{
    Properties
    {
        [PerRendererData] _MainTex ("Sprite Texture", 2D) = "white" {}
        _SpriteColor("Sprite color", Color) = (0, 0, 0, 1)
        _OutlineColor("Outline color", Color) = (0, 0, 0, 1)
        _OutlineWidth("Outline width", Range(1.0, 10.0)) = 1

    }

    SubShader
    {
        Tags {
            "Queue" = "Transparent"
            "RenderType" = "Transparent"
        }

        LOD 100
        Cull Off
        Lighting Off
        ZWrite off
        Fog { Mode Off }
        Blend SrcAlpha OneMinusSrcAlpha

        Pass{

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
            float3 outline : NORMAL;
            fixed4 color : COLOR;
        };

        sampler2D _MainTex;
        float4 _MainTex_ST;
        fixed4 _OutlineColor;
        float _OutlineWidth;
        fixed4 _SpriteColor;
        uniform float4 _MainTex_TexelSize;

        fixed4 CalShrink(float2 uv)
        {

            float2 offsets = float2(_OutlineWidth * 2, _OutlineWidth * 2);

            float2 bigsize = float2(_MainTex_TexelSize.z, _MainTex_TexelSize.w);
            float2 smallsize = float2(_MainTex_TexelSize.z - offsets.x, _MainTex_TexelSize.w - offsets.y);

            float2 uv_changed = float2
                (
                    uv.x * bigsize.x / smallsize.x - 0.5 * offsets.x / smallsize.x,
                    uv.y * bigsize.y / smallsize.y - 0.5 * offsets.y / smallsize.y
                    );

            if (uv_changed.x < 0 || uv_changed.x > 1 || uv_changed.y < 0 || uv_changed.y > 1)
            {
                return float4(0, 0, 0, 0);
            }
            return tex2D(_MainTex, uv_changed);
        }

        v2f vert(appdata v)
        {
            v2f o;
            o.vertex = UnityObjectToClipPos(v.vertex);
            o.uv = TRANSFORM_TEX(v.uv, _MainTex);
            o.outline = mul(unity_ObjectToWorld, v.vertex) * _OutlineWidth;
            o.color = _OutlineColor;
            return o;
        }

        fixed4 frag(v2f i) : SV_Target
        {
            fixed4 col = CalShrink(i.uv) * _SpriteColor;
            fixed4 outline = _OutlineColor;
                
            if (_OutlineWidth == 1) return col;
                
            if (col.a == 0)
            {
                return outline;
            }

            return col;
        }
            ENDCG
    }
    }
 
}
