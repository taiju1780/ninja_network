Shader "dissolve"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _DissolveTex ("Dissolve", 2D) = "white" {}
		_Influence("Influence",Range(0,1)) = 0
		[ENUM(Off, 0, Front, 1, Back, 2)] _CullMode("Culling Mode", int) = 0
		[ENUM(Off, 0, On, 1)] _ZWrite("ZWrite", int) = 0
		_Edge("Edge",Range(0,1)) = 0
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" "Ignore" = "True" "RenderType" = "Transparent" }

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha //Alpha
			Cull[_CullMode]
			Lighting Off
			ZWrite[_ZWrite]

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_feature EDGE_COLOR

			#include "UnityCG.cginc"

			struct appdata 
			{
				float4 vert : POSITION;
				float2 uv : TEXCOORD0;
				fixed4 color : COLOR;
			};

			struct v2f 
			{
				float2 uv : TEXCOORD0;
				float2 uv3 : TEXCOORD3;
				float4 vertex : SV_POSITION;
				fixed4 color : COLOR;
			};

			sampler2D _MainTex;
			sampler2D _DissolveTex;
			float4 _MainTex_ST;
			float4 _DissolveTex_ST;
			fixed _Edge;
			fixed _Influence;

			#ifdef EDGE_COLOR
				sampler2D _EdgeAroundRamp;
				fixed _EdgeAround;
				float _EdgeAroundPower;
				float _EdgeAroundHDR;
				float _EdgeDistorsion;
			#endif

			v2f vert(appdata v) {
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vert);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv3 = TRANSFORM_TEX(v.uv, _DissolveTex);
				o.color = v.color;
				o.color.a *= _Influence;
				return o;
			}

			fixed4 frag(v2f v) : SV_Target
			{
				fixed4 col = tex2D(_DissolveTex,v.uv3);
				fixed x = col.r;
				fixed influence = v.color.a;

				//Edge
				fixed edge = lerp(x + _Edge, x - _Edge, influence);
				fixed alpha = smoothstep(_Influence + _Edge, _Influence - _Edge, edge);

				#ifdef EDGE_COLOR
					//エッジ周りの影響度
					fixed edgearound = lerp(x + _EdgeAround, x - _EdgeAround, _Influence);
					edgearound = smoothstep(_Influence + _EdgeAround, _Influence - _EdgeAround, edgearound);
					edgearound = pow(edgearound, _EdgeAroundPower);

					//エッジ周りの歪み
					fixed avoid = 0.15f;
					fixed distort = edgearound * alpha * avoid;
					float2 cuv = lerp(i.uv, i.uv + distort - avoid, influence * _EdgeDistorsion);
					col = tex2D(_MainTex, cuv);
					col.rgb *= i.color.rgb;

					//エッジ周りの色
					fixed3 ca = tex2D(_EdgeAroundRamp, fixed2(1 - edgearound, 0)).rgb;
					ca = (col.rgb + ca) * ca * _EdgeAroundHDR;
					col.rgb = lerp(ca, col.rgb, edgearound);
				#else
					col = tex2D(_MainTex, v.uv);
					col.rgb *= v.color.rgb;
				#endif
					col.a *= alpha;

				return col;
			}
			ENDCG
		}
    }
}
