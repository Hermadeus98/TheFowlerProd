// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "Trail"
{
	Properties
	{
		[Header(Distort Texture)]_DistortTex("DistortTex", 2D) = "white" {}
		_Distort_Threshold("Distort_Threshold", Float) = 0
		_Distort_Smoothness("Distort_Smoothness", Float) = 0.5
		_DistortStrength("DistortStrength", Float) = 0.2
		[Header(Panning Texture)]_PanningTex("PanningTex", 2D) = "white" {}
		[Toggle]_PanningTex_InvertUV("PanningTex_InvertUV", Float) = 0
		_PanningTex_ManualOffset("PanningTex_ManualOffset", Float) = 0
		[Toggle][Header(EdgeFade)]_EdgeFade_Enable("EdgeFade_Enable", Float) = 1
		_EdgeFadeU("EdgeFadeU", Vector) = (0,0,0,0)
		_EdgeFadeV("EdgeFadeV", Vector) = (0,0,0,0)
		_AddedEdgeFade("AddedEdgeFade", Float) = 0
		[HDR][Header(Bicolor)][HideIf(_NOBICOLOR_ON)]_ColorA("ColorA", Color) = (0,0,0,1)
		[HDR][HideIf(_NOBICOLOR_ON)]_ColorB("ColorB", Color) = (1,1,1,1)
		[HideIf(_NOBICOLOR_ON)]_BicolorThreshold("BicolorThreshold", Float) = 0
		[HideIf(_NOBICOLOR_ON)]_BicolorSmoothness("BicolorSmoothness", Float) = 1
		[Toggle]_Bicolor_OneMinus("Bicolor_OneMinus", Float) = 0
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend One One
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
			float4 vertexColor : COLOR;
		};

		uniform float4 _ColorA;
		uniform float4 _ColorB;
		uniform float _BicolorThreshold;
		uniform float _BicolorSmoothness;
		uniform sampler2D _PanningTex;
		uniform float _PanningTex_ManualOffset;
		uniform float _PanningTex_InvertUV;
		SamplerState sampler_PanningTex;
		uniform float4 _PanningTex_ST;
		uniform float _Distort_Threshold;
		uniform float _Distort_Smoothness;
		uniform sampler2D _DistortTex;
		SamplerState sampler_DistortTex;
		uniform float4 _DistortTex_ST;
		uniform float _DistortStrength;
		uniform float _Bicolor_OneMinus;
		uniform float4 _EdgeFadeU;
		uniform float _AddedEdgeFade;
		uniform float4 _EdgeFadeV;
		uniform float _EdgeFade_Enable;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 appendResult43_g1 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
			float4 appendResult42_g1 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
			float4 lerpResult41_g1 = lerp( appendResult43_g1 , appendResult42_g1 , _PanningTex_InvertUV);
			float2 temp_output_1_0_g28 = _PanningTex_ST.zw;
			float2 break3_g28 = temp_output_1_0_g28;
			float4 appendResult5_g28 = (float4(break3_g28.y , break3_g28.x , 0.0 , 0.0));
			float4 lerpResult2_g28 = lerp( float4( temp_output_1_0_g28, 0.0 , 0.0 ) , appendResult5_g28 , _PanningTex_InvertUV);
			float4 temp_cast_3 = (_Distort_Threshold).xxxx;
			float4 temp_cast_4 = (( _Distort_Threshold + _Distort_Smoothness )).xxxx;
			float4 appendResult43_g35 = (float4(0.0 , _PanningTex_ManualOffset , 0.0 , 0.0));
			float4 appendResult42_g35 = (float4(_PanningTex_ManualOffset , 0.0 , 0.0 , 0.0));
			float4 lerpResult41_g35 = lerp( appendResult43_g35 , appendResult42_g35 , _PanningTex_InvertUV);
			float2 temp_output_1_0_g36 = _DistortTex_ST.zw;
			float2 break3_g36 = temp_output_1_0_g36;
			float4 appendResult5_g36 = (float4(break3_g36.y , break3_g36.x , 0.0 , 0.0));
			float4 lerpResult2_g36 = lerp( float4( temp_output_1_0_g36, 0.0 , 0.0 ) , appendResult5_g36 , _PanningTex_InvertUV);
			float2 temp_output_1_0_g37 = i.uv_texcoord;
			float2 break3_g37 = temp_output_1_0_g37;
			float4 appendResult5_g37 = (float4(break3_g37.y , break3_g37.x , 0.0 , 0.0));
			float4 lerpResult2_g37 = lerp( float4( temp_output_1_0_g37, 0.0 , 0.0 ) , appendResult5_g37 , _PanningTex_InvertUV);
			float2 temp_output_1_0_g38 = ( _DistortTex_ST.xy * float2( 1,1 ) );
			float2 break3_g38 = temp_output_1_0_g38;
			float4 appendResult5_g38 = (float4(break3_g38.y , break3_g38.x , 0.0 , 0.0));
			float4 lerpResult2_g38 = lerp( float4( temp_output_1_0_g38, 0.0 , 0.0 ) , appendResult5_g38 , _PanningTex_InvertUV);
			float2 panner1_g39 = ( 1.0 * _Time.y * (lerpResult2_g36).xy + ( (lerpResult2_g37).xy * (lerpResult2_g38).xy ));
			float4 tex2DNode2_g35 = tex2D( _DistortTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g35 ).xy + panner1_g39 ), ddx( i.uv_texcoord ), ddy( i.uv_texcoord ) );
			float4 smoothstepResult3_g34 = smoothstep( temp_cast_3 , temp_cast_4 , tex2DNode2_g35);
			float2 temp_output_1_0_g29 = ( i.uv_texcoord + ( (( smoothstepResult3_g34 * ( _DistortStrength + 0.0 ) * 1.0 )).r * ( 1.0 - i.vertexColor.a ) ) );
			float2 break3_g29 = temp_output_1_0_g29;
			float4 appendResult5_g29 = (float4(break3_g29.y , break3_g29.x , 0.0 , 0.0));
			float4 lerpResult2_g29 = lerp( float4( temp_output_1_0_g29, 0.0 , 0.0 ) , appendResult5_g29 , _PanningTex_InvertUV);
			float2 temp_output_1_0_g30 = ( _PanningTex_ST.xy * float2( 1,1 ) );
			float2 break3_g30 = temp_output_1_0_g30;
			float4 appendResult5_g30 = (float4(break3_g30.y , break3_g30.x , 0.0 , 0.0));
			float4 lerpResult2_g30 = lerp( float4( temp_output_1_0_g30, 0.0 , 0.0 ) , appendResult5_g30 , _PanningTex_InvertUV);
			float2 panner1_g31 = ( 1.0 * _Time.y * (lerpResult2_g28).xy + ( (lerpResult2_g29).xy * (lerpResult2_g30).xy ));
			float4 tex2DNode2_g1 = tex2D( _PanningTex, ( ( float4( float2( 0,0 ), 0.0 , 0.0 ) + lerpResult41_g1 ).xy + panner1_g31 ), ddx( i.uv_texcoord ), ddy( i.uv_texcoord ) );
			float smoothstepResult5_g32 = smoothstep( _BicolorThreshold , ( _BicolorThreshold + _BicolorSmoothness ) , tex2DNode2_g1.r);
			float lerpResult12_g32 = lerp( smoothstepResult5_g32 , ( 1.0 - smoothstepResult5_g32 ) , _Bicolor_OneMinus);
			float4 lerpResult4_g32 = lerp( _ColorA , _ColorB , lerpResult12_g32);
			float2 break9_g33 = frac( i.uv_texcoord );
			float smoothstepResult3_g33 = smoothstep( _EdgeFadeU.x , ( _AddedEdgeFade + _EdgeFadeU.y ) , break9_g33.x);
			float smoothstepResult4_g33 = smoothstep( _EdgeFadeU.z , ( _EdgeFadeU.w + _AddedEdgeFade ) , ( 1.0 - break9_g33.x ));
			float smoothstepResult5_g33 = smoothstep( _EdgeFadeV.x , ( _EdgeFadeV.y + _AddedEdgeFade ) , break9_g33.y);
			float smoothstepResult6_g33 = smoothstep( _EdgeFadeV.z , ( _EdgeFadeV.w + _AddedEdgeFade ) , ( 1.0 - break9_g33.y ));
			float lerpResult14_g33 = lerp( 1.0 , ( smoothstepResult3_g33 * smoothstepResult4_g33 * smoothstepResult5_g33 * smoothstepResult6_g33 ) , _EdgeFade_Enable);
			float temp_output_5_0 = ( lerpResult14_g33 * i.vertexColor.a );
			o.Emission = ( lerpResult4_g32 * temp_output_5_0 * i.vertexColor ).rgb;
			o.Alpha = temp_output_5_0;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit keepalpha fullforwardshadows 

		ENDCG
		Pass
		{
			Name "ShadowCaster"
			Tags{ "LightMode" = "ShadowCaster" }
			ZWrite On
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			#pragma target 3.0
			#pragma multi_compile_shadowcaster
			#pragma multi_compile UNITY_PASS_SHADOWCASTER
			#pragma skip_variants FOG_LINEAR FOG_EXP FOG_EXP2
			#include "HLSLSupport.cginc"
			#if ( SHADER_API_D3D11 || SHADER_API_GLCORE || SHADER_API_GLES || SHADER_API_GLES3 || SHADER_API_METAL || SHADER_API_VULKAN )
				#define CAN_SKIP_VPOS
			#endif
			#include "UnityCG.cginc"
			#include "Lighting.cginc"
			#include "UnityPBSLighting.cginc"
			sampler3D _DitherMaskLOD;
			struct v2f
			{
				V2F_SHADOW_CASTER;
				float2 customPack1 : TEXCOORD1;
				float3 worldPos : TEXCOORD2;
				half4 color : COLOR0;
				UNITY_VERTEX_INPUT_INSTANCE_ID
				UNITY_VERTEX_OUTPUT_STEREO
			};
			v2f vert( appdata_full v )
			{
				v2f o;
				UNITY_SETUP_INSTANCE_ID( v );
				UNITY_INITIALIZE_OUTPUT( v2f, o );
				UNITY_INITIALIZE_VERTEX_OUTPUT_STEREO( o );
				UNITY_TRANSFER_INSTANCE_ID( v, o );
				Input customInputData;
				float3 worldPos = mul( unity_ObjectToWorld, v.vertex ).xyz;
				half3 worldNormal = UnityObjectToWorldNormal( v.normal );
				o.customPack1.xy = customInputData.uv_texcoord;
				o.customPack1.xy = v.texcoord;
				o.worldPos = worldPos;
				TRANSFER_SHADOW_CASTER_NORMALOFFSET( o )
				o.color = v.color;
				return o;
			}
			half4 frag( v2f IN
			#if !defined( CAN_SKIP_VPOS )
			, UNITY_VPOS_TYPE vpos : VPOS
			#endif
			) : SV_Target
			{
				UNITY_SETUP_INSTANCE_ID( IN );
				Input surfIN;
				UNITY_INITIALIZE_OUTPUT( Input, surfIN );
				surfIN.uv_texcoord = IN.customPack1.xy;
				float3 worldPos = IN.worldPos;
				half3 worldViewDir = normalize( UnityWorldSpaceViewDir( worldPos ) );
				surfIN.vertexColor = IN.color;
				SurfaceOutput o;
				UNITY_INITIALIZE_OUTPUT( SurfaceOutput, o )
				surf( surfIN, o );
				#if defined( CAN_SKIP_VPOS )
				float2 vpos = IN.pos;
				#endif
				half alphaRef = tex3D( _DitherMaskLOD, float3( vpos.xy * 0.25, o.Alpha * 0.9375 ) ).a;
				clip( alphaRef - 0.01 );
				SHADOW_CASTER_FRAGMENT( IN )
			}
			ENDCG
		}
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
1920;0;1920;1019;1388;501.5;1;True;True
Node;AmplifyShaderEditor.VertexColorNode;2;-915,92.5;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;9;-947,-339.5;Inherit;False;SF_DistortTexture;1;;34;96c99f2862d31594fbaa887784570dd8;0;3;16;FLOAT;0;False;17;FLOAT;1;False;11;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;11;-687,41.5;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;10;-623,-318.5;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;7;-836,-185.5;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleAddOpNode;8;-496,-196.5;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;4;-370,74.5;Inherit;False;SF_EdgeFade;30;;33;2c737c027c7911941847cd940f44e2cc;0;1;8;FLOAT2;0,0;False;1;FLOAT;0
Node;AmplifyShaderEditor.FunctionNode;1;-399,-188.5;Inherit;False;SF_PanningTexture;18;;1;b045855c7f4c7344eb8723194efc0969;0;6;3;SAMPLER2D;;False;5;FLOAT2;0,0;False;8;FLOAT2;0,0;False;6;FLOAT2;0,0;False;14;FLOAT2;1,1;False;7;FLOAT2;0,0;False;2;COLOR;0;FLOAT;22
Node;AmplifyShaderEditor.FunctionNode;3;-120,-214.5;Inherit;False;SF_Bicolor;35;;32;8f1c0adb31a562646a4d2a8fec362420;0;3;9;COLOR;0,0,0,0;False;10;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;5;-126,122.5;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;6;154,-35.5;Inherit;False;3;3;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;502,-91;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;Trail;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;4;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;11;0;2;4
WireConnection;10;0;9;0
WireConnection;10;1;11;0
WireConnection;8;0;7;0
WireConnection;8;1;10;0
WireConnection;1;5;8;0
WireConnection;3;1;1;0
WireConnection;5;0;4;0
WireConnection;5;1;2;4
WireConnection;6;0;3;0
WireConnection;6;1;5;0
WireConnection;6;2;2;0
WireConnection;0;2;6;0
WireConnection;0;9;5;0
ASEEND*/
//CHKSM=DF5EA04D4EE47242126519102A718B96BC17BE12