/*---------------------------------------------------------------------
* 			Copyright (C) 2020 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   SQLPackageZipItem.cs
* 功能描述： SQL脚本打包基础项
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2020-01-06 21:36:23
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2020-01-06 21:36:23		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;

namespace TechScan.Tool.U8.ServiceDeploy.SQL.Impl
{
    /// <summary>
    /// SQL脚本打包基础项
    /// </summary>
    public class SQLPackageZipItem : BasePackageZipItem
    {
        #region Ctor

        public SQLPackageZipItem(PackageType pkType) : base(DeployTypes.SQL, pkType)
        { }
        #endregion
    }
}
