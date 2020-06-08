/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   ApplicationPoolConfig.cs
* 功能描述： IIS-应用程序池配置
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-23 14:30:23
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-23 14:30:23		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using Microsoft.Web.Administration;
using System;
using TechScan.Tool.U8.ServiceDeploy.Base.Config;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Config
{
    /// <summary>
    /// IIS-应用程序池配置
    /// </summary>
    public class ApplicationPoolConfig
        : BaseConfig
    {
        #region Property

        /// <summary>
        /// 应用程序池名称
        /// </summary>
        public string @PoolName
        {
            get; set;
        }

        /// <summary>
        /// 托管管道模式
        /// </summary>
        public ManagedPipelineMode @ManagedPipelineMode
        {
            get;
            set;
        }

        /// <summary>
        /// .Net Framework运行版本
        /// </summary>
        public string @ManagedRuntimeVersion
        {
            get;
            set;
        }

        /// <summary>
        /// 是否启用32位应用程序支持
        /// </summary>
        public Boolean @Enable32BitAppOnWin64
        {
            get; set;
        }

        #endregion

        #region Ctor
        public ApplicationPoolConfig(string poolName = ConstantValues.DEFAULT_IIS_APPLICATIONPOOL_NAME,
            ManagedPipelineMode mpm = ManagedPipelineMode.Integrated,
            bool enable32BitAppOnWin64 = true)
        {
            @PoolName = poolName;
            @ManagedPipelineMode = mpm;
            @ManagedRuntimeVersion = ConstantValues.DEFAULT_IIS_APPLICATIONPOOL_MANAGEDRUNTIMEVERSION;
            @Enable32BitAppOnWin64 = enable32BitAppOnWin64;
        }
        #endregion

        #region Methods

        public override bool IsNullOrEmpty()
        {
            return false;
        }

        #endregion
    }
}
