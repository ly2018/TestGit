using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LYTools
{
    public class Types
    {
        //SQL Server 数据库引擎类型
        public static string[] MsSqlType = { "bigint", "binary", "bit", "char", "date", "datetime", "datetime2", "datetimeoffset", "decimal", "varbinary", "float", "image", "int", "money", "nchar", "ntext", "numeric", "nvarchar", "real", "rowversion", "smalldatetime", "smallint", "smallmoney", "sql_variant", "text", "time", "timestamp", "tinyint", "uniqueidentifier", "varbinary", "varchar", "xml" };
        //.NET Framework 类型
        public static string[] DotNetType = { "int", "Byte[]", "bool", "string", "DateTime", "DateTime", "DateTime2", "DateTimeOffset", "Decimal", "Byte[]", "Double", "Byte[]", "int", "Decimal", "string", "string", "Decimal", "string", "Single", "Byte[]", "DateTime", "short", "decimal", "Object", "string", "TimeSpan", "Byte[]", "Byte", "Guid", "Byte[]", "string", "Xml" };
        /// <summary>
        /// SQL Server 数据库引擎类型和.NET Framework 类型互转方法
        /// </summary>
        /// <param name="type">类型名称</param>
        /// <param name="isToDotNetType">转换方向</param>
        /// <returns></returns>
        public static string GetType(string type, bool isToDotNetType)
        {
            string result = "string";//默认string数据类型
            if (isToDotNetType)
            {
                for (int i = 0; i < MsSqlType.Length; i++)
                {
                    if (MsSqlType[i] == type)
                    {
                        result = DotNetType[i];
                        break;
                    }
                }
            }
            else
            {
                result = "varchar";
                for (int i = 0; i < DotNetType.Length; i++)
                {
                    if (DotNetType[i] == type)
                    {
                        result = MsSqlType[i];
                        break;
                    }
                }
            }
            return result;
        }
    }
}
