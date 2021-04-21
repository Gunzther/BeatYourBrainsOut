Shader "TechArt/Unlit/Character"
{
	Properties
	{
		_Frame("Frame", Int) = 1
		[NoScaleOffset] _MainTex("Main Tex", 2D) = "white" {}
		[NoScaleOffset] _SecondTex("Second Tex", 2D) = "white" {}
		_Row("Row", Int) = 1
		_Column("Column", Int) = 1
		_PlayerCol("Player Color", Color) = (1, 0, 0, 0)
		_DissolveCol("Dissolve Color", Color) = (1, 0, 0, 0)
		_LapValue("Overlap Color Value", Range(0,1)) = 0
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
				sampler2D _SecondTex;
				float _Row;
				float _Column;
				float _Frame;
				fixed4 _DissolveCol;
				float _LapValue;
				fixed4 _PlayerCol;

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

					//o.color = v.color * red;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 red = fixed4(1.0, 0.2, 0.2, 0);

					fixed4 col = tex2D(_MainTex, i.uv);
					float cutoff = 0.5;
					clip(col.a - cutoff);

					fixed4 playerCol = tex2D(_SecondTex, i.uv);
					col.rgb = lerp(col, _PlayerCol, playerCol.r);

					col.a = lerp(0.0, col.a, _DissolveCol.a);
					return fixed4(lerp(col.rgb, red, _LapValue), col.a);
				}
				ENDCG
			}
		}
}