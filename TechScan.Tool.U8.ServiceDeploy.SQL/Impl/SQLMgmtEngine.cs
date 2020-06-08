/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLMgmtEngine.cs
* 功能描述： SQL数据库操作引擎
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-04 22:05:06
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-04 22:05:06		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Data;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL数据库操作引擎
    /// </summary>
    public class SQLMgmtEngine
    {
        #region Property
        /// <summary>
        /// SQL文件临时解压目录名
        /// </summary>
        public const string SQL_UNZIP_TEMP_PATH = "SqlUnZipTmp";

        internal const string Dot = ".";
        public const string SqlTableInfo = @"SELECT 
[SchemaName] = su.name,
[TableName] = so.name, 
[RowCount] = MAX(si.rows) 
FROM 
sys.sysobjects so WITH (NOLOCK) LEFT JOIN
sys.schemas su WITH (NOLOCK) ON so.uid = su.schema_id LEFT JOIN
sys.sysindexes si WITH (NOLOCK) ON si.id = OBJECT_ID(su.name + '.' + so.name)
WHERE 
so.xtype = 'U' 
 {0} 
GROUP BY 
su.name, so.name
ORDER BY su.name, so.name";
        #endregion

        #region Methods
        public static DataTable GetDatabaseInfo(DbServerInfo server, string database)
        {
            if (server.IsCloud)
            {
                server.Database = database;
                return SqlHelper.Query(string.Format("SELECT '{0}' AS DatabaseName, Name AS Logical_Name, Physical_Name, CAST(size AS decimal(30,0))*8 AS Size, state, type FROM sys.database_files", database), server);
            }
            else
            {
                return SqlHelper.Query("SELECT DB_NAME(database_id) AS DatabaseName, Name AS Logical_Name, Physical_Name, CAST(size AS decimal(30,0))*8 AS Size, state, type FROM sys.master_files WITH (NOLOCK) WHERE DB_NAME(database_id) = '" + database + "'", server);
            }
        }
        public static string ParseObjectName(string objectName, out string schemaName)
        {
            var pos = objectName.IndexOf(Dot);
            if (pos != -1)
            {
                schemaName = objectName.Substring(0, pos);
                return objectName.Substring(pos + 1);
            }
            else
            {
                schemaName = "dbo";
                return objectName;
            }
        }
        public static DataTable GetDatabasesInfo(DbServerInfo server, bool bOnlyUFIDA = true)
        {
            if (bOnlyUFIDA)
            {
                return SqlHelper.Query("SELECT * FROM sys.databases WITH (NOLOCK) WHERE name LIKE 'UFDATA_%' AND state=0", server);
            }
            else
            {
                return SqlHelper.Query("SELECT * FROM sys.databases WITH (NOLOCK) WHERE state=0", server);
            }
        }

        public static int GetServerVersion(DbServerInfo server)
        {
            var version = SqlHelper.ExecuteScalar("SELECT SERVERPROPERTY('ProductVersion')", server);
            var value = version.ToString();
            var major = value.Split('.')[0];
            return Convert.ToInt32(major);
        }

        public static DbServerInfo GetServerInfo(DbServerInfo server, string catalog)
        {
            return new DbServerInfo
            {
                IsCloud = server.IsCloud,
                AuthType = server.AuthType,
                Server = server.Server,
                Database = catalog,
                User = server.User,
                Password = server.Password
            };
        }

        public static string GetObjectName(object schemaName, string objectName)
        {
            if (schemaName != null && !string.IsNullOrEmpty(schemaName.ToString()))
                return string.Format("{0}{1}{2}", schemaName, Dot, objectName);
            else
                return objectName;
        }
        public static void GetOsInfo(DbServerInfo server, out DateTime serverStartTime)
        {
            //what's wrong with the SQL Server team??? they just keep changing the column name in different versions
            //sqlserver_start_time
            var info = SqlHelper.Query("SELECT * FROM sys.dm_os_sys_info WITH (NOLOCK)", server);
            var row = info.Rows[0];
            if (info.Columns.Contains("sqlserver_start_time"))
                serverStartTime = Convert.ToDateTime(row["sqlserver_start_time"]);
            else if (info.Columns.Contains("ms_ticks"))
            {
                var startTime = row["ms_ticks"].ToString();
                var ticks = Convert.ToInt64(startTime);
                serverStartTime = DateTime.Now.AddMilliseconds(-ticks);
            }
            else
                serverStartTime = DateTime.Now;
        }
        #endregion
    }
}
