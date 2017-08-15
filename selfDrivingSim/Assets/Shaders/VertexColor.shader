// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
Shader "Custom/VertexColor" {
	Properties{
		_MinColor("Color in Minimal", Color) = (0, 1, 0, 1)
		_MaxColor("Color in Maxmal", Color) = (1, 0, 0, 1)
		_MaxDistance("Max Distance", Float) = 1100
	}
	SubShader{
		Pass
		{		
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag

			#include "UnityCG.cginc"

			struct appdata {
				float4 vertex : POSITION;
				float4 color: COLOR;				
			};

			float _MaxDistance;
			float4 _MinColor;
			float4 _MaxColor;

			struct v2f {
				float4 vertex : SV_POSITION;
				float4 col : COLOR;
			};

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.col = v.color;

				return o;
			}

			float4 frag(v2f o) : COLOR{
				float mag = abs(length(o.vertex)) / (1200);
				float4 color = lerp(_MinColor, _MaxColor, mag);
				//float4 color = float4(o.vertex.x/mag, o.vertex.y/mag, 0, 1);
				return color;
			}

				ENDCG
			}
	}

}