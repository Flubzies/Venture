// Got the overlay shader online. 
// Added ZWrite functionality to disable trail overlapping effect.
// Overlay - https://answers.unity.com/questions/384550/shader-photoshop-overlay-effect.html
// ZWrite - https://answers.unity.com/questions/940198/what-is-the-most-efficient-way-of-drawing-lines-on.html

Shader "Custom/PhotoshopOverlayTrail" 
{
     Properties 
     {
         _MainTex ("Base (RGB) Trans (A)", 2D) = "white" {}

         _Color("Main Color", Color) = (1,1,1,0.5)
         _AlphaTex("Base (RGB) Trans (A)", 2D) = "white" {}

     }
     
     SubShader
     {
         Tags {"Queue"="Transparent" "IgnoreProjector"="True" "RenderType"="Transparent"}
         ZWrite On ZTest Less Blend SrcAlpha One Lighting Off Cull Off Fog { Mode Off } Blend DstColor SrcColor
         LOD 100
         
         Pass 
         {
             CGPROGRAM
             #pragma vertex vert_vct
             #pragma fragment frag_mult 
             #pragma fragmentoption ARB_precision_hint_fastest
             #include "UnityCG.cginc"
 
             sampler2D _MainTex;
             float4 _MainTex_ST;
 
             struct vin_vct 
             {
                 float4 vertex : POSITION;
                 float4 color : COLOR;
                 float2 texcoord : TEXCOORD0;
             };
 
             struct v2f_vct
             {
                 float4 vertex : POSITION;
                 fixed4 color : COLOR;
                 half2 texcoord : TEXCOORD0;
             };
 
             v2f_vct vert_vct(vin_vct v)
             {
                 v2f_vct o;
                 o.vertex = UnityObjectToClipPos(v.vertex);
                 o.color = v.color;
                 o.texcoord = v.texcoord;
                 return o;
             }
 
             float4 frag_mult(v2f_vct i) : COLOR
             {
                 float4 tex = tex2D(_MainTex, i.texcoord);
                 
                 float4 final;                
                 final.rgb = i.color.rgb * tex.rgb * 2;
                 final.a = i.color.a * tex.a;
                 return lerp(float4(0.5f,0.5f,0.5f,0.5f), final, final.a);
                 
             }

             fixed4 _Color;
             sampler2D _AlphaTex;
             float4 _AlphaTex_ST;
 
             struct v2f 
             {
                 float4 pos : SV_POSITION;
                 half4 color : COLOR0;
                 float2 uv : TEXCOORD0;
             };
 
             v2f vert(appdata_full v)
             {
                 v2f o;
                 o.color = v.color;
 
                 o.pos = UnityObjectToClipPos(v.vertex);
                 o.uv = TRANSFORM_TEX(v.texcoord, _AlphaTex);
                 return o;
             }
 
             fixed4 frag(v2f i) : SV_Target
             {
                 fixed4 texcol = tex2D(_AlphaTex, i.uv) * i.color;
                 return texcol * _Color;
             }
 
             ENDCG
             
         }
     }
 }