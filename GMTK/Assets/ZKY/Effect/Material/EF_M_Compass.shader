// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EF_M_Compass"
{
	Properties
	{
		_EF_T_Compass_Outer("EF_T_Compass_Outer", 2D) = "white" {}
		_UVControl1("UVControl1", Vector) = (0.9,1.19,0.05,-0.08)
		_EF_T_Noise_001("EF_T_Noise_001", 2D) = "white" {}
		[HDR]_OutColor("OutColor", Color) = (0.1981132,0.1934407,0.1934407,1)
		_EF_T_Gradient_001("EF_T_Gradient_001", 2D) = "white" {}
		_NoiseControl("NoiseControl", Vector) = (1,1,-0.2,0.05)
		_Range("Range", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Transparent+0" "IsEmissive" = "true"  }
		Cull Back
		Blend SrcAlpha OneMinusSrcAlpha
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _EF_T_Compass_Outer;
		uniform float4 _UVControl1;
		uniform float4 _OutColor;
		uniform sampler2D _EF_T_Noise_001;
		uniform float4 _NoiseControl;
		uniform sampler2D _EF_T_Gradient_001;
		uniform float _Range;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 appendResult20 = (float2(_UVControl1.x , _UVControl1.y));
			float2 appendResult21 = (float2(_UVControl1.z , _UVControl1.w));
			float2 uv_TexCoord14 = i.uv_texcoord * appendResult20 + appendResult21;
			float4 tex2DNode5 = tex2D( _EF_T_Compass_Outer, uv_TexCoord14 );
			float2 appendResult58 = (float2(_NoiseControl.z , _NoiseControl.w));
			float2 CenteredUV15_g2 = ( i.uv_texcoord - float2( 0.5,0.5 ) );
			float2 break17_g2 = CenteredUV15_g2;
			float2 appendResult23_g2 = (float2(( length( CenteredUV15_g2 ) * 1.0 * 2.0 ) , ( atan2( break17_g2.x , break17_g2.y ) * ( 1.0 / 6.28318548202515 ) * 1.0 )));
			float2 panner30 = ( 1.0 * _Time.y * appendResult58 + appendResult23_g2);
			float2 appendResult50 = (float2(_NoiseControl.x , _NoiseControl.y));
			float2 CenteredUV15_g3 = ( i.uv_texcoord - float2( 0.5,0.5 ) );
			float2 break17_g3 = CenteredUV15_g3;
			float2 appendResult23_g3 = (float2(( length( CenteredUV15_g3 ) * 1.0 * 2.0 ) , ( atan2( break17_g3.x , break17_g3.y ) * ( 1.0 / 6.28318548202515 ) * 1.0 )));
			o.Emission = ( tex2DNode5 + ( _OutColor * ( ( pow( tex2DNode5.r , 1.0 ) * tex2D( _EF_T_Noise_001, ( panner30 * appendResult50 ) ).r ) * pow( ( 1.0 - tex2D( _EF_T_Gradient_001, appendResult23_g3 ).r ) , _Range ) ) ) ).rgb;
			o.Alpha = tex2DNode5.a;
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
-1673;250;1538;913;2076.441;-603.1599;1;True;True
Node;AmplifyShaderEditor.Vector4Node;19;-1745.661,233.735;Inherit;False;Property;_UVControl1;UVControl1;2;0;Create;True;0;0;0;False;0;False;0.9,1.19,0.05,-0.08;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;49;-1478.318,1053.453;Inherit;False;Property;_NoiseControl;NoiseControl;7;0;Create;True;0;0;0;False;0;False;1,1,-0.2,0.05;1,1,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;51;-1730.729,810.1807;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;21;-1503.661,279.735;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;20;-1527.661,124.735;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.FunctionNode;31;-1434.732,812.7643;Inherit;False;Polar Coordinates;-1;;2;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;58;-1238.441,1137.16;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.DynamicAppendNode;50;-979.947,1112.754;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;30;-1109.662,963.8334;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;-0.2,0.05;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;14;-1368.5,83;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.77,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.FunctionNode;46;-1217.722,1267.117;Inherit;False;Polar Coordinates;-1;;3;7dab8e02884cf104ebefaa2e788e4162;0;4;1;FLOAT2;0,0;False;2;FLOAT2;0.5,0.5;False;3;FLOAT;1;False;4;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;55;-847.9889,969.1512;Inherit;False;2;2;0;FLOAT2;0,0;False;1;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;5;-593,272.5;Inherit;True;Property;_EF_T_Compass_Outer;EF_T_Compass_Outer;1;0;Create;True;0;0;0;False;0;False;-1;61dc5987f44ecc54696a34c05759b6fd;61dc5987f44ecc54696a34c05759b6fd;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;43;-878.7074,1238.43;Inherit;True;Property;_EF_T_Gradient_001;EF_T_Gradient_001;6;0;Create;True;0;0;0;False;0;False;-1;49780af9f61abd54a873c6ff3de84784;b34c9c14e62f2594990bb29a75d09651;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;29;-681.0453,954.122;Inherit;True;Property;_EF_T_Noise_001;EF_T_Noise_001;4;0;Create;True;0;0;0;False;0;False;-1;315f9cb4b8147ea4bae8c0c63f457177;315f9cb4b8147ea4bae8c0c63f457177;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;44;-546.9141,1257.51;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;57;-306.0908,1453.489;Inherit;False;Property;_Range;Range;8;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;41;-288.316,474.5946;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-267.9718,974.0014;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;45;-210.5141,1284.811;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;2;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;35;-542.7147,699.3322;Inherit;False;Property;_OutColor;OutColor;5;1;[HDR];Create;True;0;0;0;False;0;False;0.1981132,0.1934407,0.1934407,1;0.1981132,0.1934407,0.1934407,1;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;42;2.451522,997.4489;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;38;225.3881,701.7029;Inherit;True;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;37;329.0526,450.9341;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.OneMinusNode;53;-313.0796,703.5445;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;25;-840.8665,595.4799;Inherit;True;Property;_EF_T_Compass_Outer_M;EF_T_Compass_Outer_M;3;0;Create;True;0;0;0;False;0;False;-1;6564e8d60c10b3c44b7f3e29c526150a;6564e8d60c10b3c44b7f3e29c526150a;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PowerNode;54;-103.0988,721.7749;Inherit;True;False;2;0;FLOAT;0;False;1;FLOAT;5;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;668.0961,157.464;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;EF_M_Compass;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;True;0;False;Custom;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;0;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;21;0;19;3
WireConnection;21;1;19;4
WireConnection;20;0;19;1
WireConnection;20;1;19;2
WireConnection;31;1;51;0
WireConnection;58;0;49;3
WireConnection;58;1;49;4
WireConnection;50;0;49;1
WireConnection;50;1;49;2
WireConnection;30;0;31;0
WireConnection;30;2;58;0
WireConnection;14;0;20;0
WireConnection;14;1;21;0
WireConnection;55;0;30;0
WireConnection;55;1;50;0
WireConnection;5;1;14;0
WireConnection;43;1;46;0
WireConnection;29;1;55;0
WireConnection;44;0;43;1
WireConnection;41;0;5;1
WireConnection;27;0;41;0
WireConnection;27;1;29;1
WireConnection;45;0;44;0
WireConnection;45;1;57;0
WireConnection;42;0;27;0
WireConnection;42;1;45;0
WireConnection;38;0;35;0
WireConnection;38;1;42;0
WireConnection;37;0;5;0
WireConnection;37;1;38;0
WireConnection;53;0;5;1
WireConnection;54;0;53;0
WireConnection;0;2;37;0
WireConnection;0;9;5;4
ASEEND*/
//CHKSM=A363079B53BA725FC41592C881212FE14E95CE1D