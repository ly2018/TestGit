using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace LYTools
{
    public class Common
    {
        /// <summary>
        /// 创建txt并写入类容
        /// </summary>
        /// <param name="path">路径及文件名称</param>
        /// <param name="text"></param>
        public static void CreateTextFile(string path, string text,bool mode)
        {
            FileStream fs = null;
            FileMode fm = FileMode.Create;
            if (mode)
            {
                fm = FileMode.Create;
            }
            else
            {
                fm = File.Exists(path) ? FileMode.Append : FileMode.Create;
            }
            fs = new FileStream(path, fm);

            StreamWriter sw = new StreamWriter(fs,System.Text.Encoding.UTF8);
            //开始写入
            sw.Write(text);
            //清空缓冲区
            sw.Flush();
            //关闭流
            sw.Close();
            fs.Close();
        }
    }
}
