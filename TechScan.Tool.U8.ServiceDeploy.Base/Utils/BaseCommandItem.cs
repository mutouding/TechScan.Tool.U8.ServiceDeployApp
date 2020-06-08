/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   BaseCommandItem.cs
* 功能描述： 基础CMD命令项
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-08 14:54:53
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-08 14:54:53		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System.Collections.Generic;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Utils
{
    /// <summary>
    /// 基础CMD命令项
    /// </summary>
    public abstract class BaseCommandItem
    {
        #region Property

        /// <summary>
        /// 是否需要执行CMD命令
        /// </summary>
        public bool IsCmdRequired
        {
            get;set;
        }
        /// <summary>
        /// 命令行语句集合
        /// </summary>
        public IList<string> Commands
        {
            get;set;
        }

        #endregion
    }
}
