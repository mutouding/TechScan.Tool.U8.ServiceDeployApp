/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLDeployHistoryRepository.cs
* 功能描述： SQL脚本部署历史仓库
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-13 10:17:59
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-13 10:17:59		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Data;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL脚本部署历史仓库
    /// </summary>
    public class SQLDeployHistoryRepository :
        BaseDeployHistory<SQLDeployHistoryItem, SQLConfig, SQLDeployParams>
    {
        #region Property

        #endregion

        #region Ctor

        public SQLDeployHistoryRepository(HistoryType hisType, string historyDir = "", string historyListFileName = "")
            : base(hisType)
        {
            HisType = hisType;
            HistoryDir = historyDir;
            HistoryListFileName = historyListFileName;
            Details = base.GetHistoryList();
        }

        #endregion

        #region Methods

        public override Tuple<bool, string> AddHistory(SQLDeployHistoryItem t)
        {
            return base.AddHistory(t);
        }
        protected override DataSet BuildHistoryDataConstruction()
        {
            DataTable dtDeployMain = new DataTable("DeployMain");
            dtDeployMain.Columns.Add("GUID", typeof(string));
            dtDeployMain.Columns.Add("发布时间", typeof(string));
            dtDeployMain.Columns.Add("类型", typeof(string));
            dtDeployMain.Columns.Add("发布描述", typeof(string));
            dtDeployMain.Columns.Add("发布版本", typeof(string));
            dtDeployMain.Columns.Add("版本描述", typeof(string));
            dtDeployMain.Columns.Add("打包人", typeof(string));
            dtDeployMain.Columns.Add("打包时间", typeof(string));
            dtDeployMain.Columns.Add("DeployVersion_SourcePkgFile", typeof(string));//部署源文件
            dtDeployMain.Columns.Add("DeployVersion_HistoryPkgFile", typeof(string));//部署备份文件

            DataTable dtDbInfo = new DataTable("DataBase");
            dtDbInfo.Columns.Add("GUID", typeof(string));
            dtDbInfo.Columns.Add("服务器", typeof(string));
            dtDbInfo.Columns.Add("认证类型", typeof(string));
            dtDbInfo.Columns.Add("数据库", typeof(string));
            dtDbInfo.Columns.Add("用户名", typeof(string));
            dtDbInfo.Columns.Add("密码", typeof(string));
          
            DataSet dsData = new DataSet("DeployHistory");

            dsData.Tables.Add(dtDeployMain);
            dsData.Tables.Add(dtDbInfo);
           
            return dsData;
        }

        public override DataSet ToDataSet()
        {
            //return base.ToDataSet();
            if (Details == null || Details.Count == 0)
            {
                return null;
            }

            DataSet dsRtn = BuildHistoryDataConstruction();

            foreach (var item in Details)
            {
                //string cGuid = System.Guid.NewGuid().ToString("N").ToUpper();
                string cGuid = item.GUID;
                #region 部署配置
                DataRow drMain = dsRtn.Tables["DeployMain"].NewRow();
                drMain["GUID"] = cGuid;
                drMain["发布时间"] = item.DeployParam?.DeployDateTime?.ToString("yyyy-MM-dd HH:mm:ss");
                drMain["类型"] = item.DeployParam?.PublishType == PublishTypes.General ? "正常发布" : "服务回滚";
                drMain["发布描述"] = item.DeployParam?.DeployDescription;
                drMain["发布版本"] = item.DeployParam?.DeployVersion?.Version;
                drMain["版本描述"] = item.DeployParam?.DeployVersion?.Description;
                drMain["打包人"] = item.DeployParam?.DeployVersion?.Packager;
                drMain["打包时间"] = item.DeployParam?.DeployVersion?.PackageDateTime.ToString("yyyy-MM-dd HH:mm:ss");
                drMain["DeployVersion_SourcePkgFile"] = item.DeployParam?.SourceDeployPackageFileName;
                drMain["DeployVersion_HistoryPkgFile"] = item.HistoryPackageFileFullName;
                dsRtn.Tables["DeployMain"].Rows.Add(drMain);
                #endregion

                #region 数据库信息

                foreach (var eachServer in item.DeployParam?.DbServers)
                {
                    DataRow drDbInfo = dsRtn.Tables["DataBase"].NewRow();
                    drDbInfo["GUID"] = cGuid;
                    drDbInfo["服务器"] = eachServer?.Server;
                    drDbInfo["认证类型"] = eachServer?.AuthType.ToString();
                    drDbInfo["数据库"] = eachServer?.Database;
                    drDbInfo["用户名"] = eachServer?.User;
                    drDbInfo["密码"] = "******";//eachServer?.Password;
                    dsRtn.Tables["DataBase"].Rows.Add(drDbInfo);
                }
                
                #endregion
            }

            return dsRtn;
        }

        #endregion
    }
}
