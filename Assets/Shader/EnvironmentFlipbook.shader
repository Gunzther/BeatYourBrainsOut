Shader "TechArt/Unlit/EvironmentFlipbook"
{
	Properties
	{
		_Frame("Frame", Int) = 1
		[NoScaleOffset] _MainTex("Main Tex", 2D) = "white" {}
		_Row("Row", Int) = 1
		_Column("Column", Int) = 1
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
			Fog { Mode Off }
			Blend SrcAlpha OneMinusSrcAlpha

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
					float4 color : COLOR;
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
					fixed4 color : COLOR;
				};

				sampler2D _MainTex;
				float _Row;
				float _Column;
				float _Frame;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);

					float2 tiling;
					tiling.x = 1 / _Column;
					tiling.y = 1 / _Row;

					float2 offset;
					offset.x = tiling.x * (_Frame - 1);
					offset.y = 1 - (tiling.y * ceil(_Frame / _Column));

					o.uv = v.uv * tiling + offset;

					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{

					fixed4 col = tex2D(_MainTex, i.uv);
					float cutoff = 0.5;
					clip(col.a - cutoff);

					return col;
				}
				ENDCG
			}
		}
}