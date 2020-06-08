/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLDeployBaseException.cs
* 功能描述： SQL脚本部署基础数据异常
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-09 16:43:58
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-09 16:43:58		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Exceptions
{
    /// <summary>
    /// SQL脚本部署基础数据异常
    /// </summary>
    public class SQLDeployBaseException
        : DeployBaseException
    {
        #region 属性


        #endregion

        #region 构造函数
        public SQLDeployBaseException()
            : this("SQL脚本部署时发生基础数据异常！")
        {
        }

        public SQLDeployBaseException(string message)
            : base(message)
        {
        }

        public SQLDeployBaseException(string message, Exception e)
            : base(message, e)
        { }

        #endregion
    }
}
