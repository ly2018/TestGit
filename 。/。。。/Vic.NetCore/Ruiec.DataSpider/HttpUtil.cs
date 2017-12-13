using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Ruiec.DataSpider
{
    public class HttpUtil
    {
        public static T PostWebRequest<T>(string postUrl, string paramData, Encoding dataEncode)
        {
            string ret = string.Empty;
            T data = default(T);
            try
            {
                HttpWebRequest webReq = (HttpWebRequest)WebRequest.Create(new Uri(postUrl));
                webReq.Method = "POST";
                webReq.ContentType = "application/x-www-form-urlencoded";
                Stream newStream = webReq.GetRequestStream();
                if (!string.IsNullOrEmpty(paramData))
                {
                    byte[] byteArray = dataEncode.GetBytes(paramData); //转化
                    webReq.ContentLength = byteArray.Length;
                    newStream.Write(byteArray, 0, byteArray.Length);//写入参数
                    newStream.Close();
                }
                HttpWebResponse response = (HttpWebResponse)webReq.GetResponse();
                StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.Default);
                ret = sr.ReadToEnd();
                sr.Close();
                response.Close();
                newStream.Close();
                data = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ret);
                return data;
            }
            catch (Exception ex)
            {
                ret = ex.Message;
                return data;
            }
        }
        public static string HttpGetMath(string url, string paramsValue)
        {
            string result = string.Empty;
            Uri uri = new Uri(url);
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(uri + "?" + paramsValue);
            request.Method = "Get";
            System.Net.ServicePointManager.DefaultConnectionLimit = 50;
            request.KeepAlive = false;
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            using (StreamReader sr = new StreamReader(response.GetResponseStream(), Encoding.UTF8))
            {
                result = sr.ReadToEnd();
            }
            response.Close();
            return result;
        }
    }
}
