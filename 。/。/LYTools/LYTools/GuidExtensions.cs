using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYTools
{
	public static class GuidExtensions
	{
		/// <summary> 
		/// 根据GUID获取16位的唯一字符串 
		/// </summary> 
		/// <param name=\"guid\"></param> 
		/// <returns></returns> 
		public static string GuidTo16String(this Guid guid)
		{
			long i = 1;
			foreach (byte b in guid.ToByteArray())
				i *= ((int)b + 1);
			return string.Format("{0:x}", i - DateTime.Now.Ticks);
		}
		/// <summary> 
		/// 根据GUID获取19位的唯一数字序列 
		/// </summary> 
		/// <returns></returns> 
		public static long GuidToLongID(this Guid guid)
		{
			byte[] buffer = guid.ToByteArray();
			return BitConverter.ToInt64(buffer, 0);
		}

	}
}
