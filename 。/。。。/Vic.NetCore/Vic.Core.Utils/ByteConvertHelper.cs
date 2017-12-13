using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using Newtonsoft.Json;

namespace Vic.Core.Utils
{
    public class ByteConvertHelper
    {
        public static string SerializeObject<T>(T data)
        {
            return JsonConvert.SerializeObject(data);
        }
        public static T DeserializeObject<T>(string data)
        {
            if (string.IsNullOrEmpty(data))
            {
                return default(T);
            }
            if (typeof(string) == typeof(T))
            {
                object temp = data;
                return (T)temp;
            }
            data = data.TrimStart('\"').TrimEnd('\"');
            return JsonConvert.DeserializeObject<T>(data);
        }


        public static T DeserializeFileObject<T>(string filePath)
        {
            string data = null;
            using (FileStream fs = new FileStream(filePath, FileMode.Open, FileAccess.Read))
            {
                using (StreamReader reader = new StreamReader(fs))
                {
                    data = reader.ReadToEnd();
                }
            }
            if (string.IsNullOrEmpty(data))
            {
                return default(T);
            }
            return JsonConvert.DeserializeObject<T>(data);
        }

        /// <summary>
        /// 将对象转换为byte数组
        /// </summary>
        /// <param name="obj">被转换对象</param>
        /// <returns>转换后byte数组</returns>
        public static byte[] Object2Bytes(object obj)
        {
            string json = JsonConvert.SerializeObject(obj);
            byte[] serializedResult = System.Text.Encoding.UTF8.GetBytes(json);
            return serializedResult;
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static object Bytes2Object(byte[] buff)
        {
            string json = System.Text.Encoding.UTF8.GetString(buff);
            return JsonConvert.DeserializeObject<object>(json);
        }

        /// <summary>
        /// 将byte数组转换成对象
        /// </summary>
        /// <param name="buff">被转换byte数组</param>
        /// <returns>转换完成后的对象</returns>
        public static T Bytes2Object<T>(byte[] buff)
        {
            string json = System.Text.Encoding.UTF8.GetString(buff);
            return JsonConvert.DeserializeObject<T>(json);
        }
    }
}

