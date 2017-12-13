using System;
using System.Collections.Generic;
using System.Text;
using Vic.Core.Utils.WebMethod;

namespace Vic.Core.Utils.Spider
{
    //头条文章发布接口 POST  (需要通过微博OAuth授权认证)
    //https://mp.weixin.qq.com/misc/getqrcode?fakeid=3599243278&token=927492517&style=1
    //10001	system error    系统错误
    //10008	param xxx invalid 参数xxx不符合要求
    //11001	sass check error 发布过于频繁
    //11002	create statuses error 微博发送失败
    //11003	bind error  文章关联微博失败
    public class WeiBoHelper
    {
        public static string access_token = "2.009GUiwG77IDaBcaf82511fcIuB51C";
        public static string req_url = "https://api.weibo.com/proxy/article/publish.json";
        public static string qr_img = "https://wx1.sinaimg.cn/large/006WIu6sgy1fjslc61hrtj30rs0fmtbj.jpg";
        public static RetModel PublishArticle(ReqModel model)
        {
            try
            {
                model.text = model.title;
                model.cover = "";// qr_img;
                model.access_token = access_token;

                //model.content = model.title;
                //string param = string.Format("title={0}&content={1}&cover={2}&summary={3}&text={4}&access_token={5}", model.title, RFC3986Encoder.Encode(model.content), RFC3986Encoder.Encode(model.cover), model.summary, model.text, model.access_token);
                string param = string.Format("title={0}&content={1}&cover={2}&summary={3}&text={4}&access_token={5}", model.title, System.Web.HttpUtility.UrlEncode(model.content), System.Web.HttpUtility.UrlEncode(model.cover), model.summary, model.text, model.access_token);

                var ret = HttpUtil.PostWebRequest<RetModel>(req_url, param, Encoding.UTF8);
                return ret;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }

    /// <summary>
    /// 请求参数
    /// </summary>
    public class ReqModel
    {

        public string title { get; set; }//        文章标题，限定32个中英文字符以内
        public string content { get; set; }//  正文内容，限制90000个中英文字符内，需要urlencode
        public string cover { get; set; }//   文章封面图片地址url
        public string summary { get; set; }//   文章导语
        public string text { get; set; }//   与其绑定短微博内容，限制1900个中英文字符内
        public string access_token { get; set; }//   认证token
    }

    public class RetModel
    {
        public string code { get; set; }// 返回码，成功返回100000，失败返回其他
        public string msg { get; set; }// 默认为空，code不为100000返回错误信息，错误描述详见错误对照表
        public Data data { get; set; }// 结果数据，成功时返回，失败返回空

    }
    public class Data
    {
        public string object_id { get; set; }//对象id
        public string url { get; set; }// 文章url
        public string mid { get; set; }//短微博对象id
    }
}