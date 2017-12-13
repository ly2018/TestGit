using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Xml.Linq;
using System.Data;

namespace LYTools
{
    /// <summary>
    /// 生成辅助类
    /// </summary>
    public class NHibernateMap
    {
        public static string GetCSText(DataTable dt, string namespaceStr, string className, bool IsGenerateViewModels, string tempStr)
        {
            if (string.IsNullOrEmpty(namespaceStr)) namespaceStr = "Com.Sureba.NHibernate";
            if (string.IsNullOrEmpty(className)) className = "Test";
            string content = "";
            if (dt == null) content = "\t\tpublic virtual int id { get; set; }\n";

            //遍历数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString();
                name = name.Substring(1);

                if (name == "RowVersion")
                {
                    continue;
                }
                string temp = "\t\t/// <summary>\n\t\t///$description$\n\t\t/// </summary>\n\t\tpublic virtual $type$ $name$ { get; set; }\n";
                if (IsGenerateViewModels)
                {
                    temp = "\t\t/// <summary>\n\t\t///$description$\n\t\t/// </summary>\n\t\tpublic $type$ $name$ { get; set; }\n";//用于生成DTO模型
                }
                //string length = dt.Rows[i][1].ToString();
                //string notnull = dt.Rows[i][2].ToString() == "1" ? "false" : "true";
                //string unique = dt.Rows[i][3].ToString();
                string sqltype = dt.Rows[i][4].ToString();
                string description = dt.Rows[i][5].ToString();
                string type = LYTools.Types.GetType(sqltype, true);
                if (type == "DateTime")
                {
                    type += "?";
                }
                if (!IsGenerateViewModels && name == "ID")
                {
                    temp = "\t\t/// <summary>\n\t\t///$description$\n\t\t/// </summary>\n\t\t[PrimaryKey]\n\t\tpublic virtual $type$ $name$ { get; set; }\n";
                }
                temp = temp.Replace("$name$", name).Replace("$type$", type).Replace("$description$", description);
                content += temp;
            }
            if (IsGenerateViewModels)
            {
                content += "\t\t/// <summary>\n\t\t///列表集合\n\t\t/// </summary>\n\t\tpublic List<" + className.Replace(tempStr, "") + "_ViewModel> " + className.Replace(tempStr, "") + "List { get; set; }\n";//用于生成DTO列表字段
            }
            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\n");
            sb.Append("using System.Collections.Generic;\n");
            sb.Append("using System.Linq;\n");
            //sb.Append("using System.Text;\n\n");

            sb.Append("using System.Web;\n");
            if (!IsGenerateViewModels)
            {
                sb.Append("using Orchard.Core.Security.Models;\n");
                sb.Append("using Orchard.Data;\n");
                sb.Append("using Orchard.Data.Conventions;\n\n");
            }
            else
            {
                sb.Append("using " + namespaceStr + ";\n\n");
            }
            sb.Append("namespace " + namespaceStr + "\n");
            sb.Append("{\n");
            //sb.Append("\t[Serializable]\n");

            if (IsGenerateViewModels)
            {
                className += "_ViewModel : BasePager";
            }
            else
            {
                sb.Append("\t[DataTable(\"" + tempStr + "\", \"F\")]\n");
            }
            if (!string.IsNullOrEmpty(tempStr))
            {
                className = className.Replace(tempStr, "");
            }
            sb.Append("\tpublic class " + className + "\n");
            sb.Append("\t{\n");
            sb.Append(content);
            sb.Append("\t}\n");
            sb.Append("}\n");

