/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DeployBaseException.cs
* 功能描述： 部署项目基类异常
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 11:32:34
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 11:32:34		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Exceptions
{
    /// <summary>
    /// 部署项目基类异常
    /// </summary>
    public class DeployBaseException
        : ApplicationException
    {
        #region 构造函数
        public DeployBaseException()
            : this("部署基础异常！")
        {
        }

        public DeployBaseException(string message)
            : base(message)
        {
        }

        public DeployBaseException(string message, Exception e)
            : base(message, e)
        { }

        #endregion
    }
}
