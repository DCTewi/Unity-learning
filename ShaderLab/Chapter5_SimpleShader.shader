// Upgrade NOTE: replaced 'mul(UNITY_MATRIX_MVP,*)' with 'UnityObjectToClipPos(*)'

Shader "Custom/Chapter5_SimpleShader"
{
	Properties
	{
		_Color ("Color Tint", Color) = (1.0, 1.0, 1.0)
	}
	SubShader
	{
		Pass
		{
			CGPROGRAM
			// 表示vert这个函数内包含了顶点着色器的代码
			#pragma vertex vert
			// 表示frag这个函数内包含了片元着色器的代码
			#pragma fragment frag

			// Cg代码中，需要定义一个和属性名称、类型都匹配的变量
			fixed4 _Color;

			// 定义顶点着色器的输入
			struct a2v
			{
				float4 vertex : POSITION;       // 顶点坐标
				float3 normal : NORMAL;         // 法线方向
				float4 texcoord : TEXCOORD0;    // 第一套纹理
			};

			// 定义顶点着色器的输出
			struct v2f
			{
				float4 position : SV_POSITION;  // 位置信息
				float3 color : COLOR0;          // 颜色信息
			};

			// 逐顶点执行 : 返回值是顶点在裁剪空间中的位置
			v2f vert(a2v v)// : SV_POSITION  // 添加上之后报错，v2f中的position已经被定义为顶点信息语义
			{
				v2f res;
				// 位置映射到裁剪空间
				res.position = UnityObjectToClipPos(v.vertex);
				// 把法线方向分量范围从[-1.0, 1.0]映射到了[0.0, 1.0]
				res.color = v.normal * 0.5 + fixed3(0.5, 0.5, 0.5);
				return res;
			}

			// 接受顶点着色器的输出
			fixed4 frag(v2f i) : SV_TARGET
			{
				// 插值后的颜色
				fixed3 c = i.color;
				// 乘上自定义属性
				c *= _Color.rgb;
				// 显示到屏幕上
				return fixed4(c, 1.0);
			}
			ENDCG
		}
	}
}