            return sb.ToString();
        }
        /// <summary>
        /// 生成接口CS文件
        /// </summary>
        /// <param name="namespaceStr"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static string GetInterfaceCsText(string namespaceStr, string className, string tempStr)
        {
            if (string.IsNullOrEmpty(namespaceStr)) namespaceStr = "Com.Sureba.NHibernate";
            if (string.IsNullOrEmpty(className)) className = "Test";
            string content = "";
            //默认接口的方法
            content += "\t\t/// <summary>\n\t\t///新增\n\t\t/// </summary>\n\t\tbool Create($className$ model);\n";
            content += "\t\t/// <summary>\n\t\t///编辑\n\t\t/// </summary>\n\t\tbool Edit($className$ model);\n";
            content += "\t\t/// <summary>\n\t\t///删除\n\t\t/// </summary>\n\t\tbool Delete(string[] array);\n";
            content += "\t\t/// <summary>\n\t\t///查询单个\n\t\t/// </summary>\n\t\t$className$ Get(string id);\n";
            content += "\t\t/// <summary>\n\t\t///表达式查询单个\n\t\t/// </summary>\n\t\t$className$ Get(Expression<Func<$className$, bool>> predicate);\n";
            content += "\t\t/// <summary>\n\t\t///表达式查询总数\n\t\t/// </summary>\n\t\tint Count(Expression<Func<$className$, bool>> predicate);\n";
            content += "\t\t/// <summary>\n\t\t///查询全部列表-尽量少用\n\t\t/// </summary>\n\t\tList<$className$> List();\n";
            content += "\t\t/// <summary>\n\t\t///表达式查询列表\n\t\t/// </summary>\n\t\tList<$className$> QueryList(Expression<Func<$className$, bool>> predicate);\n";
            content += "\t\t/// <summary>\n\t\t///表达式查询属性值\n\t\t/// </summary>\n\t\tIQueryable<object> QueryProperty(Expression<Func<$className$, bool>> predicate, Expression<Func<$className$, object>> selector);\n";
            content += "\t\t/// <summary>\n\t\t///表达式查询返回键值对列表\n\t\t/// </summary>\n\t\tList<KeyValuePair<string, string>> QueryKeyValueList(Expression<Func<$className$, bool>> predicate, Expression<Func<$className$, KeyValuePair<string, string>>> selector);\n";
            //content += "\t\t/// <summary>\n\t\t///表达式查询列表与排序\n\t\t/// </summary>\n\t\tList<$className$> QueryList(Expression<Func<$className$, bool>> predicate,Action<Orderable<$className$>> order);\n";
            content += "\t\t/// <summary>\n\t\t///分页查询列表\n\t\t/// </summary>\n\t\tList<$className$> PageList(Expression<Func<$className$, bool>> predicate,int pageSize, int curPage,out int count);\n";
            //content += "\t\t/// <summary>\n\t\t///分页查询列表与排序\n\t\t/// </summary>\n\t\tList<$className$> PageOrderList(Expression<Func<$className$, bool>> predicate,Action<Orderable<$className$>> order,int pageSize, int curPage,out int count);\n";


            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\n");
            sb.Append("using System.Collections.Generic;\n");
            sb.Append("using System.Linq;\n");
            sb.Append("using System.Web;\n");
            sb.Append("using " + namespaceStr + ";\n");
            sb.Append("using " + namespaceStr.Replace("Contracts", "Models") + ";\n");
            //sb.Append("using Orchard.Core.Security.Models;\n");
            sb.Append("using System.Linq.Expressions;\n");
            sb.Append("using Orchard;\n");
            sb.Append("using Orchard.Data;\n\n");

            sb.Append("namespace " + namespaceStr + "\n");
            sb.Append("{\n");
            //sb.Append("\t[Serializable]\n");

            if (!string.IsNullOrEmpty(tempStr))
            {
                className = className.Replace(tempStr, "");
            }

            content = content.Replace("$className$", className);

            sb.Append("\tpublic interface " + "I" + className + "Contract: IDependency" + "\n");
            sb.Append("\t{\n");
            sb.Append(content);
            sb.Append("\t}\n");
            sb.Append("}\n");

            return sb.ToString();
        }


