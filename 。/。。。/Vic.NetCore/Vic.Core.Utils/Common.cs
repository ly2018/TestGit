using Microsoft.AspNetCore.Http;
using System;
using System.DrawingCore;
using System.IO;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Text.RegularExpressions;
using static Vic.Core.Utils.Enum.AM_Enum;

namespace Vic.Core.Utils
{
    public class Common
    {
        /// <summary>
        /// 返回png格式的验证码字节数组，同时传入的参数code将获得明文的验证码及长度。
        /// </summary>
        /// <param name="code"></param>
        /// <returns></returns>
        public static byte[] VerificationCode(out string code, int length)
        {
            string[] chars = "A,B,C,D,E,F,G,H,J,K,L,M,N,P,Q,R,S,T,U,V,W,X,Y,Z,a,b,c,d,e,f,g,h,i,j,k,m,n,p,q,r,s,t,u,v,w,x,y,z,2,3,4,5,6,7,8,9".Split(',');
            Random rnd = new Random();
            if (length < 4 || length > 10)
                length = 6;
            string ext = string.Empty;
            for (int i = 0; i < length; i++)
            {
                ext += chars[(int)Math.Floor(rnd.NextDouble() * chars.Length)];
            }
            byte[] Data = CreateCheckCodeImage(ext);
            code = ext.ToLower();
            return Data;
        }

        private static byte[] CreateCheckCodeImage(string checkCode)
        {
            if (checkCode == null || checkCode.Trim() == String.Empty)
                return null;
            int codeW = 100;// 80;
            int codeH = 39;// 22;
            int fontSize = 20;
            //创建画布
            Bitmap bmp = new Bitmap(codeW, codeH);
            Graphics g = Graphics.FromImage(bmp);
            g.Clear(Color.White);
            Random rnd = new Random();
            string[] font = { "Impact", "Courier New", "Harrington", "Gungsuh", "Courier New" };
            Color[] color = { Color.Black, Color.Red, Color.Blue, Color.Green, Color.DarkBlue };
            FontStyle[] style = { FontStyle.Regular, FontStyle.Bold, FontStyle.Italic, FontStyle.Underline };
            try
            {
                //画噪线 
                for (int i = 0; i < 1; i++)
                {
                    int x1 = rnd.Next(codeW);
                    int y1 = rnd.Next(codeH);
                    int x2 = rnd.Next(codeW);
                    int y2 = rnd.Next(codeH);
                    Color clr = color[rnd.Next(color.Length)];
                    g.DrawLine(new Pen(clr), x1, y1, x2, y2);
                }
                //画验证码字符串 
                for (int i = 0; i < checkCode.Length; i++)
                {
                    string fnt = font[rnd.Next(font.Length)];

                    FontStyle fontstyle = style[rnd.Next(style.Length)];
                    Font ft;
                    try
                    {
                        ft = new Font(fnt, fontSize, fontstyle);
                    }
                    catch
                    {
                        ft = new Font("Arial", fontSize, fontstyle);
                    }

                    Color clr = color[rnd.Next(color.Length)];
                    g.DrawString(checkCode[i].ToString(), ft, new SolidBrush(clr), (float)i * 15 + 2, (float)0);
                }
                //画噪点 
                for (int i = 0; i < 100; i++)
                {
                    int x = rnd.Next(bmp.Width);
                    int y = rnd.Next(bmp.Height);
                    Color clr = color[rnd.Next(color.Length)];
                    bmp.SetPixel(x, y, clr);
                }
                MemoryStream Ms = new MemoryStream();
                bmp.Save(Ms, System.DrawingCore.Imaging.ImageFormat.Png);
                byte[] Data = Ms.ToArray();
                Ms.Dispose();
                return Data;
            }
            finally
            {
                g.Dispose();
                bmp.Dispose();
            }
        }

        //静态变量
        private static char[] constant =
          {
            '0','1','2','3','4','5','6','7','8','9',
            'a','b','c','d','e','f','g','h','i','j','k','l','m','n','o','p','q','r','s','t','u','v','w','x','y','z',
            'A','B','C','D','E','F','G','H','I','J','K','L','M','N','O','P','Q','R','S','T','U','V','W','X','Y','Z'
          };
        /// <summary>
        /// 生成的随机数
        /// </summary>
        /// <param name="Length">需要产生的长度</param>
        /// <returns>返回随机数</returns>
        public static string GenerateRandom(int Length)
        {
            System.Text.StringBuilder newRandom = new System.Text.StringBuilder(62);
            Random rd = new Random();
            for (int i = 0; i < Length; i++)
            {
                newRandom.Append(constant[rd.Next(62)]);
            }
            return newRandom.ToString();
        }

        /// <summary>
        /// 参数重拍
        /// </summary>
        /// <param name="url">原始地址</param>
        /// <param name="ParamText">参数名称</param>
        /// <param name="ParamValue">参数值</param>
        /// <returns></returns>
        public static string BuildUrl(string url, string ParamText, string ParamValue)
        {
            Regex reg = new Regex(string.Format("{0}=[^&]*", ParamText), RegexOptions.IgnoreCase);
            Regex reg1 = new Regex("[&]{2,}", RegexOptions.IgnoreCase);
            string _url = reg.Replace(url, "");
            if (_url.IndexOf("?") == -1)
                _url += string.Format("?{0}={1}", ParamText, ParamValue);//?
            else
                _url += string.Format("&{0}={1}", ParamText, ParamValue);//&
            _url = reg1.Replace(_url, "&");
            _url = _url.Replace("?&", "?");
            return _url;
        }

