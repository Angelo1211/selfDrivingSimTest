// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'
Shader "Custom/VertexColor" {
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
				return o.col;
			}

				ENDCG
			}
	}

}