/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   DeployVersion.cs
* 功能描述： 发布版本对象
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-24 17:35:41
* Client：   MYLPC
* 文件版本： V1.0.1

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-24 17:35:41		Myl		Create
* V1.0.1	2019-12-29         		Myl		部署版本类改为从基类包对象
* 
======================================================================
//--------------------------------------------------------------------*/

using TechScan.Tool.U8.ServiceDeploy.Base.Enums;

namespace TechScan.Tool.U8.ServiceDeploy.Base.Impl
{
    /// <summary>
    /// 发布版本对象
    /// </summary>
    public class DeployVersion : BasePackageZipItem
    {
        #region Fields & Property

        ///// <summary>
        ///// 版本号
        ///// </summary>
        //public string VersionCode
        //{
        //    get; set;
        //}

        ///// <summary>
        ///// 版本描述
        ///// </summary>
        //public string VersionDescription
        //{
        //    get; set;
        //}

        #endregion

        #region Ctor

        public DeployVersion(DeployTypes depType, PackageType pkType)
            : base(depType, pkType)
        { }

        #endregion
    }
}
