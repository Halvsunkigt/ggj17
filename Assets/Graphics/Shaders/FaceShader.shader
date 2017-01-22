Shader "Unlit/FaceShader"
{
	Properties
	{
		_MainTex ("Texture", 2D) = "white" {}
		_Noise ("PerlinNoise", 2D) = "white" {}

		_ScanlineSpeed ("Scanline speed", Float) = 10.0
		_ScanlineFrequency ("Scanline frequency", Float) = 10.0
		_ScanlineStrength ("Scanline strength", Float) = 1.0

		_InterlaceStrength ("Interlace strength", Float) = 1.0
		_InterlaceOffset ("Interlace offset", Float) = 1.0

		_DistortionStrength ("Distortion strength", Float) = 1.0

		_NoiseStrength ("Noise strength", Float) = 1.0

		_TopGlow ("Top glow", Float) = 1.0

		_VignetteStrength ("Vignette strength", Float) = 1.0

		_ScreenColor ("Screen color", Color) = (0,0,1,1)

		_TurnOff ("Turn off", Range (0, 1)) = 0.0
	}
	SubShader
	{
		Tags { "RenderType"="Opaque" }
		LOD 100

		Pass
		{
			CGPROGRAM
			#pragma vertex vert
			#pragma fragment frag
			// make fog work
			#pragma multi_compile_fog
			
			#include "UnityCG.cginc"

			struct appdata
			{
				float4 vertex : POSITION;
				float2 uv : TEXCOORD0;
			};

			struct v2f
			{
				float2 uv : TEXCOORD0;
				UNITY_FOG_COORDS(1)
				float4 vertex : SV_POSITION;
			};

			sampler2D _MainTex;
			sampler2D _Noise;
			float4 _MainTex_ST;
			float _ScanlineSpeed;
			float _ScanlineFrequency;
			float _ScanlineStrength;
			float _DistortionStrength;
			float _NoiseStrength;
			float _TopGlow;
			float _InterlaceStrength;
			float _InterlaceOffset;
			float _VignetteStrength;
			float4 _ScreenColor;
			float _TurnOff;
			
			v2f vert (appdata v)
			{
				v2f o;
				o.vertex = UnityObjectToClipPos(v.vertex);
				o.uv = TRANSFORM_TEX(v.uv, _MainTex);
				UNITY_TRANSFER_FOG(o,o.vertex);
				return o;
			}

			float rand(float3 co)
 			{
     			return frac(sin( dot(co.xyz ,float3(12.9898,78.233,45.5432) )) * 43758.5453);
 			}

			fixed4 frag (v2f i) : SV_Target
			{
				float distortion = 0.5-abs(frac((i.uv.y*2 +_Time.y*0.1)*10.0)*2.0-1.0);
				float distortion2 = 0.5-(frac((i.uv.y +_Time.y*0.1)*10.0)*2.0-1.0);
				float distortion3 = 3*(tex2D(_Noise,fixed2(0,_Time.y*0.5*0.1*5))-0.3);
				distortion3 = round(pow(distortion3, 2)*3);
				float f = 10.0 * (1.0 / (10*20));
				float distortion4 = pow(tex2D(_Noise, f*floor(i.uv.y / f)+_Time.y)+0.4,4);
				// sample the texture
				float scanLines = abs(sin(i.uv.y*_ScanlineFrequency+_Time.y*_ScanlineSpeed));
				float2 i2 = i.uv+fixed2(0,(scanLines-0.5)*0.005)+fixed2(distortion*distortion3*distortion4*0.05, 0)*_DistortionStrength;

				fixed4 col = tex2D(_MainTex, i2)*_ScreenColor;
				float offset1 = _InterlaceOffset*0.01+abs(sin(_Time.y*15)*0.003);
				float offset2 = _InterlaceOffset*0.01+abs(sin(_Time.y*15+123.456)*0.003);
				fixed4 col2 = tex2D(_MainTex, i2+fixed2(offset1,0))*_ScreenColor;
				fixed4 col3 = tex2D(_MainTex, i2+fixed2(-offset2,0))*_ScreenColor;

				float flicker = 0.5*(tex2D(_Noise,fixed2(0,_Time.y*0.1*5))-0.3);
				fixed4 falloutLines = (1-scanLines);
				fixed4 interlacing = col2*0.75f*(1-scanLines)+col3*0.75f*scanLines;
				interlacing *= _InterlaceStrength;
				fixed4 backlight = (pow(i.uv.y,2)*0.6+0.4);
				backlight = pow(backlight,4)+flicker;
				float noise = rand(float3(i.uv.x+_Time.y,i.uv.y+_Time.y,1))*fixed4(1,1,1,1);

				fixed4 screenlineColor = pow(falloutLines,5)*0.5*pow(backlight, 1.2);

				fixed2 cur = i.uv + float2(-0.5,-0.5);
				float vignette = length(pow(cur,3));
				vignette = smoothstep(0.22,0.3,smoothstep(fixed4(1,1,1,1)*vignette,0.35,0.2));

				float _TurnOff2 = clamp(_TurnOff*1.2-0.2,0,1);
				cur = fixed2(cur.x*(1-_TurnOff2*5),cur.y);
				float offLight = (1-length(pow(cur*_TurnOff2*50,1)))+(1-length(pow(cur*0.7*_TurnOff2*50,1)));
				//fixed4(1,1,1,1)*offLight;
				offLight*=(0.4+noise*0.6)*(1-pow(falloutLines,5)*0.5);
				return lerp(vignette*((interlacing+col) + screenlineColor*_ScanlineStrength + backlight*_ScreenColor*_TopGlow)*lerp(1, clamp(0,1,(0.8+noise)), _NoiseStrength), offLight, ceil(_TurnOff));
			}
			ENDCG
		}
	}
}
