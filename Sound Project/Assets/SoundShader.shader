Shader "Custom/SoundShader" {
	Properties {
		_DefaultColor("Default Color", Color) = (0,0,0,1)
		_HighlightColor("Highlight Color", Color) = (1,1,1,1)
	}
	SubShader {
        Pass {
            CGPROGRAM 

            #pragma vertex vert
            #pragma fragment frag
            
            fixed4 _DefaultColor;
            fixed4 _HighlightColor;
            float4 _SoundPos = (0,0,0,0);
            float _SoundTimeOffset = 0;
            
            struct v2f {
	            float4 pos : SV_POSITION;
	            float4 wp : TEXCOORD0;
	         };

            v2f vert(float4 v:POSITION) {
            	v2f o;
            	o.pos = mul(UNITY_MATRIX_MVP, v);
                o.wp = mul(_Object2World, v);
                return o;
            }

            fixed4 frag(v2f i) : COLOR {
            	float dist = distance(i.wp, _SoundPos);
            	float weight = clamp(abs((dist - (_SoundTimeOffset * 4))) / 0.2, 0, 1);
                return lerp(_HighlightColor, _DefaultColor, weight);
            }

            ENDCG
        }
    }
	FallBack "Diffuse"
}
