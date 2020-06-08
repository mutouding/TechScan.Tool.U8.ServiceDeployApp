/*---------------------------------------------------------------------
* 			Copyright (C) 2019 TECHSCAN 版权所有。
*           www.techscan.cn
*┌──────────────────────────────────┐
*│　此技术信息为本公司机密信息，未经本公司书面同意禁止向第三方披露．　│
*│　版权所有：上海太迅自动识别技术有限公司 　 　　　　　　　　　　　　│
*└──────────────────────────────────┘
*
* 文件名：   IISPackageZipParams.cs
* 功能描述： IIS打包参数对象类
* 作者：     Myl
* CLR版本：  4.0.30319.42000
* 创建时间： 2019-12-26 11:15:56
* Client：   MYLPC
* 文件版本： V1.0.0

===============================版本履历===============================
* Ver 		变更日期 				负责人 	变更内容
* V1.0.0	2019-12-26 11:15:56		Myl		Create
*
======================================================================
//--------------------------------------------------------------------*/

using TechScan.Tool.U8.ServiceDeploy.Base.Enums;
using TechScan.Tool.U8.ServiceDeploy.Base.Impl;

namespace TechScan.Tool.U8.ServiceDeploy.IIS.Impl
{
    /// <summary>
    /// IIS打包参数对象类
    /// </summary>
    public class IISPackageZipParams : BasePackageZipParams<IISPackageZipItem>
    {
        #region Property
        #endregion

        #region Ctor
        public IISPackageZipParams(IISPackageZipItem zipItem) :
            base(DeployTypes.IIS, zipItem)
        { }
        #endregion

        #region Methods
        #endregion
    }
}