        /// <summary>
        /// 上传图像
        /// </summary>
        /// <param name="HttpContext">上下文</param>
        /// <param name="Path">路径枚举</param>
        /// <param name="IsFullPath">是否返回全路径</param>
        public static string UploadImgageReturnPath(HttpContext HttpContext, ImgPathEnum Path, bool IsFullPath)
        {
            System.IO.Stream userfile = HttpContext.Request.Body;
            string FileName = DateTime.Now.ToString("yyyyMMddHHmmssfff");
            string FileExt = HttpContext.Request.Headers["fileext"];
            if (userfile == null)
            {
                return "File Upload error (文件上传错误！)";
            }
            if (string.IsNullOrEmpty(FileExt))
            {
                FileExt = "png";
            }
            FileExt = FileExt.ToLower();
            string[] extArr = new string[] { "gif", "jpg", "jpeg", "png", "ico" };
            if (string.IsNullOrEmpty(FileExt) || !extArr.Contains(FileExt))
            {
                return "File format error (文件格式错误！)";
            }
            string uploadfolder = string.Format("{0}\\img\\{1}\\", ConstDefine.ImgSavePath, (int)Path);
            string uploadfileurl = ConstDefine.ImgSiteDomain;
            if (!Directory.Exists(uploadfolder))
            {
                Directory.CreateDirectory(uploadfolder);
            }
            FileExt = "." + FileExt;
            byte[] buffer = new byte[256];
            int len;
            string FileFullPath = uploadfolder + FileName + FileExt;
            FileStream fs = new FileStream(FileFullPath, FileMode.Create, FileAccess.Write);
            while ((len = userfile.Read(buffer, 0, buffer.Length)) > 0)
            {
                fs.Write(buffer, 0, len);
            }
            fs.Dispose();

            string imgUrl = string.Empty;
            if (IsFullPath)
            {
                imgUrl = uploadfileurl + "/" + (int)Path + "/" + FileName + FileExt;
            }
            else
            {
                imgUrl = FileName + FileExt;
            }
            try
            {
                MakeThumbnail(FileFullPath, FileFullPath + "_small.jpeg", 150, 80);
            }
            catch (Exception ex)
            {
                return imgUrl;
            }

            return imgUrl;
        }

        /// <summary>
        /// 生成图片缩略图
        /// </summary>
        /// <param name="sourcePath">图片路径</param>
        /// <param name="newPath">新图片路径</param>
        /// <param name="width">宽度</param>
        /// <param name="height">高度</param>
        public static void MakeThumbnail(string sourcePath, string newPath, int width, int height)
        {
            System.DrawingCore.Image ig = System.DrawingCore.Image.FromFile(sourcePath);
            int towidth = width;
            int toheight = height;
            int x = 0;
            int y = 0;
            int ow = ig.Width;
            int oh = ig.Height;
            if ((double)ig.Width / (double)ig.Height > (double)towidth / (double)toheight)
            {
                oh = ig.Height;
                ow = ig.Height * towidth / toheight;
                y = 0;
                x = (ig.Width - ow) / 2;

            }
            else
            {
                ow = ig.Width;
                oh = ig.Width * height / towidth;
                x = 0;
                y = (ig.Height - oh) / 2;
            }
            System.DrawingCore.Image bitmap = new System.DrawingCore.Bitmap(towidth, toheight);
            System.DrawingCore.Graphics g = System.DrawingCore.Graphics.FromImage(bitmap);
            g.InterpolationMode = System.DrawingCore.Drawing2D.InterpolationMode.High;
            g.SmoothingMode = System.DrawingCore.Drawing2D.SmoothingMode.HighQuality;
            g.Clear(System.DrawingCore.Color.Transparent);
            g.DrawImage(ig, new System.DrawingCore.Rectangle(0, 0, towidth, toheight), new System.DrawingCore.Rectangle(x, y, ow, oh), GraphicsUnit.Pixel);
            try
            {
                bitmap.Save(newPath, System.DrawingCore.Imaging.ImageFormat.Jpeg);
            }
            catch (Exception ex)
            {
                throw ex;
            }
            finally
            {
                ig.Dispose();
                bitmap.Dispose();
                g.Dispose();
            }
        }
        /// <summary>
        /// 获取图片完整访问地址
        /// </summary>
        /// <param name="url">图片名称</param>
        /// <param name="Path">路径枚举</param>
        /// <param name="IsSmall">是否获取压缩小图</param>
        /// <returns></returns>
        public static string GetImgFullUrl(string url, ImgPathEnum Path, bool IsSmall = true)
        {
            if (IsSmall)
                return string.Format("{0}/{1}/{2}{3}", ConstDefine.ImgSiteDomain, (int)Path, url, "_small.jpeg");
            else
                return string.Format("{0}/{1}/{2}", ConstDefine.ImgSiteDomain, (int)Path, url);

        }
        /// <summary>
        /// 通过图片完整访问地址获取图片名称
        /// </summary>
        /// <param name="fullUrl"></param>
        /// <returns></returns>
        public static string GetImgNameByImgFullUrl(string fullUrl)
        {
            if (!fullUrl.Contains("/"))
                return "";
            string[] data = fullUrl.Split('/');
            return data[data.Length - 1];
        }
    }
}
