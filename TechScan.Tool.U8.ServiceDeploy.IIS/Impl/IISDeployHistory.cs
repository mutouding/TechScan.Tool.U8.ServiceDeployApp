/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISDeployHistory.cs
* 功能描述： Android服务部署历史操作
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 14:14:50
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 14:14:50		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Microsoft.Web.Administration;
using System;
using System.Data;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.IIS.Config;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl
{
    /// <summary>
    /// Android服务部署历史操作
    /// </summary>
    public class IISDeployHistoryRepository :
        BaseDeployHistory<IISDeployHistoryItem, IISConfig, IISDeployParams>
    {
        #region Property

        #endregion

        #region Ctor

        public IISDeployHistoryRepository(HistoryType hisType, string historyDir = "", string historyListFileName = "")
            : base(hisType)
        {
            HisType = hisType;
            HistoryDir = historyDir;
            HistoryListFileName = historyListFileName;
            Details = base.GetHistoryList();
        }

        #endregion

        #region Methods

        /// <summary>
        /// 添加部署历史项
        /// </summary>
        /// <param name="t"></param>
        /// <returns></returns>
        public override Tuple<bool, string> AddHistory(IISDeployHistoryItem t)
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

            DataTable dtConfigSite = new DataTable("DeploySite");
            dtConfigSite.Columns.Add("GUID", typeof(string));
            dtConfigSite.Columns.Add("SiteName", typeof(string));
            dtConfigSite.Columns.Add("VirtualSiteName", typeof(string));
            dtConfigSite.Columns.Add("PhysicalPath", typeof(string));
            dtConfigSite.Columns.Add("Ports", typeof(string));
            dtConfigSite.Columns.Add("MIME", typeof(string));
            dtConfigSite.Columns.Add("添加用户权限", typeof(string));//Myl 20200107 Add
            dtConfigSite.Columns.Add("用户", typeof(string));//Myl 20200107 Add

            DataTable dtConfigAppPoolSite = new DataTable("DeployAppPool");
            dtConfigAppPoolSite.Columns.Add("GUID", typeof(string));
            dtConfigAppPoolSite.Columns.Add("PoolName", typeof(string));
            dtConfigAppPoolSite.Columns.Add("ManagedPipelineMode", typeof(string));
            dtConfigAppPoolSite.Columns.Add("ManagedRuntimeVersion", typeof(string));
            dtConfigAppPoolSite.Columns.Add("Enable32BitAppOnWin64", typeof(string));

            DataSet dsData = new DataSet("DeployHistory");

            dsData.Tables.Add(dtDeployMain);
            dsData.Tables.Add(dtConfigSite);
            dsData.Tables.Add(dtConfigAppPoolSite);

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

                #region 网站信息
                DataRow drDeploySite = dsRtn.Tables["DeploySite"].NewRow();
                drDeploySite["GUID"] = cGuid;
                drDeploySite["SiteName"] = item.ConfigData?.SiteName;
                drDeploySite["VirtualSiteName"] = item.ConfigData?.VirtualSiteName;
                drDeploySite["PhysicalPath"] = item.ConfigData?.PhysicalPath;
                drDeploySite["Ports"] = new Func<string>(() =>
                {
                    string cPortsTemp = string.Empty;
                    foreach (var vPort in item.ConfigData.Ports)
                    {
                        cPortsTemp += $"{vPort.ToString()}/";
                    }
                    return cPortsTemp.TrimEnd(new char[] { '/' });
                })();
                drDeploySite["MIME"] = new Func<string>(() =>
                {
                    string cMimeTemp = string.Empty;
                    foreach (var vMIME in item.ConfigData.MIME_Types)
                    {
                        cMimeTemp += $"{vMIME.Key}:{vMIME.Value}/";
                    }
                    return cMimeTemp.TrimEnd(new char[] { '/' });
                })();
                drDeploySite["添加用户权限"] = (item.ConfigData?.VirtualSiteUserAuth?.IsAddUserAuth ?? true) ? "是" : "否";
                drDeploySite["用户"] = new Func<string>(() =>
                {
                    string cUserTemp = string.Empty;
                    foreach (var vUser in item.ConfigData?.VirtualSiteUserAuth?.Users)
                    {
                        cUserTemp += $"{vUser},";
                    }
                    return cUserTemp.TrimEnd(new char[] { ',' });
                })();
              
                dsRtn.Tables["DeploySite"].Rows.Add(drDeploySite);
                #endregion

                #region AppPool配置

                DataRow drDeployAppPool = dsRtn.Tables["DeployAppPool"].NewRow();
                drDeployAppPool["GUID"] = cGuid;
                drDeployAppPool["PoolName"] = item.ConfigData?.AppPoolConfig?.PoolName;
                drDeployAppPool["ManagedPipelineMode"] = Enum.GetName(typeof(ManagedPipelineMode), item.ConfigData?.AppPoolConfig?.ManagedPipelineMode);
                drDeployAppPool["ManagedRuntimeVersion"] = item.ConfigData?.AppPoolConfig?.ManagedRuntimeVersion;
                drDeployAppPool["Enable32BitAppOnWin64"] = (item.ConfigData?.AppPoolConfig?.Enable32BitAppOnWin64 ?? false) ? "True" : "False";
                dsRtn.Tables["DeployAppPool"].Rows.Add(drDeployAppPool);

                #endregion

            }

            return dsRtn;
        }

        #endregion
    }
}
