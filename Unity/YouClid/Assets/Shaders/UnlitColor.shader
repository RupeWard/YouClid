Shader "Custom/UnlitColor" 
{
	Properties 
	{
		_Color ("Main Color", Color) = (1,1,1,1)
	}
	SubShader
	{
		Pass
		{
			Tags { "Queue" = "Opaque" }
			
			CGPROGRAM
			
			#include "UnityCG.cginc"
			#pragma vertex vert
			#pragma fragment frag

			//Textures
			uniform float4 _Color; 
			
			struct input
			{
				float4 vertex : POSITION;
			};
			
			struct v2f
			{
				float4  pos : SV_POSITION;
			};

			v2f vert (input v)
			{
				v2f o;
				o.pos = mul (UNITY_MATRIX_MVP, v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i) : COLOR
			{
				return _Color;
			}
			ENDCG
		}
	}
	FallBack "Diffuse"
}
