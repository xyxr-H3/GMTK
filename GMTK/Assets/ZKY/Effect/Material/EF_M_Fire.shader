// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EF_M_Fire"
{
	Properties
	{
		_Noise("Noise", 2D) = "white" {}
		_Gradient("Gradient", 2D) = "white" {}
		_WrapTex("WrapTex", 2D) = "white" {}
		_InnerColor("InnerColor", Color) = (0.8773585,0.3773142,0.120016,0)
		_OuterColor("OuterColor", Color) = (0.7924528,0.1981132,0.286535,0)
		_EmissionIntensity("Emission Intensity", Float) = 1
		_FlowSpeed("Flow Speed", Vector) = (0.15,-1,0,0)
		_Edge("Edge", Range( 0 , 1)) = 0.3046266
		_MaskHeight("Mask Height", Range( -1 , 1)) = 0.4370175
		_FireShape("FireShape", 2D) = "white" {}
		_WrapIntensity("WrapIntensity", Range( 0 , 0.4)) = 0.1552941
		_WrapSpeed("WrapSpeed", Vector) = (0.4,0.1,0,0)
		_NoiseUV("NoiseUV", Vector) = (0,0,0,0)
		_BasePower("BasePower", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Transparent"  "Queue" = "Transparent+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		CGINCLUDE
		#include "UnityShaderVariables.cginc"
		#include "UnityPBSLighting.cginc"
		#include "Lighting.cginc"
		#pragma target 3.0
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform float4 _OuterColor;
		uniform float4 _InnerColor;
		uniform sampler2D _Noise;
		uniform float2 _FlowSpeed;
		uniform float2 _NoiseUV;
		uniform float _Edge;
		uniform sampler2D _Gradient;
		uniform float _MaskHeight;
		uniform sampler2D _FireShape;
		uniform sampler2D _WrapTex;
		uniform float2 _WrapSpeed;
		uniform float _WrapIntensity;
		uniform float _BasePower;
		uniform float _EmissionIntensity;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float2 uv_TexCoord16 = i.uv_texcoord * _NoiseUV;
			float2 panner22 = ( 1.0 * _Time.y * _FlowSpeed + uv_TexCoord16);
			float Noise27 = tex2D( _Noise, panner22 ).r;
			float clampResult6 = clamp( ( 1.0 - tex2D( _Gradient, i.uv_texcoord ).r ) , 0.0 , 1.0 );
			float Gradient12 = ( clampResult6 - _MaskHeight );
			float smoothstepResult34 = smoothstep( Noise27 , ( Noise27 + _Edge ) , Gradient12);
			float2 uv_TexCoord9 = i.uv_texcoord * float2( 0.5,0.5 );
			float2 panner11 = ( 1.0 * _Time.y * _WrapSpeed + uv_TexCoord9);
			float2 appendResult25 = (float2(( i.uv_texcoord.x + ( (tex2D( _WrapTex, panner11 ).r*2.0 + -1.0) * _WrapIntensity * ( 1.0 - Gradient12 ) ) ) , i.uv_texcoord.y));
			float4 tex2DNode29 = tex2D( _FireShape, appendResult25 );
			float FireShape32 = ( tex2DNode29.r * tex2DNode29.a );
			float Opacity38 = ( smoothstepResult34 * FireShape32 );
			float4 lerpResult43 = lerp( _OuterColor , _InnerColor , pow( Opacity38 , _BasePower ));
			float4 Color45 = ( lerpResult43 * _EmissionIntensity );
			o.Emission = Color45.rgb;
			o.Alpha = Opacity38;
		}

		ENDCG
		CGPROGRAM
		#pragma surface surf Unlit alpha:fade keepalpha fullforwardshadows 

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
-1776;270;1538;920;2192.068;628.4337;1;True;True
Node;AmplifyShaderEditor.CommentaryNode;1;-3716.273,-662.8083;Inherit;False;1694.562;776.9368;Basic;12;27;23;22;16;15;12;10;7;6;4;3;2;Basic;1,0.2464419,0,1;0;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-3584.223,-603.6221;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;3;-3277.161,-612.8081;Inherit;True;Property;_Gradient;Gradient;1;0;Create;True;0;0;0;False;0;False;-1;b34c9c14e62f2594990bb29a75d09651;b34c9c14e62f2594990bb29a75d09651;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.OneMinusNode;4;-2922.161,-593.8081;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.ClampOpNode;6;-2702.848,-548.9714;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;7;-2857.581,-340.9804;Inherit;False;Property;_MaskHeight;Mask Height;8;0;Create;True;0;0;0;False;0;False;0.4370175;0.37;-1;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;5;-4306.045,479.2206;Inherit;False;2393.846;871.0668;FireShape;15;32;29;25;24;21;20;19;18;17;14;13;11;9;8;48;FireShape;1,1,1,1;0;0
Node;AmplifyShaderEditor.SimpleSubtractOpNode;10;-2515.284,-483.7727;Inherit;True;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;8;-4256.045,859.3354;Inherit;False;Property;_WrapSpeed;WrapSpeed;11;0;Create;True;0;0;0;False;0;False;0.4,0.1;0.2,-0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-4251.226,695.4318;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;0.5,0.5;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;11;-3883.927,758.3612;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;12;-2245.711,-501.6484;Inherit;False;Gradient;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;14;-3619.725,696.2858;Inherit;True;Property;_WrapTex;WrapTex;2;0;Create;True;0;0;0;False;0;False;-1;a25d4884c9bd70c4f91774968f2e9e41;37c7ecd5c2af9f241ac800b3aef22aa6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;13;-3442.227,1120.287;Inherit;True;12;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.OneMinusNode;17;-3175.998,1079.72;Inherit;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-3412.892,975.8268;Inherit;False;Property;_WrapIntensity;WrapIntensity;10;0;Create;True;0;0;0;False;0;False;0.1552941;0.339;0;0.4;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;49;-3853.286,-108.3649;Inherit;False;Property;_NoiseUV;NoiseUV;12;0;Create;True;0;0;0;False;0;False;0,0;0.5,0.5;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.ScaleAndOffsetNode;19;-3294.877,746.2002;Inherit;False;3;0;FLOAT;0;False;1;FLOAT;2;False;2;FLOAT;-1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;20;-3094.914,801.8473;Inherit;False;3;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;16;-3666.273,-255.2793;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector2Node;15;-3560.944,-49.87085;Inherit;False;Property;_FlowSpeed;Flow Speed;6;0;Create;True;0;0;0;False;0;False;0.15,-1;0.15,-1;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.TextureCoordinatesNode;21;-3385.024,526.9376;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;22;-3324.944,-119.6521;Inherit;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;24;-3041.929,556.575;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;23;-3090.614,-147.2213;Inherit;True;Property;_Noise;Noise;0;0;Create;True;0;0;0;False;0;False;-1;315f9cb4b8147ea4bae8c0c63f457177;37c7ecd5c2af9f241ac800b3aef22aa6;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.DynamicAppendNode;25;-2854.778,580.5255;Inherit;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;26;-1676.087,347.1495;Inherit;False;1167.936;630.5809;Opacity;8;38;36;35;34;33;31;30;28;Opacity;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;27;-2670.837,-180.0926;Inherit;False;Noise;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SamplerNode;29;-2645.918,544.408;Inherit;True;Property;_FireShape;FireShape;9;0;Create;True;0;0;0;False;0;False;-1;469c39ec74d67d6479e142281e00ed34;e63eca92819097642afbd0cb47444f82;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;28;-1652.104,841.6331;Inherit;False;Property;_Edge;Edge;7;0;Create;True;0;0;0;False;0;False;0.3046266;0.562;0;1;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;30;-1633.309,545.5967;Inherit;True;27;Noise;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;48;-2247.288,595.6965;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;33;-1431.478,476.3272;Inherit;False;12;Gradient;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;32;-2101.708,589.2814;Inherit;True;FireShape;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleAddOpNode;31;-1367.453,731.1209;Inherit;True;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;35;-1059.375,790.234;Inherit;False;32;FireShape;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.SmoothstepOpNode;34;-1144.033,517.1935;Inherit;True;3;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;36;-866.2715,628.1494;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.CommentaryNode;37;-1761.716,-593.4504;Inherit;False;1093.768;616.9246;Color;9;45;44;43;42;41;40;39;51;50;Color;0.9622642,0.7118753,0.1770203,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;38;-698.0517,552.3282;Inherit;True;Opacity;-1;True;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;51;-1543.068,-54.43372;Inherit;False;Property;_BasePower;BasePower;13;0;Create;True;0;0;0;False;0;False;1;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.GetLocalVarNode;39;-1725.057,-178.7772;Inherit;True;38;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.PowerNode;50;-1432.068,-153.4337;Inherit;False;False;2;0;FLOAT;0;False;1;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.ColorNode;40;-1711.716,-357.2585;Inherit;False;Property;_InnerColor;InnerColor;3;0;Create;True;0;0;0;False;0;False;0.8773585,0.3773142,0.120016,0;1,0.4352806,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;41;-1704.769,-543.4504;Inherit;False;Property;_OuterColor;OuterColor;4;0;Create;True;0;0;0;False;0;False;0.7924528,0.1981132,0.286535,0;0.9882353,0.09803916,0.269326,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;42;-1279.598,-92.52595;Inherit;False;Property;_EmissionIntensity;Emission Intensity;5;0;Create;True;0;0;0;False;0;False;1;2;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.LerpOp;43;-1384.159,-385.1145;Inherit;True;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;44;-1086.044,-175.3494;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;FLOAT;0;False;1;COLOR;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;45;-885.9484,-177.9876;Inherit;False;Color;-1;True;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;46;-355.3367,23.94775;Inherit;True;45;Color;1;0;OBJECT;;False;1;COLOR;0
Node;AmplifyShaderEditor.GetLocalVarNode;47;-347.3367,345.9478;Inherit;False;38;Opacity;1;0;OBJECT;;False;1;FLOAT;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;EF_M_Fire;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Transparent;0.5;True;True;0;False;Transparent;;Transparent;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;True;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;1;2;0
WireConnection;4;0;3;1
WireConnection;6;0;4;0
WireConnection;10;0;6;0
WireConnection;10;1;7;0
WireConnection;11;0;9;0
WireConnection;11;2;8;0
WireConnection;12;0;10;0
WireConnection;14;1;11;0
WireConnection;17;0;13;0
WireConnection;19;0;14;1
WireConnection;20;0;19;0
WireConnection;20;1;18;0
WireConnection;20;2;17;0
WireConnection;16;0;49;0
WireConnection;22;0;16;0
WireConnection;22;2;15;0
WireConnection;24;0;21;1
WireConnection;24;1;20;0
WireConnection;23;1;22;0
WireConnection;25;0;24;0
WireConnection;25;1;21;2
WireConnection;27;0;23;1
WireConnection;29;1;25;0
WireConnection;48;0;29;1
WireConnection;48;1;29;4
WireConnection;32;0;48;0
WireConnection;31;0;30;0
WireConnection;31;1;28;0
WireConnection;34;0;33;0
WireConnection;34;1;30;0
WireConnection;34;2;31;0
WireConnection;36;0;34;0
WireConnection;36;1;35;0
WireConnection;38;0;36;0
WireConnection;50;0;39;0
WireConnection;50;1;51;0
WireConnection;43;0;41;0
WireConnection;43;1;40;0
WireConnection;43;2;50;0
WireConnection;44;0;43;0
WireConnection;44;1;42;0
WireConnection;45;0;44;0
WireConnection;0;2;46;0
WireConnection;0;9;47;0
ASEEND*/
//CHKSM=B41FABA7C5A67A91F5BA3BF89048A37653A65832