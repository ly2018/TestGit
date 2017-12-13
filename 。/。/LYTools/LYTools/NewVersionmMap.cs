using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;

namespace LYTools
{
    public class NewVersionmMap
    {
        /// <summary>
        /// /// <summary>
        /// 根据目替换得到生成内容
        /// </summary>
        /// <param name="dt">表格传值得时候生成实体</param>
        /// <param name="className">表完全名称</param>
        /// <param name="templatePath">模板路径</param>
        /// <param name="nameSpace">命名空间前缀</param>
        /// <param name="currPrefix">当前表前缀</param>
        /// <param name="generateFilePath">生成文件保存路径</param>
        /// <param name="saveFileNameFormat">保存文件格式名称,eg: I{0}Service.cs</param>
        public static void GetTemplateText(DataTable dt, string className, string templatePath, string nameSpace, string currPrefix, string generateFilePath, string saveFileNameFormat)
        {
            string txt = string.Empty;
            using (StreamReader sr = new StreamReader(templatePath))
            {
                txt = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            className = className.Replace(currPrefix, "");
            txt = txt.Replace("$ClassName$", className).Replace("$NameSpace$", nameSpace).Replace("$currStartName$", currPrefix);
            if (dt != null)
            {
                string content = GetTableEntityFile(dt, saveFileNameFormat.EndsWith("Dto.cs"));
                txt = txt.Replace("$content$", content);
            }
            LYTools.Common.CreateTextFile(generateFilePath + string.Format(saveFileNameFormat, className), txt, true);
        }
        /// <summary>
        /// 生成实体字段
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        private static string GetTableEntityFile(DataTable dt, bool isDto = false)
        {
            string content = string.Empty;
            //遍历数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString();
                name = name.Substring(1);
                if (name == "RowVersion" || name == "ID" || name == "CreatorID" || name == "CreateTime" || name == "LastUpdateUserID" || name == "LastUpdateTime")
                {
                    continue;
                }
                string temp = "\t\t/// <summary>\n\t\t///$description$\n\t\t/// </summary>\n\t\t[Column(\"F$name$\")]$MaxLength$\n\t\tpublic $type$ $name$ { get; set; }\n";//用于生成实体模型
                if (isDto)
                {
                    temp = "\t\t/// <summary>\n\t\t///$description$\n\t\t/// </summary>\n\t\tpublic $type$ $name$ { get; set; }\n";//用于生成实体模型
                }
                string length = dt.Rows[i][1].ToString();
                if (length == "-1")
                {
                    length = "8000";
                }
                //string notnull = dt.Rows[i][2].ToString() == "1" ? "false" : "true";
                ////非空
                //if (notnull == "true") {
                //}
                //string unique = dt.Rows[i][3].ToString() == "1" ? "false" : "true";
                ////唯一
                //if (unique == "true")
                //{
                //}
                string sqltype = dt.Rows[i][4].ToString();
                string description = dt.Rows[i][5].ToString();
                string type = LYTools.Types.GetType(sqltype, true);
                if (type.ToLower() == "string")
                {
                    temp = temp.Replace("$MaxLength$", string.Format("\n\t\t[MaxLength({0})]", length));
                }
                else
                {
                    temp = temp.Replace("$MaxLength$", "");
                }
                if (type == "DateTime2")
                {
                    type = "DateTime";
                }
                temp = temp.Replace("$name$", name).Replace("$type$", type).Replace("$description$", description);
                content += temp;
            }
            return content;
        }

        /// <summary>
        /// 根据模板生成视图
        /// </summary>
        /// <param name="dt">表</param>
        /// <param name="namespaceStr">实体命名空间</param>
        /// <param name="className">类名称</param>
        /// <param name="currStartName">当前表前缀</param>
        /// <param name="serverPath">服务地址</param>
        /// <param name="viewsFilePath">文件地址</param>
        /// <returns></returns>
        public static string GetViewsText(DataTable dt, string namespaceStr, string className, string currStartName, string serverPath, string viewsFilePath)
        {
            try
            {
                NewViewTemplate temp = new NewViewTemplate(dt, namespaceStr, className.Replace(currStartName, ""));

                string txtIndex = "NewIndex.txt";
                string txtEdit = "NewEdit.txt";
                string txtCreate = "NewCreate.txt";

                if (!System.IO.Directory.Exists(viewsFilePath + temp.ClassName))
                {
                    System.IO.Directory.CreateDirectory(viewsFilePath + temp.ClassName);
                }

                using (StreamReader sr = new StreamReader(serverPath + txtIndex, Encoding.Default, true, 1024 * 100))
                {
                    txtIndex = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                txtIndex = txtIndex.Replace("$NameSpace$", temp.NameSpaceStr).Replace("$ClassName$", temp.ClassName).Replace("$Url$", temp.Url).Replace("$HeadHtml$", temp.HeadHtml).Replace("$BodyHtml$", temp.BodyHtml);

                LYTools.Common.CreateTextFile(viewsFilePath + temp.ClassName + "\\Index.cshtml", txtIndex, true);

                using (StreamReader sr = new StreamReader(serverPath + txtEdit, Encoding.Default, true, 1024 * 100))
                {
                    txtEdit = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                txtEdit = txtEdit.Replace("$NameSpace$", temp.NameSpaceStr).Replace("$ClassName$", temp.ClassName).Replace("$Url$", temp.Url).Replace("$EditHtml$", temp.EditHtml);

                LYTools.Common.CreateTextFile(viewsFilePath + temp.ClassName + "\\Edit.cshtml", txtEdit, true);


                using (StreamReader sr = new StreamReader(serverPath + txtCreate, Encoding.Default, true, 1024 * 100))
                {
                    txtCreate = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                txtCreate = txtCreate.Replace("$NameSpace$", temp.NameSpaceStr).Replace("$ClassName$", temp.ClassName).Replace("$Url$", temp.Url).Replace("$CreateHtml$", temp.CreateHtml);

                LYTools.Common.CreateTextFile(viewsFilePath + temp.ClassName + "\\Create.cshtml", txtCreate, true);


                return "成功生成视图";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }
    }
}
