Shader "Custom/SurfaceDiffuseMultiTexture"
{
	Properties
	{
		_MainTex ("Main Texture", 2D) = "white" {}
		_LightMapTex ("LightMap Texture (RGB)", 2D) = "white" {}
	}
	
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 200
		
		CGPROGRAM
		#pragma surface surf Lambert

		sampler2D _MainTex;
		sampler2D _LightMapTex;

		struct Input
		{
			float2 uv_MainTex; 
			float2 uv_LightMapTex;
		};

		void surf (Input IN, inout SurfaceOutput o)
		{
			half4 mainTextureColor = tex2D (_MainTex, IN.uv_MainTex);
			half4 lightMapTextureColor = tex2D (_LightMapTex, IN.uv_LightMapTex);
			
			o.Albedo = mainTextureColor.rgb * lightMapTextureColor.rgb * 1.5;
		}
		
		ENDCG
	}
	 
	FallBack "Diffuse"
}
