using HtmlAgilityPack;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Vic.Core.Utils.Models;
using Vic.Core.Utils.WebMethod;

namespace Vic.Core.Utils.Spider
{
    public class DotNetDocHelper
    {
        public const string PickUrl = "https://docs.microsoft.com/zh-cn/aspnet/core/";//"https://docs.microsoft.com/zh-cn/dotnet";
        public const string ImgUrl = "https://docs.microsoft.com/zh-cn/dotnet/images/";
        public static int numError = 0;

        private static List<ReqModel> ListData;

        public List<ReqModel> InitDotNetDocHelper(int j)
        {
            if (ListData == null)
            {
                ListData = new List<ReqModel>();
            }
            var listData = JsonConvert.DeserializeObject<List<ParentModel>>(File.ReadAllText(@"E://LY_Project/WorkPlace_2017/Vic.NetCore/db/toc.json"));
            foreach (var item in listData)
            {
                if (item.href != null && !item.href.Contains("http"))
                    item.href = PickUrl + item.href.Replace("..", "");
                var InnerHtml = PickData(item.href);

                //数据保存
                SaveData(new ReqModel()
                {
                    title = item.toc_title,
                    content = InnerHtml,
                    cover = item.href,
                });

                if (item.children != null && item.children.Any())
                {
                    GenerateTreeData(item.children, item.href);
                }
                if (!string.IsNullOrEmpty(item.tocHref))
                {
                    string str = HttpUtil.HttpGetMath(PickUrl + item.tocHref.Replace("..", ""), "");
                    var jsonData = JsonConvert.DeserializeObject<RootDoc>(str);
                    PickRemoteData(jsonData);
                }
            }
            //var jsonServices = JObject.Parse(File.ReadAllText(@"E://LY_Project/WorkPlace_2017/Vic.NetCore/db/toc.json"));
            //var requiredServices = JsonConvert.DeserializeObject<RootDoc>(jsonServices.ToString());
            //var data = requiredServices.items[j - 1];
            //requiredServices.items = new List<ParentModel>() { data };
            //PickRemoteData(requiredServices);
            return ListData;
        }


        private static void PickRemoteData(RootDoc requiredServices)
        {
            foreach (var item in requiredServices.items)
            {
                if (!item.href.Contains("http"))
                    item.href = PickUrl + item.href.Replace("..", "");
                var InnerHtml = PickData(item.href);

                //数据保存
                SaveData(new ReqModel()
                {
                    title = item.toc_title,
                    content = InnerHtml,
                    cover = item.href,
                });

                if (item.children != null && item.children.Any())
                {
                    GenerateTreeData(item.children);
                }
                if (!string.IsNullOrEmpty(item.tocHref))
                {
                    string str = HttpUtil.HttpGetMath(PickUrl + item.tocHref.Replace("..", ""), "");
                    var jsonData = JsonConvert.DeserializeObject<RootDoc>(str);
                    PickRemoteData(jsonData);
                }
            }
        }

        private static void GenerateTreeData(List<baseModel> children)
        {
            foreach (var item in children)
            {
                if (string.IsNullOrWhiteSpace(item.href))
                {
                    continue;
                }
                if (!item.href.Contains("http"))
                {
                    item.href = PickUrl + item.href.Replace("..", "");
                }
                var InnerHtml = PickData(item.href);

                //数据保存
                SaveData(new ReqModel()
                {
                    title = item.toc_title,
                    content = InnerHtml,
                    cover = item.href,
                });

                if (!string.IsNullOrEmpty(item.tocHref))
                {
                    try
                    {
                        string str = HttpUtil.HttpGetMath(PickUrl + item.tocHref.Replace("..", ""), "");
                        var jsonData = JsonConvert.DeserializeObject<RootDoc>(str);
                        PickRemoteData(jsonData);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
        }

        private static void GenerateTreeData(List<baseModel> children, string parent)
        {
            foreach (var item in children)
            {
                //if (string.IsNullOrEmpty(item.href))
                //{
                //    item.href = parent + item.toc_title.Replace(" ", "-");
                //}
                //else
                {
                    if (item.href != null && !item.href.Contains("http"))
                    {
                        item.href = PickUrl + item.href;
                    }
                    else
                    {
                        continue;
                    }
                }
                var InnerHtml = PickData(item.href.Trim());

                //数据保存
                SaveData(new ReqModel()
                {
                    title = item.toc_title,
                    content = InnerHtml,
                    cover = item.href,
                });

                if (!string.IsNullOrEmpty(item.tocHref))
                {
                    try
                    {
                        string str = HttpUtil.HttpGetMath(PickUrl + item.tocHref.Replace("..", ""), "");
                        var jsonData = JsonConvert.DeserializeObject<RootDoc>(str);
                        PickRemoteData(jsonData);
                    }
                    catch (Exception ex)
                    {
                        continue;
                    }
                }
            }
        }


        public static string PickData(string url)
        {
            try
            {
                if (string.IsNullOrEmpty(url))
                {
                    return "";
                }

                var web = new HtmlWeb();
                var doc = web.Load(url);
                var InnerHtml = doc.DocumentNode.SelectSingleNode("//div/div[@class='content']").InnerHtml;
                return InnerHtml.Replace("../images/", ImgUrl);
            }
            catch (Exception)
            {
                return "";
            }
        }

        public static void SaveData(ReqModel data)
        {
            if (data.content.Contains("可能拼写有误") || data.content.Contains("或者正在查找的页面不再可用"))
            {
                numError++;
                return;
            }
            if (ListData == null)
            {
                ListData = new List<ReqModel>();
            }
            if (!ListData.Any(a => a.title == data.title))
            {
                ListData.Add(data);
            }
            //bool ret = WebPassport.WeiBoHelper.PublishArticle(data);
        }
    }
}
