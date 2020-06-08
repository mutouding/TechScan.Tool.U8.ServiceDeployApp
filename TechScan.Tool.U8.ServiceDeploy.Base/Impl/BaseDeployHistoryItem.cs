/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BaseDeployHistoryItem.cs
* 功能描述： 基本部署项对象
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-30 13:03:36
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-30 13:03:36		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using TechScan.Tool.U8.ServiceDeploy.Base.Config;
using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 基本部署项对象
    /// </summary>
    /// <typeparam name="T">基本配置</typeparam>
    /// <typeparam name="U">基本部署参数</typeparam>
    public class BaseDeployHistoryItem<T, U> : BaseHistoryItem, IDisposable
        where T : BaseConfig
        where U : BaseDeployParams
    {
        #region Property

        /// <summary>
        /// 设置或者获取部署时的配置对象
        /// （比如Android服务发布时，当时的IISConfig对象）
        /// </summary>
        public T ConfigData
        { get; set; }
        /// <summary>
        /// 部署时的数据
        /// </summary>
        public U DeployParam
        {
            get;
            set;
        }

        #endregion

        #region Ctor

        public BaseDeployHistoryItem(HistoryType @type,string cHistoryPackageFileFullName="", T configData = null, U deployParam=null)
        {
            TYPE = @type;
            HistoryPackageFileFullName = cHistoryPackageFileFullName;
            ConfigData = configData;
            DeployParam = deployParam;
        }

        #endregion
        public void Dispose()
        {
            return;
        }
    }
}
