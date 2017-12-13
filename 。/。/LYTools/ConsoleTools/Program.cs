using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LYTools;
using DBHelper;
using System.Data;

namespace ConsoleTools
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("123".Substring(1));
            //测试类型转换
            //Console.WriteLine(Types.GetType("byte[]",false));
            //Console.WriteLine(Types.MsSqlType.Length + "==========" + Types.DotNetType.Length);
            //for (int i = 0; i < Types.MsSqlType.Length; i++)
            //{
            //    Console.WriteLine(Types.MsSqlType[i] + "==========" + Types.DotNetType[i]);
            //}

            //测试数据库连接
            string conStr = "Server=(local);initial catalog=hibDB;Integrated Security=SSPI;";
            DataTable dt = DBHelper.DBHelper.GetDataTableBySql(conStr, DBHelper.DBHelper.GetAllTableSql);
            Console.WriteLine(dt.Rows[0][0]);
            DataTable dta = DBHelper.DBHelper.GetDataTableBySql(conStr, DBHelper.DBHelper.GetTableAttrSql(dt.Rows[0][0].ToString()));
            Console.WriteLine(dta.Columns[0] + "\t" + dta.Columns[1] + "\t" + dta.Columns[2] + "\t" + dta.Columns[3] + "\t" + dta.Columns[4] + "\t" + dta.Columns[5]);

            for (int i = 0; i < dta.Rows.Count; i++)
            {
                Console.WriteLine(dta.Rows[i][0] + "\t" + dta.Rows[i][1] + "\t" + dta.Rows[i][2] + "\t" + dta.Rows[i][3] + "\t" + dta.Rows[i][4] + "\t" + dta.Rows[i][5]);
            }

            //测试对象生成
            //string className = "Test";
            //string namespaceStr = "";
            //string path = AppDomain.CurrentDomain.BaseDirectory;
            //string str= LYTools.NHibernateMap.GetCSText("",namespaceStr,className);
            //LYTools.Common.CreateTextFile(path+className+".cs",str,true);

            //LYTools.NHibernateMap.SaveXML(path,null, namespaceStr, className);
            Console.ReadKey();
        }
    }
}
