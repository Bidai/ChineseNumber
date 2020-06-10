using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ChineseConvert
{
    static public class ChineseNumber
    {
        //可供识别的数值字符
        static string[] NumberChars = new string[] {
             "0零〇０",
			 "1一壹①㈠⑴⒈Ⅰⅰ１❶",
			 "2二两贰②㈡⑵⒉Ⅱⅱ２貮❷",
			 "3三叁③㈢⑶⒊Ⅲⅲ３❸",
			  "4四肆④㈣⑷⒋Ⅳⅳ４❹",
			  "5五伍⑤㈤⑸⒌Ⅴⅴ５❺",
			  "6六陆⑥㈥⑹⒍Ⅵⅵ６❻",
			  "7七柒⑦㈦⑺⒎Ⅶⅶ７❼",
			  "8八捌⑧㈧⑻⒏Ⅷⅷ８❽",
			  "9九玖⑨㈨⑼⒐Ⅸⅸ９❾",
			  "⑩㈩⑽⒑Ⅹⅹ❿",
			  "⑾⒒⑾⑪", "⑿⒓⑿⑫", "⒀⒔⒀⑬", "⒁⒕⒁⑭", "⒂⒖⒂⑮", "⒃⒗⒃⑯", "⒄⒘⒄⑰", "⒅⒙⒅⑱", "⒆⒚⒆⑲", "⒇⒛⒇⑳"
		};
        //可供识别的单位字符
        static string[] UnitChars = new string[] {
            "十拾", "廿念", "百佰", "千仟kK", "万", "兆mM", "亿", "吉gG", "太tT"
        };
        //记录每个中文单位对应的数值
        static ulong [] UnitValues  = { 10, 20, 100, 1000, 10000, 1000000, 100000000, 1000000000, 1000000000000 };

		/// <summary>
		/// 解析辅助函数，用以进行递归解析
		/// </summary>
		/// <param name="types">元素类型,跟values对应。false表示数值，true表示单位</param>
		/// <param name="values">元素值，跟types对应。</param>
		/// <param name="idx">当前解析数值索引</param>
		/// <param name="len">长度</param>
		/// <returns>解析结果数值</returns>
		static ulong GetChsValue(bool[] types, ulong[] values, int idx, int len)
		{
			ulong result = 0;
			int lastpt = len;//指针指向后一个单位
			if (len > 1 && !types[idx + len - 1] && types[idx + len - 2] && values[idx + len - 2] >= 100)//关注如二百五的情况
			{
				values[idx + len - 1] *= values[idx + len - 2] / 10;
			}
			for (int i = (len - 1); i >= 0; --i)//从后向前扫描
			{
				if (types[idx + i])
				{
					if (lastpt < len && values[idx + i] <= values[idx + lastpt])//需要递归
					{
						int j = i - 1;
						while (j >= 0 && values[idx + j] <= values[idx + lastpt]) --j;//寻找大单位，两个大单位之间递归
						if (j < 0 || (types[idx + j] && values[idx + j] > values[idx + lastpt])) ++j;
						values[idx + j] = GetChsValue(types, values, idx + j, lastpt - j);
						types[idx + j] = false;//变成数字处理
						for (uint k = 1; lastpt + k <= len; ++k)
						{
							types[idx + j + k] = types[idx + lastpt + k - 1];
							values[idx + j + k] = values[idx + lastpt + k - 1];
						}
						len -= lastpt - 1 - j;
						lastpt = j + 1;
						i = j;
					}
					else
					{
						lastpt = i;
					}
				}
			}
			//扫描完毕，开始计算
			for (uint i = 0; i < len; ++i)
			{
				if (types[idx + i])//遇到单位
				{
					if (i != 0)
					{
						if (types[idx + i - 1] && 10 == values[idx + i] && 20 == values[idx + i - 1])//非常规处理：“廿十”
							continue;
						if (types[idx + i - 1] || 0 == values[idx + i - 1])//非常规处理：单位紧挨如“千百”，或数值为0如“万零百”
							result += values[idx + i];
						else
							result += values[idx + i - 1] * values[idx + i];//正常赋值
					}
					else//单位打头
						result = values[idx + i];
				}
				else if (i != 0 && !types[idx + i - 1])//遇到数字叠加
					values[idx + i] += values[idx + i - 1] * 10;
			}
			if (!types[idx + len - 1])
				result += values[idx + len - 1];
			return result;
		}

		/// <summary>
		/// 中文数字字符串转换为长整形
		/// </summary>
		/// <param name="chs">中文或者阿拉伯数字，可混用，如"1百零3"</param>
		/// <returns>识别的整数</returns>
		/// <remarks>"二百五"识别为250，二百零五识别为205，三万三等类似。支持大写，比如肆万贰识别为42000。</remarks>
		static public ulong ToUlong(string chs)
		{
			if (string.IsNullOrWhiteSpace(chs)) return 0;
			bool[] types = new bool[chs.Length];
			ulong[] values = new ulong[chs.Length];
			int len = chs.Length;
			//数字字符串信息记录
			for (int k = 0; k < len; ++k)
			{
				int i = 0;
				//支持的数字字符查找
				while (i < NumberChars.Length)
				{
					int j = 0;
					while (j < NumberChars[i].Length)
					{
						if (NumberChars[i][j] == chs[k])
						{
							types[k] = false;
							values[k] = (ulong)i;
							break;
						}
						++j;
					}
					if (j < NumberChars[i].Length) break;
					++i;
				}
				//支持的单位字符查找
				if (NumberChars.Length == i)
				{
					for (i = 0; i < UnitChars.Length; ++i)
					{
						int j = 0;
						while (j < UnitChars[i].Length)
						{
							if (chs[k] == UnitChars[i][j])
							{
								types[k] = true;
								values[k] = UnitValues[i];
								break;
							}
							++j;
						}
						if (j < UnitChars[i].Length) break;
					}
					//非数字非单位字符
					if (UnitChars.Length == i)
					{
						len = k;
						break;
					}
				}
			}
			if (len == 0)
				return 0;
			//进入递归转换
			return GetChsValue(types, values, 0, len);
		}
	}
}
