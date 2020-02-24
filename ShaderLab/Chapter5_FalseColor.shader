Shader "Custom/Chapter5_FalseColor"
{
    SubShader
    {
        Pass
        {
            CGPROGRAM

            #pragma vertex vert
            #pragma fragment frag

            #include "UnityCG.cginc"


            struct v2f
            {
                float4 pos : SV_POSITION;
                fixed4 color : COLOR0;
            };

            v2f vert(appdata_full v)
            {
                v2f res;
                res.pos = UnityObjectToClipPos(v.vertex);
                // 可视化法线
                //res.color = fixed4(v.normal * 0.5 + fixed3(0.5, 0.5, 0.5), 1.0);

                // 可视化切线
                //res.color = fixed4(v.tangent.xyz * 0.5 + fixed3(0.5, 0.5, 0.5), 1.0);

                // 可视化副切线
                //fixed3 binormal = cross(v.normal, v.tangent.xyz) * v.tangent.w;
                //res.color = fixed4(binormal * 0.5 + fixed3(0.5, 0.5, 0.5), 1.0);

                // 可视化第一组纹理坐标
                //res.color = fixed4(v.texcoord.xy, 0.0, 1.0);

                // 可视化第二组纹理坐标
                res.color = fixed4(v.texcoord1.xy, 0.0, 1.0);

                // 可视化第一组纹理坐标的小数部分
                // res.color = frac(v.texcoord);
                // if (any(saturate(v.texcoord) - v.texcoord))
                // {
                //     res.color.b = 0.5;
                // }
                // res.color.a = 1.0;

                // 可视化顶点颜色
                // res.color = v.color;

                return res;
            }

            fixed4 frag(v2f i) : SV_TARGET
            {
                return i.color;
            }
            ENDCG
        }
    }
}
