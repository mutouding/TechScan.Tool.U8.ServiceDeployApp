/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLConfig.cs
* 功能描述： SQL连接配置
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-23 17:32:33
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-23 17:32:33		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;
using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Config;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Config
{
    /// <summary>
    /// SQL连接配置
    /// </summary>
    [Serializable]
    public class SQLConfig : BaseConfig
    {
        #region Property

        /// <summary>
        /// 数据库连接超时时间（秒）
        /// 默认30秒
        /// </summary>
        public int ConnectionTimeout { get; set; }

        /// <summary>
        /// 当前历史数据库服务器连接对象列表
        /// （其实记录一个够了，这里用集合存储历史联过的数据库，为了扩展用）
        /// </summary>
        public IList<DbServerInfo> DbServers
        {
            get;
            set;
        }

        #endregion

        #region Ctor

        public SQLConfig()
        {
            //默认30秒
            ConnectionTimeout = 30;
        }

        #endregion

        #region Methods

        #endregion
    }
}