        /// <summary>
        /// 生成服务cs文件
        /// </summary>
        /// <param name="namespaceStr"></param>
        /// <param name="className"></param>
        /// <returns></returns>
        public static string GetServiceCsText(string namespaceStr, string className)
        {
            if (string.IsNullOrEmpty(namespaceStr)) namespaceStr = "Com.Sureba.NHibernate";
            if (string.IsNullOrEmpty(className)) className = "Test";
            string content = "";
            //默认接口的方法
            //content += "\t\tprivate readonly IRepository<$className$> _I$className$Service;\n";
            //content += "\t\tprivate readonly IRepository<$className$> _I$className$Service;\n";
            //content += "\t\t/// <summary>\n\t\t///新增\n\t\t/// </summary>\n\t\tpublic bool Create($className$ model);\n";
            //content += "\t\t/// <summary>\n\t\t///编辑\n\t\t/// </summary>\n\t\tpublic bool Edit($className$ model);\n";
            //content += "\t\t/// <summary>\n\t\t///删除\n\t\t/// </summary>\n\t\tpublic bool Delete(string[] array);\n";
            //content += "\t\t/// <summary>\n\t\t///查询单个\n\t\t/// </summary>\n\t\tpublic $className$ Get(string id);\n";
            //content += "\t\t/// <summary>\n\t\t///查询列表\n\t\t/// </summary>\n\t\tpublic List<$className$> List(out int count);\n";


            StringBuilder sb = new StringBuilder();
            sb.Append("using System;\n");
            sb.Append("using System.Collections.Generic;\n");
            sb.Append("using System.Linq;\n");
            sb.Append("using System.Web;\n");
            sb.Append("using IBMS.Common.Models;\n");
            sb.Append("using Orchard.Data;\n");
            sb.Append("using IBMS.Common.Contracts;\n\n");

            sb.Append("namespace " + namespaceStr + "\n");
            sb.Append("{\n");
            //sb.Append("\t[Serializable]\n");

            string tempStr = "";
            if (className.StartsWith("T_SYS_"))
            {
                tempStr = "T_SYS_";

            }
            if (className.StartsWith("T_IBM_"))
            {
                tempStr = "T_IBM_";
            }
            if (className.StartsWith("T_PMS_"))
            {
                tempStr = "T_PMS_";
            }
            if (!string.IsNullOrEmpty(tempStr))
            {
                className = className.Replace(tempStr, "");
            }

            content = content.Replace("$className$", className);

            sb.Append("\tpublic class " + "I" + className + "Service: " + "I" + className + "Contract" + "\n");
            sb.Append("\t{\n");
            //sb.Append(content);
            sb.Append("\t}\n");
            sb.Append("}\n");

            return sb.ToString();
        }
        /// <summary>
        /// 根据模板cs类生成CS
        /// </summary>
        /// <param name="className"></param>
        /// <param name="modelPath"></param>
        /// <returns></returns>
        public static string GetServiceModelCSText(DataTable dt, string className, string modelPath, string Namespace, bool isGenControll = false, bool isNewVersion = false)
        {
            string txt = string.Empty;
            using (StreamReader sr = new StreamReader(modelPath))
            {
                txt = sr.ReadToEnd();
                sr.Close();
                sr.Dispose();
            }
            txt = txt.Replace("$ClassName$", className).Replace("$NameSpace$", Namespace);
            if (!isNewVersion)
                txt = txt.Replace("Func<$className$", "Func<" + className);

            if (isGenControll)
            {
                string tempAdd = string.Empty;
                string tempEdit = string.Empty;
                //遍历数据
                for (int i = 0; i < dt.Rows.Count; i++)
                {
                    string name = dt.Rows[i][0].ToString();
                    name = name.Substring(1);

                    if (name == "RowVersion" || name == "ID")
                    {
                        continue;
                    }
                    string sqltype = dt.Rows[i][4].ToString();
                    string type = LYTools.Types.GetType(sqltype, true);
                    if (type == "DateTime"|| type == "DateTime2")
                    {
                        tempAdd += string.Format("\t\t\t{0}=DateTime.Now,\n", name);
                        if (name != "CreateTime")
                            tempEdit += string.Format("\t\t\tcurrModel.{0}=DateTime.Now;\n", name);
                    }
                    else
                    {
                        if (name == "CreatorID" || name == "LastUpdateUserID")
                        {
                            tempAdd += string.Format("\t\t\t{0}=_currUser.ID,\n", name);
                            if (name != "CreatorID")
                                tempEdit += string.Format("\t\t\tcurrModel.{0}=_currUser.ID;\n", name);
                        }
                        else
                        {
                            tempAdd += string.Format("\t\t\t{0}=model.{0},\n", name);
                            tempEdit += string.Format("\t\t\tcurrModel.{0}=model.{0};\n", name);
                        }
                    }
                }
                txt = txt.Replace("$tempAdd$", tempAdd).Replace("$tempEdit$", tempEdit);
            }

            return txt;
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
                ViewTemplate temp = new ViewTemplate(dt, namespaceStr, className.Replace(currStartName, ""));

                string txtIndex = "Index.txt";
                string txtEdit = "Edit.txt";
                string txtCreate = "Create.txt";

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
                txtIndex = txtIndex.Replace("$NameSpaceStr$", temp.NameSpaceStr).Replace("$ClassName$", temp.ClassName).Replace("$Url$", temp.Url).Replace("$HeadHtml$", temp.HeadHtml).Replace("$BodyHtml$", temp.BodyHtml);

                LYTools.Common.CreateTextFile(viewsFilePath + temp.ClassName + "\\Index.cshtml", txtIndex, true);

                using (StreamReader sr = new StreamReader(serverPath + txtEdit, Encoding.Default, true, 1024 * 100))
                {
                    txtEdit = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                txtEdit = txtEdit.Replace("$NameSpaceStr$", temp.NameSpaceStr).Replace("$ClassName$", temp.ClassName).Replace("$Url$", temp.Url).Replace("$EditHtml$", temp.EditHtml);

                LYTools.Common.CreateTextFile(viewsFilePath + temp.ClassName + "\\Edit.cshtml", txtEdit, true);


                using (StreamReader sr = new StreamReader(serverPath + txtCreate, Encoding.Default, true, 1024 * 100))
                {
                    txtCreate = sr.ReadToEnd();
                    sr.Close();
                    sr.Dispose();
                }
                txtCreate = txtCreate.Replace("$NameSpaceStr$", temp.NameSpaceStr).Replace("$ClassName$", temp.ClassName).Replace("$Url$", temp.Url).Replace("$CreateHtml$", temp.CreateHtml);

                LYTools.Common.CreateTextFile(viewsFilePath + temp.ClassName + "\\Create.cshtml", txtCreate, true);


                return "成功生成视图";
            }
            catch (Exception ex)
            {

                return ex.Message;
            }
        }

