Shader "Custom/UnlitMultiTextureAngleWithShadows"
{
   	Properties 
   	{
      	_MainTex ("Texture", 2D) = "white" {}
      	_CloudTex ("Clouds", 2D) = "white" {}
      	_CloudAlpha ("Cloud Alpha", Range(0, 1)) = 0
      	_LightMapTex ("Lightmap", 2D) = "white" {}
      	_Angle ("Angle", Float) = 0
   	}
   
   	SubShader 
   	{
  	  	Tags {"Queue" = "Geometry" "RenderType" = "Opaque" "LightMode" = "Always" "LightMode" = "ForwardBase"}

      	Pass 
      	{ 
         	CGPROGRAM
         	
         	#include "UnityCG.cginc"
       		#include "AutoLight.cginc"
       		
       		#pragma vertex vert
         	#pragma fragment frag 
            #pragma multi_compile_fwdbase
            #pragma fragmentoption ARB_precision_hint_fastest
          	
         	uniform sampler2D _MainTex;
         	uniform sampler2D _CloudTex;
         	uniform sampler2D _LightMapTex;
         
         	float4 _MainTex_ST;
         	float4 _LightMapTex_ST;
         	float4 _CloudTex_ST;
         	float _Angle;
         	float _CloudAlpha;
 
         	struct vertexInput 
         	{
            	float4 vertex : POSITION;
            	float4 texcoord : TEXCOORD0;
         	};
         
         	struct vertexOutput 
         	{
            	float4 pos : SV_POSITION;
            	float2 uv[3] : TEXCOORD0;
            	
            	LIGHTING_COORDS(3, 4) //This tells it to put the vertex attributes required for lighting into TEXCOORD1 and TEXCOORD2.
         	};
 
         	vertexOutput vert(appdata_full v)
         	{
            	vertexOutput output;
            	
            	float sinX = sin( -_Angle );
            	float cosX = cos( -_Angle );
            	float2x2 rotationMatrix = float2x2( cosX, -sinX, sinX, cosX);
            	float2 texUV = mul ( v.texcoord.xy, rotationMatrix );
            	
            	output.pos = mul(UNITY_MATRIX_MVP, v.vertex);
            	output.uv[0] = TRANSFORM_TEX(texUV, _MainTex);
            	output.uv[1] = TRANSFORM_TEX(v.texcoord, _LightMapTex);
            	output.uv[2] = TRANSFORM_TEX(v.texcoord, _CloudTex);
            	
            	TRANSFER_VERTEX_TO_FRAGMENT(output) //This sets up the vertex attributes required for lighting and passes them through to the fragment shader.
            
            	return output;
         	}
 
         	float4 frag(vertexOutput i) : COLOR
         	{
         		fixed atten = LIGHT_ATTENUATION(i); //This gets the shadow and attenuation values combined.
                
                float cloudCol = (tex2D(_CloudTex, float2(i.uv[2]) * _CloudTex_ST.xy + _CloudTex_ST.zw));
                cloudCol = cloudCol + ((1 - cloudCol) * (1 - _CloudAlpha));
                    
         		float4 textureColor = 	tex2D(_MainTex, float2(i.uv[0]) * _MainTex_ST.xy + _MainTex_ST.zw) * 
         								tex2D(_LightMapTex, float2(i.uv[1]) * _LightMapTex_ST.xy + _LightMapTex_ST.zw) *
         								cloudCol * 1.5;
         		
            	return textureColor * atten;
         	}
         
         	ENDCG
      	}
   	}
	
	Fallback "Diffuse"
}