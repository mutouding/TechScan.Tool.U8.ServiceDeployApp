/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISRegWCF.cs
* 功能描述： IIS注册WCF组件命令操作对象
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-08 15:14:45
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-08 15:14:45		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using System.Collections.Generic;
using TechScan.Tool.U8.ServiceDeploy.Base.Utils;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Config
{
    /// <summary>
    /// IIS注册WCF组件命令操作对象
    /// </summary>
    public class IISRegWCF : BaseCommandItem
    {
        #region Property
        #endregion

        #region Ctor

        public IISRegWCF(bool isCmdRequired, IList<string> lstCmds = null)
        {
            IsCmdRequired = isCmdRequired;
            Commands = lstCmds ?? ConstantValues.DEFAULT_COMMAND_REGWCF;
        }
        
        #endregion
    }
}
