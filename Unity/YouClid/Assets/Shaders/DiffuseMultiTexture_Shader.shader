Shader "Custom/DiffuseMultiTexture"
{
   Properties 
   {
      _MainTex ("Texture", 2D) = "white" {}
      _LightMapTex ("Lightmap", 2D) = "white" {}
      
      _Color ("Overall Diffuse Color Filter", Color) = (1,1,1,1)
      _SpecColor ("Specular Material Color", Color) = (1,1,1,1) 
      _Shininess ("Shininess", Float) = 10
   }
   
   SubShader 
   {
  	  Tags {"Queue" = "Geometry"}
      Pass 
      {
         CGPROGRAM
 
         #pragma vertex vert  
         #pragma fragment frag
 
         #include "UnityCG.cginc" 
         uniform float4 _LightColor0; // color of light source (from "Lighting.cginc")
 
         // User-specified properties
         uniform sampler2D _MainTex;
         uniform sampler2D _LightMapTex;
            
         uniform float4 _Color;
         uniform float4 _SpecColor;
         uniform float _Shininess;
         
         float4 _MainTex_ST;
         float4 _LightMapTex_ST;
 
         struct vertexInput
         {
            float4 vertex : POSITION;
            float3 normal : NORMAL;
            float4 texcoord : TEXCOORD0;
         };
         
         struct vertexOutput
         {
            float4 pos : SV_POSITION;
            float2 uv[2] : TEXCOORD0;
            float3 diffuseColor : TEXCOORD2;
            float3 specularColor : TEXCOORD3;
         };
 
         vertexOutput vert(vertexInput input) 
         {
            vertexOutput output;
 
            float4x4 modelMatrix = _Object2World;
            float4x4 modelMatrixInverse = _World2Object; 
               // multiplication with unity_Scale.w is unnecessary 
               // because we normalize transformed vectors
 
            float3 normalDirection = normalize(float3(
               mul(float4(input.normal, 0.0), modelMatrixInverse)));
            float3 viewDirection = normalize(float3(
               float4(_WorldSpaceCameraPos, 1.0) 
               - mul(modelMatrix, input.vertex)));
            float3 lightDirection;
            float attenuation;
 
            if (0.0 == _WorldSpaceLightPos0.w) // directional light?
            {
               attenuation = 1.0; // no attenuation
               lightDirection = 
                  normalize(float3(_WorldSpaceLightPos0));
            } 
            else // point or spot light
            {
               float3 vertexToLightSource = float3(_WorldSpaceLightPos0
                  - mul(modelMatrix, input.vertex));
               float distance = length(vertexToLightSource);
               attenuation = 1.0 / distance; // linear attenuation 
               lightDirection = normalize(vertexToLightSource);
            }
 
            float3 ambientLighting = 
               float3(UNITY_LIGHTMODEL_AMBIENT) * float3(_Color);
 
            float3 diffuseReflection = 
               attenuation * float3(_LightColor0) * float3(_Color)
               * max(0.0, dot(normalDirection, lightDirection));
 
            float3 specularReflection;
            if (dot(normalDirection, lightDirection) < 0.0) 
               // light source on the wrong side?
            {
               specularReflection = float3(0.0, 0.0, 0.0); 
                  // no specular reflection
            }
            else // light source on the right side
            {
               specularReflection = attenuation * float3(_LightColor0) 
                  * float3(_SpecColor) * pow(max(0.0, dot(
                  reflect(-lightDirection, normalDirection), 
                  viewDirection)), _Shininess);
            }
 
            output.diffuseColor = ambientLighting + diffuseReflection;
            output.specularColor = specularReflection;
            
            output.pos = mul(UNITY_MATRIX_MVP, input.vertex);
            output.uv[0] = TRANSFORM_TEX(input.texcoord, _MainTex);
            output.uv[1] = TRANSFORM_TEX(input.texcoord, _LightMapTex);
            
            return output;
         }
 
         float4 frag(vertexOutput input) : COLOR
         {
         	float4 textureColor = tex2D(_MainTex, float2(input.uv[0]) * _MainTex_ST.xy + _MainTex_ST.zw) * tex2D(_LightMapTex, float2(input.uv[1]) * _LightMapTex_ST.xy + _LightMapTex_ST.zw);
            return float4(input.diffuseColor * 3.5 * textureColor, 1.0);
         }
 
         ENDCG
      }
   }
   
   Fallback "Diffuse"
}