// Made with Amplify Shader Editor
// Available at the Unity Asset Store - http://u3d.as/y3X 
Shader "My shaders/Lava"
{
	Properties
	{
		_EdgeLength ( "Edge length", Range( 2, 50 ) ) = 2
		_MainTexture("Main Texture", 2D) = "white" {}
		_VertexOffset("VertexOffset", Vector) = (0,0,0,0)
		_Noise("Noise", 2D) = "white" {}
		_Flowmap("Flowmap", 2D) = "white" {}
		_Tiling("Tiling", Float) = 0
		_SpeedPan1("SpeedPan1", Vector) = (0,0,0,0)
		_SpeedPan2("SpeedPan2", Vector) = (-0.12,-0.1,0,0)
		[HideInInspector] _texcoord( "", 2D ) = "white" {}
		[HideInInspector] __dirty( "", Int ) = 1
	}

	SubShader
	{
		Tags{ "RenderType" = "Opaque"  "Queue" = "Geometry+0" }
		Cull Back
		CGPROGRAM
		#include "UnityShaderVariables.cginc"
		#include "Tessellation.cginc"
		#pragma target 4.6
		#pragma surface surf Standard keepalpha addshadow fullforwardshadows vertex:vertexDataFunc tessellate:tessFunction 
		struct Input
		{
			float2 uv_texcoord;
		};

		uniform sampler2D _Noise;
		uniform float2 _SpeedPan1;
		uniform float _Tiling;
		uniform float2 _SpeedPan2;
		uniform float3 _VertexOffset;
		uniform sampler2D _MainTexture;
		uniform sampler2D _Flowmap;
		uniform float _EdgeLength;

		float4 tessFunction( appdata_full v0, appdata_full v1, appdata_full v2 )
		{
			return UnityEdgeLengthBasedTess (v0.vertex, v1.vertex, v2.vertex, _EdgeLength);
		}

		void vertexDataFunc( inout appdata_full v )
		{
			float2 temp_cast_0 = (_Tiling).xx;
			float2 uv_TexCoord6 = v.texcoord.xy * temp_cast_0;
			float2 panner2 = ( _Time.y * _SpeedPan1 + uv_TexCoord6);
			float2 panner3 = ( _Time.y * _SpeedPan2 + uv_TexCoord6);
			float2 appendResult23 = (float2(panner2.x , panner3.y));
			float2 UV36 = appendResult23;
			float3 ase_vertex3Pos = v.vertex.xyz;
			float4 transform47 = mul(unity_ObjectToWorld,float4( ase_vertex3Pos , 0.0 ));
			float4 lerpResult33 = lerp( float4( 0,0,0,0 ) , tex2Dlod( _Noise, float4( UV36, 0, 0.0) ) , ( transform47 * float4( _VertexOffset , 0.0 ) ));
			v.vertex.xyz += lerpResult33.rgb;
		}

		void surf( Input i , inout SurfaceOutputStandard o )
		{
			float2 temp_cast_0 = (_Tiling).xx;
			float2 uv_TexCoord6 = i.uv_texcoord * temp_cast_0;
			float2 panner2 = ( _Time.y * _SpeedPan1 + uv_TexCoord6);
			float2 PannerA40 = panner2;
			float2 panner3 = ( _Time.y * _SpeedPan2 + uv_TexCoord6);
			float2 appendResult23 = (float2(panner2.x , panner3.y));
			float2 UV36 = appendResult23;
			float4 tex2DNode12 = tex2D( _Flowmap, UV36 );
			float2 PannerB41 = panner3;
			o.Albedo = ( tex2D( _MainTexture, ( float4( PannerA40, 0.0 , 0.0 ) + tex2DNode12 ).rg ) * tex2D( _MainTexture, ( float4( PannerB41, 0.0 , 0.0 ) + tex2DNode12 ).rg ) ).rgb;
			o.Alpha = 1;
		}

		ENDCG
	}
	Fallback "Diffuse"
	CustomEditor "ASEMaterialInspector"
}
/*ASEBEGIN
Version=15301
14;333;1010;692;1945.401;-329.7688;1.605644;True;False
Node;AmplifyShaderEditor.CommentaryNode;45;-3121.681,-280.1857;Float;False;1479.865;632.4569;;13;18;7;6;19;20;3;2;22;21;23;36;40;41;UV Movement;1,1,1,1;0;0
Node;AmplifyShaderEditor.RangedFloatNode;18;-3071.681,-43.38291;Float;False;Property;_Tiling;Tiling;9;0;Create;True;0;0;False;0;0;1;0;0;0;1;FLOAT;0
Node;AmplifyShaderEditor.Vector2Node;20;-2820.714,191.2712;Float;False;Property;_SpeedPan2;SpeedPan2;11;0;Create;True;0;0;False;0;-0.12,-0.1;-0.3,-0.35;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.Vector2Node;19;-2875.111,-230.1857;Float;False;Property;_SpeedPan1;SpeedPan1;10;0;Create;True;0;0;False;0;0,0;0.12,-0.12;0;3;FLOAT2;0;FLOAT;1;FLOAT;2
Node;AmplifyShaderEditor.SimpleTimeNode;7;-2825.372,71.02705;Float;False;1;0;FLOAT;1;False;1;FLOAT;0
Node;AmplifyShaderEditor.TextureCoordinatesNode;6;-2884.641,-60.63171;Float;False;0;-1;2;3;2;SAMPLER2D;;False;0;FLOAT2;3,3;False;1;FLOAT2;0,0;False;5;FLOAT2;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PannerNode;3;-2523.837,59.82471;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.PannerNode;2;-2525.339,-106.0321;Float;False;3;0;FLOAT2;0,0;False;2;FLOAT2;0,0;False;1;FLOAT;1;False;1;FLOAT2;0
Node;AmplifyShaderEditor.BreakToComponentsNode;21;-2332.54,-222.9263;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.BreakToComponentsNode;22;-2320.223,13.93152;Float;False;FLOAT2;1;0;FLOAT2;0,0;False;16;FLOAT;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4;FLOAT;5;FLOAT;6;FLOAT;7;FLOAT;8;FLOAT;9;FLOAT;10;FLOAT;11;FLOAT;12;FLOAT;13;FLOAT;14;FLOAT;15
Node;AmplifyShaderEditor.CommentaryNode;46;-1546.096,-300.6634;Float;False;1356.091;605.4608;;9;42;43;44;12;15;17;10;1;11;Flowmap + Albedo;1,1,1,1;0;0
Node;AmplifyShaderEditor.DynamicAppendNode;23;-2059.761,-75.04271;Float;False;FLOAT2;4;0;FLOAT;0;False;1;FLOAT;0;False;2;FLOAT;0;False;3;FLOAT;0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;36;-1884.816,-79.69447;Float;False;UV;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;42;-1496.096,-81.71301;Float;False;36;0;1;FLOAT2;0
Node;AmplifyShaderEditor.CommentaryNode;38;-1397.869,362.3265;Float;False;817.6091;595.5166;;7;26;35;24;37;27;33;47;Vertex Displacement;1,1,1,1;0;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;40;-2307.205,-110.7942;Float;False;PannerA;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.RegisterLocalVarNode;41;-2302.534,121.5445;Float;False;PannerB;-1;True;1;0;FLOAT2;0,0;False;1;FLOAT2;0
Node;AmplifyShaderEditor.GetLocalVarNode;43;-1174.299,99.17056;Float;False;41;0;1;FLOAT2;0
Node;AmplifyShaderEditor.SamplerNode;12;-1281.852,-104.6135;Float;True;Property;_Flowmap;Flowmap;8;0;Create;True;0;0;False;0;None;11a4ff5fb39bb9b43b942cbed34ebc38;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.PosVertexDataNode;24;-1347.869,412.3265;Float;False;0;0;5;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.GetLocalVarNode;44;-1181.45,-250.6634;Float;False;40;0;1;FLOAT2;0
Node;AmplifyShaderEditor.ObjectToWorldTransfNode;47;-1151.669,412.269;Float;False;1;0;FLOAT4;0,0,0,1;False;5;FLOAT4;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.Vector3Node;26;-1339.347,576.4626;Float;False;Property;_VertexOffset;VertexOffset;6;0;Create;True;0;0;False;0;0,0,0;0,0.84,0;0;4;FLOAT3;0;FLOAT;1;FLOAT;2;FLOAT;3
Node;AmplifyShaderEditor.GetLocalVarNode;37;-1329.843,750.1697;Float;False;36;0;1;FLOAT2;0
Node;AmplifyShaderEditor.SimpleAddOpNode;17;-961.6706,-246.8014;Float;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleAddOpNode;15;-935.7905,102.537;Float;False;2;2;0;FLOAT2;0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;27;-930.3868,564.7611;Float;False;2;2;0;FLOAT4;0,0,0,0;False;1;FLOAT3;0,0,0;False;1;FLOAT4;0
Node;AmplifyShaderEditor.SamplerNode;10;-772.5308,74.79738;Float;True;Property;_TextureSample0;Texture Sample 0;5;0;Create;True;0;0;False;0;None;None;True;0;False;white;Auto;False;Instance;1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;35;-1117.763,727.8431;Float;True;Property;_Noise;Noise;7;0;Create;True;0;0;False;0;None;07574bf962e411c4e91ec6bdda268731;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SamplerNode;1;-776.2347,-144.1314;Float;True;Property;_MainTexture;Main Texture;5;0;Create;True;0;0;False;0;None;30e06c5ee7c73024d81d7a254ab91e08;True;0;False;white;Auto;False;Object;-1;Auto;Texture2D;6;0;SAMPLER2D;;False;1;FLOAT2;0,0;False;2;FLOAT;0;False;3;FLOAT2;0,0;False;4;FLOAT2;0,0;False;5;FLOAT;1;False;5;COLOR;0;FLOAT;1;FLOAT;2;FLOAT;3;FLOAT;4
Node;AmplifyShaderEditor.SimpleMultiplyOpNode;11;-359.0052,-1.464719;Float;False;2;2;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.LerpOp;33;-764.2599,594.9728;Float;False;3;0;COLOR;0,0,0,0;False;1;COLOR;0,0,0,0;False;2;COLOR;0,0,0,0;False;1;COLOR;0
Node;AmplifyShaderEditor.StandardSurfaceOutputNode;0;0,0;Float;False;True;6;Float;ASEMaterialInspector;0;0;Standard;My shaders/Lava;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;False;Back;0;False;-1;0;False;-1;False;0;0;False;0;Opaque;0.5;True;True;0;False;Opaque;;Geometry;All;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;True;0;False;-1;False;0;False;-1;255;False;-1;255;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;0;False;-1;True;2;2;10;25;False;0.5;True;0;1;False;-1;1;False;-1;0;0;False;-1;0;False;-1;-1;False;-1;-1;False;-1;0;False;0;0,0,0,0;VertexOffset;True;False;Cylindrical;False;Relative;0;;-1;-1;-1;0;0;0;0;False;0;0;0;False;-1;-1;0;False;-1;16;0;FLOAT3;0,0,0;False;1;FLOAT3;0,0,0;False;2;FLOAT3;0,0,0;False;3;FLOAT;0;False;4;FLOAT;0;False;5;FLOAT;0;False;6;FLOAT3;0,0,0;False;7;FLOAT3;0,0,0;False;8;FLOAT;0;False;9;FLOAT;0;False;10;FLOAT;0;False;13;FLOAT3;0,0,0;False;11;FLOAT3;0,0,0;False;12;FLOAT3;0,0,0;False;14;FLOAT4;0,0,0,0;False;15;FLOAT3;0,0,0;False;0
WireConnection;6;0;18;0
WireConnection;3;0;6;0
WireConnection;3;2;20;0
WireConnection;3;1;7;0
WireConnection;2;0;6;0
WireConnection;2;2;19;0
WireConnection;2;1;7;0
WireConnection;21;0;2;0
WireConnection;22;0;3;0
WireConnection;23;0;21;0
WireConnection;23;1;22;1
WireConnection;36;0;23;0
WireConnection;40;0;2;0
WireConnection;41;0;3;0
WireConnection;12;1;42;0
WireConnection;47;0;24;0
WireConnection;17;0;44;0
WireConnection;17;1;12;0
WireConnection;15;0;43;0
WireConnection;15;1;12;0
WireConnection;27;0;47;0
WireConnection;27;1;26;0
WireConnection;10;1;15;0
WireConnection;35;1;37;0
WireConnection;1;1;17;0
WireConnection;11;0;1;0
WireConnection;11;1;10;0
WireConnection;33;1;35;0
WireConnection;33;2;27;0
WireConnection;0;0;11;0
WireConnection;0;11;33;0
ASEEND*/
//CHKSM=EB62EEB75BDCE726759773F3CF60D5911EDB3F9D