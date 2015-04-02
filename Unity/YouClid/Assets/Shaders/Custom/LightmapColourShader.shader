Shader "Stick Sports/Lightmap Colour Shader"
{
	Properties
	{
		_MainTint("Color Tint", Color) = (1,1,1,1)
		_LightMap ("Lightmap", 2D) = "gray" {}
	}
	SubShader
	{
  	  	Tags {"Queue" = "Geometry" "RenderType" = "Opaque" "LightMode" = "ForwardBase"}

      	Pass 
      	{
			CGPROGRAM
         	#include "UnityCG.cginc"
       		#include "AutoLight.cginc"
       		
       		#pragma vertex vert
         	#pragma fragment frag 
            #pragma multi_compile_fwdbase
            #pragma fragmentoption ARB_precision_hint_fastest

         	float4 _MainTint;
         	float4 _LightMap_ST;
         	sampler2D _LightMap;
 
         	struct vertexInput 
         	{
            	float4 vertex : POSITION;
            	float4 texcoord : TEXCOORD0;
         	};
         
         	struct vertexOutput 
         	{
            	float4 pos : SV_POSITION;
            	float2 uv : TEXCOORD0;
            	
            	LIGHTING_COORDS(1, 2) 						//This tells it to put the vertex attributes required for lighting into TEXCOORD1 and TEXCOORD2.
         	};

         	vertexOutput vert(appdata_full v)
         	{
            	vertexOutput output;
            
            	output.pos = mul(UNITY_MATRIX_MVP, v.vertex);
            	output.uv = v.texcoord * _LightMap_ST.xy + _LightMap_ST.zw;
            	
            	TRANSFER_VERTEX_TO_FRAGMENT(output) 			//This sets up the vertex attributes required for lighting and passes them through to the fragment shader.
            	return output;
         	}

        	float4 frag(vertexOutput i) : COLOR
         	{
         		float atten = LIGHT_ATTENUATION(i); 	//This gets the shadow and attenuation values combined.
          		float4 lightMapColor = 	tex2D(_LightMap, i.uv ) * 2.0;
            	return  _MainTint * atten * lightMapColor;
         	}
			ENDCG
		}
	} 
	FallBack "Mobile/Diffuse"
}
