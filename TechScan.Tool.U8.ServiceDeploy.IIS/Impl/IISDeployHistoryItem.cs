/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISDeployHistoryItem.cs
* 功能描述： IIS部署历史项
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 13:44:54
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 13:44:54		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;
using TechScan.Tool.U8.ServiceDeploy.IIS.Config;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl
{
    /// <summary>
    /// IIS部署历史项
    /// </summary>
    public class IISDeployHistoryItem :
        BaseDeployHistoryItem<IISConfig, IISDeployParams>
    {
        #region Ctor

        public IISDeployHistoryItem(string cHistoryPackageFileFullName = "", IISConfig configData = null, IISDeployParams deployParam = null) :
            base(Base.Enums.HistoryType.DEPLOY_IIS, cHistoryPackageFileFullName, configData, deployParam)
        { }

        #endregion
    }
}
