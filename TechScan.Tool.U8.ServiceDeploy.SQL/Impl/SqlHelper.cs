/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SqlHelper.cs
* 功能描述： SQL数据库操作帮助类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-04 22:07:57
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-04 22:07:57		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System.Data;
using System.Data.SqlClient;
using System.Text;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;
using TechScan.Tool.U8.ServiceDeploy.SQL.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    public partial class SqlHelper
    {
    }
    /// <summary>
    /// SQL数据库操作帮助类
    /// </summary>
    public partial class SqlHelper
    {
        public static SqlConnection CreateNewConnection(DbServerInfo dbInfo)
        {
            var builder = new SqlConnectionStringBuilder
            {
                ApplicationName = "",// Settings.Title,
                IntegratedSecurity = dbInfo.AuthType == AuthTypes.Windows,
                DataSource = dbInfo.Server,
                UserID = dbInfo.User,
                Password = dbInfo.Password,
                InitialCatalog = dbInfo.Database ?? string.Empty,
                ConnectTimeout = dbInfo.ConnectionTimeout
            };
            return new SqlConnection(builder.ConnectionString);
        }

        public static DataSet QuerySet(string sql, DbServerInfo info)
        {
            string message;
            return QuerySet(sql, info, out message);
        }

        public static DataSet QuerySet(string sql, DbServerInfo info, out string message)
        {
            using (var connection = CreateNewConnection(info))
            {
                if (info.IsCloud && !string.IsNullOrEmpty(info.Database))
                {
                    connection.Open();
                    connection.ChangeDatabase(info.Database);
                }
                var result = new StringBuilder();
                connection.InfoMessage += (s, e) => { result.AppendLine(e.Message); };
                var command = new SqlCommand(sql, connection);
                command.StatementCompleted += (s, e) => { result.AppendLine(string.Format("{0} row(s) affected.", e.RecordCount)); };
                var adapter = new SqlDataAdapter(command);
                var data = new DataSet();
                //adapter.FillSchema(data, SchemaType.Mapped);
                adapter.Fill(data);
                connection.Close();
                message = result.ToString();
                return data;
            }
        }

        public static DataTable Query(string sql, DbServerInfo info)
        {
            var data = QuerySet(sql, info);
            if (data != null && data.Tables.Count > 0)
                return data.Tables[0];
            return null;
        }

        public static string ExecuteNonQuery(string sql, DbServerInfo server)
        {
            using (var connection = CreateNewConnection(server))
            {
                var result = new StringBuilder();
                connection.InfoMessage += (s, e) => { result.AppendLine(e.Message); };
                var command = new SqlCommand(sql, connection);
                connection.Open();
                command.ExecuteNonQuery();
                connection.Close();
                return result.ToString();
            }
        }

        public static object ExecuteScalar(string sql, DbServerInfo server)
        {
            object result;
            using (var connection = CreateNewConnection(server))
            {
                var command = new SqlCommand(sql, connection);
                connection.Open();
                result = command.ExecuteScalar();
                connection.Close();
            }
            return result;
        }
    }
}
