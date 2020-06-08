/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLDeployParams.cs
* 功能描述： SQL脚本部署参数
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 16:34:32
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 16:34:32		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.SQL.Config;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL脚本部署参数
    /// </summary>
    public class SQLDeployParams 
        : BaseDeployParams
    {
        #region Property

        /// <summary>
        /// 部署时用的数据库连接列表
        /// （用列表时为了支持后期的多个数据库部署扩展）
        /// </summary>
        public IList<DbServerInfo> DbServers
        {
            get;
            set;
        }

        #endregion

        #region Ctor
        public SQLDeployParams(string cSourceDeployPackageFileName, PublishTypes publishType, DeployVersion dVersion = null)
            : base(cSourceDeployPackageFileName, publishType, dVersion)
        {

        }
        #endregion
    }
}