        /// <summary>
        /// 方法体加上大括号
        /// </summary>
        /// <param name="str"></param>
        /// <returns></returns>
        private string MethodKuoHao(string str)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("\t\t{\n");
            sb.Append(str);
            sb.Append("\t\t}\n");
            return sb.ToString();
        }
        /// <summary>
        /// 保存xml文件
        /// </summary>
        /// <param name="path">路径名称~\\ People.hbm.xml</param>
        /// <param name="classElement"></param>
        public static void SaveXML(string path, DataTable dt, string namespaceStr, string className)
        {
            if (string.IsNullOrEmpty(namespaceStr)) namespaceStr = "Com.Sureba.NHibernate";
            if (string.IsNullOrEmpty(className)) className = "Test";
            path = path + className + ".hbm.xml";
            XNamespace ns = "urn:nhibernate-mapping-2.2";
            XDocument xDoc = new XDocument(
            new XDeclaration("1.0", "UTF-8", null),
            new XElement(ns + "hibernate-mapping"));
            XElement myClass = new XElement(ns + "class", new XAttribute("name", namespaceStr + "." + className + ", " + namespaceStr), new XAttribute("table", className));
            //遍历数据
            for (int i = 0; i < dt.Rows.Count; i++)
            {
                string name = dt.Rows[i][0].ToString();
                string length = dt.Rows[i][1].ToString();
                string notnull = dt.Rows[i][2].ToString() == "1" ? "false" : "true";
                string unique = dt.Rows[i][3].ToString();
                string sqltype = dt.Rows[i][4].ToString();
                //string description = dt.Rows[i][5].ToString();
                string type = LYTools.Types.GetType(sqltype, true);
                if (unique == "1")//主键列
                {
                    XElement property = new XElement(ns + "id", new XAttribute("name", name), new XAttribute("type", type), new XAttribute("unsaved-value", "null"));
                    XElement column = new XElement(ns + "column", new XAttribute("name", name)
                        , new XAttribute("length", length)
                        , new XAttribute("sql-type", sqltype)
                        , new XAttribute("not-null", "true")
                        , new XAttribute("unique", "true")
                        , new XAttribute("index", "PK__" + className + "__" + DateTime.Now.ToString("yyyyMMddhhmmssfff")));
                    XElement generator = new XElement(ns + "generator", new XAttribute("class", "native"));
                    property.Add(column);
                    property.Add(generator);
                    myClass.Add(property);
                }
                else
                {
                    XElement property = new XElement(ns + "property", new XAttribute("name", name), new XAttribute("type", type));
                    XElement column = new XElement(ns + "column", new XAttribute("name", name), new XAttribute("length", length), new XAttribute("sql-type", sqltype), new XAttribute("not-null", notnull));
                    property.Add(column);
                    myClass.Add(property);
                }
            }
            xDoc.Root.Add(myClass);
            xDoc.Save(path);
        }
    }
}
