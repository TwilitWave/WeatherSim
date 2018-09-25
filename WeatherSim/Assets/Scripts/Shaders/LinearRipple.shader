Shader "Custom/LinearRipple"
{
	Properties
	{		
		_MainTex ("Texture", 2D) = "white" {}
		_DistMap("Distortion Map", 2D) = "white" {}
		_Strength("Strength", Range(0.0, 1.0)) = 0.1
		_Speed("Speed", float) = 2.0
		_Offset("Offset", range(0, 1)) = 0
	}
	SubShader
	{
		Tags { "Queue" = "Transparent" "RenderType" = "Transparent" }
		LOD 100

		Pass
		{
			Blend SrcAlpha OneMinusSrcAlpha

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
			sampler2D _MainTex;
			sampler2D _DistMap;
			float _Strength;
			float _Frequency;
			float _Speed;
			float _Offset;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = v.uv;
				return o;
			}
			
			fixed4 frag (v2f i) : SV_Target
			{
				float2 displacement = tex2D(_DistMap, float2(i.uv.x + (_Time.x + _Offset) * _Speed, i.uv.y + (_Time.x + _Offset) * _Speed)).xy;
				displacement = ((displacement * 2) - 1) * _Strength;

				fixed4 col = tex2D(_MainTex, i.uv + displacement);
				return col;
			}
			ENDCG
		}
	}
}
