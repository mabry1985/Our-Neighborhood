// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "BOXOPHOBIC/Polyverse Skies/Standard"
{
	Properties
	{
		[StyledBanner(Polyverse Skies Standard)]_TITLE1("< TITLE >", Float) = 1
		[StyledCategory(Sky)]_SkyCat1("[ Sky Cat ]", Float) = 1
		_SkyColor("Sky Color", Color) = (0.4980392,0.7450981,1,1)
		_EquatorColor("Equator Color", Color) = (1,0.747,0,1)
		_GroundColor("Ground Color", Color) = (0.4980392,0.497,0,1)
		_EquatorHeight("Equator Height", Range( 0 , 1)) = 0.5
		_EquatorSmoothness("Equator Smoothness", Range( 0.01 , 1)) = 0.5
		[StyledCategory(Pattern)]_PatternCat("[ Pattern Cat ]", Float) = 1
		[Toggle(_ENABLEPATTERNOVERLAY_ON)] _EnablePatternOverlay("Enable Pattern Overlay", Float) = 0
		[NoScaleOffset]_PatternCubemap("Pattern Cubemap", CUBE) = "black" {}
		_PatternContrast("Pattern Contrast", Range( 0 , 2)) = 0.5
		[StyledCategory(Stars)]_StarsCat1("[ Stars Cat ]", Float) = 1
		[Toggle(_ENABLESTARS_ON)] _EnableStars("Enable Stars", Float) = 0
		[NoScaleOffset]_StarsCubemap("Stars Cubemap", CUBE) = "white" {}
		[IntRange]_StarsLayer("Stars Layers", Range( 1 , 3)) = 2
		_StarsSize("Stars Size", Range( 0 , 0.99)) = 0.5
		_StarsIntensity("Stars Intensity", Range( 0 , 5)) = 2
		_StarsSunMask("Stars Sun Mask", Range( 0 , 1)) = 0
		_StarsHeightMask("Stars Height Mask", Range( 0 , 1)) = 0
		[Space(10)][Toggle(_ENABLESTARSTWINKLING_ON)] _EnableStarsTwinkling("Enable Stars Twinkling", Float) = 0
		[NoScaleOffset]_TwinklingTexture("Twinkling Texture", 2D) = "white" {}
		_TwinklingContrast("Twinkling Contrast", Range( 0 , 2)) = 1
		_TwinklingSpeed("Twinkling Speed", Float) = 0.05
		[StyledCategory(Sun)]_SunCat1("[ Sun Cat ]", Float) = 1
		[Toggle(_ENABLESUN_ON)] _EnableSun("Enable Sun", Float) = 0
		_SunColor("Sun Color", Color) = (1,1,1,1)
		[NoScaleOffset]_SunTexture("Sun Texture", 2D) = "black" {}
		_SunSize("Sun Size", Range( 0.1 , 1)) = 0.5
		_SunIntensity("Sun Intensity", Range( 1 , 10)) = 1
		[StyledCategory(Moon)]_MoonCat("[ Moon Cat ]", Float) = 1
		[Toggle(_ENABLEMOON_ON)] _EnableMoon("Enable Moon", Float) = 0
		_MoonColor("Moon Color", Color) = (1,1,1,1)
		[NoScaleOffset]_MoonTexture("Moon Texture", 2D) = "black" {}
		_MoonSize("Moon Size", Range( 0.1 , 1)) = 0.5
		_MoonIntensity("Moon Intensity", Range( 1 , 10)) = 1
		[StyledCategory(Clouds)]_CloudsCat1("[ Clouds Cat ]", Float) = 1
		[Toggle(_ENABLECLOUDS_ON)] _EnableClouds("Enable Clouds", Float) = 0
		_CloudsLightColor("Clouds Light Color", Color) = (1,1,1,1)
		_CloudsShadowColor("Clouds Shadow Color", Color) = (0.4980392,0.7450981,1,1)
		[NoScaleOffset]_CloudsCubemap("Clouds Cubemap", CUBE) = "black" {}
		_CloudsHeight("Clouds Height", Range( -0.5 , 0.5)) = 0
		[Space(10)][Toggle(_CLOUDSLITBYSUN_ON)] _CloudsLitbySun("Clouds Lit by Sun", Float) = 0
		[Space(10)][Toggle(_ENABLECLOUDSROTATION_ON)] _EnableCloudsRotation("Enable Clouds Rotation", Float) = 0
		[IntRange]_CloudsRotation("Clouds Rotation", Range( 0 , 360)) = 360
		_CloudsRotationSpeed("Clouds Rotation Speed", Float) = 0.5
		[StyledCategory(Fog)]_FogCat1("[ Fog Cat ]", Float) = 1
		[Toggle(_ENABLEBUILTINFOG_ON)] _EnableBuiltinFog("Enable Fog", Float) = 0
		_FogHeight("Fog Height", Range( 0 , 1)) = 0
		_FogSmoothness("Fog Smoothness", Range( 0.01 , 1)) = 0
		_FogFill("Fog Fill", Range( 0 , 1)) = 0

	}
	
	SubShader
	{
		
		
		Tags { "RenderType"="Background" "Queue"="Background" "PreviewType"="Skybox" "IgnoreProjector"="True" }
	LOD 100

		CGINCLUDE
		#pragma target 3.0
		ENDCG
		Blend Off
		Cull Off
		ColorMask RGBA
		ZWrite Off
		ZTest LEqual
		
		
		
		Pass
		{
			Name "Unlit"
			Tags { "LightMode"="ForwardBase" }
			CGPROGRAM

			

			#ifndef UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX
			//only defining to not throw compilation error over Unity 5.5
			#define UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(input)
			#endif
			#pragma vertex vert
			#pragma fragment frag
			#pragma multi_compile_instancing
			#include "UnityCG.cginc"
			#include "UnityShaderVariables.cginc"
			#pragma shader_feature_local _ENABLEBUILTINFOG_ON
			#pragma shader_feature_local _ENABLECLOUDS_ON
			#pragma shader_feature_local _ENABLEMOON_ON
			#pragma shader_feature_local _ENABLESUN_ON
			#pragma shader_feature_local _ENABLESTARS_ON
			#pragma shader_feature_local _ENABLEPATTERNOVERLAY_ON
			#pragma shader_feature_local _ENABLESTARSTWINKLING_ON
			#pragma shader_feature_local _CLOUDSLITBYSUN_ON
			#pragma shader_feature_local _ENABLECLOUDSROTATION_ON


			struct appdata
			{
				float4 vertex : POSITION;
				float4 color : COLOR;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				
			};
			
			struct v2f
			{
				float4 vertex : SV_POSITION;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				float4 ase_texcoord5 : TEXCOORD5;
				float4 ase_texcoord6 : TEXCOORD6;
			};

			uniform half _SkyCat1;
			uniform half _StarsCat1;
			uniform half _SunCat1;
			uniform half _CloudsCat1;
			uniform half _MoonCat;
			uniform half _TITLE1;
			uniform half _FogCat1;
			uniform half _PatternCat;
			uniform half4 _EquatorColor;
			uniform half4 _GroundColor;
			uniform half4 _SkyColor;
			uniform half _EquatorHeight;
			uniform half _EquatorSmoothness;
			uniform half _PatternContrast;
			uniform samplerCUBE _PatternCubemap;
			uniform half3 GlobalSunDirection;
			uniform half _StarsSunMask;
			uniform samplerCUBE _StarsCubemap;
			uniform half _StarsLayer;
			uniform half _StarsSize;
			uniform half _StarsHeightMask;
			uniform sampler2D _TwinklingTexture;
			uniform half _TwinklingSpeed;
			uniform half _TwinklingContrast;
			uniform half _StarsIntensity;
			uniform sampler2D _SunTexture;
			uniform half _SunSize;
			uniform half4 _SunColor;
			uniform half _SunIntensity;
			uniform sampler2D _MoonTexture;
			uniform half3 GlobalMoonDirection;
			uniform half _MoonSize;
			uniform half4 _MoonColor;
			uniform half _MoonIntensity;
			uniform half4 _CloudsShadowColor;
			uniform half4 _CloudsLightColor;
			uniform samplerCUBE _CloudsCubemap;
			uniform half _CloudsRotation;
			uniform half _CloudsRotationSpeed;
			uniform half _CloudsHeight;
			uniform half _FogHeight;
			uniform half _FogSmoothness;
			uniform half _FogFill;
			float4 CalculateContrast( float contrastValue, float4 colorTarget )
			{
				float t = 0.5 * ( 1.0 - contrastValue );
				return mul( float4x4( contrastValue,0,0,t, 0,contrastValue,0,t, 0,0,contrastValue,t, 0,0,0,1 ), colorTarget );
			}

			
			v2f vert ( appdata v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO(o);
				UNITY_TRANSFER_INSTANCE_ID(v, o);

				half3 GlobalSunDirection1005 = GlobalSunDirection;
				float3 ase_worldPos = mul(unity_ObjectToWorld, v.vertex).xyz;
				float3 ase_worldViewDir = UnityWorldSpaceViewDir(ase_worldPos);
				ase_worldViewDir = normalize(ase_worldViewDir);
				float dotResult732 = dot( GlobalSunDirection1005 , ase_worldViewDir );
				#ifdef _ENABLESTARS_ON
				float staticSwitch947 = saturate( (0.0 + (dotResult732 - -1.0) * (1.0 - 0.0) / (-( 1.0 - _StarsSunMask ) - -1.0)) );
				#else
				float staticSwitch947 = 0.0;
				#endif
				float vertexToFrag762 = staticSwitch947;
				o.ase_texcoord1.x = vertexToFrag762;
				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float3 appendResult675 = (float3(v.vertex.xyz.x , ( v.vertex.xyz.y * CAMERA_MODE300 ) , v.vertex.xyz.z));
				#ifdef _ENABLESTARS_ON
				float3 staticSwitch941 = appendResult675;
				#else
				float3 staticSwitch941 = float3( 0,0,0 );
				#endif
				float3 vertexToFrag763 = staticSwitch941;
				o.ase_texcoord1.yzw = vertexToFrag763;
				#ifdef _ENABLESTARS_ON
				float staticSwitch953 = saturate( (0.1 + (abs( v.vertex.xyz.y ) - 0.0) * (1.0 - 0.1) / (_StarsHeightMask - 0.0)) );
				#else
				float staticSwitch953 = 0.0;
				#endif
				float vertexToFrag856 = staticSwitch953;
				o.ase_texcoord2.x = vertexToFrag856;
				float2 temp_cast_0 = (_TwinklingSpeed).xx;
				float4 ase_clipPos = UnityObjectToClipPos(v.vertex);
				float4 screenPos = ComputeScreenPos(ase_clipPos);
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 appendResult568 = (float2(ase_screenPosNorm.x , ase_screenPosNorm.y));
				float2 panner569 = ( _Time.y * temp_cast_0 + appendResult568);
				#ifdef _ENABLESTARS_ON
				float2 staticSwitch956 = panner569;
				#else
				float2 staticSwitch956 = float2( 0,0 );
				#endif
				float2 vertexToFrag761 = staticSwitch956;
				o.ase_texcoord2.yz = vertexToFrag761;
				float3 temp_output_962_0 = cross( GlobalSunDirection , half3(0,1,0) );
				float3 normalizeResult967 = normalize( temp_output_962_0 );
				float dotResult968 = dot( normalizeResult967 , v.vertex.xyz );
				float3 normalizeResult965 = normalize( cross( GlobalSunDirection1005 , temp_output_962_0 ) );
				float dotResult969 = dot( normalizeResult965 , v.vertex.xyz );
				float2 appendResult970 = (float2(dotResult968 , dotResult969));
				float2 break972 = appendResult970;
				float2 appendResult980 = (float2(break972.x , ( break972.y * CAMERA_MODE300 )));
				float2 temp_cast_1 = (-1.0).xx;
				float2 temp_cast_2 = (1.0).xx;
				float2 temp_cast_3 = (0.0).xx;
				float2 temp_cast_4 = (1.0).xx;
				#ifdef _ENABLESUN_ON
				float2 staticSwitch940 = (temp_cast_3 + (( appendResult980 * (20.0 + (_SunSize - 0.1) * (2.0 - 20.0) / (1.0 - 0.1)) ) - temp_cast_1) * (temp_cast_4 - temp_cast_3) / (temp_cast_2 - temp_cast_1));
				#else
				float2 staticSwitch940 = float2( 0,0 );
				#endif
				float2 vertexToFrag993 = staticSwitch940;
				o.ase_texcoord3.xy = vertexToFrag993;
				float dotResult988 = dot( GlobalSunDirection1005 , v.vertex.xyz );
				#ifdef _ENABLESUN_ON
				float staticSwitch1027 = saturate( dotResult988 );
				#else
				float staticSwitch1027 = 0.0;
				#endif
				float vertexToFrag997 = staticSwitch1027;
				o.ase_texcoord2.w = vertexToFrag997;
				float3 temp_output_1058_0 = cross( GlobalMoonDirection , half3(0,1,0) );
				float3 normalizeResult1039 = normalize( temp_output_1058_0 );
				float dotResult1036 = dot( normalizeResult1039 , v.vertex.xyz );
				half3 GlobalMoonDirection1073 = GlobalMoonDirection;
				float3 normalizeResult1064 = normalize( cross( GlobalMoonDirection1073 , temp_output_1058_0 ) );
				float dotResult1067 = dot( normalizeResult1064 , v.vertex.xyz );
				float2 appendResult1066 = (float2(dotResult1036 , dotResult1067));
				float2 break1063 = appendResult1066;
				float2 appendResult1069 = (float2(break1063.x , ( break1063.y * CAMERA_MODE300 )));
				float2 temp_cast_5 = (-1.0).xx;
				float2 temp_cast_6 = (1.0).xx;
				float2 temp_cast_7 = (0.0).xx;
				float2 temp_cast_8 = (1.0).xx;
				#ifdef _ENABLEMOON_ON
				float2 staticSwitch1057 = (temp_cast_7 + (( appendResult1069 * (20.0 + (_MoonSize - 0.1) * (2.0 - 20.0) / (1.0 - 0.1)) ) - temp_cast_5) * (temp_cast_8 - temp_cast_7) / (temp_cast_6 - temp_cast_5));
				#else
				float2 staticSwitch1057 = float2( 0,0 );
				#endif
				float2 vertexToFrag1043 = staticSwitch1057;
				o.ase_texcoord3.zw = vertexToFrag1043;
				float dotResult1054 = dot( GlobalMoonDirection1073 , v.vertex.xyz );
				#ifdef _ENABLEMOON_ON
				float staticSwitch1052 = saturate( dotResult1054 );
				#else
				float staticSwitch1052 = 0.0;
				#endif
				float vertexToFrag1051 = staticSwitch1052;
				o.ase_texcoord4.x = vertexToFrag1051;
				float3 appendResult56 = (float3(cos( radians( ( _CloudsRotation + ( _Time.y * _CloudsRotationSpeed ) ) ) ) , 0.0 , ( sin( radians( ( _CloudsRotation + ( _Time.y * _CloudsRotationSpeed ) ) ) ) * -1.0 )));
				#ifdef _ENABLECLOUDS_ON
				float3 staticSwitch1122 = appendResult56;
				#else
				float3 staticSwitch1122 = float3( 0,0,0 );
				#endif
				float3 vertexToFrag774 = staticSwitch1122;
				o.ase_texcoord4.yzw = vertexToFrag774;
				float3 appendResult266 = (float3(0.0 , 1.0 , 0.0));
				#ifdef _ENABLECLOUDS_ON
				float3 staticSwitch1123 = appendResult266;
				#else
				float3 staticSwitch1123 = float3( 0,0,0 );
				#endif
				float3 vertexToFrag775 = staticSwitch1123;
				o.ase_texcoord5.xyz = vertexToFrag775;
				float3 appendResult58 = (float3(sin( radians( ( _CloudsRotation + ( _Time.y * _CloudsRotationSpeed ) ) ) ) , 0.0 , cos( radians( ( _CloudsRotation + ( _Time.y * _CloudsRotationSpeed ) ) ) )));
				#ifdef _ENABLECLOUDS_ON
				float3 staticSwitch1124 = appendResult58;
				#else
				float3 staticSwitch1124 = float3( 0,0,0 );
				#endif
				float3 vertexToFrag776 = staticSwitch1124;
				o.ase_texcoord6.xyz = vertexToFrag776;
				
				o.ase_texcoord = v.vertex;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord5.w = 0;
				o.ase_texcoord6.w = 0;
				float3 vertexValue = float3(0, 0, 0);
				#if ASE_ABSOLUTE_VERTEX_POS
				vertexValue = v.vertex.xyz;
				#endif
				vertexValue = vertexValue;
				#if ASE_ABSOLUTE_VERTEX_POS
				v.vertex.xyz = vertexValue;
				#else
				v.vertex.xyz += vertexValue;
				#endif
				o.vertex = UnityObjectToClipPos(v.vertex);
				return o;
			}
			
			fixed4 frag (v2f i ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID(i);
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX(i);
				fixed4 finalColor;
				float4 lerpResult180 = lerp( _GroundColor , _SkyColor , ceil( i.ase_texcoord.xyz.y ));
				float4 lerpResult288 = lerp( _EquatorColor , lerpResult180 , saturate( pow( (0.0 + (abs( i.ase_texcoord.xyz.y ) - 0.0) * (1.0 - 0.0) / (_EquatorHeight - 0.0)) , ( 1.0 - _EquatorSmoothness ) ) ));
				half4 SKY218 = lerpResult288;
				half4 PATTERN513 = saturate( CalculateContrast(_PatternContrast,texCUBE( _PatternCubemap, i.ase_texcoord.xyz )) );
				float4 blendOpSrc574 = PATTERN513;
				float4 blendOpDest574 = SKY218;
				#ifdef _ENABLEPATTERNOVERLAY_ON
				float4 staticSwitch524 = ( saturate( (( blendOpDest574 > 0.5 ) ? ( 1.0 - 2.0 * ( 1.0 - blendOpDest574 ) * ( 1.0 - blendOpSrc574 ) ) : ( 2.0 * blendOpDest574 * blendOpSrc574 ) ) ));
				#else
				float4 staticSwitch524 = SKY218;
				#endif
				float vertexToFrag762 = i.ase_texcoord1.x;
				float3 vertexToFrag763 = i.ase_texcoord1.yzw;
				float4 texCUBENode564 = texCUBE( _StarsCubemap, vertexToFrag763 );
				float temp_output_537_0 = (3.0 + (_StarsLayer - 1.0) * (1.0 - 3.0) / (3.0 - 1.0));
				float vertexToFrag856 = i.ase_texcoord2.x;
				float2 vertexToFrag761 = i.ase_texcoord2.yz;
				#ifdef _ENABLESTARSTWINKLING_ON
				float staticSwitch878 = ( saturate( pow( tex2D( _TwinklingTexture, vertexToFrag761 ).r , _TwinklingContrast ) ) * floor( ( vertexToFrag762 * ( ( texCUBENode564.r + ( texCUBENode564.g * step( temp_output_537_0 , 2.0 ) ) + ( texCUBENode564.b * step( temp_output_537_0 , 1.0 ) ) ) + _StarsSize ) * vertexToFrag856 ) ) );
				#else
				float staticSwitch878 = floor( ( vertexToFrag762 * ( ( texCUBENode564.r + ( texCUBENode564.g * step( temp_output_537_0 , 2.0 ) ) + ( texCUBENode564.b * step( temp_output_537_0 , 1.0 ) ) ) + _StarsSize ) * vertexToFrag856 ) );
				#endif
				half STARS630 = ( staticSwitch878 * _StarsIntensity );
				#ifdef _ENABLESTARS_ON
				float4 staticSwitch918 = ( staticSwitch524 + STARS630 );
				#else
				float4 staticSwitch918 = staticSwitch524;
				#endif
				float2 vertexToFrag993 = i.ase_texcoord3.xy;
				float4 tex2DNode995 = tex2D( _SunTexture, vertexToFrag993 );
				half4 SUN1004 = ( tex2DNode995.r * _SunColor * _SunIntensity );
				float vertexToFrag997 = i.ase_texcoord2.w;
				half SUN_MASK1003 = ( tex2DNode995.a * vertexToFrag997 );
				float4 lerpResult176 = lerp( staticSwitch918 , SUN1004 , SUN_MASK1003);
				#ifdef _ENABLESUN_ON
				float4 staticSwitch919 = lerpResult176;
				#else
				float4 staticSwitch919 = staticSwitch918;
				#endif
				float2 vertexToFrag1043 = i.ase_texcoord3.zw;
				float4 tex2DNode1049 = tex2D( _MoonTexture, vertexToFrag1043 );
				half4 MOON1077 = ( tex2DNode1049.r * _MoonColor * _MoonIntensity );
				float vertexToFrag1051 = i.ase_texcoord4.x;
				half MOON_MASK1078 = ( tex2DNode1049.a * vertexToFrag1051 );
				float4 lerpResult1114 = lerp( staticSwitch919 , MOON1077 , MOON_MASK1078);
				#ifdef _ENABLEMOON_ON
				float4 staticSwitch1113 = lerpResult1114;
				#else
				float4 staticSwitch1113 = staticSwitch919;
				#endif
				float lerpResult268 = lerp( 1.0 , ( unity_OrthoParams.y / unity_OrthoParams.x ) , unity_OrthoParams.w);
				half CAMERA_MODE300 = lerpResult268;
				float3 appendResult1129 = (float3(i.ase_texcoord.xyz.x , ( i.ase_texcoord.xyz.y * CAMERA_MODE300 ) , i.ase_texcoord.xyz.z));
				float3 vertexToFrag774 = i.ase_texcoord4.yzw;
				float3 vertexToFrag775 = i.ase_texcoord5.xyz;
				float3 vertexToFrag776 = i.ase_texcoord6.xyz;
				#ifdef _ENABLECLOUDSROTATION_ON
				float3 staticSwitch1164 = mul( float3x3(vertexToFrag774, vertexToFrag775, vertexToFrag776), appendResult1129 );
				#else
				float3 staticSwitch1164 = appendResult1129;
				#endif
				float3 break245 = staticSwitch1164;
				float3 appendResult246 = (float3(break245.x , ( break245.y + ( _CloudsHeight * -1.0 ) ) , break245.z));
				float4 texCUBENode41 = texCUBE( _CloudsCubemap, appendResult246 );
				half Clouds_G397 = texCUBENode41.g;
				half3 GlobalSunDirection1005 = GlobalSunDirection;
				#ifdef _ENABLECLOUDSROTATION_ON
				float3 staticSwitch1166 = mul( float3x3(vertexToFrag774, vertexToFrag775, vertexToFrag776), GlobalSunDirection1005 );
				#else
				float3 staticSwitch1166 = GlobalSunDirection1005;
				#endif
				float3 normalizeResult1163 = normalize( staticSwitch1166 );
				float3 temp_cast_0 = (0.0).xxx;
				float3 temp_cast_1 = (1.0).xxx;
				float3 temp_cast_2 = (-1.0).xxx;
				float3 temp_cast_3 = (1.0).xxx;
				float dotResult89 = dot( normalizeResult1163 , (temp_cast_2 + ((texCUBENode41).rgb - temp_cast_0) * (temp_cast_3 - temp_cast_2) / (temp_cast_1 - temp_cast_0)) );
				#ifdef _CLOUDSLITBYSUN_ON
				float staticSwitch391 = saturate( (0.0 + (dotResult89 - -1.0) * (1.0 - 0.0) / (1.0 - -1.0)) );
				#else
				float staticSwitch391 = Clouds_G397;
				#endif
				float4 lerpResult101 = lerp( _CloudsShadowColor , _CloudsLightColor , staticSwitch391);
				half4 CLOUDS222 = lerpResult101;
				half CLOUDS_MASK223 = texCUBENode41.a;
				float4 lerpResult227 = lerp( staticSwitch1113 , CLOUDS222 , CLOUDS_MASK223);
				#ifdef _ENABLECLOUDS_ON
				float4 staticSwitch1120 = lerpResult227;
				#else
				float4 staticSwitch1120 = staticSwitch1113;
				#endif
				float lerpResult678 = lerp( saturate( pow( (0.0 + (abs( i.ase_texcoord.xyz.y ) - 0.0) * (1.0 - 0.0) / (_FogHeight - 0.0)) , ( 1.0 - _FogSmoothness ) ) ) , 0.0 , _FogFill);
				half FOG_MASK359 = lerpResult678;
				float4 lerpResult317 = lerp( unity_FogColor , staticSwitch1120 , FOG_MASK359);
				#ifdef _ENABLEBUILTINFOG_ON
				float4 staticSwitch921 = lerpResult317;
				#else
				float4 staticSwitch921 = staticSwitch1120;
				#endif
				
				
				finalColor = staticSwitch921;
				return finalColor;
			}
			ENDCG
		}
	}
	
	
	
}
/*ASEBEGIN
Version=17602
1927;7;1906;1014;1400.957;6427.767;1;True;False
Node;AmplifyShaderEditor.SimpleTimeNode;701;-896,1664;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;260;-896,1792;Half;False;Property;_CloudsRotationSpeed;Clouds Rotation Speed;44;0;Create;True;0;0;False;0;0.5;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.OrthoParams;267;-896,-4480;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1007;-448,-4480;Half;False;Constant;_Float7;Float 7;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleDivideOpNode;309;-592,-4480;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;255;-640,1664;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;48;-896,1536;Half;False;Property;_CloudsRotation;Clouds Rotation;43;1;[IntRange];Create;True;0;0;False;0;360;0;0;360;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;268;-256,-4480;Inherit;False;3;0;FLOAT;1;False;1;FLOAT;0.5;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;276;-512,1536;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;300;0,-4480;Half;False;CAMERA_MODE;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RadiansOpNode;47;-384,1536;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;62;-224,1792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1171;-896,-1792;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;674;-896,-1600;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;673;-640,-1616;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;938;-896,-512;Half;False;Global;GlobalSunDirection;GlobalSunDirection;38;0;Create;True;0;0;False;0;0,0,0;0.3830222,0.6427875,-0.6634141;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.SinOpNode;59;128,1600;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1080;128,1664;Half;False;Constant;_Float26;Float 26;50;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;675;-448,-1792;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1005;-640,-512;Half;False;GlobalSunDirection;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SinOpNode;61;128,1920;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;365;128,1984;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1177;128,1760;Half;False;Constant;_Float1;Float 1;50;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;60;320,1600;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1081;128,1840;Half;False;Constant;_Float27;Float 27;50;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CosOpNode;55;128,1536;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;961;-896,-352;Half;False;Constant;_Vector2;Vector 2;9;0;Create;True;0;0;False;0;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;733;1664,-1472;Half;False;Property;_StarsSunMask;Stars Sun Mask;17;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1148;1408,-1792;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;512,1920;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1010;0,-1456;Half;False;Constant;_Float10;Float 10;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1011;0,-1376;Half;False;Constant;_Float11;Float 11;47;0;Create;True;0;0;False;0;3;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;266;512,1728;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ViewDirInputsCoordNode;731;1408,-1648;Float;False;World;False;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;528;0,-1536;Half;False;Property;_StarsLayer;Stars Layers;14;1;[IntRange];Create;False;0;0;False;0;2;0;1;3;0;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;738;1984,-1472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1172;1408,-1152;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;941;-208,-1742.622;Float;False;Property;_ENABLESTARS_ON;Enable Stars;12;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;947;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;56;512,1536;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.CrossProductOpNode;962;-576,-384;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1013;384,-1264;Half;False;Constant;_Float13;Float 13;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector3Node;1061;-896,512;Half;False;Global;GlobalMoonDirection;GlobalMoonDirection;38;0;Create;True;0;0;False;0;0,0,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.Vector3Node;1062;-896,672;Half;False;Constant;_Vector3;Vector 3;9;0;Create;True;0;0;False;0;0,1,0;0,0,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.RangedFloatNode;1016;1984,-1552;Half;False;Constant;_Float16;Float 16;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1123;768,1744;Float;False;Property;_ENABLECLOUDS_ON;Enable Clouds;36;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1120;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.GetLocalVarNode;1127;1408,1936;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1124;768,1920;Float;False;Property;_ENABLECLOUDS_ON;Enable Clouds;36;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1120;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;567;3456,-1792;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;1122;768,1584;Float;False;Property;_ENABLECLOUDS_ON;Enable Clouds;36;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1120;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;1178;1408,1792;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.NegateNode;740;2176,-1472;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1017;1792,-1040;Half;False;Constant;_Float17;Float 17;47;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1012;384,-1344;Half;False;Constant;_Float12;Float 12;47;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;732;1744,-1792;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;537;384,-1536;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;1;False;2;FLOAT;3;False;3;FLOAT;3;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode;964;-384,-512;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;831;2000,-960;Half;False;Property;_StarsHeightMask;Stars Height Mask;18;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1167;2032,-1056;Half;False;Constant;_Float0;Float 0;47;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;763;48,-1728;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1014;1984,-1712;Half;False;Constant;_Float14;Float 14;47;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1015;1984,-1632;Half;False;Constant;_Float15;Float 15;47;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;828;2048,-1152;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1018;1792,-960;Half;False;Constant;_Float18;Float 18;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;967;-384,-384;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;568;3712,-1792;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1128;1664,1920;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;774;1024,1600;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.VertexToFragmentNode;775;1024,1760;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.StepOpNode;533;640,-1536;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;776;1024,1936;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;1173;-384,-128;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1073;-640,512;Half;False;GlobalMoonDirection;-1;True;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;832;2304,-1152;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;726;2304,-1792;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;-0.5;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleTimeNode;633;3712,-1600;Inherit;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StepOpNode;534;640,-1408;Inherit;False;2;0;FLOAT;3;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode;1058;-576,640;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.NormalizeNode;965;-192,-512;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SamplerNode;564;384,-1792;Inherit;True;Property;_StarsCubemap;Stars Cubemap;13;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;white;LockedToCube;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;546;3712,-1680;Half;False;Property;_TwinklingSpeed;Twinkling Speed;22;0;Create;True;0;0;False;0;0.05;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.CrossProductOpNode;1038;-384,512;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DotProductOpNode;969;0,-512;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1168;-896,-3712;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;569;4016,-1792;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;530;832,-1536;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;724;2480,-1792;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1129;1792,1792;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;529;832,-1664;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;968;0,-384;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;822;2496,-1152;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.MatrixFromVectors;54;1408,1536;Inherit;False;FLOAT3x3;True;4;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;1;FLOAT3x3;0
Node;AmplifyShaderEditor.RangedFloatNode;471;-384,-3136;Half;False;Property;_EquatorSmoothness;Equator Smoothness;6;0;Create;True;0;0;False;0;0.5;0;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1175;-384,896;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;619;384,-1088;Half;False;Property;_StarsSize;Stars Size;15;0;Create;True;0;0;False;0;0.5;0;0;0.99;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;1064;-192,512;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;49;1984,1664;Inherit;False;2;2;0;FLOAT3x3;0,0,0,1,0,0,1,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleAddOpNode;527;1024,-1792;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1009;-384,-3376;Half;False;Constant;_Float9;Float 9;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalizeNode;1039;-384,640;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.DynamicAppendNode;970;160,-512;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1008;-384,-3456;Half;False;Constant;_Float8;Float 8;47;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;947;2688,-1744;Float;False;Property;_ENABLESTARS_ON;Enable Stars;12;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;918;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;953;2688,-1104;Float;False;Property;_ENABLESTARS_ON;Enable Stars;12;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;918;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;956;4288,-1744;Float;False;Property;_ENABLESTARS_ON;Enable Stars;12;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;918;False;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;212;-384,-3264;Half;False;Property;_EquatorHeight;Equator Height;5;0;Create;True;0;0;False;0;0.5;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.AbsOpNode;287;-192,-3664;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;1036;0,640;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;210;0,-3328;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1164;2240,1728;Float;False;Property;_EnableCloudsRotation;Enable Clouds Rotation;42;0;Create;True;0;0;False;1;Space(10);0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1090;2688,1968;Half;False;Constant;_Float31;Float 31;53;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;856;2944,-1088;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;1067;0,512;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;626;1152,-1392;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;972;320,-512;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.RangedFloatNode;241;2688,1872;Half;False;Property;_CloudsHeight;Clouds Height;40;0;Create;True;0;0;False;0;0;0;-0.5;0.5;0;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;762;2944,-1728;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;761;4544,-1728;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.OneMinusNode;475;-64,-3136;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;971;320,-384;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;566;4864,-1792;Inherit;True;Property;_TwinklingTexture;Twinkling Texture;20;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;876;3456,-1408;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;973;352,-304;Half;False;Constant;_Float4;Float 4;36;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1019;352,-224;Half;False;Constant;_Float19;Float 19;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;470;224,-3152;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1021;352,-64;Half;False;Constant;_Float21;Float 21;47;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;278;3008,1952;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;555;4864,-1600;Half;False;Property;_TwinklingContrast;Twinkling Contrast;21;0;Create;True;0;0;False;0;1;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1020;352,-144;Half;False;Constant;_Float20;Float 20;47;0;Create;True;0;0;False;0;20;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1170;-896,-2560;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;975;0,-256;Half;False;Property;_SunSize;Sun Size;27;0;Create;True;0;0;False;0;0.5;0;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CeilOpNode;1169;-640,-3216;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;976;640,-416;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;245;2688,1712;Inherit;False;FLOAT3;1;0;FLOAT3;0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.DynamicAppendNode;1066;160,512;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;980;832,-512;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FloorOpNode;886;3712,-1408;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;182;640,-3360;Half;False;Property;_SkyColor;Sky Color;2;0;Create;True;0;0;False;0;0.4980392,0.7450981,1,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TFHCRemapNode;981;640,-256;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;1;False;3;FLOAT;20;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;254;-640,-2560;Inherit;True;Property;_PatternCubemap;Pattern Cubemap;9;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RelayNode;303;896,-3216;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;194;640,-3536;Half;False;Property;_GroundColor;Ground Color;4;0;Create;True;0;0;False;0;0.4980392,0.497,0,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;1065;320,640;Inherit;False;300;CAMERA_MODE;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;554;5184,-1728;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;1063;320,512;Inherit;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.SimpleAddOpNode;244;3152,1856;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;283;-640,-2368;Half;False;Property;_PatternContrast;Pattern Contrast;10;0;Create;True;0;0;False;0;0.5;0;0;2;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;208;384,-3136;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1071;352,720;Half;False;Constant;_Float30;Float 30;36;0;Create;True;0;0;False;0;0.1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;181;640,-3712;Half;False;Property;_EquatorColor;Equator Color;3;0;Create;True;0;0;False;0;1,0.747,0,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1047;352,800;Half;False;Constant;_Float28;Float 28;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;246;3328,1712;Inherit;False;FLOAT3;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;985;1024,-512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1040;352,960;Half;False;Constant;_Float3;Float 3;47;0;Create;True;0;0;False;0;2;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1072;640,608;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1046;0,768;Half;False;Property;_MoonSize;Moon Size;33;0;Create;True;0;0;False;0;0.5;0;0.1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1147;4224,1616;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.PosVertexDataNode;1174;2816,0;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1023;1024,-304;Half;False;Constant;_Float6;Float 6;47;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;881;5344,-1408;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RelayNode;417;1152,-3136;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1024;1024,-224;Half;False;Constant;_Float22;Float 22;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;180;1024,-3520;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1022;1024,-384;Half;False;Constant;_Float5;Float 5;47;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleContrastOpNode;519;-256,-2560;Inherit;False;2;1;COLOR;0,0,0,0;False;0;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;756;5344,-1728;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1028;2816,-128;Inherit;False;1005;GlobalSunDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1070;352,880;Half;False;Constant;_Float29;Float 29;47;0;Create;True;0;0;False;0;20;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DynamicAppendNode;1069;832,512;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TFHCRemapNode;1068;640,768;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0.1;False;2;FLOAT;1;False;3;FLOAT;20;False;4;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;41;3520,1712;Inherit;True;Property;_CloudsCubemap;Clouds Cubemap;39;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;black;LockedToCube;False;Object;-1;Auto;Cube;6;0;SAMPLER2D;;False;1;FLOAT3;0,0,0;False;2;FLOAT;0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DotProductOpNode;988;3072,-128;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;877;5504,-1728;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;288;1280,-3712;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;115;4544,1520;Inherit;False;2;2;0;FLOAT3x3;0,0,0,1,0,0,1,0,1;False;1;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.TFHCRemapNode;989;1280,-512;Inherit;False;5;0;FLOAT2;0,0;False;1;FLOAT2;-1,0;False;2;FLOAT2;1,0;False;3;FLOAT2;0,0;False;4;FLOAT2;1,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SaturateNode;517;-64,-2560;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1094;4224,1808;Half;False;Constant;_Float32;Float 32;54;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1056;2816,896;Inherit;False;1073;GlobalMoonDirection;1;0;OBJECT;;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;629;5760,-1600;Half;False;Property;_StarsIntensity;Stars Intensity;16;0;Create;True;0;0;False;0;2;0;0;5;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;940;1568,-464;Float;False;Property;_ENABLESUN_ON;Enable Sun;24;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1027;False;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1042;1024,640;Half;False;Constant;_Float24;Float 24;47;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;513;256,-2560;Half;False;PATTERN;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1096;4224,1968;Half;False;Constant;_Float34;Float 34;54;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1166;4704,1520;Float;False;Property;_ENABLECLOUDSROTATION_ON;Enable Clouds Rotation;42;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1164;False;9;1;FLOAT3;0,0,0;False;0;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT3;0,0,0;False;4;FLOAT3;0,0,0;False;5;FLOAT3;0,0,0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1044;1024,800;Half;False;Constant;_Float25;Float 25;47;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;218;1536,-3712;Half;False;SKY;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1075;1024,512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;5;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RangedFloatNode;1041;1024,720;Half;False;Constant;_Float23;Float 23;47;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1176;2816,1024;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.StaticSwitch;878;5760,-1792;Float;False;Property;_EnableStarsTwinkling;Enable Stars Twinkling;19;0;Create;True;0;0;False;1;Space(10);0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ComponentMaskNode;126;4224,1712;Inherit;False;True;True;True;False;1;0;COLOR;0,0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1095;4224,1888;Half;False;Constant;_Float33;Float 33;54;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;994;3232,-128;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;993;1808,-448;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.NormalizeNode;1163;5056,1520;Inherit;False;1;0;FLOAT3;0,0,0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;882;6144,-1792;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;1054;3072,896;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCRemapNode;1074;1280,512;Inherit;False;5;0;FLOAT2;0,0;False;1;FLOAT2;-1,0;False;2;FLOAT2;1,0;False;3;FLOAT2;0,0;False;4;FLOAT2;1,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.StaticSwitch;1027;3424,-80;Float;False;Property;_ENABLESUN_ON;Enable Sun;24;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;919;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;516;-896,-5376;Inherit;False;513;PATTERN;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;220;-896,-5056;Inherit;False;218;SKY;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;116;4544,1744;Inherit;False;5;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;1,1,1;False;3;FLOAT3;-1,-1,-1;False;4;FLOAT3;1,1,1;False;1;FLOAT3;0
Node;AmplifyShaderEditor.RangedFloatNode;1098;5344,1664;Half;False;Constant;_Float36;Float 36;54;0;Create;True;0;0;False;0;-1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;996;2176,-64;Half;False;Property;_SunIntensity;Sun Intensity;28;0;Create;True;0;0;False;0;1;0;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;998;2176,-256;Half;False;Property;_SunColor;Sun Color;25;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;1055;3232,896;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;630;6400,-1792;Half;False;STARS;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BlendOpsNode;574;-704,-5376;Inherit;False;Overlay;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;1;False;1;COLOR;0
Node;AmplifyShaderEditor.VertexToFragmentNode;997;3664,-64;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1057;1568,560;Float;False;Property;_ENABLEMOON_ON;Enable Moon;30;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1052;False;9;1;FLOAT2;0,0;False;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT2;0,0;False;6;FLOAT2;0,0;False;7;FLOAT2;0,0;False;8;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;995;2176,-512;Inherit;True;Property;_SunTexture;Sun Texture;26;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;1097;5344,1584;Half;False;Constant;_Float35;Float 35;54;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1099;5344,1744;Half;False;Constant;_Float37;Float 37;54;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.DotProductOpNode;89;5216,1520;Inherit;False;2;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;524;-384,-5376;Float;False;Property;_EnablePatternOverlay;Enable Pattern Overlay;8;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;1052;3424,944;Float;False;Property;_ENABLEMOON_ON;Enable Moon;30;0;Create;False;0;0;False;0;0;0;0;False;;Toggle;2;Key0;Key1;Reference;1113;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;632;-384,-5056;Inherit;False;630;STARS;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1001;4096,-416;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;397;3840,1840;Half;False;Clouds_G;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;1043;1808,576;Inherit;False;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1002;2560,-512;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;236;5600,1520;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;-1;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;399;5760,1856;Inherit;False;397;Clouds_G;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;1048;2176,768;Half;False;Property;_MoonColor;Moon Color;31;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;104;5792,1520;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1004;4352,-512;Half;False;SUN;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1045;2176,960;Half;False;Property;_MoonIntensity;Moon Intensity;34;0;Create;True;0;0;False;0;1;0;1;10;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;631;-48,-5248;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1003;4352,-256;Half;False;SUN_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.VertexToFragmentNode;1051;3664,960;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PosVertexDataNode;1179;-896,2560;Inherit;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1049;2176,512;Inherit;True;Property;_MoonTexture;Moon Texture;32;1;[NoScaleOffset];Create;True;0;0;False;0;-1;None;None;True;0;False;black;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.AbsOpNode;314;-512,2560;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1109;-512,2784;Half;False;Constant;_Float40;Float 40;55;0;Create;True;0;0;False;0;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;313;-896,2752;Half;False;Property;_FogHeight;Fog Height;47;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1111;128,-5056;Inherit;False;1003;SUN_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1053;4096,608;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;325;-896,2880;Half;False;Property;_FogSmoothness;Fog Smoothness;48;0;Create;True;0;0;False;0;0;0;0.01;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;1059;2560,512;Inherit;False;3;3;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1112;128,-5152;Inherit;False;1004;SUN;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;391;6016,1776;Float;False;Property;_CloudsLitbySun;Clouds Lit by Sun;41;0;Create;True;0;0;False;1;Space(10);0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;FLOAT;0;False;0;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT;0;False;7;FLOAT;0;False;8;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;918;128,-5376;Float;False;Property;_EnableStars;Enable Stars;12;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1108;-512,2688;Half;False;Constant;_Float39;Float 39;55;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;1077;4352,512;Half;False;MOON;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.TFHCRemapNode;315;-320,2560;Inherit;False;5;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;3;FLOAT;0;False;4;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;176;448,-5248;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.ColorNode;232;6400,1536;Half;False;Property;_CloudsShadowColor;Clouds Shadow Color;38;0;Create;True;0;0;False;0;0.4980392,0.7450981,1,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RelayNode;407;6480,1872;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;261;6400,1696;Half;False;Property;_CloudsLightColor;Clouds Light Color;37;0;Create;True;0;0;False;0;1,1,1,1;0,0,0,0;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RegisterLocalVarNode;1078;4352,768;Half;False;MOON_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;329;-256,2880;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;677;-64,2560;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;919;640,-5376;Float;False;Property;_EnableSun;Enable Sun;24;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;101;6784,1536;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;1116;640,-5056;Inherit;False;1078;MOON_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;1115;640,-5152;Inherit;False;1077;MOON;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.SaturateNode;316;128,2560;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;222;7232,1520;Half;False;CLOUDS;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;223;3840,1968;Half;False;CLOUDS_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1110;128,2752;Half;False;Constant;_Float41;Float 41;55;0;Create;True;0;0;False;0;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;1114;960,-5248;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;679;128,2880;Half;False;Property;_FogFill;Fog Fill;49;0;Create;True;0;0;False;0;0;0;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;1113;1152,-5376;Float;False;Property;_EnableMoon;Enable Moon;30;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;229;1152,-5056;Inherit;False;223;CLOUDS_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;228;1152,-5152;Inherit;False;222;CLOUDS;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;678;384,2560;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;227;1472,-5248;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;359;768,2560;Half;False;FOG_MASK;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;436;1664,-5056;Inherit;False;359;FOG_MASK;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.FogAndAmbientColorsNode;312;1664,-5152;Inherit;False;unity_FogColor;0;1;COLOR;0
Node;AmplifyShaderEditor.StaticSwitch;1120;1664,-5376;Float;False;Property;_EnableClouds;Enable Clouds;36;0;Create;True;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;317;2048,-5248;Inherit;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1183;128,-6144;Half;False;Property;_CloudsCat1;[ Clouds Cat ];35;0;Create;True;0;0;True;1;StyledCategory(Clouds);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1182;-736,-6144;Half;False;Property;_SkyCat1;[ Sky Cat ];1;0;Create;True;0;0;True;1;StyledCategory(Sky);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1186;-400,-6144;Half;False;Property;_StarsCat1;[ Stars Cat ];11;0;Create;True;0;0;True;1;StyledCategory(Stars);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1184;-896,-6144;Half;False;Property;_TITLE1;< TITLE >;0;0;Create;True;0;0;True;1;StyledBanner(Polyverse Skies Standard);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1185;-224,-6144;Half;False;Property;_SunCat1;[ Sun Cat ];23;0;Create;True;0;0;True;1;StyledCategory(Sun);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1181;320,-6144;Half;False;Property;_FogCat1;[ Fog Cat ];45;0;Create;True;0;0;True;1;StyledCategory(Fog);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.StaticSwitch;921;2304,-5376;Float;False;Property;_EnableBuiltinFog;Enable Fog;46;0;Create;False;0;0;False;0;0;0;0;True;;Toggle;2;Key0;Key1;Create;False;9;1;COLOR;0,0,0,0;False;0;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;3;COLOR;0,0,0,0;False;4;COLOR;0,0,0,0;False;5;COLOR;0,0,0,0;False;6;COLOR;0,0,0,0;False;7;COLOR;0,0,0,0;False;8;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;1188;-576,-6144;Half;False;Property;_PatternCat;[ Pattern Cat ];7;0;Create;True;0;0;True;1;StyledCategory(Pattern);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;1189;-64,-6144;Half;False;Property;_MoonCat;[ Moon Cat ];29;0;Create;True;0;0;True;1;StyledCategory(Moon);1;1;1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1180;2816,-5376;Float;False;True;-1;2;;100;1;BOXOPHOBIC/Polyverse Skies/Standard;0770190933193b94aaa3065e307002fa;True;Unlit;0;0;Unlit;2;True;0;1;False;-1;0;False;-1;0;1;False;-1;0;False;-1;True;0;False;-1;0;False;-1;True;False;True;2;False;-1;True;True;True;True;True;0;False;-1;True;False;255;False;-1;255;False;-1;255;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;True;2;False;-1;True;0;False;-1;True;False;0;False;-1;0;False;-1;True;4;RenderType=Background=RenderType;Queue=Background=Queue=0;PreviewType=Skybox;IgnoreProjector=True;True;2;0;False;False;False;False;False;False;False;False;False;True;1;LightMode=ForwardBase;False;0;;0;0;Standard;1;Vertex Position,InvertActionOnDeselection;1;0;1;True;False;;0
Node;AmplifyShaderEditor.CommentaryNode;952;1408,-1280;Inherit;False;1793.068;100;Stars Horizon Height Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;480;640,-3840;Inherit;False;770.3374;100;Color Gradient Colors;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1006;-896,-640;Inherit;False;2942.402;100;Calculate Sun Position;0;SUN;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1162;4224,1408;Inherit;False;1697.376;100;Light Calculation on Clouds;0;;1,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1031;2176,384;Inherit;False;509.1301;100;Moon Texture, Color and Intensity;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;955;3456,-1920;Inherit;False;2815.233;100;Stars Twinkling ;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;949;1408,-1920;Inherit;False;1751.366;100;Stars Sun Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;914;6400,1408;Inherit;False;522.4746;100;Cloud Colors;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;942;-896,-1920;Inherit;False;2123.633;100;Stars Cubemap Coords and Cubemap RGB Layer;0;STARS;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;639;-896,-2688;Inherit;False;1047.013;100;Pattern Overlay Texture and Contrast;0;PATTERN OVERLAY;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;991;2176,-640;Inherit;False;512.5464;100;Sun Texture, Color and Intensity;0;;0,0.4980392,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1030;-896,384;Inherit;False;2948.242;100;Calculate Moon Position;0;MOON;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;700;-896,2432;Inherit;False;1411.941;100;Fog Coords on Screen;0;BUILT-IN FOG;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;431;-896,-4608;Inherit;False;770.2415;100;Switch between Perspective / Orthographic camera;0;CAMERA MODE;1,0,1,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1026;2816,-256;Inherit;False;1029.934;100;Direction Negative Z Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1035;2816,768;Inherit;False;1054.961;100;Direction Negative Z Mask;0;;1,0,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1136;-896,1408;Inherit;False;3008.206;100;Clouds Coordinates;0;CLOUDS;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1187;-896,-6272;Inherit;False;1350.748;100;Drawers;0;;1,0.252,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1117;-896,-5504;Inherit;False;3457.506;100;;0;FINAL COLOR;0.4980392,1,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;479;-896,-3840;Inherit;False;1405.403;100;Color Gradient Calculation;0;SKY;0,0.4980392,0,1;0;0
Node;AmplifyShaderEditor.CommentaryNode;1091;2688,1408;Inherit;False;1347.811;100;Clouds Cubemap;0;;0,0.4980392,1,1;0;0
WireConnection;309;0;267;2
WireConnection;309;1;267;1
WireConnection;255;0;701;0
WireConnection;255;1;260;0
WireConnection;268;0;1007;0
WireConnection;268;1;309;0
WireConnection;268;2;267;4
WireConnection;276;0;48;0
WireConnection;276;1;255;0
WireConnection;300;0;268;0
WireConnection;47;0;276;0
WireConnection;62;0;47;0
WireConnection;673;0;1171;2
WireConnection;673;1;674;0
WireConnection;59;0;62;0
WireConnection;675;0;1171;1
WireConnection;675;1;673;0
WireConnection;675;2;1171;3
WireConnection;1005;0;938;0
WireConnection;61;0;62;0
WireConnection;365;0;62;0
WireConnection;60;0;59;0
WireConnection;60;1;1080;0
WireConnection;55;0;62;0
WireConnection;58;0;61;0
WireConnection;58;1;1081;0
WireConnection;58;2;365;0
WireConnection;266;0;1081;0
WireConnection;266;1;1177;0
WireConnection;266;2;1081;0
WireConnection;738;0;733;0
WireConnection;941;0;675;0
WireConnection;56;0;55;0
WireConnection;56;1;1081;0
WireConnection;56;2;60;0
WireConnection;962;0;938;0
WireConnection;962;1;961;0
WireConnection;1123;0;266;0
WireConnection;1124;0;58;0
WireConnection;1122;0;56;0
WireConnection;740;0;738;0
WireConnection;732;0;1148;0
WireConnection;732;1;731;0
WireConnection;537;0;528;0
WireConnection;537;1;1010;0
WireConnection;537;2;1011;0
WireConnection;537;3;1011;0
WireConnection;537;4;1010;0
WireConnection;964;0;1005;0
WireConnection;964;1;962;0
WireConnection;763;0;941;0
WireConnection;828;0;1172;2
WireConnection;967;0;962;0
WireConnection;568;0;567;1
WireConnection;568;1;567;2
WireConnection;1128;0;1178;2
WireConnection;1128;1;1127;0
WireConnection;774;0;1122;0
WireConnection;775;0;1123;0
WireConnection;533;0;537;0
WireConnection;533;1;1012;0
WireConnection;776;0;1124;0
WireConnection;1073;0;1061;0
WireConnection;832;0;828;0
WireConnection;832;1;1017;0
WireConnection;832;2;831;0
WireConnection;832;3;1167;0
WireConnection;832;4;1018;0
WireConnection;726;0;732;0
WireConnection;726;1;1014;0
WireConnection;726;2;740;0
WireConnection;726;3;1015;0
WireConnection;726;4;1016;0
WireConnection;534;0;537;0
WireConnection;534;1;1013;0
WireConnection;1058;0;1061;0
WireConnection;1058;1;1062;0
WireConnection;965;0;964;0
WireConnection;564;1;763;0
WireConnection;1038;0;1073;0
WireConnection;1038;1;1058;0
WireConnection;969;0;965;0
WireConnection;969;1;1173;0
WireConnection;569;0;568;0
WireConnection;569;2;546;0
WireConnection;569;1;633;0
WireConnection;530;0;564;3
WireConnection;530;1;534;0
WireConnection;724;0;726;0
WireConnection;1129;0;1178;1
WireConnection;1129;1;1128;0
WireConnection;1129;2;1178;3
WireConnection;529;0;564;2
WireConnection;529;1;533;0
WireConnection;968;0;967;0
WireConnection;968;1;1173;0
WireConnection;822;0;832;0
WireConnection;54;0;774;0
WireConnection;54;1;775;0
WireConnection;54;2;776;0
WireConnection;1064;0;1038;0
WireConnection;49;0;54;0
WireConnection;49;1;1129;0
WireConnection;527;0;564;1
WireConnection;527;1;529;0
WireConnection;527;2;530;0
WireConnection;1039;0;1058;0
WireConnection;970;0;968;0
WireConnection;970;1;969;0
WireConnection;947;0;724;0
WireConnection;953;0;822;0
WireConnection;956;0;569;0
WireConnection;287;0;1168;2
WireConnection;1036;0;1039;0
WireConnection;1036;1;1175;0
WireConnection;210;0;287;0
WireConnection;210;1;1008;0
WireConnection;210;2;212;0
WireConnection;210;3;1008;0
WireConnection;210;4;1009;0
WireConnection;1164;1;1129;0
WireConnection;1164;0;49;0
WireConnection;856;0;953;0
WireConnection;1067;0;1064;0
WireConnection;1067;1;1175;0
WireConnection;626;0;527;0
WireConnection;626;1;619;0
WireConnection;972;0;970;0
WireConnection;762;0;947;0
WireConnection;761;0;956;0
WireConnection;475;0;471;0
WireConnection;566;1;761;0
WireConnection;876;0;762;0
WireConnection;876;1;626;0
WireConnection;876;2;856;0
WireConnection;470;0;210;0
WireConnection;470;1;475;0
WireConnection;278;0;241;0
WireConnection;278;1;1090;0
WireConnection;1169;0;1168;2
WireConnection;976;0;972;1
WireConnection;976;1;971;0
WireConnection;245;0;1164;0
WireConnection;1066;0;1036;0
WireConnection;1066;1;1067;0
WireConnection;980;0;972;0
WireConnection;980;1;976;0
WireConnection;886;0;876;0
WireConnection;981;0;975;0
WireConnection;981;1;973;0
WireConnection;981;2;1019;0
WireConnection;981;3;1020;0
WireConnection;981;4;1021;0
WireConnection;254;1;1170;0
WireConnection;303;0;1169;0
WireConnection;554;0;566;1
WireConnection;554;1;555;0
WireConnection;1063;0;1066;0
WireConnection;244;0;245;1
WireConnection;244;1;278;0
WireConnection;208;0;470;0
WireConnection;246;0;245;0
WireConnection;246;1;244;0
WireConnection;246;2;245;2
WireConnection;985;0;980;0
WireConnection;985;1;981;0
WireConnection;1072;0;1063;1
WireConnection;1072;1;1065;0
WireConnection;881;0;886;0
WireConnection;417;0;208;0
WireConnection;180;0;194;0
WireConnection;180;1;182;0
WireConnection;180;2;303;0
WireConnection;519;1;254;0
WireConnection;519;0;283;0
WireConnection;756;0;554;0
WireConnection;1069;0;1063;0
WireConnection;1069;1;1072;0
WireConnection;1068;0;1046;0
WireConnection;1068;1;1071;0
WireConnection;1068;2;1047;0
WireConnection;1068;3;1070;0
WireConnection;1068;4;1040;0
WireConnection;41;1;246;0
WireConnection;988;0;1028;0
WireConnection;988;1;1174;0
WireConnection;877;0;756;0
WireConnection;877;1;881;0
WireConnection;288;0;181;0
WireConnection;288;1;180;0
WireConnection;288;2;417;0
WireConnection;115;0;54;0
WireConnection;115;1;1147;0
WireConnection;989;0;985;0
WireConnection;989;1;1022;0
WireConnection;989;2;1024;0
WireConnection;989;3;1023;0
WireConnection;989;4;1024;0
WireConnection;517;0;519;0
WireConnection;940;0;989;0
WireConnection;513;0;517;0
WireConnection;1166;1;1147;0
WireConnection;1166;0;115;0
WireConnection;218;0;288;0
WireConnection;1075;0;1069;0
WireConnection;1075;1;1068;0
WireConnection;878;1;881;0
WireConnection;878;0;877;0
WireConnection;126;0;41;0
WireConnection;994;0;988;0
WireConnection;993;0;940;0
WireConnection;1163;0;1166;0
WireConnection;882;0;878;0
WireConnection;882;1;629;0
WireConnection;1054;0;1056;0
WireConnection;1054;1;1176;0
WireConnection;1074;0;1075;0
WireConnection;1074;1;1042;0
WireConnection;1074;2;1044;0
WireConnection;1074;3;1041;0
WireConnection;1074;4;1044;0
WireConnection;1027;0;994;0
WireConnection;116;0;126;0
WireConnection;116;1;1094;0
WireConnection;116;2;1096;0
WireConnection;116;3;1095;0
WireConnection;116;4;1096;0
WireConnection;1055;0;1054;0
WireConnection;630;0;882;0
WireConnection;574;0;516;0
WireConnection;574;1;220;0
WireConnection;997;0;1027;0
WireConnection;1057;0;1074;0
WireConnection;995;1;993;0
WireConnection;89;0;1163;0
WireConnection;89;1;116;0
WireConnection;524;1;220;0
WireConnection;524;0;574;0
WireConnection;1052;0;1055;0
WireConnection;1001;0;995;4
WireConnection;1001;1;997;0
WireConnection;397;0;41;2
WireConnection;1043;0;1057;0
WireConnection;1002;0;995;1
WireConnection;1002;1;998;0
WireConnection;1002;2;996;0
WireConnection;236;0;89;0
WireConnection;236;1;1098;0
WireConnection;236;2;1099;0
WireConnection;236;3;1097;0
WireConnection;236;4;1099;0
WireConnection;104;0;236;0
WireConnection;1004;0;1002;0
WireConnection;631;0;524;0
WireConnection;631;1;632;0
WireConnection;1003;0;1001;0
WireConnection;1051;0;1052;0
WireConnection;1049;1;1043;0
WireConnection;314;0;1179;2
WireConnection;1053;0;1049;4
WireConnection;1053;1;1051;0
WireConnection;1059;0;1049;1
WireConnection;1059;1;1048;0
WireConnection;1059;2;1045;0
WireConnection;391;1;399;0
WireConnection;391;0;104;0
WireConnection;918;1;524;0
WireConnection;918;0;631;0
WireConnection;1077;0;1059;0
WireConnection;315;0;314;0
WireConnection;315;1;1108;0
WireConnection;315;2;313;0
WireConnection;315;3;1108;0
WireConnection;315;4;1109;0
WireConnection;176;0;918;0
WireConnection;176;1;1112;0
WireConnection;176;2;1111;0
WireConnection;407;0;391;0
WireConnection;1078;0;1053;0
WireConnection;329;0;325;0
WireConnection;677;0;315;0
WireConnection;677;1;329;0
WireConnection;919;1;918;0
WireConnection;919;0;176;0
WireConnection;101;0;232;0
WireConnection;101;1;261;0
WireConnection;101;2;407;0
WireConnection;316;0;677;0
WireConnection;222;0;101;0
WireConnection;223;0;41;4
WireConnection;1114;0;919;0
WireConnection;1114;1;1115;0
WireConnection;1114;2;1116;0
WireConnection;1113;1;919;0
WireConnection;1113;0;1114;0
WireConnection;678;0;316;0
WireConnection;678;1;1110;0
WireConnection;678;2;679;0
WireConnection;227;0;1113;0
WireConnection;227;1;228;0
WireConnection;227;2;229;0
WireConnection;359;0;678;0
WireConnection;1120;1;1113;0
WireConnection;1120;0;227;0
WireConnection;317;0;312;0
WireConnection;317;1;1120;0
WireConnection;317;2;436;0
WireConnection;921;1;1120;0
WireConnection;921;0;317;0
WireConnection;1180;0;921;0
ASEEND*/
//CHKSM=D9E0226F77384F90E9C324600828BDC3F3886573