using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using Ruiec.DataSpider.WebPassport;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace Ruiec.DataSpider
{
    class Program
    {
        public const string PickUrl = "https://docs.microsoft.com/zh-cn/dotnet";
        public const string ImgUrl = "https://docs.microsoft.com/zh-cn/dotnet/images/";
        public static int numError = 0;
        static void Main(string[] args)
        {
            //Console.WriteLine("Hello HtmlAgilityPack!");
            //Vic.Core.Utils.Spider.DotNetDocHelper doc = new Vic.Core.Utils.Spider.DotNetDocHelper();
            //var data = doc.InitDotNetDocHelper(0);
            //string basePath =@"E:\\LY_Project\WorkPlace_2017\Vic.NetCore\db\\";
            //FaceDemo.FaceMatch(basePath+"1.jpg", basePath + "2.jpg", basePath + "3.jpg");

            //Console.WriteLine(data.Count());

            //var jsonServices = JObject.Parse(File.ReadAllText(@"E://LY_Project/WorkPlace_2017/Vic.NetCore/db/toc.json"));
            //var requiredServices = JsonConvert.DeserializeObject<Root>(jsonServices.ToString());
            //PickRemoteData(requiredServices);

            //Console.WriteLine(numError);
            Console.ReadKey();
        }

        private static void PickRemoteData(Root requiredServices)
        {
            foreach (var item in requiredServices.items)
            {
                item.href = PickUrl + item.href.Replace("..", "");
                var InnerHtml = PickData(item.href);

                //数据保存
                SaveData(new ReqModel()
                {
                    title = item.toc_title,
                    content = InnerHtml,
                });

                if (item.children != null && item.children.Any())
                {
                    GenerateTreeData(item.children);
                }
                if (!string.IsNullOrEmpty(item.tocHref))
                {
                    string str = HttpUtil.HttpGetMath(PickUrl + item.tocHref.Replace("..", ""), "");
                    var jsonData = JsonConvert.DeserializeObject<Root>(str);
                    PickRemoteData(jsonData);
                }
            }
        }

        private static void GenerateTreeData(List<baseModel> children)
        {
            foreach (var item in children)
            {
                item.href = PickUrl + item.href.Replace("..", "");
                var InnerHtml = PickData(item.href);

                //数据保存
                SaveData(new ReqModel()
                {
                    title = item.toc_title,
                    content = InnerHtml,
                });

                if (!string.IsNullOrEmpty(item.tocHref))
                {
                    string str = HttpUtil.HttpGetMath(PickUrl + item.tocHref.Replace("..", ""), "");
                    var jsonData = JsonConvert.DeserializeObject<Root>(str);
                    PickRemoteData(jsonData);
                }
            }
        }

        public static string PickData(string url)
        {
            var web = new HtmlWeb();
            var doc = web.Load(url);
            var InnerHtml = doc.DocumentNode.SelectSingleNode("//div/div[@class='content']").InnerHtml;
            return InnerHtml.Replace("../images/", ImgUrl);
        }

        public static void SaveData(ReqModel data)
        {
            if (data.content.Contains("可能拼写有误") || data.content.Contains("或者正在查找的页面不再可用"))
            {
                numError++;
                return;
            }
            //bool ret = WebPassport.WeiBoHelper.PublishArticle(data);
        }
    }
}
