using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;

namespace DBHelper
{
    public class DBHelper
    {
        //获取数据库所有的表名称 Name为列名称
        public const string GetAllTableSql ="select Name from sysobjects where xtype='u' and status>=0";
        //获取表的所有属性的sql
        public static string GetTableAttrSql(string TableName)
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("select c.name ,c.length ,c.isnullable AS 'not-null',");
            sb.Append("[unique]=case when exists(SELECT 1 FROM sysobjects where xtype='PK' and parent_obj=c.id and name in (");
            sb.Append("SELECT name FROM sysindexes WHERE indid in(SELECT indid FROM sysindexkeys WHERE id = c.id AND colid=c.colid))) then '1' else '0' end,");
            sb.Append("t.name as 'sql-type' ,(select value from sys.extended_properties as ex where ex.major_id = c.id and ex.minor_id = c.colid) as description ");
            sb.Append("from  syscolumns as c inner join sys.tables as ta on c.id=ta.object_id ");
            sb.Append("inner join  (select name,system_type_id from sys.types where name<>'sysname') as t on c.xtype=t.system_type_id ");
            sb.Append("left join syscomments e on c.cdefault=e.id");
            sb.Append(" where ta.name='" + TableName + "' order by c.colid");
            return sb.ToString();
        }
        #region 根据sql返回 DataTable
        public static DataTable GetDataTableBySql(string ConnString, string sql)
        {
            SqlDataAdapter da = null;
            DataTable tab = new DataTable();
            using (SqlConnection Conn = new SqlConnection(ConnString))
            {
                try
                {
                    Conn.Open();
                    da = new SqlDataAdapter(sql, Conn);
                    da.Fill(tab);
                }
                catch (Exception ex)
                {
                    throw ex;
                }
                finally
                {
                    da.SelectCommand.Parameters.Clear();
                    Conn.Close();
                    Conn.Dispose();
                }
                return tab;
            }
        }
        #endregion
    }
}
