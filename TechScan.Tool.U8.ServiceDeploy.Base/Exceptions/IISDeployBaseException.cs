/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISDeployBaseException.cs
* 功能描述： IIS部署基础数据异常（e.g:IIS版本低于7等异常）
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 11:17:59
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 11:17:59		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Exceptions
{
    /// <summary>
    /// IIS部署基础数据异常（e.g:IIS版本低于7等异常）
    /// </summary>
    public class IISDeployBaseException
        : DeployBaseException
    {
        #region 属性


        #endregion

        #region 构造函数
        public IISDeployBaseException()
            : this("IIS部署时发生基础数据异常！")
        {
        }

        public IISDeployBaseException(string message)
            : base(message)
        {
        }

        public IISDeployBaseException(string message, Exception e)
            : base(message, e)
        { }

        #endregion
    }
}
