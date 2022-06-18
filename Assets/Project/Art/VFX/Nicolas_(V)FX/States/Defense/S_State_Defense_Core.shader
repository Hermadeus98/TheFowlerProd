// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "S_State_Defense_Core"
{
	Properties
	{
		[HideInInspector] _EmissionColor("Emission Color", Color) = (1,1,1,1)
		[HideInInspector] _AlphaCutoff("Alpha Cutoff ", Range(0, 1)) = 0.5
		[ASEBegin][Header(Panning Texture)]_PanningTex("PanningTex", 2D) = "white" {}
		[Toggle]_PanningTex_InvertUV("PanningTex_InvertUV", Float) = 0
		_PanningTex_ManualOffset("PanningTex_ManualOffset", Float) = 0
		[Header(TimeStep)]_TimeStep("TimeStep", Float) = 60
		[Header(Flicker)]_Flicker_Speed("Flicker_Speed", Float) = 1
		_Flicker_Offset("Flicker_Offset", Float) = 1
		_Flicker_Min("Flicker_Min", Float) = 1
		_Flicker_Max("Flicker_Max", Float) = 2
		_Displacement("Displacement", Float) = 0.05
		_Shine_Threshold("Shine_Threshold", Float) = 0
		_Shine_Smoothness("Shine_Smoothness", Float) = 0.5
		_Refraction("Refraction", Float) = 0
		[HDR]_ShineColor("ShineColor", Color) = (0.1568627,1,0.8610147,0)
		[Toggle][Header(Fresnel)]_Fresnel_Enable("Fresnel_Enable", Float) = 0
		_FresnelScale("FresnelScale", Float) = 1
		_FresnelPower("FresnelPower", Float) = 3
		_FresnelThreshold("FresnelThreshold", Float) = 0
		_FresnelSmoothess("FresnelSmoothess", Float) = 1
		[HDR][Header(Bicolor)][HideIf(_NOBICOLOR_ON)]_ColorA("ColorA", Color) = (0,0,0,1)
		[HDR][HideIf(_NOBICOLOR_ON)]_ColorB("ColorB", Color) = (1,1,1,1)
		[HideIf(_NOBICOLOR_ON)]_BicolorThreshold("BicolorThreshold", Float) = 0
		[HideIf(_NOBICOLOR_ON)]_BicolorSmoothness("BicolorSmoothness", Float) = 1
		[Toggle]_Bicolor_OneMinus("Bicolor_OneMinus", Float) = 0
		[Toggle][Header(EdgeFade)]_EdgeFade_Enable("EdgeFade_Enable", Float) = 1
		_EdgeFadeU("EdgeFadeU", Vector) = (0,0,0,0)
		_EdgeFadeV("EdgeFadeV", Vector) = (0,0,0,0)
		_AddedEdgeFade("AddedEdgeFade", Float) = 0
		[Header(DepthFade)]_DF_Distance("DF_Distance", Float) = 0.5
		[Toggle]_DF_OneMinus("DF_OneMinus", Float) = 0
		[Header(Cracks)]_Cracks_Emission("Cracks_Emission", Float) = 0
		[Header(Alpha Sharp)]_AlphaThreshold("AlphaThreshold", Float) = 0
		_AlphaSmoothness("AlphaSmoothness", Float) = 1
		[ASEEnd][Toggle]_AlphaSharp_OneMinus("AlphaSharp_OneMinus", Float) = 0

		[HideInInspector]_RenderQueueType("Render Queue Type", Float) = 4
		[HideInInspector][ToggleUI]_AddPrecomputedVelocity("Add Precomputed Velocity", Float) = 1
		//[HideInInspector]_ShadowMatteFilter("Shadow Matte Filter", Float) = 2
		[HideInInspector]_StencilRef("Stencil Ref", Int) = 0
		[HideInInspector]_StencilWriteMask("StencilWrite Mask", Int) = 6
		[HideInInspector]_StencilRefDepth("StencilRefDepth", Int) = 0
		[HideInInspector]_StencilWriteMaskDepth("_StencilWriteMaskDepth", Int) = 8
		[HideInInspector]_StencilRefMV("_StencilRefMV", Int) = 32
		[HideInInspector]_StencilWriteMaskMV("_StencilWriteMaskMV", Int) = 40
		[HideInInspector]_StencilRefDistortionVec("_StencilRefDistortionVec", Int) = 4
		[HideInInspector]_StencilWriteMaskDistortionVec("_StencilWriteMaskDistortionVec", Int) = 4
		[HideInInspector]_StencilWriteMaskGBuffer("_StencilWriteMaskGBuffer", Int) = 14
		[HideInInspector]_StencilRefGBuffer("_StencilRefGBuffer", Int) = 2
		[HideInInspector]_ZTestGBuffer("_ZTestGBuffer", Int) = 4
		[HideInInspector][ToggleUI]_RequireSplitLighting("_RequireSplitLighting", Float) = 0
		[HideInInspector][ToggleUI]_ReceivesSSR("_ReceivesSSR", Float) = 0
		[HideInInspector]_SurfaceType("_SurfaceType", Float) = 1
		[HideInInspector]_BlendMode("_BlendMode", Float) = 0
		[HideInInspector]_SrcBlend("_SrcBlend", Float) = 1
		[HideInInspector]_DstBlend("_DstBlend", Float) = 0
		[HideInInspector]_AlphaSrcBlend("Vec_AlphaSrcBlendtor1", Float) = 1
		[HideInInspector]_AlphaDstBlend("_AlphaDstBlend", Float) = 0
		[HideInInspector][ToggleUI]_ZWrite("_ZWrite", Float) = 0
		[HideInInspector][ToggleUI]_TransparentZWrite("_TransparentZWrite", Float) = 1
		[HideInInspector]_CullMode("Cull Mode", Float) = 2
		[HideInInspector]_TransparentSortPriority("_TransparentSortPriority", Int) = 0
		[HideInInspector][ToggleUI]_EnableFogOnTransparent("_EnableFogOnTransparent", Float) = 1
		[HideInInspector]_CullModeForward("_CullModeForward", Float) = 2
		[HideInInspector][Enum(Front, 1, Back, 2)]_TransparentCullMode("_TransparentCullMode", Float) = 2
		[HideInInspector]_ZTestDepthEqualForOpaque("_ZTestDepthEqualForOpaque", Int) = 4
		[HideInInspector][Enum(UnityEngine.Rendering.CompareFunction)]_ZTestTransparent("_ZTestTransparent", Float) = 4
		[HideInInspector][ToggleUI]_TransparentBackfaceEnable("_TransparentBackfaceEnable", Float) = 0
		[HideInInspector][ToggleUI]_AlphaCutoffEnable("_AlphaCutoffEnable", Float) = 0
		[HideInInspector][ToggleUI]_UseShadowThreshold("_UseShadowThreshold", Float) = 0
		[HideInInspector][ToggleUI]_DoubleSidedEnable("_DoubleSidedEnable", Float) = 0
		[HideInInspector][Enum(Flip, 0, Mirror, 1, None, 2)]_DoubleSidedNormalMode("_DoubleSidedNormalMode", Float) = 2
		[HideInInspector]_DoubleSidedConstants("_DoubleSidedConstants", Vector) = (1, 1, -1, 0)
		[HideInInspector]_DistortionEnable("_DistortionEnable",Float) = 0
		[HideInInspector]_DistortionOnly("_DistortionOnly",Float) = 0
		//_TessPhongStrength( "Tess Phong Strength", Range( 0, 1 ) ) = 0.5
		//_TessValue( "Tess Max Tessellation", Range( 1, 32 ) ) = 16
		//_TessMin( "Tess Min Distance", Float ) = 10
		//_TessMax( "Tess Max Distance", Float ) = 25
		//_TessEdgeLength ( "Tess Edge length", Range( 2, 50 ) ) = 16
		//_TessMaxDisp( "Tess Max Displacement", Float ) = 25
	}

	SubShader
	{
		LOD 0

		

		Tags { "RenderPipeline"="HDRenderPipeline" "RenderType"="Opaque" "Queue"="Transparent-250" }

		HLSLINCLUDE
		#pragma target 4.5
		#pragma only_renderers d3d11 ps4 xboxone vulkan metal switch
		#pragma instancing_options renderinglayer

		#ifndef ASE_TESS_FUNCS
		#define ASE_TESS_FUNCS
		float4 FixedTess( float tessValue )
		{
			return tessValue;
		}
		
		float CalcDistanceTessFactor (float4 vertex, float minDist, float maxDist, float tess, float4x4 o2w, float3 cameraPos )
		{
			float3 wpos = mul(o2w,vertex).xyz;
			float dist = distance (wpos, cameraPos);
			float f = clamp(1.0 - (dist - minDist) / (maxDist - minDist), 0.01, 1.0) * tess;
			return f;
		}

		float4 CalcTriEdgeTessFactors (float3 triVertexFactors)
		{
			float4 tess;
			tess.x = 0.5 * (triVertexFactors.y + triVertexFactors.z);
			tess.y = 0.5 * (triVertexFactors.x + triVertexFactors.z);
			tess.z = 0.5 * (triVertexFactors.x + triVertexFactors.y);
			tess.w = (triVertexFactors.x + triVertexFactors.y + triVertexFactors.z) / 3.0f;
			return tess;
		}

		float CalcEdgeTessFactor (float3 wpos0, float3 wpos1, float edgeLen, float3 cameraPos, float4 scParams )
		{
			float dist = distance (0.5 * (wpos0+wpos1), cameraPos);
			float len = distance(wpos0, wpos1);
			float f = max(len * scParams.y / (edgeLen * dist), 1.0);
			return f;
		}

		float DistanceFromPlaneASE (float3 pos, float4 plane)
		{
			return dot (float4(pos,1.0f), plane);
		}

		bool WorldViewFrustumCull (float3 wpos0, float3 wpos1, float3 wpos2, float cullEps, float4 planes[6] )
		{
			float4 planeTest;
			planeTest.x = (( DistanceFromPlaneASE(wpos0, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[0]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[0]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.y = (( DistanceFromPlaneASE(wpos0, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[1]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[1]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.z = (( DistanceFromPlaneASE(wpos0, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[2]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[2]) > -cullEps) ? 1.0f : 0.0f );
			planeTest.w = (( DistanceFromPlaneASE(wpos0, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos1, planes[3]) > -cullEps) ? 1.0f : 0.0f ) +
						  (( DistanceFromPlaneASE(wpos2, planes[3]) > -cullEps) ? 1.0f : 0.0f );
			return !all (planeTest);
		}

		float4 DistanceBasedTess( float4 v0, float4 v1, float4 v2, float tess, float minDist, float maxDist, float4x4 o2w, float3 cameraPos )
		{
			float3 f;
			f.x = CalcDistanceTessFactor (v0,minDist,maxDist,tess,o2w,cameraPos);
			f.y = CalcDistanceTessFactor (v1,minDist,maxDist,tess,o2w,cameraPos);
			f.z = CalcDistanceTessFactor (v2,minDist,maxDist,tess,o2w,cameraPos);

			return CalcTriEdgeTessFactors (f);
		}

		float4 EdgeLengthBasedTess( float4 v0, float4 v1, float4 v2, float edgeLength, float4x4 o2w, float3 cameraPos, float4 scParams )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;
			tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
			tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
			tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
			tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			return tess;
		}

		float4 EdgeLengthBasedTessCull( float4 v0, float4 v1, float4 v2, float edgeLength, float maxDisplacement, float4x4 o2w, float3 cameraPos, float4 scParams, float4 planes[6] )
		{
			float3 pos0 = mul(o2w,v0).xyz;
			float3 pos1 = mul(o2w,v1).xyz;
			float3 pos2 = mul(o2w,v2).xyz;
			float4 tess;

			if (WorldViewFrustumCull(pos0, pos1, pos2, maxDisplacement, planes))
			{
				tess = 0.0f;
			}
			else
			{
				tess.x = CalcEdgeTessFactor (pos1, pos2, edgeLength, cameraPos, scParams);
				tess.y = CalcEdgeTessFactor (pos2, pos0, edgeLength, cameraPos, scParams);
				tess.z = CalcEdgeTessFactor (pos0, pos1, edgeLength, cameraPos, scParams);
				tess.w = (tess.x + tess.y + tess.z) / 3.0f;
			}
			return tess;
		}
		#endif //ASE_TESS_FUNCS

		ENDHLSL

		
		Pass
		{
			
			Name "Forward Unlit"
			Tags { "LightMode"="ForwardOnly" }

			Blend [_SrcBlend] [_DstBlend], [_AlphaSrcBlend] [_AlphaDstBlend]
			Cull [_CullMode]
			ZTest [_ZTestTransparent]
			ZWrite [_ZWrite]

			Stencil
			{
				Ref [_StencilRef]
				WriteMask [_StencilWriteMask]
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define HAVE_MESH_MODIFICATION 1
			#define ASE_SRP_VERSION 999999
			#define REQUIRE_OPAQUE_TEXTURE 1

			#define SHADERPASS SHADERPASS_FORWARD_UNLIT
			#pragma multi_compile _ DEBUG_DISPLAY

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl"

			#if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
				#define LIGHTLOOP_DISABLE_TILE_AND_CLUSTER
				#define HAS_LIGHTLOOP
				#define SHADOW_OPTIMIZE_REGISTER_USAGE 1

				#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/CommonLighting.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/Shadow/HDShadowContext.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/HDShadow.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/LightLoopDef.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/PunctualLightCommon.hlsl"
				#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Lighting/LightLoop/HDShadowLoop.hlsl"
			#endif



			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_VERT_POSITION
			#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float3 positionRWS : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _EdgeFadeV;
			float4 _EdgeFadeU;
			float4 _ShineColor;
			float4 _PanningTex_ST;
			float4 _ColorB;
			float4 _ColorA;
			float _DF_OneMinus;
			float _Shine_Threshold;
			float _Shine_Smoothness;
			float _FresnelThreshold;
			float _FresnelSmoothess;
			float _FresnelScale;
			float _FresnelPower;
			float _Fresnel_Enable;
			float _Cracks_Emission;
			float _AlphaThreshold;
			float _DF_Distance;
			float _Flicker_Min;
			float _AddedEdgeFade;
			float _AlphaSmoothness;
			float _Refraction;
			float _Bicolor_OneMinus;
			float _BicolorSmoothness;
			float _BicolorThreshold;
			float _Displacement;
			float _Flicker_Offset;
			float _TimeStep;
			float _PanningTex_InvertUV;
			float _PanningTex_ManualOffset;
			float _Flicker_Speed;
			float _Flicker_Max;
			float _EdgeFade_Enable;
			float _AlphaSharp_OneMinus;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _PanningTex;
			SAMPLER(sampler_PanningTex);


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			float4 ASEHDSampleSceneColor(float2 uv, float lod, float exposureMultiplier)
			{
				#if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT)
				return float4( SampleCameraColor(uv, lod) * exposureMultiplier, 1.0 );
				#endif
				return float4(0.0, 0.0, 0.0, 1.0);
			}
			

			struct SurfaceDescription
			{
				float3 Color;
				float3 Emission;
				float4 ShadowTint;
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
				surfaceData.color = surfaceDescription.Color;
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription , FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);

				#if defined(_ENABLE_SHADOW_MATTE) && SHADERPASS == SHADERPASS_FORWARD_UNLIT
					HDShadowContext shadowContext = InitShadowContext();
					float shadow;
					float3 shadow3;
					posInput = GetPositionInput(fragInputs.positionSS.xy, _ScreenSize.zw, fragInputs.positionSS.z, UNITY_MATRIX_I_VP, UNITY_MATRIX_V);
					float3 normalWS = normalize(fragInputs.tangentToWorld[1]);
					uint renderingLayers = _EnableLightLayers ? asuint(unity_RenderingLayer.x) : DEFAULT_LIGHT_LAYERS;
					ShadowLoopMin(shadowContext, posInput, normalWS, asuint(_ShadowMatteFilter), renderingLayers, shadow3);
					shadow = dot(shadow3, float3(1.0f/3.0f, 1.0f/3.0f, 1.0f/3.0f));

					float4 shadowColor = (1 - shadow)*surfaceDescription.ShadowTint.rgba;
					float  localAlpha  = saturate(shadowColor.a + surfaceDescription.Alpha);

					#ifdef _SURFACE_TYPE_TRANSPARENT
						surfaceData.color = lerp(shadowColor.rgb*surfaceData.color, lerp(lerp(shadowColor.rgb, surfaceData.color, 1 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow), surfaceDescription.Alpha);
					#else
						surfaceData.color = lerp(lerp(shadowColor.rgb, surfaceData.color, 1 - surfaceDescription.ShadowTint.a), surfaceData.color, shadow);
					#endif
					localAlpha = ApplyBlendMode(surfaceData.color, localAlpha).a;
					surfaceDescription.Alpha = localAlpha;
				#endif

				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity = surfaceDescription.Alpha;
				builtinData.emissiveColor = surfaceDescription.Emission;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = inputMesh.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2Dlod( _PanningTex, float4( ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), 0, 0.0) );
				float4 break22 = temp_output_7_0;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float4 ase_clipPos = TransformWorldToHClip( TransformObjectToWorld(inputMesh.positionOS));
				float4 screenPos = ComputeScreenPos( ase_clipPos , _ProjectionParams.x );
				o.ase_texcoord2 = screenPos;
				float3 vertexPos1_g55 = inputMesh.positionOS;
				float4 ase_clipPos1_g55 = TransformWorldToHClip( TransformObjectToWorld(vertexPos1_g55));
				float4 screenPos1_g55 = ComputeScreenPos( ase_clipPos1_g55 , _ProjectionParams.x );
				o.ase_texcoord3 = screenPos1_g55;
				float3 ase_worldNormal = TransformObjectToWorldNormal(inputMesh.normalOS);
				o.ase_texcoord4.xyz = ase_worldNormal;
				
				o.ase_texcoord1.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord1.zw = 0;
				o.ase_texcoord4.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = ( inputMesh.normalOS * temp_output_69_0 * _Displacement );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS = inputMesh.normalOS;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				o.positionRWS = positionRWS;
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			float4 Frag( VertexOutput packedInput ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.tangentToWorld = k_identity3x3;
				float3 positionRWS = packedInput.positionRWS;

				input.positionSS = packedInput.positionCS;
				input.positionRWS = positionRWS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = GetWorldSpaceNormalizeViewDir( input.positionRWS );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = packedInput.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), ddx( texCoord9_g43 ), ddy( texCoord9_g43 ) );
				float4 break22 = temp_output_7_0;
				float smoothstepResult5_g54 = smoothstep( _BicolorThreshold , ( _BicolorThreshold + _BicolorSmoothness ) , break22.r);
				float lerpResult12_g54 = lerp( smoothstepResult5_g54 , ( 1.0 - smoothstepResult5_g54 ) , _Bicolor_OneMinus);
				float4 lerpResult4_g54 = lerp( _ColorA , _ColorB , lerpResult12_g54);
				float4 screenPos = packedInput.ase_texcoord2;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 texCoord7_g49 = packedInput.ase_texcoord1.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break9_g49 = frac( texCoord7_g49 );
				float smoothstepResult3_g49 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g49.x);
				float smoothstepResult4_g49 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.x ));
				float smoothstepResult5_g49 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g49.y);
				float smoothstepResult6_g49 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.y ));
				float lerpResult14_g49 = lerp( 1.0 , ( smoothstepResult3_g49 * smoothstepResult4_g49 * smoothstepResult5_g49 * smoothstepResult6_g49 ) , _EdgeFade_Enable);
				float temp_output_12_0 = lerpResult14_g49;
				float4 screenPos1_g55 = packedInput.ase_texcoord3;
				float4 ase_screenPosNorm1 = screenPos1_g55 / screenPos1_g55.w;
				ase_screenPosNorm1.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm1.z : ase_screenPosNorm1.z * 0.5 + 0.5;
				float screenDepth1_g55 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm1.xy ),_ZBufferParams);
				float distanceDepth1_g55 = saturate( abs( ( screenDepth1_g55 - LinearEyeDepth( ase_screenPosNorm1.z,_ZBufferParams ) ) / ( _DF_Distance ) ) );
				float lerpResult3_g55 = lerp( distanceDepth1_g55 , ( 1.0 - distanceDepth1_g55 ) , _DF_OneMinus);
				float4 fetchOpaqueVal29 = ASEHDSampleSceneColor(( ase_screenPosNorm + ( temp_output_7_0 * _Refraction * temp_output_12_0 * lerpResult3_g55 ) ).xy, 0.0, GetInverseCurrentExposureMultiplier());
				float3 ase_worldNormal = packedInput.ase_texcoord4.xyz;
				float clampResult11_g48 = clamp( _FresnelPower , 0.0 , 50.0 );
				float fresnelNdotV1_g48 = dot( ase_worldNormal, V );
				float fresnelNode1_g48 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV1_g48, clampResult11_g48 ) );
				float smoothstepResult4_g48 = smoothstep( _FresnelThreshold , ( _FresnelThreshold + _FresnelSmoothess ) , fresnelNode1_g48);
				float lerpResult9_g48 = lerp( 1.0 , smoothstepResult4_g48 , _Fresnel_Enable);
				float temp_output_8_0 = lerpResult9_g48;
				float lerpResult35 = lerp( break22.r , temp_output_8_0 , 0.8);
				float smoothstepResult20 = smoothstep( _Shine_Threshold , ( _Shine_Threshold + _Shine_Smoothness ) , lerpResult35);
				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float temp_output_7_0_g53 = ( _AlphaThreshold + 0.0 );
				float temp_output_1_0_g53 = break22.g;
				float lerpResult9_g53 = lerp( temp_output_1_0_g53 , ( 1.0 - temp_output_1_0_g53 ) , _AlphaSharp_OneMinus);
				float smoothstepResult2_g53 = smoothstep( temp_output_7_0_g53 , ( temp_output_7_0_g53 + _AlphaSmoothness ) , lerpResult9_g53);
				
				surfaceDescription.Color = ( lerpResult4_g54 + fetchOpaqueVal29 + ( ( smoothstepResult20 + ( break22.g * _Cracks_Emission * temp_output_69_0 ) ) * _ShineColor ) ).rgb;
				surfaceDescription.Emission = 0;
				surfaceDescription.Alpha = saturate( ( ( temp_output_8_0 * temp_output_12_0 ) - smoothstepResult2_g53 ) );
				surfaceDescription.AlphaClipThreshold = _AlphaCutoff;
				surfaceDescription.ShadowTint = float4( 0, 0 ,0 ,1 );
				float2 Distortion = float2 ( 0, 0 );
				float DistortionBlur = 0;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				BSDFData bsdfData = ConvertSurfaceDataToBSDFData( input.positionSS.xy, surfaceData );

				float4 outColor = ApplyBlendMode( bsdfData.color + builtinData.emissiveColor * GetCurrentExposureMultiplier(), builtinData.opacity );
				outColor = EvaluateAtmosphericScattering( posInput, V, outColor );

				#ifdef DEBUG_DISPLAY
					int bufferSize = int(_DebugViewMaterialArray[0].x);
					for (int index = 1; index <= bufferSize; index++)
					{
						int indexMaterialProperty = int(_DebugViewMaterialArray[index].x);
						if (indexMaterialProperty != 0)
						{
							float3 result = float3(1.0, 0.0, 1.0);
							bool needLinearToSRGB = false;

							GetPropertiesDataDebug(indexMaterialProperty, result, needLinearToSRGB);
							GetVaryingsDataDebug(indexMaterialProperty, input, result, needLinearToSRGB);
							GetBuiltinDataDebug(indexMaterialProperty, builtinData, posInput, result, needLinearToSRGB);
							GetSurfaceDataDebug(indexMaterialProperty, surfaceData, result, needLinearToSRGB);
							GetBSDFDataDebug(indexMaterialProperty, bsdfData, result, needLinearToSRGB);

							if (!needLinearToSRGB)
								result = SRGBToLinear(max(0, result));

							outColor = float4(result, 1.0);
						}
					}

					if (_DebugFullScreenMode == FULLSCREENDEBUGMODE_TRANSPARENCY_OVERDRAW)
					{
						float4 result = _DebugTransparencyOverdrawWeight * float4(TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_COST, TRANSPARENCY_OVERDRAW_A);
						outColor = result;
					}
				#endif
				return outColor;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "ShadowCaster"
			Tags { "LightMode"="ShadowCaster" }

			Cull [_CullMode]
			ZWrite On
			ZClip [_ZClip]
			ColorMask 0

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define HAVE_MESH_MODIFICATION 1
			#define ASE_SRP_VERSION 999999

			#define SHADERPASS SHADERPASS_SHADOWS

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl"

			#define ASE_NEEDS_VERT_NORMAL


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			CBUFFER_START( UnityPerMaterial )
			float4 _EdgeFadeV;
			float4 _EdgeFadeU;
			float4 _ShineColor;
			float4 _PanningTex_ST;
			float4 _ColorB;
			float4 _ColorA;
			float _DF_OneMinus;
			float _Shine_Threshold;
			float _Shine_Smoothness;
			float _FresnelThreshold;
			float _FresnelSmoothess;
			float _FresnelScale;
			float _FresnelPower;
			float _Fresnel_Enable;
			float _Cracks_Emission;
			float _AlphaThreshold;
			float _DF_Distance;
			float _Flicker_Min;
			float _AddedEdgeFade;
			float _AlphaSmoothness;
			float _Refraction;
			float _Bicolor_OneMinus;
			float _BicolorSmoothness;
			float _BicolorThreshold;
			float _Displacement;
			float _Flicker_Offset;
			float _TimeStep;
			float _PanningTex_InvertUV;
			float _PanningTex_ManualOffset;
			float _Flicker_Speed;
			float _Flicker_Max;
			float _EdgeFade_Enable;
			float _AlphaSharp_OneMinus;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _PanningTex;
			SAMPLER(sampler_PanningTex);


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest(surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold);
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE (BuiltinData, builtinData);
				builtinData.opacity = surfaceDescription.Alpha;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = inputMesh.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2Dlod( _PanningTex, float4( ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), 0, 0.0) );
				float4 break22 = temp_output_7_0;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float3 ase_worldPos = GetAbsolutePositionWS( TransformObjectToWorld( (inputMesh.positionOS).xyz ) );
				o.ase_texcoord.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(inputMesh.normalOS);
				o.ase_texcoord1.xyz = ase_worldNormal;
				
				o.ase_texcoord2.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = ( inputMesh.normalOS * temp_output_69_0 * _Displacement );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
					#ifdef WRITE_NORMAL_BUFFER
					, out float4 outNormalBuffer : SV_Target0
						#ifdef WRITE_MSAA_DEPTH
						, out float1 depthColor : SV_Target1
						#endif
					#elif defined(WRITE_MSAA_DEPTH)
					, out float4 outNormalBuffer : SV_Target0
					, out float1 depthColor : SV_Target1
					#elif defined(SCENESELECTIONPASS)
					, out float4 outColor : SV_Target0
					#endif
					#ifdef _DEPTHOFFSET_ON
					, out float outputDepth : SV_Depth
					#endif
					
					)
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );

				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);

				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float3 ase_worldPos = packedInput.ase_texcoord.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = packedInput.ase_texcoord1.xyz;
				float clampResult11_g48 = clamp( _FresnelPower , 0.0 , 50.0 );
				float fresnelNdotV1_g48 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode1_g48 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV1_g48, clampResult11_g48 ) );
				float smoothstepResult4_g48 = smoothstep( _FresnelThreshold , ( _FresnelThreshold + _FresnelSmoothess ) , fresnelNode1_g48);
				float lerpResult9_g48 = lerp( 1.0 , smoothstepResult4_g48 , _Fresnel_Enable);
				float temp_output_8_0 = lerpResult9_g48;
				float2 texCoord7_g49 = packedInput.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break9_g49 = frac( texCoord7_g49 );
				float smoothstepResult3_g49 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g49.x);
				float smoothstepResult4_g49 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.x ));
				float smoothstepResult5_g49 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g49.y);
				float smoothstepResult6_g49 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.y ));
				float lerpResult14_g49 = lerp( 1.0 , ( smoothstepResult3_g49 * smoothstepResult4_g49 * smoothstepResult5_g49 * smoothstepResult6_g49 ) , _EdgeFade_Enable);
				float temp_output_12_0 = lerpResult14_g49;
				float temp_output_7_0_g53 = ( _AlphaThreshold + 0.0 );
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = packedInput.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), ddx( texCoord9_g43 ), ddy( texCoord9_g43 ) );
				float4 break22 = temp_output_7_0;
				float temp_output_1_0_g53 = break22.g;
				float lerpResult9_g53 = lerp( temp_output_1_0_g53 , ( 1.0 - temp_output_1_0_g53 ) , _AlphaSharp_OneMinus);
				float smoothstepResult2_g53 = smoothstep( temp_output_7_0_g53 , ( temp_output_7_0_g53 + _AlphaSmoothness ) , lerpResult9_g53);
				
				surfaceDescription.Alpha = saturate( ( ( temp_output_8_0 * temp_output_12_0 ) - smoothstepResult2_g53 ) );
				surfaceDescription.AlphaClipThreshold = _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription,input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif

				#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer( ConvertSurfaceDataToNormalData( surfaceData ), posInput.positionSS, outNormalBuffer );
				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.positionCS.z;
				#endif
				#elif defined(WRITE_MSAA_DEPTH)
				outNormalBuffer = float4( 0.0, 0.0, 0.0, 1.0 );
				depthColor = packedInput.positionCS.z;
				#elif defined(SCENESELECTIONPASS)
				outColor = float4( _ObjectId, _PassValue, 1.0, 1.0 );
				#endif
			}
			ENDHLSL
		}

		
		Pass
		{
			
			Name "META"
			Tags { "LightMode"="Meta" }

			Cull Off

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define HAVE_MESH_MODIFICATION 1
			#define ASE_SRP_VERSION 999999
			#define REQUIRE_OPAQUE_TEXTURE 1

			#define SHADERPASS SHADERPASS_LIGHT_TRANSPORT

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl"

			CBUFFER_START( UnityPerMaterial )
			float4 _EdgeFadeV;
			float4 _EdgeFadeU;
			float4 _ShineColor;
			float4 _PanningTex_ST;
			float4 _ColorB;
			float4 _ColorA;
			float _DF_OneMinus;
			float _Shine_Threshold;
			float _Shine_Smoothness;
			float _FresnelThreshold;
			float _FresnelSmoothess;
			float _FresnelScale;
			float _FresnelPower;
			float _Fresnel_Enable;
			float _Cracks_Emission;
			float _AlphaThreshold;
			float _DF_Distance;
			float _Flicker_Min;
			float _AddedEdgeFade;
			float _AlphaSmoothness;
			float _Refraction;
			float _Bicolor_OneMinus;
			float _BicolorSmoothness;
			float _BicolorThreshold;
			float _Displacement;
			float _Flicker_Offset;
			float _TimeStep;
			float _PanningTex_InvertUV;
			float _PanningTex_ManualOffset;
			float _Flicker_Speed;
			float _Flicker_Max;
			float _EdgeFade_Enable;
			float _AlphaSharp_OneMinus;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END

			CBUFFER_START( UnityMetaPass )
			bool4 unity_MetaVertexControl;
			bool4 unity_MetaFragmentControl;
			CBUFFER_END

			float unity_OneOverOutputBoost;
			float unity_MaxOutputValue;
			sampler2D _PanningTex;
			SAMPLER(sampler_PanningTex);


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_VERT_POSITION


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 uv1 : TEXCOORD1;
				float4 uv2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};


			float4 ASEHDSampleSceneColor(float2 uv, float lod, float exposureMultiplier)
			{
				#if defined(REQUIRE_OPAQUE_TEXTURE) && defined(_SURFACE_TYPE_TRANSPARENT) && defined(SHADERPASS) && (SHADERPASS != SHADERPASS_LIGHT_TRANSPORT)
				return float4( SampleCameraColor(uv, lod) * exposureMultiplier, 1.0 );
				#endif
				return float4(0.0, 0.0, 0.0, 1.0);
			}
			

			struct SurfaceDescription
			{
				float3 Color;
				float3 Emission;
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData( FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData )
			{
				ZERO_INITIALIZE( SurfaceData, surfaceData );
				surfaceData.color = surfaceDescription.Color;
			}

			void GetSurfaceAndBuiltinData( SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData )
			{
				#if _ALPHATEST_ON
				DoAlphaTest( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData( fragInputs, surfaceDescription, V, surfaceData );
				ZERO_INITIALIZE( BuiltinData, builtinData );
				builtinData.opacity = surfaceDescription.Alpha;
				builtinData.emissiveColor = surfaceDescription.Emission;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID( inputMesh );
				UNITY_TRANSFER_INSTANCE_ID( inputMesh, o );

				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = inputMesh.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2Dlod( _PanningTex, float4( ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), 0, 0.0) );
				float4 break22 = temp_output_7_0;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float4 ase_clipPos = TransformWorldToHClip( TransformObjectToWorld(inputMesh.positionOS));
				float4 screenPos = ComputeScreenPos( ase_clipPos , _ProjectionParams.x );
				o.ase_texcoord1 = screenPos;
				float3 vertexPos1_g55 = inputMesh.positionOS;
				float4 ase_clipPos1_g55 = TransformWorldToHClip( TransformObjectToWorld(vertexPos1_g55));
				float4 screenPos1_g55 = ComputeScreenPos( ase_clipPos1_g55 , _ProjectionParams.x );
				o.ase_texcoord2 = screenPos1_g55;
				float3 ase_worldPos = GetAbsolutePositionWS( TransformObjectToWorld( (inputMesh.positionOS).xyz ) );
				o.ase_texcoord3.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(inputMesh.normalOS);
				o.ase_texcoord4.xyz = ase_worldNormal;
				
				o.ase_texcoord.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.zw = 0;
				o.ase_texcoord3.w = 0;
				o.ase_texcoord4.w = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = ( inputMesh.normalOS * temp_output_69_0 * _Displacement );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float2 uv = float2( 0.0, 0.0 );
				if( unity_MetaVertexControl.x )
				{
					uv = inputMesh.uv1.xy * unity_LightmapST.xy + unity_LightmapST.zw;
				}
				else if( unity_MetaVertexControl.y )
				{
					uv = inputMesh.uv2.xy * unity_DynamicLightmapST.xy + unity_DynamicLightmapST.zw;
				}

				o.positionCS = float4( uv * 2.0 - 1.0, inputMesh.positionOS.z > 0 ? 1.0e-4 : 0.0, 1.0 );
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 uv1 : TEXCOORD1;
				float4 uv2 : TEXCOORD2;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.uv1 = v.uv1;
				o.uv2 = v.uv2;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.uv1 = patch[0].uv1 * bary.x + patch[1].uv1 * bary.y + patch[2].uv1 * bary.z;
				o.uv2 = patch[0].uv2 * bary.x + patch[1].uv2 * bary.y + patch[2].uv2 * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			float4 Frag( VertexOutput packedInput  ) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				FragInputs input;
				ZERO_INITIALIZE( FragInputs, input );
				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput( input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS );

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = packedInput.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), ddx( texCoord9_g43 ), ddy( texCoord9_g43 ) );
				float4 break22 = temp_output_7_0;
				float smoothstepResult5_g54 = smoothstep( _BicolorThreshold , ( _BicolorThreshold + _BicolorSmoothness ) , break22.r);
				float lerpResult12_g54 = lerp( smoothstepResult5_g54 , ( 1.0 - smoothstepResult5_g54 ) , _Bicolor_OneMinus);
				float4 lerpResult4_g54 = lerp( _ColorA , _ColorB , lerpResult12_g54);
				float4 screenPos = packedInput.ase_texcoord1;
				float4 ase_screenPosNorm = screenPos / screenPos.w;
				ase_screenPosNorm.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm.z : ase_screenPosNorm.z * 0.5 + 0.5;
				float2 texCoord7_g49 = packedInput.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break9_g49 = frac( texCoord7_g49 );
				float smoothstepResult3_g49 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g49.x);
				float smoothstepResult4_g49 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.x ));
				float smoothstepResult5_g49 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g49.y);
				float smoothstepResult6_g49 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.y ));
				float lerpResult14_g49 = lerp( 1.0 , ( smoothstepResult3_g49 * smoothstepResult4_g49 * smoothstepResult5_g49 * smoothstepResult6_g49 ) , _EdgeFade_Enable);
				float temp_output_12_0 = lerpResult14_g49;
				float4 screenPos1_g55 = packedInput.ase_texcoord2;
				float4 ase_screenPosNorm1 = screenPos1_g55 / screenPos1_g55.w;
				ase_screenPosNorm1.z = ( UNITY_NEAR_CLIP_VALUE >= 0 ) ? ase_screenPosNorm1.z : ase_screenPosNorm1.z * 0.5 + 0.5;
				float screenDepth1_g55 = LinearEyeDepth(SampleCameraDepth( ase_screenPosNorm1.xy ),_ZBufferParams);
				float distanceDepth1_g55 = saturate( abs( ( screenDepth1_g55 - LinearEyeDepth( ase_screenPosNorm1.z,_ZBufferParams ) ) / ( _DF_Distance ) ) );
				float lerpResult3_g55 = lerp( distanceDepth1_g55 , ( 1.0 - distanceDepth1_g55 ) , _DF_OneMinus);
				float4 fetchOpaqueVal29 = ASEHDSampleSceneColor(( ase_screenPosNorm + ( temp_output_7_0 * _Refraction * temp_output_12_0 * lerpResult3_g55 ) ).xy, 0.0, GetInverseCurrentExposureMultiplier());
				float3 ase_worldPos = packedInput.ase_texcoord3.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = packedInput.ase_texcoord4.xyz;
				float clampResult11_g48 = clamp( _FresnelPower , 0.0 , 50.0 );
				float fresnelNdotV1_g48 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode1_g48 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV1_g48, clampResult11_g48 ) );
				float smoothstepResult4_g48 = smoothstep( _FresnelThreshold , ( _FresnelThreshold + _FresnelSmoothess ) , fresnelNode1_g48);
				float lerpResult9_g48 = lerp( 1.0 , smoothstepResult4_g48 , _Fresnel_Enable);
				float temp_output_8_0 = lerpResult9_g48;
				float lerpResult35 = lerp( break22.r , temp_output_8_0 , 0.8);
				float smoothstepResult20 = smoothstep( _Shine_Threshold , ( _Shine_Threshold + _Shine_Smoothness ) , lerpResult35);
				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float temp_output_7_0_g53 = ( _AlphaThreshold + 0.0 );
				float temp_output_1_0_g53 = break22.g;
				float lerpResult9_g53 = lerp( temp_output_1_0_g53 , ( 1.0 - temp_output_1_0_g53 ) , _AlphaSharp_OneMinus);
				float smoothstepResult2_g53 = smoothstep( temp_output_7_0_g53 , ( temp_output_7_0_g53 + _AlphaSmoothness ) , lerpResult9_g53);
				
				surfaceDescription.Color = ( lerpResult4_g54 + fetchOpaqueVal29 + ( ( smoothstepResult20 + ( break22.g * _Cracks_Emission * temp_output_69_0 ) ) * _ShineColor ) ).rgb;
				surfaceDescription.Emission = 0;
				surfaceDescription.Alpha = saturate( ( ( temp_output_8_0 * temp_output_12_0 ) - smoothstepResult2_g53 ) );
				surfaceDescription.AlphaClipThreshold =  _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData( surfaceDescription,input, V, posInput, surfaceData, builtinData );

				BSDFData bsdfData = ConvertSurfaceDataToBSDFData( input.positionSS.xy, surfaceData );
				LightTransportData lightTransportData = GetLightTransportData( surfaceData, builtinData, bsdfData );

				float4 res = float4( 0.0, 0.0, 0.0, 1.0 );
				if( unity_MetaFragmentControl.x )
				{
					res.rgb = clamp( pow( abs( lightTransportData.diffuseColor ), saturate( unity_OneOverOutputBoost ) ), 0, unity_MaxOutputValue );
				}

				if( unity_MetaFragmentControl.y )
				{
					res.rgb = lightTransportData.emissiveColor;
				}

				return res;
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "SceneSelectionPass"
			Tags { "LightMode"="SceneSelectionPass" }

			Cull [_CullMode]
			ZWrite On

			ColorMask 0

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define HAVE_MESH_MODIFICATION 1
			#define ASE_SRP_VERSION 999999

			#define SHADERPASS SHADERPASS_DEPTH_ONLY
			#define SCENESELECTIONPASS
			#pragma editor_sync_compilation

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl"

			int _ObjectId;
			int _PassValue;

			CBUFFER_START( UnityPerMaterial )
			float4 _EdgeFadeV;
			float4 _EdgeFadeU;
			float4 _ShineColor;
			float4 _PanningTex_ST;
			float4 _ColorB;
			float4 _ColorA;
			float _DF_OneMinus;
			float _Shine_Threshold;
			float _Shine_Smoothness;
			float _FresnelThreshold;
			float _FresnelSmoothess;
			float _FresnelScale;
			float _FresnelPower;
			float _Fresnel_Enable;
			float _Cracks_Emission;
			float _AlphaThreshold;
			float _DF_Distance;
			float _Flicker_Min;
			float _AddedEdgeFade;
			float _AlphaSmoothness;
			float _Refraction;
			float _Bicolor_OneMinus;
			float _BicolorSmoothness;
			float _BicolorThreshold;
			float _Displacement;
			float _Flicker_Offset;
			float _TimeStep;
			float _PanningTex_InvertUV;
			float _PanningTex_ManualOffset;
			float _Flicker_Speed;
			float _Flicker_Max;
			float _EdgeFade_Enable;
			float _AlphaSharp_OneMinus;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _PanningTex;
			SAMPLER(sampler_PanningTex);


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_NORMAL


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};


			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity =  surfaceDescription.Alpha;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = inputMesh.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2Dlod( _PanningTex, float4( ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), 0, 0.0) );
				float4 break22 = temp_output_7_0;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float3 ase_worldPos = GetAbsolutePositionWS( TransformObjectToWorld( (inputMesh.positionOS).xyz ) );
				o.ase_texcoord.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(inputMesh.normalOS);
				o.ase_texcoord1.xyz = ase_worldNormal;
				
				o.ase_texcoord2.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =  ( inputMesh.normalOS * temp_output_69_0 * _Displacement );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
					, out float4 outColor : SV_Target0
					#ifdef _DEPTHOFFSET_ON
					, out float outputDepth : SV_Depth
					#endif
					
					)
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceData surfaceData;
				BuiltinData builtinData;
				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float3 ase_worldPos = packedInput.ase_texcoord.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = packedInput.ase_texcoord1.xyz;
				float clampResult11_g48 = clamp( _FresnelPower , 0.0 , 50.0 );
				float fresnelNdotV1_g48 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode1_g48 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV1_g48, clampResult11_g48 ) );
				float smoothstepResult4_g48 = smoothstep( _FresnelThreshold , ( _FresnelThreshold + _FresnelSmoothess ) , fresnelNode1_g48);
				float lerpResult9_g48 = lerp( 1.0 , smoothstepResult4_g48 , _Fresnel_Enable);
				float temp_output_8_0 = lerpResult9_g48;
				float2 texCoord7_g49 = packedInput.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break9_g49 = frac( texCoord7_g49 );
				float smoothstepResult3_g49 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g49.x);
				float smoothstepResult4_g49 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.x ));
				float smoothstepResult5_g49 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g49.y);
				float smoothstepResult6_g49 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.y ));
				float lerpResult14_g49 = lerp( 1.0 , ( smoothstepResult3_g49 * smoothstepResult4_g49 * smoothstepResult5_g49 * smoothstepResult6_g49 ) , _EdgeFade_Enable);
				float temp_output_12_0 = lerpResult14_g49;
				float temp_output_7_0_g53 = ( _AlphaThreshold + 0.0 );
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = packedInput.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), ddx( texCoord9_g43 ), ddy( texCoord9_g43 ) );
				float4 break22 = temp_output_7_0;
				float temp_output_1_0_g53 = break22.g;
				float lerpResult9_g53 = lerp( temp_output_1_0_g53 , ( 1.0 - temp_output_1_0_g53 ) , _AlphaSharp_OneMinus);
				float smoothstepResult2_g53 = smoothstep( temp_output_7_0_g53 , ( temp_output_7_0_g53 + _AlphaSmoothness ) , lerpResult9_g53);
				
				surfaceDescription.Alpha = saturate( ( ( temp_output_8_0 * temp_output_12_0 ) - smoothstepResult2_g53 ) );
				surfaceDescription.AlphaClipThreshold =  _AlphaCutoff;

				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif

				outColor = float4( _ObjectId, _PassValue, 1.0, 1.0 );
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "DepthForwardOnly"
			Tags { "LightMode"="DepthForwardOnly" }

			Cull [_CullMode]
			ZWrite On
			Stencil
			{
				Ref [_StencilRefDepth]
				WriteMask [_StencilWriteMaskDepth]
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}


			ColorMask 0 0

			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define HAVE_MESH_MODIFICATION 1
			#define ASE_SRP_VERSION 999999

			#define SHADERPASS SHADERPASS_DEPTH_ONLY
			#pragma multi_compile _ WRITE_MSAA_DEPTH

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl"

			CBUFFER_START( UnityPerMaterial )
			float4 _EdgeFadeV;
			float4 _EdgeFadeU;
			float4 _ShineColor;
			float4 _PanningTex_ST;
			float4 _ColorB;
			float4 _ColorA;
			float _DF_OneMinus;
			float _Shine_Threshold;
			float _Shine_Smoothness;
			float _FresnelThreshold;
			float _FresnelSmoothess;
			float _FresnelScale;
			float _FresnelPower;
			float _Fresnel_Enable;
			float _Cracks_Emission;
			float _AlphaThreshold;
			float _DF_Distance;
			float _Flicker_Min;
			float _AddedEdgeFade;
			float _AlphaSmoothness;
			float _Refraction;
			float _Bicolor_OneMinus;
			float _BicolorSmoothness;
			float _BicolorThreshold;
			float _Displacement;
			float _Flicker_Offset;
			float _TimeStep;
			float _PanningTex_InvertUV;
			float _PanningTex_ManualOffset;
			float _Flicker_Speed;
			float _Flicker_Max;
			float _EdgeFade_Enable;
			float _AlphaSharp_OneMinus;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _PanningTex;
			SAMPLER(sampler_PanningTex);


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_NORMAL


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 positionCS : SV_Position;
				float4 ase_texcoord : TEXCOORD0;
				float4 ase_texcoord1 : TEXCOORD1;
				float4 ase_texcoord2 : TEXCOORD2;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity =  surfaceDescription.Alpha;
			}

			VertexOutput VertexFunction( VertexInput inputMesh  )
			{
				VertexOutput o;
				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = inputMesh.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2Dlod( _PanningTex, float4( ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), 0, 0.0) );
				float4 break22 = temp_output_7_0;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float3 ase_worldPos = GetAbsolutePositionWS( TransformObjectToWorld( (inputMesh.positionOS).xyz ) );
				o.ase_texcoord.xyz = ase_worldPos;
				float3 ase_worldNormal = TransformObjectToWorldNormal(inputMesh.normalOS);
				o.ase_texcoord1.xyz = ase_worldNormal;
				
				o.ase_texcoord2.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord.w = 0;
				o.ase_texcoord1.w = 0;
				o.ase_texcoord2.zw = 0;
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue =  ( inputMesh.normalOS * temp_output_69_0 * _Displacement );
				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif

				inputMesh.normalOS =  inputMesh.normalOS ;

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				o.positionCS = TransformWorldToHClip(positionRWS);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
					#ifdef WRITE_NORMAL_BUFFER
					, out float4 outNormalBuffer : SV_Target0
						#ifdef WRITE_MSAA_DEPTH
						, out float1 depthColor : SV_Target1
						#endif
					#elif defined(WRITE_MSAA_DEPTH)
					, out float4 outNormalBuffer : SV_Target0
					, out float1 depthColor : SV_Target1
					#elif defined(SCENESELECTIONPASS)
					, out float4 outColor : SV_Target0
					#endif
					#ifdef _DEPTHOFFSET_ON
					, out float outputDepth : SV_Depth
					#endif
					
					)
			{
				UNITY_SETUP_INSTANCE_ID( packedInput );
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);

				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.positionCS;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = float3( 1.0, 1.0, 1.0 );

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float3 ase_worldPos = packedInput.ase_texcoord.xyz;
				float3 ase_worldViewDir = ( _WorldSpaceCameraPos.xyz - ase_worldPos );
				ase_worldViewDir = normalize(ase_worldViewDir);
				float3 ase_worldNormal = packedInput.ase_texcoord1.xyz;
				float clampResult11_g48 = clamp( _FresnelPower , 0.0 , 50.0 );
				float fresnelNdotV1_g48 = dot( ase_worldNormal, ase_worldViewDir );
				float fresnelNode1_g48 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV1_g48, clampResult11_g48 ) );
				float smoothstepResult4_g48 = smoothstep( _FresnelThreshold , ( _FresnelThreshold + _FresnelSmoothess ) , fresnelNode1_g48);
				float lerpResult9_g48 = lerp( 1.0 , smoothstepResult4_g48 , _Fresnel_Enable);
				float temp_output_8_0 = lerpResult9_g48;
				float2 texCoord7_g49 = packedInput.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break9_g49 = frac( texCoord7_g49 );
				float smoothstepResult3_g49 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g49.x);
				float smoothstepResult4_g49 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.x ));
				float smoothstepResult5_g49 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g49.y);
				float smoothstepResult6_g49 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.y ));
				float lerpResult14_g49 = lerp( 1.0 , ( smoothstepResult3_g49 * smoothstepResult4_g49 * smoothstepResult5_g49 * smoothstepResult6_g49 ) , _EdgeFade_Enable);
				float temp_output_12_0 = lerpResult14_g49;
				float temp_output_7_0_g53 = ( _AlphaThreshold + 0.0 );
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = packedInput.ase_texcoord2.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), ddx( texCoord9_g43 ), ddy( texCoord9_g43 ) );
				float4 break22 = temp_output_7_0;
				float temp_output_1_0_g53 = break22.g;
				float lerpResult9_g53 = lerp( temp_output_1_0_g53 , ( 1.0 - temp_output_1_0_g53 ) , _AlphaSharp_OneMinus);
				float smoothstepResult2_g53 = smoothstep( temp_output_7_0_g53 , ( temp_output_7_0_g53 + _AlphaSmoothness ) , lerpResult9_g53);
				
				surfaceDescription.Alpha = saturate( ( ( temp_output_8_0 * temp_output_12_0 ) - smoothstepResult2_g53 ) );
				surfaceDescription.AlphaClipThreshold =  _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif

				#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer( ConvertSurfaceDataToNormalData( surfaceData ), posInput.positionSS, outNormalBuffer );
				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.positionCS.z;
				#endif
				#elif defined(WRITE_MSAA_DEPTH)
				outNormalBuffer = float4( 0.0, 0.0, 0.0, 1.0 );
				depthColor = packedInput.positionCS.z;
				#elif defined(SCENESELECTIONPASS)
				outColor = float4( _ObjectId, _PassValue, 1.0, 1.0 );
				#endif
			}

			ENDHLSL
		}

		
		Pass
		{
			
			Name "Motion Vectors"
			Tags { "LightMode"="MotionVectors" }

			Cull [_CullMode]

			ZWrite On

			Stencil
			{
				Ref [_StencilRefMV]
				WriteMask [_StencilWriteMaskMV]
				Comp Always
				Pass Replace
				Fail Keep
				ZFail Keep
			}


			HLSLPROGRAM
			#pragma multi_compile_instancing
			#define HAVE_MESH_MODIFICATION 1
			#define ASE_SRP_VERSION 999999

			#define SHADERPASS SHADERPASS_MOTION_VECTORS
			#pragma multi_compile _ WRITE_MSAA_DEPTH

			#pragma shader_feature _SURFACE_TYPE_TRANSPARENT
			#pragma shader_feature_local _ _BLENDMODE_ALPHA _BLENDMODE_ADD _BLENDMODE_PRE_MULTIPLY
			#pragma shader_feature_local _ALPHATEST_ON
			#pragma shader_feature_local _ENABLE_FOG_ON_TRANSPARENT

			#pragma vertex Vert
			#pragma fragment Frag

			#include "Packages/com.unity.render-pipelines.core/ShaderLibrary/Common.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderVariables.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/FragInputs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/RenderPipeline/ShaderPass/ShaderPass.cs.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphHeader.hlsl"

			CBUFFER_START( UnityPerMaterial )
			float4 _EdgeFadeV;
			float4 _EdgeFadeU;
			float4 _ShineColor;
			float4 _PanningTex_ST;
			float4 _ColorB;
			float4 _ColorA;
			float _DF_OneMinus;
			float _Shine_Threshold;
			float _Shine_Smoothness;
			float _FresnelThreshold;
			float _FresnelSmoothess;
			float _FresnelScale;
			float _FresnelPower;
			float _Fresnel_Enable;
			float _Cracks_Emission;
			float _AlphaThreshold;
			float _DF_Distance;
			float _Flicker_Min;
			float _AddedEdgeFade;
			float _AlphaSmoothness;
			float _Refraction;
			float _Bicolor_OneMinus;
			float _BicolorSmoothness;
			float _BicolorThreshold;
			float _Displacement;
			float _Flicker_Offset;
			float _TimeStep;
			float _PanningTex_InvertUV;
			float _PanningTex_ManualOffset;
			float _Flicker_Speed;
			float _Flicker_Max;
			float _EdgeFade_Enable;
			float _AlphaSharp_OneMinus;
			float4 _EmissionColor;
			float _RenderQueueType;
			#ifdef _ADD_PRECOMPUTED_VELOCITY
			float _AddPrecomputedVelocity;
			#endif
			#ifdef _ENABLE_SHADOW_MATTE
			float _ShadowMatteFilter;
			#endif
			float _StencilRef;
			float _StencilWriteMask;
			float _StencilRefDepth;
			float _StencilWriteMaskDepth;
			float _StencilRefMV;
			float _StencilWriteMaskMV;
			float _StencilRefDistortionVec;
			float _StencilWriteMaskDistortionVec;
			float _StencilWriteMaskGBuffer;
			float _StencilRefGBuffer;
			float _ZTestGBuffer;
			float _RequireSplitLighting;
			float _ReceivesSSR;
			float _SurfaceType;
			float _BlendMode;
			float _SrcBlend;
			float _DstBlend;
			float _AlphaSrcBlend;
			float _AlphaDstBlend;
			float _ZWrite;
			float _TransparentZWrite;
			float _CullMode;
			float _TransparentSortPriority;
			float _EnableFogOnTransparent;
			float _CullModeForward;
			float _TransparentCullMode;
			float _ZTestDepthEqualForOpaque;
			float _ZTestTransparent;
			float _TransparentBackfaceEnable;
			float _AlphaCutoffEnable;
			float _AlphaCutoff;
			float _UseShadowThreshold;
			float _DoubleSidedEnable;
			float _DoubleSidedNormalMode;
			float4 _DoubleSidedConstants;
			#ifdef TESSELLATION_ON
				float _TessPhongStrength;
				float _TessValue;
				float _TessMin;
				float _TessMax;
				float _TessEdgeLength;
				float _TessMaxDisp;
			#endif
			CBUFFER_END
			sampler2D _PanningTex;
			SAMPLER(sampler_PanningTex);


			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Material.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/Unlit/Unlit.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/BuiltinUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/Material/MaterialUtilities.hlsl"
			#include "Packages/com.unity.render-pipelines.high-definition/Runtime/ShaderLibrary/ShaderGraphFunctions.hlsl"

			#define ASE_NEEDS_VERT_NORMAL
			#define ASE_NEEDS_FRAG_WORLD_VIEW_DIR


			struct VertexInput
			{
				float3 positionOS : POSITION;
				float3 normalOS : NORMAL;
				float3 previousPositionOS : TEXCOORD4;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					float3 precomputedVelocity : TEXCOORD5;
				#endif
				float4 ase_texcoord : TEXCOORD0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct VertexOutput
			{
				float4 vmeshPositionCS : SV_Position;
				float3 vmeshInterp00 : TEXCOORD0;
				float3 vpassInterpolators0 : TEXCOORD1; //interpolators0
				float3 vpassInterpolators1 : TEXCOORD2; //interpolators1
				float4 ase_texcoord3 : TEXCOORD3;
				float4 ase_texcoord4 : TEXCOORD4;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};

			
			struct SurfaceDescription
			{
				float Alpha;
				float AlphaClipThreshold;
			};

			void BuildSurfaceData(FragInputs fragInputs, SurfaceDescription surfaceDescription, float3 V, out SurfaceData surfaceData)
			{
				ZERO_INITIALIZE(SurfaceData, surfaceData);
			}

			void GetSurfaceAndBuiltinData(SurfaceDescription surfaceDescription, FragInputs fragInputs, float3 V, inout PositionInputs posInput, out SurfaceData surfaceData, out BuiltinData builtinData)
			{
				#if _ALPHATEST_ON
				DoAlphaTest ( surfaceDescription.Alpha, surfaceDescription.AlphaClipThreshold );
				#endif

				BuildSurfaceData(fragInputs, surfaceDescription, V, surfaceData);
				ZERO_INITIALIZE(BuiltinData, builtinData);
				builtinData.opacity =  surfaceDescription.Alpha;
			}

			VertexInput ApplyMeshModification(VertexInput inputMesh, float3 timeParameters, inout VertexOutput o )
			{
				_TimeParameters.xyz = timeParameters;
				float mulTime1_g56 = _TimeParameters.x * _Flicker_Speed;
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = inputMesh.ase_texcoord.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2Dlod( _PanningTex, float4( ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), 0, 0.0) );
				float4 break22 = temp_output_7_0;
				float temp_output_2_0_g56 = sin( ( mulTime1_g56 + ( break22.b * _Flicker_Offset ) ) );
				float lerpResult12_g56 = lerp( _Flicker_Min , _Flicker_Max , ( ( temp_output_2_0_g56 + 1.0 ) / 2.0 ));
				float temp_output_69_0 = lerpResult12_g56;
				
				float3 ase_worldNormal = TransformObjectToWorldNormal(inputMesh.normalOS);
				o.ase_texcoord3.xyz = ase_worldNormal;
				
				o.ase_texcoord4.xy = inputMesh.ase_texcoord.xy;
				
				//setting value to unused interpolator channels and avoid initialization warnings
				o.ase_texcoord3.w = 0;
				o.ase_texcoord4.zw = 0;

				#ifdef ASE_ABSOLUTE_VERTEX_POS
				float3 defaultVertexValue = inputMesh.positionOS.xyz;
				#else
				float3 defaultVertexValue = float3( 0, 0, 0 );
				#endif
				float3 vertexValue = ( inputMesh.normalOS * temp_output_69_0 * _Displacement );

				#ifdef ASE_ABSOLUTE_VERTEX_POS
				inputMesh.positionOS.xyz = vertexValue;
				#else
				inputMesh.positionOS.xyz += vertexValue;
				#endif
				inputMesh.normalOS =  inputMesh.normalOS ;
				return inputMesh;
			}

			VertexOutput VertexFunction(VertexInput inputMesh)
			{
				VertexOutput o = (VertexOutput)0;
				VertexInput defaultMesh = inputMesh;

				UNITY_SETUP_INSTANCE_ID(inputMesh);
				UNITY_TRANSFER_INSTANCE_ID(inputMesh, o);
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );

				inputMesh = ApplyMeshModification( inputMesh, _TimeParameters.xyz, o);

				float3 positionRWS = TransformObjectToWorld(inputMesh.positionOS);
				float3 normalWS = TransformObjectToWorldNormal(inputMesh.normalOS);

				float3 VMESHpositionRWS = positionRWS;
				float4 VMESHpositionCS = TransformWorldToHClip(positionRWS);

				//#if defined(UNITY_REVERSED_Z)
				//	VMESHpositionCS.z -= unity_MotionVectorsParams.z * VMESHpositionCS.w;
				//#else
				//	VMESHpositionCS.z += unity_MotionVectorsParams.z * VMESHpositionCS.w;
				//#endif

				float4 VPASSpreviousPositionCS;
				float4 VPASSpositionCS = mul(UNITY_MATRIX_UNJITTERED_VP, float4(VMESHpositionRWS, 1.0));

				bool forceNoMotion = unity_MotionVectorsParams.y == 0.0;
				if (forceNoMotion)
				{
					VPASSpreviousPositionCS = float4(0.0, 0.0, 0.0, 1.0);
				}
				else
				{
					bool hasDeformation = unity_MotionVectorsParams.x > 0.0;
					float3 effectivePositionOS = (hasDeformation ? inputMesh.previousPositionOS : defaultMesh.positionOS);
					#if defined(_ADD_PRECOMPUTED_VELOCITY)
					effectivePositionOS -= inputMesh.precomputedVelocity;
					#endif

					#if defined(HAVE_MESH_MODIFICATION)
						VertexInput previousMesh = defaultMesh;
						previousMesh.positionOS = effectivePositionOS ;
						VertexOutput test = (VertexOutput)0;
						float3 curTime = _TimeParameters.xyz;
						previousMesh = ApplyMeshModification(previousMesh, _LastTimeParameters.xyz, test);
						_TimeParameters.xyz = curTime;
						float3 previousPositionRWS = TransformPreviousObjectToWorld(previousMesh.positionOS);
					#else
						float3 previousPositionRWS = TransformPreviousObjectToWorld(effectivePositionOS);
					#endif

					#ifdef ATTRIBUTES_NEED_NORMAL
						float3 normalWS = TransformPreviousObjectToWorldNormal(defaultMesh.normalOS);
					#else
						float3 normalWS = float3(0.0, 0.0, 0.0);
					#endif

					#if defined(HAVE_VERTEX_MODIFICATION)
						//ApplyVertexModification(inputMesh, normalWS, previousPositionRWS, _LastTimeParameters.xyz);
					#endif

					VPASSpreviousPositionCS = mul(UNITY_MATRIX_PREV_VP, float4(previousPositionRWS, 1.0));
				}

				o.vmeshPositionCS = VMESHpositionCS;
				o.vmeshInterp00.xyz = VMESHpositionRWS;

				o.vpassInterpolators0 = float3(VPASSpositionCS.xyw);
				o.vpassInterpolators1 = float3(VPASSpreviousPositionCS.xyw);
				return o;
			}

			#if defined(TESSELLATION_ON)
			struct VertexControl
			{
				float3 positionOS : INTERNALTESSPOS;
				float3 normalOS : NORMAL;
				float3 previousPositionOS : TEXCOORD4;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					float3 precomputedVelocity : TEXCOORD5;
				#endif
				float4 ase_texcoord : TEXCOORD0;

				UNITY_VERTEX_INPUT_INSTANCE_ID
			};

			struct TessellationFactors
			{
				float edge[3] : SV_TessFactor;
				float inside : SV_InsideTessFactor;
			};

			VertexControl Vert ( VertexInput v )
			{
				VertexControl o;
				UNITY_SETUP_INSTANCE_ID(v);
				UNITY_TRANSFER_INSTANCE_ID(v, o);
				o.positionOS = v.positionOS;
				o.normalOS = v.normalOS;
				o.previousPositionOS = v.previousPositionOS;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					o.precomputedVelocity = v.precomputedVelocity;
				#endif
				o.ase_texcoord = v.ase_texcoord;
				return o;
			}

			TessellationFactors TessellationFunction (InputPatch<VertexControl,3> v)
			{
				TessellationFactors o;
				float4 tf = 1;
				float tessValue = _TessValue; float tessMin = _TessMin; float tessMax = _TessMax;
				float edgeLength = _TessEdgeLength; float tessMaxDisp = _TessMaxDisp;
				#if (SHADEROPTIONS_CAMERA_RELATIVE_RENDERING != 0)
				float3 cameraPos = 0;
				#else
				float3 cameraPos = _WorldSpaceCameraPos;
				#endif
				#if defined(ASE_FIXED_TESSELLATION)
				tf = FixedTess( tessValue );
				#elif defined(ASE_DISTANCE_TESSELLATION)
				tf = DistanceBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), tessValue, tessMin, tessMax, GetObjectToWorldMatrix(), cameraPos );
				#elif defined(ASE_LENGTH_TESSELLATION)
				tf = EdgeLengthBasedTess(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, GetObjectToWorldMatrix(), cameraPos, _ScreenParams );
				#elif defined(ASE_LENGTH_CULL_TESSELLATION)
				tf = EdgeLengthBasedTessCull(float4(v[0].positionOS,1), float4(v[1].positionOS,1), float4(v[2].positionOS,1), edgeLength, tessMaxDisp, GetObjectToWorldMatrix(), cameraPos, _ScreenParams, _FrustumPlanes );
				#endif
				o.edge[0] = tf.x; o.edge[1] = tf.y; o.edge[2] = tf.z; o.inside = tf.w;
				return o;
			}

			[domain("tri")]
			[partitioning("fractional_odd")]
			[outputtopology("triangle_cw")]
			[patchconstantfunc("TessellationFunction")]
			[outputcontrolpoints(3)]
			VertexControl HullFunction(InputPatch<VertexControl, 3> patch, uint id : SV_OutputControlPointID)
			{
			   return patch[id];
			}

			[domain("tri")]
			VertexOutput DomainFunction(TessellationFactors factors, OutputPatch<VertexControl, 3> patch, float3 bary : SV_DomainLocation)
			{
				VertexInput o = (VertexInput) 0;
				o.positionOS = patch[0].positionOS * bary.x + patch[1].positionOS * bary.y + patch[2].positionOS * bary.z;
				o.normalOS = patch[0].normalOS * bary.x + patch[1].normalOS * bary.y + patch[2].normalOS * bary.z;
				o.previousPositionOS = patch[0].previousPositionOS * bary.x + patch[1].previousPositionOS * bary.y + patch[2].previousPositionOS * bary.z;
				#if defined (_ADD_PRECOMPUTED_VELOCITY)
					o.precomputedVelocity = patch[0].precomputedVelocity * bary.x + patch[1].precomputedVelocity * bary.y + patch[2].precomputedVelocity * bary.z;
				#endif
				o.ase_texcoord = patch[0].ase_texcoord * bary.x + patch[1].ase_texcoord * bary.y + patch[2].ase_texcoord * bary.z;
				#if defined(ASE_PHONG_TESSELLATION)
				float3 pp[3];
				for (int i = 0; i < 3; ++i)
					pp[i] = o.positionOS.xyz - patch[i].normalOS * (dot(o.positionOS.xyz, patch[i].normalOS) - dot(patch[i].positionOS.xyz, patch[i].normalOS));
				float phongStrength = _TessPhongStrength;
				o.positionOS.xyz = phongStrength * (pp[0]*bary.x + pp[1]*bary.y + pp[2]*bary.z) + (1.0f-phongStrength) * o.positionOS.xyz;
				#endif
				UNITY_TRANSFER_INSTANCE_ID(patch[0], o);
				return VertexFunction(o);
			}
			#else
			VertexOutput Vert ( VertexInput v )
			{
				return VertexFunction( v );
			}
			#endif

			void Frag( VertexOutput packedInput
						, out float4 outMotionVector : SV_Target0
						#ifdef WRITE_NORMAL_BUFFER
						, out float4 outNormalBuffer : SV_Target1
							#ifdef WRITE_MSAA_DEPTH
								, out float1 depthColor : SV_Target2
							#endif
						#elif defined(WRITE_MSAA_DEPTH)
						, out float4 outNormalBuffer : SV_Target1
						, out float1 depthColor : SV_Target2
						#endif

						#ifdef _DEPTHOFFSET_ON
							, out float outputDepth : SV_Depth
						#endif
						
					)
			{
				UNITY_SETUP_STEREO_EYE_INDEX_POST_VERTEX( packedInput );
				UNITY_SETUP_INSTANCE_ID( packedInput );
				FragInputs input;
				ZERO_INITIALIZE(FragInputs, input);
				input.tangentToWorld = k_identity3x3;
				input.positionSS = packedInput.vmeshPositionCS;
				input.positionRWS = packedInput.vmeshInterp00.xyz;

				PositionInputs posInput = GetPositionInput(input.positionSS.xy, _ScreenSize.zw, input.positionSS.z, input.positionSS.w, input.positionRWS);

				float3 V = GetWorldSpaceNormalizeViewDir(input.positionRWS);

				SurfaceDescription surfaceDescription = (SurfaceDescription)0;
				float3 ase_worldNormal = packedInput.ase_texcoord3.xyz;
				float clampResult11_g48 = clamp( _FresnelPower , 0.0 , 50.0 );
				float fresnelNdotV1_g48 = dot( ase_worldNormal, V );
				float fresnelNode1_g48 = ( 0.0 + _FresnelScale * pow( 1.0 - fresnelNdotV1_g48, clampResult11_g48 ) );
				float smoothstepResult4_g48 = smoothstep( _FresnelThreshold , ( _FresnelThreshold + _FresnelSmoothess ) , fresnelNode1_g48);
				float lerpResult9_g48 = lerp( 1.0 , smoothstepResult4_g48 , _Fresnel_Enable);
				float temp_output_8_0 = lerpResult9_g48;
				float2 texCoord7_g49 = packedInput.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 break9_g49 = frac( texCoord7_g49 );
				float smoothstepResult3_g49 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g49.x);
				float smoothstepResult4_g49 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.x ));
				float smoothstepResult5_g49 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g49.y);
				float smoothstepResult6_g49 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g49.y ));
				float lerpResult14_g49 = lerp( 1.0 , ( smoothstepResult3_g49 * smoothstepResult4_g49 * smoothstepResult5_g49 * smoothstepResult6_g49 ) , _EdgeFade_Enable);
				float temp_output_12_0 = lerpResult14_g49;
				float temp_output_7_0_g53 = ( _AlphaThreshold + 0.0 );
				float4 appendResult43_g43 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
				float4 appendResult42_g43 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
				float4 lerpResult41_g43 = lerp( appendResult43_g43 , appendResult42_g43 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g45 = _PanningTex_ST.zw;
				float2 break3_g45 = temp_output_1_0_g45;
				float4 appendResult5_g45 = (float4(break3_g45.y , break3_g45.x , 0.0 , 0.0));
				float4 lerpResult2_g45 = lerp( float4( temp_output_1_0_g45, 0.0 , 0.0 ) , appendResult5_g45 , _PanningTex_InvertUV);
				float2 texCoord9_g43 = packedInput.ase_texcoord4.xy * float2( 1,1 ) + float2( 0,0 );
				float2 temp_output_1_0_g46 = texCoord9_g43;
				float2 break3_g46 = temp_output_1_0_g46;
				float4 appendResult5_g46 = (float4(break3_g46.y , break3_g46.x , 0.0 , 0.0));
				float4 lerpResult2_g46 = lerp( float4( temp_output_1_0_g46, 0.0 , 0.0 ) , appendResult5_g46 , _PanningTex_InvertUV);
				float2 temp_output_1_0_g47 = ( _PanningTex_ST.xy * float2( 1,1 ) );
				float2 break3_g47 = temp_output_1_0_g47;
				float4 appendResult5_g47 = (float4(break3_g47.y , break3_g47.x , 0.0 , 0.0));
				float4 lerpResult2_g47 = lerp( float4( temp_output_1_0_g47, 0.0 , 0.0 ) , appendResult5_g47 , _PanningTex_InvertUV);
				float2 panner1_g44 = ( ( floor( ( _TimeParameters.x * _TimeStep ) ) / _TimeStep ) * (lerpResult2_g45).xy + ( (lerpResult2_g46).xy * (lerpResult2_g47).xy ));
				float4 temp_output_7_0 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g43 ).xy + panner1_g44 ), ddx( texCoord9_g43 ), ddy( texCoord9_g43 ) );
				float4 break22 = temp_output_7_0;
				float temp_output_1_0_g53 = break22.g;
				float lerpResult9_g53 = lerp( temp_output_1_0_g53 , ( 1.0 - temp_output_1_0_g53 ) , _AlphaSharp_OneMinus);
				float smoothstepResult2_g53 = smoothstep( temp_output_7_0_g53 , ( temp_output_7_0_g53 + _AlphaSmoothness ) , lerpResult9_g53);
				
				surfaceDescription.Alpha = saturate( ( ( temp_output_8_0 * temp_output_12_0 ) - smoothstepResult2_g53 ) );
				surfaceDescription.AlphaClipThreshold = _AlphaCutoff;

				SurfaceData surfaceData;
				BuiltinData builtinData;
				GetSurfaceAndBuiltinData(surfaceDescription, input, V, posInput, surfaceData, builtinData);

				float4 VPASSpositionCS = float4(packedInput.vpassInterpolators0.xy, 0.0, packedInput.vpassInterpolators0.z);
				float4 VPASSpreviousPositionCS = float4(packedInput.vpassInterpolators1.xy, 0.0, packedInput.vpassInterpolators1.z);

				#ifdef _DEPTHOFFSET_ON
				VPASSpositionCS.w += builtinData.depthOffset;
				VPASSpreviousPositionCS.w += builtinData.depthOffset;
				#endif

				float2 motionVector = CalculateMotionVector( VPASSpositionCS, VPASSpreviousPositionCS );
				EncodeMotionVector( motionVector * 0.5, outMotionVector );

				bool forceNoMotion = unity_MotionVectorsParams.y == 0.0;
				if( forceNoMotion )
					outMotionVector = float4( 2.0, 0.0, 0.0, 0.0 );

				#ifdef WRITE_NORMAL_BUFFER
				EncodeIntoNormalBuffer( ConvertSurfaceDataToNormalData( surfaceData ), posInput.positionSS, outNormalBuffer );

				#ifdef WRITE_MSAA_DEPTH
				depthColor = packedInput.vmeshPositionCS.z;
				#endif
				#elif defined(WRITE_MSAA_DEPTH)
				outNormalBuffer = float4( 0.0, 0.0, 0.0, 1.0 );
				depthColor = packedInput.vmeshPositionCS.z;
				#endif

				#ifdef _DEPTHOFFSET_ON
				outputDepth = posInput.deviceDepth;
				#endif
			}

			ENDHLSL
		}

	
	}
	CustomEditor "Rendering.HighDefinition.HDUnlitGUI"
	Fallback "Hidden/InternalErrorShader"
	
}
/*ASEBEGIN
Version=18900
-24;594;1920;490;577.5358;-110.9972;1;True;False
Node;AmplifyShaderEditor.FunctionNode;70;-1992.239,-153.282;Inherit;False;SF_SteppedTime;10;;57;acd127cf8f86d3d47a5b97e56cc9ba1d;0;1;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;7;-1711.73,-117.4969;Inherit;False;SF_PanningTexture;0;;43;b045855c7f4c7344eb8723194efc0969;0;7;3;SAMPLER2D;;False;46;FLOAT;0;False;5;FLOAT2;0,0;False;8;FLOAT2;0,0;False;6;FLOAT2;0,0;False;14;FLOAT2;1,1;False;7;FLOAT2;0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;12;-419,331.5;Inherit;False;SF_EdgeFade;35;;49;2c737c027c7911941847cd940f44e2cc;0;1;8;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.BreakToComponentsNode;22;-1356.461,-269.6008;Inherit;False;COLOR;1;0;COLOR;0,0,0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.FunctionNode;8;-654,254.5;Inherit;False;SF_Fresnel;23;;48;328e81c82eef2f646b26eac37838dca5;0;1;12;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;379.1625,176.5;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;59;-516.4946,-109.0471;Inherit;False;SF_AlphaSharp;44;;53;1a46ba76a207bfe4e97ac05d03cb8401;0;2;1;FLOAT;0;False;6;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;44;633.5416,148.5258;Inherit;False;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;73;898.9553,444.1465;Inherit;False;Property;_Displacement;Displacement;18;0;Create;True;0;0;0;False;0;False;0.05;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.NormalVertexDataNode;71;657.2374,318.1226;Inherit;False;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;69;-737.5665,-429.1701;Inherit;False;SF_Flicker;12;;56;15da182ec18f26346a4a9baee3c35498;0;1;7;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;13;-408,427.5;Inherit;False;SF_DepthFade;40;;55;adc458ead34511148bae829420de626c;0;1;6;FLOAT3;0,0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;72;927.8789,318.1226;Inherit;False;3;3;0;FLOAT3;0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT3;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;32;-279.2944,111.1418;Inherit;False;4;4;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.FunctionNode;10;-253,-31.5;Inherit;False;SF_Bicolor;29;;54;8f1c0adb31a562646a4d2a8fec362420;0;3;9;COLOR;0,0,0,0;False;10;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;63;-338.7924,-315.8193;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;25;-296.0378,-517.9208;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ScreenPosInputsNode;30;-134.2944,70.14178;Float;False;0;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;27;-43.29443,-319.8582;Inherit;False;Property;_ShineColor;ShineColor;22;1;[HDR];Create;True;0;0;0;False;0;False;0.1568627,1,0.8610147,0;0.7490196,0.4121495,0.1215686,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;62;176.8003,-476.3558;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;45;784.1216,170.5013;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;23;-522.0378,-554.9208;Inherit;False;Property;_Shine_Threshold;Shine_Threshold;19;0;Create;True;0;0;0;False;0;False;0;0.7;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;28;551.7056,-147.8582;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.ScreenColorNode;29;307.9824,-16.94351;Inherit;False;Global;_GrabScreen0;Grab Screen 0;12;0;Create;True;0;0;0;False;0;False;Object;-1;False;False;False;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;26;343.7056,-443.8582;Inherit;False;2;2;0;FLOAT;0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.RangedFloatNode;33;-558.2944,113.1418;Inherit;False;Property;_Refraction;Refraction;21;0;Create;True;0;0;0;False;0;False;0;0.05;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;20;-76.20535,-571.167;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;35;-291.2944,-198.8582;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0.8;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;24;-529.0378,-455.9208;Inherit;False;Property;_Shine_Smoothness;Shine_Smoothness;20;0;Create;True;0;0;0;False;0;False;0.5;0.27;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;64;-555.7556,-333.4109;Inherit;False;Property;_Cracks_Emission;Cracks_Emission;43;1;[Header];Create;True;1;Cracks;0;0;False;0;False;0;5;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;96.70557,94.14178;Inherit;False;2;2;0;FLOAT4;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;0;1200.239,-24.76925;Float;False;True;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;13;S_State_Defense_Core;7f5cb9c3ea6481f469fdd856555439ef;True;Forward Unlit;0;0;Forward Unlit;9;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Transparent=Queue=-250;True;5;0;False;True;1;0;True;-20;0;True;-21;1;0;True;-22;0;True;-23;False;False;False;False;False;False;False;False;False;False;False;False;True;0;True;-26;False;False;False;False;False;False;False;False;False;True;True;0;True;-5;255;False;-1;255;True;-6;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;0;True;-24;True;0;True;-32;False;True;1;LightMode=ForwardOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;29;Surface Type;1;  Rendering Pass ;0;  Rendering Pass;0;  Blending Mode;0;  Receive Fog;1;  Distortion;0;    Distortion Mode;0;    Distortion Only;1;  Depth Write;0;  Cull Mode;0;  Depth Test;4;Double-Sided;0;Alpha Clipping;0;Motion Vectors;1;  Add Precomputed Velocity;0;Shadow Matte;0;Cast Shadows;1;DOTS Instancing;0;GPU Instancing;1;Tessellation;0;  Phong;0;  Strength;0.5,False,-1;  Type;0;  Tess;16,False,-1;  Min;10,False,-1;  Max;25,False,-1;  Edge Length;16,False,-1;  Max Displacement;25,False,-1;Vertex Position,InvertActionOnDeselection;1;0;7;True;True;True;True;True;True;False;False;;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;5;0,0;Float;False;False;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;1;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;Motion Vectors;0;5;Motion Vectors;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;True;-26;False;False;False;False;False;False;False;False;False;True;True;0;True;-9;255;False;-1;255;True;-10;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;False;False;True;1;LightMode=MotionVectors;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;3;0,0;Float;False;False;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;1;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;SceneSelectionPass;0;3;SceneSelectionPass;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;True;-26;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=SceneSelectionPass;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;2;0,0;Float;False;False;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;1;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;META;0;2;META;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;2;False;-1;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;1;LightMode=Meta;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;1;0,0;Float;False;False;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;1;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;ShadowCaster;0;1;ShadowCaster;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;True;-26;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;False;False;True;1;False;-1;False;False;True;1;LightMode=ShadowCaster;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;6;0,0;Float;False;False;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;1;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;DistortionVectors;0;6;DistortionVectors;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;True;4;1;False;-1;1;False;-1;4;1;False;-1;1;False;-1;True;1;False;-1;1;False;-1;False;False;False;False;False;False;False;False;False;False;False;True;0;True;-26;False;False;False;False;False;False;False;False;False;True;True;0;True;-11;255;False;-1;255;True;-12;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;2;False;-1;True;3;False;-1;False;True;1;LightMode=DistortionVectors;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
Node;AmplifyShaderEditor.TemplateMultiPassMasterNode;4;0,0;Float;False;False;-1;2;Rendering.HighDefinition.HDUnlitGUI;0;1;New Amplify Shader;7f5cb9c3ea6481f469fdd856555439ef;True;DepthForwardOnly;0;4;DepthForwardOnly;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;3;RenderPipeline=HDRenderPipeline;RenderType=Opaque=RenderType;Queue=Geometry=Queue=0;True;5;0;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;0;True;-26;False;True;False;False;False;False;0;False;-1;False;False;False;False;False;False;False;True;True;0;True;-7;255;False;-1;255;True;-8;7;False;-1;3;False;-1;1;False;-1;1;False;-1;7;False;-1;1;False;-1;1;False;-1;1;False;-1;False;True;1;False;-1;False;False;True;1;LightMode=DepthForwardOnly;False;0;Hidden/InternalErrorShader;0;0;Standard;0;False;0
WireConnection;7;46;70;0
WireConnection;22;0;7;0
WireConnection;11;0;8;0
WireConnection;11;1;12;0
WireConnection;59;1;22;1
WireConnection;44;0;11;0
WireConnection;44;1;59;0
WireConnection;69;7;22;2
WireConnection;72;0;71;0
WireConnection;72;1;69;0
WireConnection;72;2;73;0
WireConnection;32;0;7;0
WireConnection;32;1;33;0
WireConnection;32;2;12;0
WireConnection;32;3;13;0
WireConnection;10;1;22;0
WireConnection;63;0;22;1
WireConnection;63;1;64;0
WireConnection;63;2;69;0
WireConnection;25;0;23;0
WireConnection;25;1;24;0
WireConnection;62;0;20;0
WireConnection;62;1;63;0
WireConnection;45;0;44;0
WireConnection;28;0;10;0
WireConnection;28;1;29;0
WireConnection;28;2;26;0
WireConnection;29;0;31;0
WireConnection;26;0;62;0
WireConnection;26;1;27;0
WireConnection;20;0;35;0
WireConnection;20;1;23;0
WireConnection;20;2;25;0
WireConnection;35;0;22;0
WireConnection;35;1;8;0
WireConnection;31;0;30;0
WireConnection;31;1;32;0
WireConnection;0;0;28;0
WireConnection;0;2;45;0
WireConnection;0;6;72;0
ASEEND*/
//CHKSM=FA3DF885EB62C6664C6032D47B3B03E8934724A4