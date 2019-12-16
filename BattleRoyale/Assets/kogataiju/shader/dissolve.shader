Shader "Custom/dissolve"
{
    Properties
    {
        _MainTex ("MainTex", 2D) = "white" {}
        _DissolveTex ("Dissolve", 2D) = "white" {}
		_Influence("Influence",Range(0,1)) = 0;
		[ENUM(Off, 0, Front, 1, Back, 2)] _CullMode("Culling Mode", int) = 0;
		[ENUM(Off, 0, On, 1)] _ZWrite("ZWrite", int) = 0;
	}
		SubShader
	{
		Tags { "Queue" = "Transparent" "Ignore" = "True" "RenderType" = "Transparent" }

		Pass{
			Blend SrcAlpha OneMinusSrcAlpha //Alpha
			Cull(_CullMode);
			Lighting Off;
			ZWrite[_ZWrite];

			CGPROGRAM

			#pragma vertex vert
			#pragma fragment frag
			#pragma shader_featuer EDGE_COLOR

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
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				o.uv3 = TRANSFORM_TEX(v.uv3, _DissolveTex);
				o.color = v.color;
				o.color.a *= _Influence;
				return o;
			}

			fixed4 frag(v2f v) : SV_Target
			{
				fixed4 color Tex2D(_DissolveTex,v.uv3);
				fixed x = cor.r;
				fixed influence = v.cor.a;

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
					col = Tex2D(_MainTex, i.uv);
					col.rgb *= i.color.rgb;
			}
		}

        CGPROGRAM
        // Physically based Standard lighting model, and enable shadows on all light types
        #pragma surface surf Standard fullforwardshadows

        // Use shader model 3.0 target, to get nicer looking lighting
        #pragma target 3.0

        sampler2D _MainTex;

        struct Input
        {
            float2 uv_MainTex;
        };

        half _Glossiness;
        half _Metallic;
        fixed4 _Color;

        // Add instancing support for this shader. You need to check 'Enable Instancing' on materials that use the shader.
        // See https://docs.unity3d.com/Manual/GPUInstancing.html for more information about instancing.
        // #pragma instancing_options assumeuniformscaling
        UNITY_INSTANCING_BUFFER_START(Props)
            // put more per-instance properties here
        UNITY_INSTANCING_BUFFER_END(Props)

        void surf (Input IN, inout SurfaceOutputStandard o)
        {
            // Albedo comes from a texture tinted by color
            fixed4 c = tex2D (_MainTex, IN.uv_MainTex) * _Color;
            o.Albedo = c.rgb;
            // Metallic and smoothness come from slider variables
            o.Metallic = _Metallic;
            o.Smoothness = _Glossiness;
            o.Alpha = c.a;
        }
        ENDCG
    }
    FallBack "Diffuse"
}
