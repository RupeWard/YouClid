Shader "Custom/UnlitMultiTexture"
{
   Properties 
   {
      _MainTex ("Texture", 2D) = "white" {}
      _LightMapTex ("Lightmap", 2D) = "white" {}
   }
   
   SubShader 
   {
  	  Tags {"Queue" = "Geometry"}
      Pass 
      { 
         CGPROGRAM
         #include "UnityCG.cginc"
         #pragma vertex vert  
         #pragma fragment frag 
		 #pragma exclude_renderers xbox360 ps3 flash
 
         uniform sampler2D _MainTex;
         uniform sampler2D _LightMapTex;
         
         float4 _MainTex_ST;
         float4 _LightMapTex_ST;
 
         struct vertexInput 
         {
            float4 vertex : POSITION;
            float4 texcoord : TEXCOORD0;
         };
         
         struct vertexOutput 
         {
            float4 pos : SV_POSITION;
            float2 uv[2] : TEXCOORD0;
         };
 
         vertexOutput vert(appdata_full input)
         {
            vertexOutput output;
            
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            output.uv[0] = TRANSFORM_TEX(input.texcoord, _MainTex);
            output.uv[1] = TRANSFORM_TEX(input.texcoord, _LightMapTex);
            
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 textureColor = 	tex2D(_MainTex, float2(input.uv[0]) * _MainTex_ST.xy + _MainTex_ST.zw) * 
         							tex2D(_LightMapTex, float2(input.uv[1]) * _LightMapTex_ST.xy + _LightMapTex_ST.zw) * 1.5;
         		
            return textureColor;
         }
         
         ENDCG
      }
   }
   
   Fallback "Diffuse"
}