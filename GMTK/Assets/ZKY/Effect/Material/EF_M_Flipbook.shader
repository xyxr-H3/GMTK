// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "EF_M_Flipbook"
{
	Properties
	{
		_Flipbook("Flipbook", 2D) = "white" {}
		_Opacity("Opacity", Float) = 1
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] _tex4coord2( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Custom"  "Queue" = "Geometry+0" "IgnoreProjector" = "True" "IsEmissive" = "true"  }
		Cull Back
		ZWrite Off
		Blend SrcAlpha OneMinusSrcAlpha
		CGPROGRAM
		#pragma target 3.0
		#pragma surface surf Unlit keepalpha noshadow 
		#undef TRANSFORM_TEX
		#define TRANSFORM_TEX(tex,name) float4(tex.xy * name##_ST.xy + name##_ST.zw, tex.z, tex.w)
		struct Input
		{
			float4 vertexColor : COLOR;
			float2 uv_texcoord;
			float4 uv2_tex4coord2;
		};

		uniform sampler2D _Flipbook;
		uniform float _Opacity;

		inline half4 LightingUnlit( SurfaceOutput s, half3 lightDir, half atten )
		{
			return half4 ( 0, 0, 0, s.Alpha );
		}

		void surf( Input i , inout SurfaceOutput o )
		{
			float4 color5 = IsGammaSpace() ? float4(1,1,1,1) : float4(1,1,1,1);
			o.Emission = saturate( ( color5 * i.vertexColor ) ).rgb;
			float4 _Vector0 = float4(1,8,60,0);
			float4 uv2s4_TexCoord9 = i.uv2_tex4coord2;
			uv2s4_TexCoord9.xy = i.uv2_tex4coord2.xy * float2( 1,8 );
			// *** BEGIN Flipbook UV Animation vars ***
			// Total tiles of Flipbook Texture
			float fbtotaltiles3 = _Vector0.x * _Vector0.y;
			// Offsets for cols and rows of Flipbook Texture
			float fbcolsoffset3 = 1.0f / _Vector0.x;
			float fbrowsoffset3 = 1.0f / _Vector0.y;
			// Speed of animation
			float fbspeed3 = 0.0 * _Vector0.z;
			// UV Tiling (col and row offset)
			float2 fbtiling3 = float2(fbcolsoffset3, fbrowsoffset3);
			// UV Offset - calculate current tile linear index, and convert it to (X * coloffset, Y * rowoffset)
			// Calculate current tile linear index
			float fbcurrenttileindex3 = round( fmod( fbspeed3 + uv2s4_TexCoord9.x, fbtotaltiles3) );
			fbcurrenttileindex3 += ( fbcurrenttileindex3 < 0) ? fbtotaltiles3 : 0;
			// Obtain Offset X coordinate from current tile linear index
			float fblinearindextox3 = round ( fmod ( fbcurrenttileindex3, _Vector0.x ) );
			// Multiply Offset X by coloffset
			float fboffsetx3 = fblinearindextox3 * fbcolsoffset3;
			// Obtain Offset Y coordinate from current tile linear index
			float fblinearindextoy3 = round( fmod( ( fbcurrenttileindex3 - fblinearindextox3 ) / _Vector0.x, _Vector0.y ) );
			// Reverse Y to get tiles from Top to Bottom
			fblinearindextoy3 = (int)(_Vector0.y-1) - fblinearindextoy3;
			// Multiply Offset Y by rowoffset
			float fboffsety3 = fblinearindextoy3 * fbrowsoffset3;
			// UV Offset
			float2 fboffset3 = float2(fboffsetx3, fboffsety3);
			// Flipbook UV
			half2 fbuv3 = i.uv_texcoord * fbtiling3 + fboffset3;
			// *** END Flipbook UV Animation vars ***
			o.Alpha = saturate( ( ( i.vertexColor.a * tex2D( _Flipbook, fbuv3 ).a ) * _Opacity ) );
		}

		ENDCG
	}
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=18912
-1641;269;1538;920;867.2437;295.0925;1;True;True
Node;AmplifyShaderEditor.TextureCoordinatesNode;9;-1236.171,691.782;Inherit;False;1;-1;4;3;2;SAMPLER2D;;False;0;FLOAT2;1,8;False;1;FLOAT2;0,0;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector4Node;7;-1187.171,462.782;Inherit;False;Constant;_Vector0;Vector 0;1;0;Create;True;0;0;0;False;0;False;1,8,60,0;0,0,0,0;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.TextureCoordinatesNode;2;-1185.171,319.782;Inherit;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;1,1;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.RangedFloatNode;10;-919.1708,603.782;Inherit;False;Constant;_Float0;Float 0;1;0;Create;True;0;0;0;False;0;False;0;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.TFHCFlipBookUVAnimation;3;-892.1708,328.782;Inherit;False;0;0;6;0;FLOAT2;0,0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.VertexColorNode;13;-644.1708,90.78201;Inherit;False;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;4;-500.1708,316.782;Inherit;True;Property;_Flipbook;Flipbook;1;0;Create;True;0;0;0;False;0;False;-1;46adb0dd06d74b340b88964cd431a229;None;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;8;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;6;FLOAT;0;False;7;SAMPLERSTATE;;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.ColorNode;5;-668.1708,-111.218;Inherit;False;Constant;_Color0;Color 0;1;0;Create;True;0;0;0;False;0;False;1,1,1,1;0,0,0,0;True;0;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;23;-134.1708,355.782;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.RangedFloatNode;26;-121.2437,521.9075;Inherit;False;Property;_Opacity;Opacity;2;0;Create;True;0;0;0;False;0;False;1;0;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;12;-385.1708,-76.21799;Inherit;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;24;39.75635,360.9075;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-986.1708,703.782;Inherit;False;2;2;0;FLOAT;0;False;1;FLOAT;7;False;1;FLOAT;0
Node;AmplifyShaderEditor.CosTime;8;-1001.171,816.782;Inherit;False;0;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SaturateNode;27;161.7563,428.9075;Inherit;False;1;0;FLOAT;0;False;1;FLOAT;0
Node;AmplifyShaderEditor.SaturateNode;28;35.75635,53.90747;Inherit;False;1;0;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;22;303,136;Float;False;True;-1;2;ASEMaterialInspector;0;0;Unlit;EF_M_Flipbook;False;False;False;False;False;False;False;False;False;False;False;False;False;False;True;False;False;False;False;False;False;Back;2;False;-1;0;False;-1;False;0;False;-1;0;False;-1;False;0;Custom;0.5;True;False;0;False;Custom;;Geometry;All;18;all;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;False;2;15;10;25;False;0.5;False;2;5;False;-1;10;False;-1;0;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;-1;0;False;0;0;False;-1;-1;0;False;-1;0;0;0;False;0.1;False;-1;0;False;-1;False;15;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;3;0;2;0
WireConnection;3;1;7;1
WireConnection;3;2;7;2
WireConnection;3;3;7;3
WireConnection;3;4;9;1
WireConnection;3;5;10;0
WireConnection;4;1;3;0
WireConnection;23;0;13;4
WireConnection;23;1;4;4
WireConnection;12;0;5;0
WireConnection;12;1;13;0
WireConnection;24;0;23;0
WireConnection;24;1;26;0
WireConnection;11;0;9;1
WireConnection;27;0;24;0
WireConnection;28;0;12;0
WireConnection;22;2;28;0
WireConnection;22;9;27;0
ASEEND*/
//CHKSM=25FCEA61CF2EB3A4F8E0C1E37F0A774D007AA06D