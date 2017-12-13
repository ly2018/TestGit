using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace Vic.Core.Utils.WebMethod
{
    public class WebRequestUtil
    {
        /// <summary>
        /// GCB Post方法
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="postUrl">Post地址</param>
        /// <param name="paramData">传递参数</param>
        /// <param name="dataEncode">编码格式</param>
        /// <returns></returns>
        public static T PostWebRequest<T>(string postUrl, string paramData, Encoding dataEncode, out string errMsg, HttpDataFormartEnum dataFormart = HttpDataFormartEnum.Formart_Json)
        {
            errMsg = null;
            T result = default(T);
            try
            {
                if (postUrl.ToLower().StartsWith("https"))
                    SetCertificatePolicy();
                HttpWebRequest httpRequset = (HttpWebRequest)HttpWebRequest.Create(postUrl);//创建http 请求               
                httpRequset.Method = "POST";//POST 提交
                //httpRequset.KeepAlive = true;
                //httpRequset.UserAgent = "Mozilla/5.0 (Windows NT 6.3; WOW64; Trident/7.0; rv:11.0) like Gecko";
                httpRequset.Accept = "text/html, application/xhtml+xml, */*";
                if (dataFormart == HttpDataFormartEnum.Formart_Json)
                    httpRequset.ContentType = " application/json";
                else
                    httpRequset.ContentType = "application/x-www-form-urlencoded";
                byte[] bytes = dataEncode.GetBytes(paramData);
                //httpRequset.ContentLength = bytes.Length;
                //Stream stream = httpRequset.GetRequestStream();
                //stream.Write(bytes, 0, bytes.Length);
                //stream.Close();//以上是POST数据的写入
                string dataString = null;
                //提交？？
                //HttpWebResponse httpResponse = (HttpWebResponse)httpRequset.GetResponse();
                //using (Stream responsestream = httpResponse.GetResponseStream())
                //{
                //    using (StreamReader sr = new StreamReader(responsestream, dataEncode))
                //    {
                //        dataString = sr.ReadToEnd();
                //    }
                //}
                if (!string.IsNullOrEmpty(dataString))
                {
                    if (typeof(T) == typeof(string))
                    {
                        result = (T)Convert.ChangeType(dataString, typeof(T));
                    }
                    else
                    {
                        result = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(dataString);
                    }
                }
            }
            catch (Exception ex)
            {
                errMsg = ex.Message;
            }
            return result;
        }
        /// <summary>
        /// GCB API接口GET请求方法
        /// Author：王明
        /// </summary>
        /// <typeparam name="T">返回值类型</typeparam>
        /// <param name="getUrl">get地址及传递参数</param>
        /// <param name="dataEncode">编码格式</param>
        /// <param name="errorMsg">错误信息</param>
        /// <returns>指定返回格式</returns>
        public static T GetWebRequest<T>(string getUrl, Encoding dataEncode, out string errorMsg)
        {
            T resultData = default(T);
            errorMsg = null;
            try
            {
                if (getUrl.ToLower().StartsWith("https"))
                    SetCertificatePolicy();
                string ret = string.Empty;
                HttpWebRequest httpRequest = (HttpWebRequest)WebRequest.Create(getUrl);
                //httpRequest.Timeout =5000;
                //httpRequest.Method = "GET";
                //HttpWebResponse httpResponse = (HttpWebResponse)httpRequest.GetResponse();
                //StreamReader sr = new StreamReader(httpResponse.GetResponseStream(), dataEncode);
                //string result = sr.ReadToEnd();
                //ret = result.Replace("\r", "").Replace("\n", "").Replace("\t", "");
                //sr.Close();
                if (!string.IsNullOrEmpty(ret))
                {
                    resultData = Newtonsoft.Json.JsonConvert.DeserializeObject<T>(ret);
                }
            }
            catch (Exception ex)
            {
                errorMsg = ex.Message;
            }

            return resultData;
        }


        /// <summary>
        /// Sets the cert policy.
        /// </summary>
        public static void SetCertificatePolicy()
        {
            //ServicePointManager.ServerCertificateValidationCallback
            //           += RemoteCertificateValidate;
        }

        private static bool RemoteCertificateValidate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}