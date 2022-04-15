Shader "Unlit/Shading Texture"
{
	Properties
	{
		_MainTex("Texture", 2D) = "white" {}
		_Color("Color", color) = (1,1,1,1)
		_AlphaMultiplier("Alpha Multiplier", Range(0,1)) = 1
	}
		SubShader
		{
			Tags {"Queue" = "Transparent" "IgnoreProjector" = "True" "RenderType" = "Transparent"}
			LOD 100

			Zwrite Off
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
				};

				struct v2f
				{
					float2 uv : TEXCOORD0;
					float4 vertex : SV_POSITION;
				};

				float4 _Color;
				float _AlphaMultiplier;

				v2f vert(appdata v)
				{
					v2f o;
					o.vertex = UnityObjectToClipPos(v.vertex);
					o.uv = v.uv;
					return o;
				}

				fixed4 frag(v2f i) : SV_Target
				{
					fixed4 col = _Color;
					col.r = i.vertex.x / 1920;
					col.g = i.vertex.y / 1080;
					float x = (i.uv.x - 0.5) * 2;
					float y = (i.uv.y - 0.5) * 2;
					float alpha = pow(1 - sqrt(x * x + y * y),0.5);
					col.a *= step(0,alpha) * alpha * _AlphaMultiplier;
					return col;
				}
				ENDCG
			}
		}
}
